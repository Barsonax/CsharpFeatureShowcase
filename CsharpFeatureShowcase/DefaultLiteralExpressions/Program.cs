using System;

namespace DefaultLiteralExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void DefaultLiteralExpressionOld(Func<string, bool> whereClause = default(Func<string, bool>))
        {

        }

        static void DefaultLiteralExpressionNew(Func<string, bool> whereClause = default)
        {

        }
    }
}
