using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var foo = new AsyncIntegers();

            await foreach(var integer in foo)
            {
                Console.WriteLine(integer);
            }

            Console.ReadKey();
        }
    }

    public class AsyncIntegers : IAsyncEnumerable<int>
    {
        public async IAsyncEnumerator<int> GetAsyncEnumerator()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return await Task.Run(() => i);
            }
        }
    }
}
