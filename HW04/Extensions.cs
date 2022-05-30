using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace HW04
{
    public static class Extensions
    {
        public static byte[] ToByteArray(this Image image, IImageEncoder imageEncoder)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, imageEncoder);
                return memoryStream.ToArray();
            }
        }
    }
}