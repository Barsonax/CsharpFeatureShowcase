using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InParameter
{
    class Program
    {
        //For more info on writing high performance code in C# using these safe features see https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code
        static void Main(string[] args)
        {
            InParameterInternal(new MutableStruct()); 

            //a in parameter declares the intent that this method will use the parameter but will not modify it.
            void InParameterInternal(in MutableStruct input) 
            {
                //input.Value = 2; //This won't compile due to the in parameter even if Value is not readonly
            }
        }
    }

    public struct MutableStruct //readonly expresses the intent of a immutable type but also enables some compiler optimizations by avoiding defensive copies as a added benefit!
    {
        public int Value;
        public int Value2 { get; set; }
    }
}
