using System;
using System.Threading.Tasks;

namespace Csharp71
{
    class Program
    {
        static async Task Main(string[] args) //async was not possible in the main method in C# versions previous to 7.1
        {
        }

        static void DefaultLiteralExpression(Func<string, bool> whereClause = default)
        {

        }

        static void InferredTupleElementNames()
        {
            int count = 5;
            string label = "Colors used in the map";
            var pair = (count, label); // element names are "count" and "label"

            Console.WriteLine(pair.count);
            Console.WriteLine(pair.label);
        }
    }
}

