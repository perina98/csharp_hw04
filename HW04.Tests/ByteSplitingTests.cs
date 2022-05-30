using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HW04.Tests
{
    public class ByteSplitingTests
    {
        [Theory]
        [ClassData(typeof(ByteSplitingDataGenerator))]
        public void Split(byte whole, byte[] parts, byte size)
        {
            var result = ByteSpliting.Split(whole, size);

            Assert.Equal(parts, result.ToArray());
        }


        [Theory]
        [ClassData(typeof(ByteSplitingDataGenerator))]
        public void Reform(byte whole, byte[] parts, byte size)
        {
            var result = ByteSpliting.Reform(parts, size);

            Assert.Equal(whole, result);
        }
    }

    public class ByteSplitingDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            // whole, parts, size
            // Mind the endianness - lowest index are the least significant bits
            new object[] {0b1111_1111, new byte[] { 1,1,1,1,1,1,1,1 }, 1 },
            new object[] {0b1100_1111, new byte[] { 1,1,1,1,0,0,1,1 }, 1 },
            new object[] {0b1100_1100, new byte[] { 0,0,1,1,0,0,1,1 }, 1 },
            new object[] {0b0101_0101, new byte[] { 1,0,1,0,1,0,1,0 }, 1 },
            new object[] {0b0000_0000, new byte[] { 0,0,0,0,0,0,0,0 }, 1 },

            new object[] {0b1111_1111, new byte[] { 3,3,3,3 }, 2 },
            new object[] {0b1100_1111, new byte[] { 3,3,0,3 }, 2 },
            new object[] {0b1100_1100, new byte[] { 0,3,0,3 }, 2 },
            new object[] {0b0101_0101, new byte[] { 1,1,1,1 }, 2 },
            new object[] {0b0000_0000, new byte[] { 0,0,0,0 }, 2 },

            new object[] {0b1111_1111, new byte[] { 15,15 }, 4 },
            new object[] {0b1100_1111, new byte[] { 15,12 }, 4 },
            new object[] {0b1100_1100, new byte[] { 12,12 }, 4 },
            new object[] {0b0101_0101, new byte[] { 5,5 }, 4 },
            new object[] {0b0000_0000, new byte[] { 0,0 }, 4 },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}