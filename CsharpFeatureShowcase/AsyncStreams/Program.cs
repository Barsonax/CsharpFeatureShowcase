using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var foo = new AsyncIntegersOld();

           foreach (var integer in foo)
            {
                var result = await integer;
                Console.WriteLine(result);
                Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");

            //var foo = new AsyncIntegers();

            //await foreach(var integer in foo)
            //{
            //    Console.WriteLine(integer);
            //    Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");
            //}
            //Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");

            Console.ReadKey();
        }
    }

    public class AsyncIntegersOld : IEnumerable<Task<int>>
    {
        public IEnumerator<Task<int>> GetEnumerator()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return Task.Run(() => i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class AsyncIntegers : IAsyncEnumerable<int>
    {
        public async IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < 100; i++)
            {
                yield return await Task.Run(() => i);
            }
        }
    }
}
