using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            object value = 5;

            {
                if (value is int number)
                {
                    Console.WriteLine(number);
                }
            }

            switch (value)
            {
                case int number:
                    Console.WriteLine(number);
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }

            switch (value)
            {
                case int number:
                    Console.WriteLine(number);
                    break;
                case string text when text == "foo":
                    Console.WriteLine("bar");
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }

            Console.ReadKey();
        }
    }
}
