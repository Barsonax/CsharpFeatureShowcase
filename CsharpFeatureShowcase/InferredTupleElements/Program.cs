using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InferredTupleElements
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 5;
            var label = "Colors used in the map";
            var pair = (count, label); // element names are "count" and "label"

            Console.WriteLine(pair.count);
            Console.WriteLine(pair.label);
            Console.ReadKey();
        }
    }
}
