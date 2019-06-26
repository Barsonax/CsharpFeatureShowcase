using System;

namespace DefaultInterfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IVehicle bmw = new Bmw();
            bmw.DefaultMessage();

            IVehicle audi = new Audi();
            audi.DefaultMessage();

            // Default interfaces enable the traits programming pattern? https://en.wikipedia.org/wiki/Trait_(computer_programming)
            // For more info and examples on traits see also the rust programming language which uses traits instead of inheritance: https://doc.rust-lang.org/book/ch10-02-traits.html
        }
    }


    interface IVehicle
    {
        //default implementation 
        void DisplayMessage();

        void DefaultMessage() { Console.WriteLine("I am  inside default method in the interface!"); }

    }

    public class Bmw : IVehicle
    {
        public void DisplayMessage()
        {
            Console.WriteLine("I am BMW!!!");
        }
    }

    public class Audi : IVehicle
    {
        public int Number => throw new NotImplementedException();

        public void DisplayMessage()
        {
            Console.WriteLine("I am AUDI!!!");
        }
        public void DefaultMessage() => Console.WriteLine("I am  inside audi class!");
    }
}
