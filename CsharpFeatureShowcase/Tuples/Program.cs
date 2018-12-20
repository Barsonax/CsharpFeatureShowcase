using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuples
{
    class Program
    {
        static void Main(string[] args)
        {
            (int someNumber, int anotherNumber) someTuple = (5, 7); //The underlying type is a ValueTuple<int, int> struct which is different from the older Tuple<int,int> class

            //The old style horrible naming that the old Tuple class has:
            Console.WriteLine(someTuple.Item1);
            Console.WriteLine(someTuple.Item2);

            //The newer much more readable style:
            Console.WriteLine(someTuple.someNumber);
            Console.WriteLine(someTuple.anotherNumber);

            Console.ReadKey();
        }
    }
}
