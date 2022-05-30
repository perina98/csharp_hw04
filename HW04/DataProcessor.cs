using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW04
{
    public class DataProcessor
    {
        public static async Task<byte[]> ProcessData(string inputPath, string outputPath, StegoObject stegoObject, string[] imageNames, int bitsPerChunk, int parallelization)
        {
            var chunks = stegoObject.GetDataChunks(imageNames.Length);
            var procesor = new StegoImageProcessor(bitsPerChunk);
            byte[] decodedPart = Array.Empty<byte>();
            byte[] decodedData = Array.Empty<byte>();
            byte[] decodedDataBetter = Array.Empty<byte>();

            Dictionary<string, byte[]> finalData = new Dictionary<string, byte[]>();
            var sorted = new byte[6][];

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = parallelization
            };

            // zip chunks and image names together
            var zipped = chunks.Zip(imageNames, (chunk, imageName) => new { Chunk = chunk, ImageName = imageName });

            // encode chunks into images asynchronously
            await Parallel.ForEachAsync(zipped, options, async (zip, ct) =>
            {
                // load image
                Console.WriteLine($"Loading image {zip.ImageName}");
                var image = await procesor.LoadImageAsync(inputPath + zip.ImageName);
                // encode image
                var encodedImage = await procesor.EncodePayload(image, zip.Chunk);
                // save encoded image
                var fileName = outputPath + zip.ImageName.Split('.')[0] + "_" + bitsPerChunk + "bits.png";
                await procesor.SaveImageAsync(encodedImage, fileName);
                // load encoded image
                encodedImage = await procesor.LoadImageAsync(fileName);
                // decode image
                decodedPart = await procesor.ExtractPayload(encodedImage, zip.Chunk.Length);
                // concatenate decoded parts
                finalData.Add(zip.ImageName, decodedPart);
                Console.WriteLine("finished encoding image " + zip.ImageName);
            });

            foreach (var item in finalData)
            {
                var index = Array.IndexOf(imageNames, item.Key);
                sorted[index] = item.Value;
            }

            foreach (var item in sorted)
            {
                decodedData = decodedData.Concat(item).ToArray();
            }

            return decodedData;
        }

        public static void CompareData(byte[] decodedData)
        {
            bool isEqual = Samples.StringSample().SequenceEqual(Encoding.Default.GetString(decodedData));
            Console.WriteLine("Data are equal: " + isEqual);
            Console.WriteLine("Printing decoded data now:");

            Console.WriteLine(Encoding.Default.GetString(decodedData));
        }
    }
}
