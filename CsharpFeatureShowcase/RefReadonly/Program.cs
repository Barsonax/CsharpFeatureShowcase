using System;
using System.Numerics;
using System.Runtime.CompilerServices;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RefReadonly
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
            Console.ReadKey();
        }

        [Benchmark]
        public void MutableTest()
        {
            var mutableStruct = new MutableStruct();
            for (int i = 0; i < 100; i++)
            {
                ref readonly var immutableReferenceToMutableStruct = ref ReturnImmutableReference(in mutableStruct);
                immutableReferenceToMutableStruct.DoSomething();
            }

            //immutableReferenceToMutableStruct.Value = 5; //This won't compile due to ref readonly return even if Value is not readonly                  
        }

        [Benchmark]
        public void ImmutableTest()
        {
            var immutableStruct = new ImmutableStruct();
            for (int i = 0; i < 100; i++)
            {
                ref readonly var immutableReferenceToMutableStruct = ref ReturnImmutableReference(in immutableStruct);
                immutableReferenceToMutableStruct.DoSomething();
            }

            //immutableReferenceToMutableStruct.Value = 5; //This won't compile due to ref readonly return even if Value is not readonly

            //a ref readonly return is a reference which cannot be mutated               
        }

        public ref readonly T ReturnImmutableReference<T>(in T input)
        {
            return ref input; //a ref readonly return is a reference which cannot be mutated   
        }
    }

    public struct MutableStruct
    {
        public double Value;
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }
        public double Value5 { get; set; }
        public double Value6 { get; set; }
        public double Value7 { get; set; }
        public double Value8 { get; set; }
        public Vector4 SomeVector;

        public int DoSomething()
        {
            return 5;
        }
    }

    public readonly struct ImmutableStruct
    {
        public readonly double Value;
        public double Value2 { get; }
        public double Value3 { get; }
        public double Value4 { get; }
        public double Value5 { get; }
        public double Value6 { get; }
        public double Value7 { get; }
        public double Value8 { get; }
        public readonly Vector4 SomeVector;

        public int DoSomething()
        {
            return 5;
        }
    }
}
