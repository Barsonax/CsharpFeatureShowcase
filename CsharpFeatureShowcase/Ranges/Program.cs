using System;
using System.Collections.Generic;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var someRange = 1..11;
            var someText = "1234567890";
            var substring = someText[1..^1];
            Console.WriteLine(substring);
            Console.ReadKey();
        }
    }
}
