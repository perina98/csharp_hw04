using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace HW04
{
    public class StegoImageProcessor
    {
        const int BitsInByte = 8;
        public int ChunkSize;
        public int BitMask;
        public int ChunkLenght;
        // Use constructor for additional configuration

        public StegoImageProcessor(int chunkSize)
        {
            ChunkSize = chunkSize;
            ChunkLenght = BitsInByte / ChunkSize;
            BitMask = (int)(Math.Pow(2, chunkSize) - 1);

        }

        public async Task<Image<Rgba32>> LoadImageAsync(string path) => await Image.LoadAsync<Rgba32>(path);

        public Task SaveImageAsync(Image<Rgba32> image, string path) => image.SaveAsync(path);

        public Task<Image<Rgba32>> EncodePayload(Image<Rgba32> image, byte[] payload) => Task.Run(() => 
        {
            image.ProcessPixelRows(accessor =>
            {
                int payloadIndex = 0;
                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgba32> row = accessor.GetRowSpan(y);
                    for (int x = 0; x < row.Length; x++)
                    {
                        if (payloadIndex < payload.Length)
                        {
                            IEnumerable<byte> payloadByte = ByteSpliting.Split(payload[payloadIndex], ChunkSize);
                            foreach (byte b in payloadByte)
                            {
                                ref var pixel = ref row[x];
                                pixel.R = (byte)((pixel.R & (~BitMask)) | b);
                                x++;
                            }
                            payloadIndex++;
                        }
                    }
                }
            });

            return image;
        });

        public Task<byte[]> ExtractPayload(Image<Rgba32> image, int dataSize) => Task.Run(() =>
        {
            byte[] payload = new byte[dataSize];
            image.ProcessPixelRows(accessor =>
            {
                int payloadIndex = 0;
                for (int y = 0; y < accessor.Height; y++)
                {
                    Span<Rgba32> row = accessor.GetRowSpan(y);
                    for (int x = 0; x < row.Length; x++)
                    {
                        if (payloadIndex < payload.Length)
                        {
                            var bytes = new byte[ChunkLenght];
                            for (int i = 0; i < ChunkLenght; i++)
                            {
                                ref var pixel = ref row[x];
                                bytes[i] = (byte)(pixel.R & (BitMask));
                                x++;
                            }
                            payload[payloadIndex] = ByteSpliting.Reform(bytes, ChunkSize);
                            payloadIndex++;
                        }
                    }
                }

            });

            return payload;

        });
    }
}
