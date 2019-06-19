using System;
using System.Collections.Generic;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Range someRange = 1..^1; //Skip the first and the last element of a collection

            ReadOnlySpan<char> someText = "1234567890";
            ReadOnlySpan<char> substring = someText[someRange]; 
            Console.WriteLine(substring.ToString()); //Prints 23456789

            char lastChar = someText[^1];

            Console.WriteLine(lastChar);
            Console.ReadKey();
        }
    }
}
