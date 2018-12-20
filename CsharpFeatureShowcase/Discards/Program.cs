using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
