using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Horology;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Runtime.InteropServices;

namespace MemoryAndSpan
{
    class Program
    {
        static void Main(string[] args)
        {
            // Working with strings
            // Old
            string text = "Imagine this is some extremely long string.....................";

            string substring = text.Substring(0, text.Length - 1); //Allocates a new string with length - 1, expensive! Surely we can do better?

            // New
            ReadOnlySpan<char> textSpan = text.AsSpan(); //Converts the string to a span (in dotnet core this can be done implicitly), no heap allocations will happen so this is very fast.
            ReadOnlySpan<char> fastSubstring = textSpan.Slice(0, textSpan.Length); //Does not allocate a new string, fast!

            // Also works for unmanaged memory
            unsafe
            {
                IntPtr unmanagedHandle = Marshal.AllocHGlobal(256);
                Span<byte> unmanagedBytes = new Span<byte>(unmanagedHandle.ToPointer(), 256);

                // Do something with this unmanaged memory

                Marshal.FreeHGlobal(unmanagedHandle);
            }

            // And even stack allocated memory (and yes it works without unsafe!)
            Span<byte> stackallocatedBytes = stackalloc byte[256];
            // Do something with this stack allocated memory

            // Because of its extreme versatility span is limited to the stack as such the following will not compile because it cannot be boxed
            //var castedSpan = textSpan as object; //Error CS0039  Cannot convert type 'System.ReadOnlySpan<char>' to 'object' via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion

            // Neither can you use it in a closure
            Action action = () =>
            {
                //textSpan.Slice(); // Error CS8175  Cannot use ref local 'textSpan' inside an anonymous method, lambda expression, or query expression
            };

            // For scenarios where a span is needed outside the stack Memory can be used. 
            ReadOnlyMemory<char> memory = text.AsMemory();
            ReadOnlySpan<char> memoryspan = memory.Span; //Memory is a factory for spans;


            #region BadCode
            // We can also do weird stuff though.... Atleast its using safe code. As you will see below use this with care!
            Memory<char> foo = MemoryMarshal.AsMemory(text.AsMemory());
            foo.Span[0] = 'Z'; //You thought strings were immutable? Think again.
            Console.WriteLine(text); //Prints out text but the first character is replaced with a Z. Yup we modified the original string.
            string text2 = "Imagine this is some extremely long string....................."; //But now we get to the weird part
            Console.WriteLine(text2); //This prints out text2 but here the first character is also replaced with a Z! Guess why.
            #endregion

            //BenchmarkRunner.Run<ReinterpretBinaryDataBenchmark>();
            Console.ReadKey();
        }
    }

    // You can use a Span in a field provided the field is part of a ref struct.
    public ref struct CustomStructWithSpan
    {
        public Span<byte> SpanField;
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
