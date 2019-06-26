using System;
using System.Collections.Generic;

namespace LocalFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = new SomeEnumerable();
            foo.AlphabetSubset('0', '2');
            foo.AlphabetSubset2('0', '2');
            foo.AlphabetSubset3('0', '2');
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
        /// Will validate the parameters immediately but the intent of <see cref="alphabetSubsetImplementation"/> is not entirely clear.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<char> AlphabetSubset2(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
            return alphabetSubsetImplementation(start, end);
        }

        private IEnumerable<char> alphabetSubsetImplementation(char start, char end)
        {
            for (char c = start; c < end; c++)
                yield return c;
        }

        /// <summary>
        /// Will validate the parameters immediately and its clear <see cref="alphabetSubsetImplementation"/> is only used inside this method.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<char> AlphabetSubset3(char start, char end)
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
