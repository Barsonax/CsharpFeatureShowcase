using BenchmarkDotNet.Attributes;
using System;
using System.IO;

namespace MemoryAndSpan
{
    [MemoryDiagnoser]
    public class ReinterpretTextBenchmark
    {
        private readonly string _text;

        public ReinterpretTextBenchmark()
        {
            var random = new Random(4);
            var text = new char[100000];
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = 'h';
            }
            _text = new string(text);
        }

        [Benchmark(Baseline = true)]
        public void NormalWritetoFile()
        {
            using (var fileStream = File.CreateText("textfile"))
            {
                fileStream.Write(_text);
            }
        }
    }
}
