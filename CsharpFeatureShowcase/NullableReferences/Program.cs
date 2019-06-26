using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableReferences
{

    class Program
    {
        //Per file scope #nullable enable
        //Project scope <Nullable>enable</Nullable> in csproj, this used to be <NullableContextOptions>enable</NullableContextOptions> and before that <NullableReferenceTypes>true</NullableReferenceTypes>
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
            Example4();
        }

        public static void Example1()
        {
            NonNullableParameter(null); //Gives a warning because we are giving a null to a non nullable parameter
            NullableParameter(null);
        }

        public static void Example2()
        {
            SomeClass? value = null;

            NonNullableParameter(value); //Gives warning
            if (value != null)
            {
                NonNullableParameter(value); //No warning
            }
        }

        public static void Example3()
        {
            //Array consists of non nullable elements but will be initialized with nulls.
            var array = new SomeClass[100];

            var isNull = array[0] == null; //true
        }

        public static void Example4()
        {
            var dic = new Dictionary<int, SomeClass>();
            dic.TryGetValue(3, out var someClass); //someClass will not be assigned and thus be null.
            NonNullableParameter(someClass); //No warning even though we forgot to check the return of TryGetValue, might get fixed in a later version see https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-reference-types.md#null-tests

            var customDic = new CustomDictionary<int, SomeClass?>();
            if (customDic.TryGetValue(3, out var someClass1))
            {
                NonNullableParameter(someClass1);
            }
            
            NonNullableParameter(someClass1);
        }

        public static void NullableParameter(SomeClass? someclass) //Accepts a instance of SomeClass but the intent here its optional so it can be null
        {

        }

        public static void NonNullableParameter(SomeClass someclass) //Requires a instance of SomeClass so it cannot be null
        {
        }
    }

    public class SomeClass
    {

    }

    public class CustomDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _internalDictionary = new Dictionary<TKey, TValue>();
       
        public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue value)
        {
            return _internalDictionary.TryGetValue(key, out value);
        }
    }


    public class CustomList<T>
    {
        private List<T> _internalList = new List<T>();

        /// <summary>
        /// Can return null if the predicate never returns true.
        /// However we cannot make the return a type T? because that does not compile because the compiler gets confused between structs and classes.
        /// A way to workaround this is to define 2 extension methods. One with a class constraint for T and one with a struct constraint for T.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Search(Func<T, bool> predicate)
        {
            foreach (var item in _internalList)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default; //Gives warning because of nullability conflict.
        }
    }
}
