using System;

namespace RefLocal
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] { 1, 2, 3 };
            ref int referenceToValueInArray = ref GetReferenceToIndex(array, 1); //Just to demonstrate you can use ref returns but ref array[1] will work as well.
            referenceToValueInArray = 6;
            Console.WriteLine(referenceToValueInArray);
            Console.WriteLine(string.Join(", ", array));

            ref int GetReferenceToIndex(int[] inputArray, int index)
            {
                return ref inputArray[index];
            }

            Console.ReadKey();
        }
    }
}
