﻿using System;

namespace TupleCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        public static void CompareTuple()
        {
            var tuple1 = (1, 2);
            var tuple2 = (2, 6);
            Console.WriteLine(tuple1 == tuple2); //Equality is now automatically implemented for you.
        }
    }
}
