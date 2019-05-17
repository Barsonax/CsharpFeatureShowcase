using BenchmarkDotNet.Attributes;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MemoryAndSpan
{
    [Config(typeof(CustomJob))]
    [RPlotExporter]
    [MemoryDiagnoser]
    public class ReinterpretBinaryDataBenchmark
    {
        [Params(1000, 10000, 100000, 1000000, 10000000)]
        public int N;
        private RGBA[] _colorArray;

//BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.503 (1809/October2018Update/Redstone5)
//Intel Core i7-7700HQ CPU 2.80GHz(Kaby Lake), 1 CPU, 8 logical and 4 physical cores
//.NET Core SDK = 3.0.100-preview-009812

// [Host]       : .NET Core 2.1.9 (CoreCLR 4.6.27414.06, CoreFX 4.6.27415.01), 64bit RyuJIT
//  LegacyJitX64 : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3416.0
//  RyuJitX64    : .NET Core 2.1.9 (CoreCLR 4.6.27414.06, CoreFX 4.6.27415.01), 64bit RyuJIT

//Platform=X64 IterationTime = 250.0000 ms

//|                 Method |          Job |       Jit | Runtime |        N |        Mean |        Error |      StdDev | Ratio | RatioSD |    Gen 0 |    Gen 1 |    Gen 2 |  Allocated |
//|----------------------- |------------- |---------- |-------- |--------- |------------:|-------------:|------------:|------:|--------:|---------:|---------:|---------:|-----------:|
//| ReinterpretWritetoFile | LegacyJitX64 | LegacyJit |     Clr |     1000 |    215.3 us |     6.248 us |    17.11 us |  0.97 |    0.10 |   2.3438 |        - |        - |     8586 B |
//|      NormalWritetoFile | LegacyJitX64 | LegacyJit |     Clr |     1000 |    222.6 us |     5.665 us |    15.51 us |  1.00 |    0.00 |   2.4671 |        - |        - |     8586 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile |    RyuJitX64 |    RyuJit |    Core |     1000 |    209.2 us |     4.631 us |    12.44 us |  0.96 |    0.11 |   0.9191 |        - |        - |     4712 B |
//|      NormalWritetoFile |    RyuJitX64 |    RyuJit |    Core |     1000 |    219.0 us |     7.211 us |    19.62 us |  1.00 |    0.00 |   2.5338 |        - |        - |     8736 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile | LegacyJitX64 | LegacyJit |     Clr |    10000 |    253.5 us |     5.055 us |    12.01 us |  0.92 |    0.08 |  11.7188 |        - |        - |    40475 B |
//|      NormalWritetoFile | LegacyJitX64 | LegacyJit |     Clr |    10000 |    275.2 us |     5.767 us |    16.73 us |  1.00 |    0.00 |  12.2549 |        - |        - |    40470 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile |    RyuJitX64 |    RyuJit |    Core |    10000 |    229.1 us |     4.557 us |    10.83 us |  1.02 |    0.16 |        - |        - |        - |      592 B |
//|      NormalWritetoFile |    RyuJitX64 |    RyuJit |    Core |    10000 |    237.3 us |    11.869 us |    34.24 us |  1.00 |    0.00 |  12.5000 |        - |        - |    40616 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile | LegacyJitX64 | LegacyJit |     Clr |   100000 |    769.9 us |    15.208 us |    34.33 us |  0.68 |    0.04 | 122.1591 | 122.1591 | 122.1591 |   401048 B |
//|      NormalWritetoFile | LegacyJitX64 | LegacyJit |     Clr |   100000 |  1,112.6 us |    21.916 us |    34.76 us |  1.00 |    0.00 | 120.5357 | 120.5357 | 120.5357 |   401048 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile |    RyuJitX64 |    RyuJit |    Core |   100000 |    407.4 us |     8.144 us |    21.60 us |  0.41 |    0.04 |        - |        - |        - |      592 B |
//|      NormalWritetoFile |    RyuJitX64 |    RyuJit |    Core |   100000 |  1,012.0 us |    32.850 us |    96.34 us |  1.00 |    0.00 | 120.8333 | 120.8333 | 120.8333 |   400616 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile | LegacyJitX64 | LegacyJit |     Clr |  1000000 |  4,186.4 us |    80.428 us |    89.39 us |  0.65 |    0.05 | 333.3333 | 333.3333 | 333.3333 |  4002226 B |
//|      NormalWritetoFile | LegacyJitX64 | LegacyJit |     Clr |  1000000 |  6,381.4 us |   147.055 us |   433.60 us |  1.00 |    0.00 | 312.5000 | 312.5000 | 312.5000 |  4002755 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile |    RyuJitX64 |    RyuJit |    Core |  1000000 |  2,159.5 us |    61.172 us |   179.41 us |  0.37 |    0.04 |        - |        - |        - |      592 B |
//|      NormalWritetoFile |    RyuJitX64 |    RyuJit |    Core |  1000000 |  6,041.7 us |   119.666 us |   265.17 us |  1.00 |    0.00 | 312.5000 | 312.5000 | 312.5000 |  4000616 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile | LegacyJitX64 | LegacyJit |     Clr | 10000000 | 53,173.7 us | 1,034.655 us | 1,016.17 us |  0.70 |    0.03 |        - |        - |        - | 40002072 B |
//|      NormalWritetoFile | LegacyJitX64 | LegacyJit |     Clr | 10000000 | 80,662.5 us | 1,611.343 us | 4,383.78 us |  1.00 |    0.00 |        - |        - |        - | 40002072 B |
//|                        |              |           |         |          |             |              |             |       |         |          |          |          |            |
//| ReinterpretWritetoFile |    RyuJitX64 |    RyuJit |    Core | 10000000 | 39,147.0 us |   970.930 us | 2,770.12 us |  0.53 |    0.04 |        - |        - |        - |      592 B |
//|      NormalWritetoFile |    RyuJitX64 |    RyuJit |    Core | 10000000 | 74,437.7 us | 1,476.149 us | 3,361.94 us |  1.00 |    0.00 |        - |        - |        - | 40000616 B |

        [GlobalSetup]
        public void Setup()
        {
            _colorArray = new RGBA[N];
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
#if NET472
                fileStream.Write(bytes.ToArray(), 0, bytes.Length); //'.ToArray(), 0, bytes.Length' can be removed in netcore 2.1. We still need it here due to a missing API.
#elif NETCOREAPP2_1
                fileStream.Write(bytes);
#endif

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
