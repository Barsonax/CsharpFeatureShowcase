using System;
using System.Runtime.InteropServices;

namespace MemoryAndSpan
{
    public class Program
    {
        static void Main(string[] args)
        {
            SafeStackalloc(8);
            ReintepretCast();
            Console.ReadKey();
        }

        static void ReintepretCast()
        {
            var array = new byte[8];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)i; //Fill the array with some dummy values
            }

            var span = array.AsSpan();
            Span<RGBA> test = MemoryMarshal.Cast<byte, RGBA>(span); //Uses a reintrepret cast so it still refers to the same block of memory but its now viewed as a span of RGBA structs.
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
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }
        public byte Alpha { get; }
    }
}
