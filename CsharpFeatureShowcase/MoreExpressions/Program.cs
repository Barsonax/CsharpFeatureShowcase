using System;

namespace MoreExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }

    public class ExpressionEverything
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(paramName: nameof(value), message: "New name must not be null");
        }

        public ExpressionEverything(string name) => _name = name;

        public string SomeMethod() => _name;
    }
}
