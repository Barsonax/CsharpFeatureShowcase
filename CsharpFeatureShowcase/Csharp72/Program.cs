using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp72
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        //For more info on writing high performance code in C# using these safe features see https://docs.microsoft.com/en-us/dotnet/csharp/write-safe-efficient-code
        public static void InParameter()
        {
            InParameterInternal(new ImmutableStruct()); //Even though this is a struct (= value type) no actual copy will be made to to the in parameter and the fact that its readonly

            void InParameterInternal(in ImmutableStruct input) //in parameter is a readonly reference to the value of the struct
            {
                //input.Value = 2; //This won't compile due to the in parameter even if Value is not readonly
            }
        }

        public static void RefReadonly() 
        {
            var mutableStruct = new MutableStruct();

            var immutableReferenceToMutableStruct = ReturnImmutableReference(mutableStruct);

            ref readonly MutableStruct ReturnImmutableReference(in MutableStruct input) //a ref readonly return is a reference which cannot be mutated
            {
                return ref input;
            }
        }


        public readonly struct ImmutableStruct //readonly expresses the intent of a immutable type but also enables some compiler optimizations by avoiding defensive copies as a added benefit!
        {
            public readonly int Value;
            public int Value2 { get; }
        }

        public struct MutableStruct
        {
            public int Value;
            public int Value2 { get; set; }
        }
    }
}
