namespace HW04
{
    /// <summary>
    /// Object wrapping the data for steganography.
    /// </summary>
    public class StegoObject
    {
        private byte[] data;

        public byte[] Data => data;

        protected StegoObject(byte[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Method to load arbitrary data to byte array.
        /// </summary>
        /// <typeparam name="T">type of object to load</typeparam>
        /// <param name="data">object to load</param>
        /// <param name="serializationFunction">function transforming object to byte array</param>
        /// <example>StegoObject.LoadObject(Samples.StringSample(), (s) => Encoding.Default.GetBytes(s));</example>
        /// <returns>instance of StegoObject</returns>
        public static StegoObject LoadObject<T>(T data, Func<T, byte[]> serializationFunction)
        {
            return new StegoObject(serializationFunction(data));
        }

        /// <summary>
        /// Split the data into n even chunks.
        /// </summary>
        /// <param name="numberOfChunks"></param>
        /// <returns></returns>
        public IEnumerable<byte[]> GetDataChunks(int numberOfChunks)
        {
            var skipped = 0;
            var chunkBy = 0;
            for (var i = 0; i < numberOfChunks; i++)
            {
                skipped += chunkBy;
                chunkBy = (data.Length - skipped + (numberOfChunks - i - 1)) / (numberOfChunks - i);
                yield return data.Skip(skipped).Take(chunkBy).ToArray();
            }
        }
    }
}
