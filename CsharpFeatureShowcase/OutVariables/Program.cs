using System;
using System.Collections.Generic;

namespace OutVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new Dictionary<int, int>();
            dic.Add(0, 5);

            if (dic.TryGetValue(0, out int value)) 
            {
                Console.WriteLine(value);
            }

            Console.ReadKey();
        }
    }
}
