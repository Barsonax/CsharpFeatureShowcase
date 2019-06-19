using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            //old
            {
                var dic = new Dictionary<int, int>();
                dic.Add(0, 5);

                int value;
                if (dic.TryGetValue(0, out value))
                {
                    Console.WriteLine(value);
                }

                Console.ReadKey();
            }

            //new
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
}
