﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace MemoryAndSpan
{
    [MemoryDiagnoser]
    public class ReinterpretBenchmark
    {
        private readonly RGBA[] _colorArray;
        public ReinterpretBenchmark()
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
            using (var fileStream = File.Create("datafile"))
            {
                var bytes = MemoryMarshal.Cast<RGBA, byte>(_colorArray.AsSpan());
                fileStream.Write(bytes.ToArray(), 0, bytes.Length);
            }
        }

        [Benchmark(Baseline = true)]
        public void NormalWritetoFile()
        {
            using (var fileStream = File.Create("datafile"))
            {
                var bytes = GetBytes(_colorArray);
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

    public class Program
    {

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ReinterpretBenchmark>();
            //SafeStackalloc(50);
            //ReinterpretCast();
            Console.ReadKey();
        }



        static void ReinterpretCast()
        {
            var array = new byte[8];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)i; //Fill the array with some dummy values
            }

            var span = array.AsSpan();
            Span<RGBA> test = MemoryMarshal.Cast<byte, RGBA>(span); //Uses a reinterpret cast so it still refers to the same block of memory but its now viewed as a span of RGBA structs.
        }

        static void SafeStackalloc(int len)
        {
            //The following line of code does not allocate anything on the heap!
            //Be careful of stackoverflow exceptions though if the length is too large.
            Span<byte> bytes = stackalloc byte[len];
        }
    }

    public struct RGBA
    {
        public RGBA(byte red, byte green, byte blue, byte alpha) : this()
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }
        public byte Alpha { get; }
    }
}
