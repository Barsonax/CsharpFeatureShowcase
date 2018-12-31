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
        public void DisplayMessage()
        {
            Console.WriteLine("I am AUDI!!!");
        }
        public void DefaultMessage() => Console.WriteLine("I am  inside audi class!");
    }
}
