﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullableReferences
{
    class Program
    {
        #nullable enable
        static void Main(string[] args)
        {
            NonNullableParameter(null); //Gives a warning because we are giving a null to a non nullable parameter
            NullableParameter(null);

            SomeClass? value = null;

            NonNullableParameter(value); //Gives warning
            if(value != null)
            {
                NonNullableParameter(value); //No warning
            }
        }

        public static void NullableParameter(SomeClass? someclass) //Accepts a instance of SomeClass but the intent here its optional so it can be null
        {

        }

        public static void NonNullableParameter(SomeClass someclass) //Requires a instance of SomeClass so it cannot be null
        {

        }

        public class SomeClass
        {

        }
    }
}