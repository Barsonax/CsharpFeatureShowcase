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

        public string NameOld
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "New name must not be null");
                }
                _name = value;
            }
        }

        public string NameNew
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value), "New name must not be null");
        }

        public ExpressionEverything(string name) => _name = name;

        public string SomeMethod() => _name;
    }
}
