using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TupleCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        public static void CompareTuple()
        {
            var tuple1 = (1, 2);
            var tuple2 = (2, 6);
            Console.WriteLine(tuple1 == tuple2);
        }
    }
}
