using System;
using System.Collections.Generic;
using System.Text;

namespace MathAlgNet.Matrix
{
    public static class TypeOperation<T> where T : struct
    {
        public static T Add<T>(T number1, T number2)
        {
            dynamic a = number1;
            dynamic b = number2;
            return a + b;
        }

        public static T Sub<T>(T number1, T number2)
        {
            dynamic a = number1;
            dynamic b = number2;
            return a - b;
        }

        public static T Multiplication(T number1, T number2)
        {
            dynamic a = number1;
            dynamic b = number2;
            return a * b;
        }
    }
}
