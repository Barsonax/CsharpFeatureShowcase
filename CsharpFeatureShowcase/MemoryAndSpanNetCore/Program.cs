using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace MemoryAndSpan
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "daasasd";
            ReadOnlySpan<char> slice = text.AsSpan().Slice(0, 3);

            BenchmarkRunner.Run<ReinterpretBinaryDataBenchmark>();
            //BenchmarkRunner.Run<ReinterpretTextBenchmark>();
            Console.ReadKey();
        }
    }

    public class CustomJob : ManualConfig
    {
        public CustomJob()
        {
            Add(
                Job.RyuJitX64
                    .With(Runtime.Core)
                    .WithIterationTime(TimeInterval.FromMilliseconds(250)));

            Add(
                Job.LegacyJitX64
                    .With(Runtime.Clr)
                    .WithIterationTime(TimeInterval.FromMilliseconds(250)));
        }
    }
}
