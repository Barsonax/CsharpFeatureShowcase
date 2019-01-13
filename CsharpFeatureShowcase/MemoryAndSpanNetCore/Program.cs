using BenchmarkDotNet.Running;
using MemoryAndSpan;
using System;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<ReinterpretBinaryDataBenchmark>();
            BenchmarkRunner.Run<ReinterpretTextBenchmark>();
            Console.ReadKey();
        }
    }
}
