using System;
using System.Collections.Generic;

namespace Csharp70
{
    class Program
    {
        static void Main(string[] args)
        {
            RefLocal();
            Console.ReadKey();
        }

        public static void OutVariable()
        {
            var dic = new Dictionary<int, int>();
            dic.Add(0, 5);

            if (dic.TryGetValue(0, out int value))
            {
                Console.WriteLine(value);
            }
        }

        public static (int someNumber, int anotherNumber) Tuples()
        {
            (int someNumber, int anotherNumber) someTuple = (5, 7); //The underlying type is a ValueTuple<int, int> struct which is different from the older Tuple<int,int> class

            //The old style horrible naming that the old Tuple class has:
            Console.WriteLine(someTuple.Item1);
            Console.WriteLine(someTuple.Item2);

            //The newer much more readable style:
            Console.WriteLine(someTuple.someNumber);
            Console.WriteLine(someTuple.anotherNumber);

            return someTuple;
        }

        public static void DiscardTuple()
        {
            var (someNumber, _) = Tuples();

            Console.WriteLine(someNumber);
        }

        public static void DiscardOutVariable()
        {
            if (int.TryParse("2", out int _))
            {

            }
        }

        public static void PatternMatchingIf()
        {
            object value = 5;

            if (value is int number)
            {
                Console.WriteLine(number);
            }
        }

        public static void PatternMatchingSwitch()
        {
            object value = 5;

            switch (value)
            {
                case int number:
                    Console.WriteLine(number);
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }
        }

        public static void PatternMatchingSwitchWhen()
        {
            object value = 5;

            switch (value)
            {
                case int number:
                    Console.WriteLine(number);
                    break;
                case string text when text == "foo":
                    Console.WriteLine("bar");
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }
        }

        public static void RefLocal()
        {
            var array = new[] { 1, 2, 3 };
            ref var referenceToValueInArray = ref GetReferenceToIndex(array, 1); //Just to demonstrate you can use ref returns but ref array[1] will work as well.
            referenceToValueInArray = 6;
            Console.WriteLine(referenceToValueInArray);
            Console.WriteLine(string.Join(", ", array));

            ref int GetReferenceToIndex(int[] inputArray, int index) 
            {
                return ref inputArray[index];
            }
        }

        public class ExpressionEverything
        {
            private string _name;
            public string Name
            {
                get => _name;
                set => _name = value ??
                    throw new ArgumentNullException(paramName: nameof(value), message: "New name must not be null");
            }

            public ExpressionEverything(string name) => _name = name;

            public string SomeMethod() => _name;
        }
    }
}
