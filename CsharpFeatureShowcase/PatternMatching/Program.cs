using System;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            object value = new Dummy();
            //object value = "foo";
            //object value = "bar";

            //Old
            {
                var dummy = value as Dummy;
                if (dummy != null)
                {
                    Console.WriteLine(dummy);
                }
            }

            //New
            {
                if (value is Dummy dummy)
                {
                    Console.WriteLine(dummy);
                }
            }

            //Can also be used in a switch, the order is important!
            switch (value)
            {
                case Dummy dummy:
                    Console.WriteLine(dummy);
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }

            //Extra conditions can be added, the order is important!
            switch (value)
            {
                case Dummy dummy:
                    Console.WriteLine(dummy);
                    break;
                case string text when text == "foo":
                    Console.WriteLine("bar");
                    break;
                case string text:
                    Console.WriteLine(text);
                    break;
            }

            Console.ReadKey();
        }
    }

    public class Dummy
    {
        public override string ToString()
        {
            return "Iam not null";
        }
    }
}
