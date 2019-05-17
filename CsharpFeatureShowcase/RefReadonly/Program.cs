using System;
using System.Numerics;
using System.Runtime.CompilerServices;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace RefReadonly
{
    [Config(typeof(CustomJob))]
    [MemoryDiagnoser]
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
            Console.ReadKey();
        }

//BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.503 (1809/October2018Update/Redstone5)
//Intel Core i7-7700HQ CPU 2.80GHz(Kaby Lake), 1 CPU, 8 logical and 4 physical cores
//.NET Core SDK = 3.0.100-preview-009812

// [Host]       : .NET Core 2.1.9 (CoreCLR 4.6.27414.06, CoreFX 4.6.27415.01), 64bit RyuJIT
//  LegacyJitX64 : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3416.0
//  RyuJitX64    : .NET Core 2.1.9 (CoreCLR 4.6.27414.06, CoreFX 4.6.27415.01), 64bit RyuJIT

//Platform=X64 IterationTime = 250.0000 ms

//|        Method |          Job |       Jit | Runtime |       Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
//|-------------- |------------- |---------- |-------- |-----------:|----------:|----------:|------:|------:|------:|----------:|
//|   MutableTest | LegacyJitX64 | LegacyJit |     Clr |   736.5 ns | 14.685 ns | 21.525 ns |     - |     - |     - |         - |
//| ImmutableTest | LegacyJitX64 | LegacyJit |     Clr |   180.3 ns |  3.548 ns |  4.086 ns |     - |     - |     - |         - |
//|   MutableTest |    RyuJitX64 |    RyuJit |    Core | 1,192.3 ns |  7.273 ns |  6.073 ns |     - |     - |     - |         - |
//| ImmutableTest |    RyuJitX64 |    RyuJit |    Core |   177.8 ns |  3.488 ns |  4.656 ns |     - |     - |     - |         - |

        [Benchmark]
        public void MutableTest()
        {
            var mutableStruct = new MutableStruct();
            for (int i = 0; i < 100; i++)
            {
                ref readonly var immutableReferenceToMutableStruct = ref ReturnImmutableReference(in mutableStruct);
                immutableReferenceToMutableStruct.DoSomething();
                //immutableReferenceToMutableStruct.Value2 = 5; //This won't compile due to ref readonly return even if Value is not readonly     
            }
        }

        [Benchmark]
        public void ImmutableTest()
        {
            var immutableStruct = new ImmutableStruct();
            for (int i = 0; i < 100; i++)
            {
                ref readonly var immutableReferenceToImmutableStruct = ref ReturnImmutableReference(in immutableStruct);
                immutableReferenceToImmutableStruct.DoSomething();
                //immutableReferenceToImmutableStruct.Value = 5; //This won't compile due to ref readonly return even if Value is not readonly    
            }
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

        [MethodImpl(MethodImplOptions.NoInlining)]
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int DoSomething()
        {
            return 5;
        }
    }

    public class CustomJob : ManualConfig
    {
        public CustomJob()
        {
            Add(
                Job.RyuJitX64
                    .With(Runtime.Core)
                    .WithIterationTime(TimeInterval.FromMilliseconds(250)));

            Add(
                Job.LegacyJitX64
                    .With(Runtime.Clr)
                    .WithIterationTime(TimeInterval.FromMilliseconds(250)));
        }
    }
}
