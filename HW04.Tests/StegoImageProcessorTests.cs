using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HW04.Tests
{
    public class StegoImageProcessorTests
    {
        [Fact]
        public async Task EncodePayload_ExtractPayload_OneImage_Match()
        {
            var input = "The quick brown fox jumps over the lazy dog";
            var inputBytes = Encoding.Default.GetBytes(input);
            var inputImage = await Image.LoadAsync<Rgba32>("Data/Coat_of_arms_of_Bruntal.jpg");

            var stegoImageProcessor = new StegoImageProcessor(2);

            var stegoImage = await stegoImageProcessor.EncodePayload(inputImage, inputBytes);
            var outputBytes = await stegoImageProcessor.ExtractPayload(stegoImage, inputBytes.Length);

            var output = Encoding.Default.GetString(outputBytes);

            Assert.Equal(input, output);
        }
    }
}
