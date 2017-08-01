/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Integer
 * Description : Represents a 32-bit signed integer.
 * Created     : 2015/3/28
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class Integer
    {
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is an even number.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="Integer"/> object is an even number; otherwise, <see langword="false"/>.</value>
        public bool IsEven
        {
            get { return (this.value & 0x1) == 0x0; }
        }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is an odd number.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="Integer"/> object is an odd number; otherwise, <see langword="false"/>.</value>
        public bool IsOdd
        {
            get { return (this.value & 0x1) == 0x1; }
        }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is a power of two.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="Integer"/> object is a power of two; otherwise, <see langword="false"/>.</value>
        public bool IsPowerOfTwo
        {
            get { return this.value > 0 && (this.value & (this.value - 1)) == 0x0; }
        }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is a perfect square number.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="Integer"/> object is a perfect square number; otherwise, <see langword="false"/>.</value>
        public bool IsPerfectSquare
        {
            get
            {
                if (this.value < 0)
                    return false;
                int lastHexDigit = this.value & 0xF;
                if (lastHexDigit > 9)
                    return false;
                if (lastHexDigit == 0 || lastHexDigit == 1 || lastHexDigit == 4 || lastHexDigit == 9)
                {
                    int t = (int)System.Math.Floor(System.Math.Sqrt(this.value) + 0.5);
                    return (t * t) == this.value;
                }
                return false;
            }
        }

        /// <summary>
        /// Finds the greatest common divisor of two <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The greatest common divisor of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Integer Gcd(Integer x, Integer y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            while (!y.IsZero)
            {
                var remainder = Remainder(x, y);
                x = y;
                y = remainder;
            }
            return Abs(x);
        }
        
        /// <summary>
        /// Finds the greatest common divisor of a collection of <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="values">The collection containing the <see cref="Integer"/> objects.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Gcd(IEnumerable<Integer> values)
        {
            if (values == null || values.Contains(null))
                throw new ArgumentNullException("values");
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var gcd = Abs(values.ElementAt(0));
            for (var i = 1; (i < values.Count()) && (gcd > One); i++)
                gcd = Gcd(gcd, values.ElementAt(i));
            return gcd;
        }
        
        /// <summary>
        /// Finds the greatest common divisor of an array of <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="values">The array containing the <see cref="Integer"/> objects.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Gcd(params Integer[] values)
        {
            return Gcd((IEnumerable<Integer>)values);
        }

        /// <summary>
        /// Finds the least common multiple of two <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The least common multiple of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Integer Lcm(Integer x, Integer y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            if ((x.IsZero) || (y.IsZero))
            {
                return Zero;
            }
            return Abs(Multiply(Divide(x, Gcd(x, y)), y));
        }
        
        /// <summary>
        /// Finds the least common multiple of a collection of <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="values">The collection containing the <see cref="Integer"/> objects.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Lcm(IEnumerable<Integer> values)
        {
            if (null == values)
                throw new ArgumentNullException("values");
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var lcm = Abs(values.ElementAt(0));
            for (var i = 1; i < values.Count(); i++)
                lcm = Lcm(lcm, values.ElementAt(i));
            return lcm;
        }
        
        /// <summary>
        /// Finds the least common multiple of an array of <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="values">The array containing the <see cref="Integer"/> objects.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Lcm(params Integer[] values)
        {
            return Lcm((IEnumerable<Integer>)values);
        }

        /// <summary>
        /// Finds the greatest common divisor of two integers.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The greatest common divisor of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static int Gcd(int x, int y)
        {
            while (y != 0)
            {
                var remainder = x % y;
                x = y;
                y = remainder;
            }
            return System.Math.Abs(x);
        }
        
        /// <summary>
        /// Finds the greatest common divisor of a collection of integers.
        /// </summary>
        /// <param name="values">The collection containing the integers.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Gcd(IEnumerable<int> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var gcd = System.Math.Abs(values.ElementAt(0));
            for (var i = 1; (i < values.Count()) && (gcd > One); i++)
                gcd = Gcd(gcd, values.ElementAt(i));
            return gcd;
        }
        
        /// <summary>
        /// Finds the greatest common divisor of an array of integers.
        /// </summary>
        /// <param name="values">The array containing the integers.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static Integer Gcd(params int[] values)
        {
            return Gcd((IEnumerable<int>)values);
        }

        /// <summary>
        /// Finds the least common multiple of two integers.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The least common multiple of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static int Lcm(int x, int y)
        {
            if ((x == 0) || (y == 0))
            {
                return 0;
            }
            return System.Math.Abs(x / Gcd(x, y) * y);
        }
        
        /// <summary>
        /// Finds the least common multiple of a collection of integers.
        /// </summary>
        /// <param name="values">The collection containing the integers.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static int Lcm(IEnumerable<int> values)
        {
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var lcm = System.Math.Abs(values.ElementAt(0));
            for (var i = 1; i < values.Count(); i++)
                lcm = Lcm(lcm, values.ElementAt(i));
            return lcm;
        }
        
        /// <summary>
        /// Finds the least common multiple of an array of integers.
        /// </summary>
        /// <param name="values">The array containing the integers.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static int Lcm(params int[] values)
        {
            return Lcm((IEnumerable<int>)values);
        }
    }
}
