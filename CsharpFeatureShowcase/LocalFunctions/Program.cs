using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
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
        public static IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
            for (var c = start; c < end; c++)
                yield return c;
        }

        /// <summary>
        /// Will validate the parameters immediately but the intent of <see cref="alphabetSubsetImplementation"/> is not entirely clear.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<char> AlphabetSubset2(char start, char end)
        {
            if (start < 'a' || start > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
            if (end < 'a' || end > 'z')
                throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

            if (end <= start)
                throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");
            return alphabetSubsetImplementation(start, end);
        }

        private static IEnumerable<char> alphabetSubsetImplementation(char start, char end)
        {
            for (var c = start; c < end; c++)
                yield return c;
        }

        /// <summary>
        /// Will validate the parameters immediately and its clear <see cref="alphabetSubsetImplementation"/> is only used inside this method.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<char> AlphabetSubset3(char start, char end)
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
                for (var c = start; c < end; c++)
                    yield return c;
            }
        }
    }
}
