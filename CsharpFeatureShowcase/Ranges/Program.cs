using System;
using System.Collections.Generic;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Range someRange = 1..11;
            Range someText = "1234567890";
            Range substring = someText[1..^1];
            Console.WriteLine(substring);
            Console.ReadKey();
        }
    }
}
