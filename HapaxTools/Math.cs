using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HapaxTools
{
    public static class Math
    {
        public static class Exception
        {
            [Serializable]
            public class ModuloByZeroException : System.Exception
            {
                private static string msg = "Modulo by zero";
                public ModuloByZeroException() : base(msg) { }
                public ModuloByZeroException(System.Exception inner) : base(msg, inner) { }
            }

            [Serializable]
            public class DivisionByZeroException : System.Exception
            {
                private static string msg = "Division by zero";
                public DivisionByZeroException() : base(msg) { }
                public DivisionByZeroException(System.Exception inner) : base(msg, inner) { }
            }
        }

        public static int FlooredDiv(int a, int b)
        {
            if (b == 0) throw new Exception.DivisionByZeroException();
            return (a / b - Convert.ToInt32(((a < 0) ^ (b < 0)) && (a % b != 0)));
        }

        /// <summary>
        /// Floored modulo (the sign of the result is the same as the divisor's).
        /// </summary>
        public static int FMod(int a, int b)
        {
            if (b == 0) throw new Exception.ModuloByZeroException();
            return a - b * FlooredDiv(a, b);
        }
        
        /// <summary>
        /// Euclidean modulo (the result is always positive).
        /// </summary>
        public static int EMod(int a, int b)
        {
            if (b == 0) throw new Exception.ModuloByZeroException();
            int babs = System.Math.Abs(b);
            return a - babs * FlooredDiv(a, babs);
        }

        /// <summary>
        /// Greatest Common Divisor (Euclidean algorithm).
        /// </summary>
        public static long GreatestCommonDivisor(params long[] values)
        {
            return values.Aggregate(GCD);
        }

        /// <summary>
        /// Greatest Common Divisor (Euclidean algorithm).
        /// </summary>
        public static long GreatestCommonDivisor(IEnumerable<long> values)
        {
            return values.Aggregate(GCD);
        }

        public static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        /// <summary>
        /// Least Common Multiple (Reduction by the GCD).
        /// </summary>
        public static long LeastCommonMultiple(params long[] values)
        {
            return values.Aggregate(LCM);
        }

        /// <summary>
        /// Least Common Multiple (Reduction by the GCD).
        /// </summary>
        public static long LeastCommonMultiple(IEnumerable<long> values)
        {
            return values.Aggregate(LCM);
        }

        private static long LCM(long a, long b)
        {
            return System.Math.Abs(a * b) / GCD(a, b);
        }
    }
}
