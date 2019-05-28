using System;
using System.Collections.Generic;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Range someRange = 1..^1;

            string someText = "1234567890";
            string substring = someText[someRange];
            char lastChar = someText[^1];
            Console.WriteLine(substring);
            Console.WriteLine(lastChar);
            Console.ReadKey();
        }
    }
}
