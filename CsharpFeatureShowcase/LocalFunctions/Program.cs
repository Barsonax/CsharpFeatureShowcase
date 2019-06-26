using System;
using System.Collections.Generic;

namespace LocalFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();

            var foo = new SomeEnumerable();
            foo.AlphabetSubset('0', '2');
            foo.AlphabetSubsetOld('0', '2');
            foo.AlphabetSubsetNew('0', '2');
        }

        public static void Example1()
        {
            var foo = 6;

            Example1LocalFuction();

            void Example1LocalFuction()
            {
                var foo2 = foo + 2;
            }
        }
    }

    public class SomeEnumerable
    {
        /// <summary>
        /// The naive implementation. Parameters will only be validated when the collection is enumerated.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
            for (char c = start; c < end; c++)
                yield return c;
        }

        /// <summary>
        /// Will validate the parameters immediately but the intent of <see cref="alphabetSubsetImplementationOld"/> is not entirely clear.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<char> AlphabetSubsetOld(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
            return alphabetSubsetImplementationOld(start, end);
        }

        private IEnumerable<char> alphabetSubsetImplementationOld(char start, char end)
        {
            for (char c = start; c < end; c++)
                yield return c;
        }

        /// <summary>
        /// Will validate the parameters immediately and its clear alphabetSubsetImplementation is only used inside this method.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<char> AlphabetSubsetNew(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImplementation();

            IEnumerable<char> alphabetSubsetImplementation()
            {
                for (char c = start; c < end; c++)
                    yield return c;
            }
        }
    }
}
