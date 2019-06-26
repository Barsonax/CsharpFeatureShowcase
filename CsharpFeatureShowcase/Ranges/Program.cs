using System;

namespace MemoryAndSpanNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Range someRange = 1..^1; //Skip the first and the last element of a collection

            ReadOnlySpan<char> someText = "1234567890";

            Console.WriteLine(someText[3..].ToString());//Prints 4567890
            Console.WriteLine(someText[someRange].ToString()); //Prints 23456789

            char lastChar = someText[^1];
            Console.WriteLine(lastChar); //Prints 0
            Console.ReadKey();

            //Example
            var words = new string[]
            {
                // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };              // 9 (or words.Length) ^0
        }
    }
}
