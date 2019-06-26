using System;

namespace Discards
{
    class Program
    {
        static void Main(string[] args)
        {
            var (someNumber, _) = (2,6);

            Console.WriteLine(someNumber);

            if (int.TryParse("2", out int _))
            {

            }

            Console.ReadKey();
        }
    }
}
