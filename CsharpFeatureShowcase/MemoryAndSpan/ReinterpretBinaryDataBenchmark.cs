using BenchmarkDotNet.Attributes;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MemoryAndSpan
{
    [MemoryDiagnoser]
    public class ReinterpretBinaryDataBenchmark
    {
        private readonly RGBA[] _colorArray;
        public ReinterpretBinaryDataBenchmark()
        {
            _colorArray = new RGBA[100000];
            var random = new Random(4);
            for (int i = 0; i < _colorArray.Length; i++)
            {
                var r = (byte)random.Next(0, 255);
                var g = (byte)random.Next(0, 255);
                var b = (byte)random.Next(0, 255);
                var a = (byte)random.Next(0, 255);
                _colorArray[i] = new RGBA(r, g, b, a); //Fill the array with some dummy values
            }
        }

        [Benchmark]
        public void ReinterpretWritetoFile()
        {
            var bytes = MemoryMarshal.AsBytes(_colorArray.AsSpan());
            using (var fileStream = File.Create("datafile"))
            {
                fileStream.Write(bytes.ToArray(), 0, bytes.Length); //.ToArray() can be removed in netcore 2.1. We still need it here due to a missing API.
            }
        }

        [Benchmark(Baseline = true)]
        public void NormalWritetoFile()
        {
            var bytes = GetBytes(_colorArray);
            using (var fileStream = File.Create("datafile"))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        private byte[] GetBytes(RGBA[] array)
        {
            var structSize = 4;
            var bytes = new byte[array.Length * structSize];
            for (int i = 0; i < array.Length; i++)
            {
                var byteIndex = i * structSize;
                bytes[byteIndex] = array[i].Red;
                bytes[byteIndex + 1] = array[i].Green;
                bytes[byteIndex + 2] = array[i].Blue;
                bytes[byteIndex + 3] = array[i].Alpha;
            }
            return bytes;
        }
    }
}
