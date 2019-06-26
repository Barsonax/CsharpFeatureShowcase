using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace EnhancedGenericConstraints
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UnmanagedExtensions.ComputeHash(new UnmanagedStruct());

            Console.WriteLine(EnumTraits<EnumLong>.MinValue); // -1
            Console.WriteLine(EnumTraits<EnumLong>.MaxValue); // 2
            Console.WriteLine(EnumTraits<EnumLong>.IsValid(0)); //False

            Console.WriteLine(EnumLong.X.HasFlags(EnumLong.Y)); //False, allocation free compared to the build in hasflags.
        }
    }

    #region unmanagedconstraint
    public struct UnmanagedStruct
    {
        //public int[] bla; //Adding a reference type field to this struct will no longer make it a valid 'unmanaged' struct
    }

    public static class UnmanagedExtensions
    {
        public static unsafe byte[] ComputeHash<T>(T data) where T : unmanaged
        {
            var bytes = (byte*)(&data);
            using (SHA1 sha1 = SHA1.Create())
            {
                int size = sizeof(T);
                using (var ms = new UnmanagedMemoryStream(bytes, size))
                {
                    return sha1.ComputeHash(ms);
                }
            }
        }
    }

    #endregion

    #region enumconstraint
    enum EnumLong : long
    {
        X = -1,
        Y = 1,
        Z = 2,
    }

    public static class EnumTraits<TEnum> where TEnum : struct, Enum
    {
        private static readonly HashSet<TEnum> _valuesSet;
        static EnumTraits()
        {
            Type type = typeof(TEnum);
            Type underlyingType = Enum.GetUnderlyingType(type);

            EnumValues = (TEnum[])Enum.GetValues(typeof(TEnum));
            _valuesSet = new HashSet<TEnum>(EnumValues);

            List<long> longValues =
                EnumValues
                    .Select(v => Convert.ChangeType(v, underlyingType))
                    .Select(Convert.ToInt64)
                    .ToList();

            IsEmpty = longValues.Count == 0;
            if (!IsEmpty)
            {
                List<long> sorted = longValues.OrderBy(v => v).ToList();
                MinValue = sorted.Min();
                MaxValue = sorted.Max();
            }
        }

        public static bool IsEmpty { get; }
        public static long MinValue { get; }
        public static long MaxValue { get; }
        public static TEnum[] EnumValues { get; }

        // This version is almost an order of magnitude faster then Enum.IsDefined
        public static bool IsValid(TEnum value) => _valuesSet.Contains(value);
    }

    public static class EnumExtensions
    {
        /// <summary>
        /// Allocation-free version of <see cref="Enum.HasFlag(Enum)"/>
        /// </summary>
        public static bool HasFlags<TEnum>(this TEnum left, TEnum right) where TEnum : struct, Enum
        {
            Func<TEnum, long> fn = Converter<TEnum>.ConverterFn;
            return (fn(left) & fn(right)) != 0;
        }

        private static class Converter<TEnum> where TEnum : struct, Enum
        {
            public static readonly Func<TEnum, long> ConverterFn = EnumConverter.CreateConvertToLong<TEnum>();
        }
    }
    #endregion

    #region delegateConstraint
    public static class EnumConverter
    {
        public static Func<T, long> CreateConvertToLong<T>() where T : struct, Enum
        {
            DynamicMethodGenerator<Func<T, long>> generator = DynamicMethodGenerator.Create<Func<T, long>>("ConvertToLong");

            ILGenerator ilGen = generator.GetILGenerator();

            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Conv_I8);
            ilGen.Emit(OpCodes.Ret);
            return generator.Generate();
        }
    }

    /// <summary>
    /// Non-generic facade for creating dynamic methods for a given delegate type
    /// </summary>
    public static class DynamicMethodGenerator
    {
        public static DynamicMethodGenerator<TDelegate> Create<TDelegate>(string name)
            where TDelegate : Delegate
            => new DynamicMethodGenerator<TDelegate>(name);
    }

    /// <summary>
    /// Generator class for constructing delegates of type <typeparamref name="TDelegate"/>.
    /// </summary>
    public sealed class DynamicMethodGenerator<TDelegate> where TDelegate : Delegate
    {
        private readonly DynamicMethod _method;

        internal DynamicMethodGenerator(string name)
        {
            MethodInfo invoke = typeof(TDelegate).GetMethod("Invoke");

            Type[] parameterTypes = invoke.GetParameters().Select(p => p.ParameterType).ToArray();

            _method = new DynamicMethod(name, invoke.ReturnType,
                parameterTypes, restrictedSkipVisibility: true);
        }

        public ILGenerator GetILGenerator() => _method.GetILGenerator();

        public TDelegate Generate()
        {
            return (TDelegate)_method.CreateDelegate(typeof(TDelegate));
        }
    }
    #endregion
}
