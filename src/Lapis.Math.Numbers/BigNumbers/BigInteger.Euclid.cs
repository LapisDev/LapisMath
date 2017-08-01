/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigInteger
 * Description : Represents an arbitrarily large interger.
 * Created     : 2015/6/25
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigInteger
    {
        /// <summary>
        /// Indicates whether the value of the current <see cref="BigInteger"/> object is an even number.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="BigInteger"/> object is an even number; otherwise, <see langword="false"/>.</value>
        public bool IsEven
        {
            get { return (this._data[0] & 0x1) == 0x0; }
        }

        /// <summary>
        /// Indicates whether the value of the current <see cref="BigInteger"/> object is an odd number.
        /// </summary>
        /// <value><see langword="true"/> if the value of the <see cref="BigInteger"/> object is an odd number; otherwise, <see langword="false"/>.</value>
        public bool IsOdd
        {
            get { return (this._data[0] & 0x1) == 0x1; }
        }

        /// <summary>
        /// Finds the greatest common divisor of two <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The greatest common divisor of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Gcd(BigInteger x, BigInteger y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            BigInteger remainder;
            while (!y.IsZero)
            {
                DivideRemainder(x, y, out remainder);
                x = y;
                y = remainder;
            }
            return Abs(x);
        }
        
        /// <summary>
        /// Finds the greatest common divisor of a collection of <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="values">The collection containing the <see cref="BigInteger"/> objects.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static BigInteger Gcd(IEnumerable<BigInteger> values)
        {
            if (values == null || values.Contains(null))
                throw new ArgumentNullException("values");
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var gcd = Abs(values.ElementAt(0));
            for (var i = 1; (i < values.Count()) && (gcd.CompareTo(One) > 0); i++)
                gcd = Gcd(gcd, values.ElementAt(i));
            return gcd;
        }
        
        /// <summary>
        /// Finds the greatest common divisor of an array of <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="values">The array containing the <see cref="BigInteger"/> objects.</param>
        /// <returns>The greatest common divisor of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static BigInteger Gcd(params BigInteger[] values)
        {
            return Gcd((IEnumerable<BigInteger>)values);
        }

        /// <summary>
        /// Finds the least common multiple of two <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>The least common multiple of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Lcm(BigInteger x, BigInteger y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            if ((x.IsZero) || (y.IsZero))
            {
                return Zero;
            }
            BigInteger r;
            return Abs(Multiply(DivideRemainder(x, Gcd(x, y), out r), y));
        }
        
        /// <summary>
        /// Finds the least common multiple of a collection of <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="values">The collection containing the <see cref="BigInteger"/> objects.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static BigInteger Lcm(IEnumerable<BigInteger> values)
        {
            if (values == null || values.Contains(null))
                throw new ArgumentNullException("values");
            if (values.Count() == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            var lcm = Abs(values.ElementAt(0));
            for (var i = 1; i < values.Count(); i++)
                lcm = Lcm(lcm, values.ElementAt(i));
            return lcm;
        }
        
        /// <summary>
        /// Finds the least common multiple of an array of <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="values">The array containing the <see cref="BigInteger"/> objects.</param>
        /// <returns>The least common multiple of <paramref name="values"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        public static BigInteger Lcm(params BigInteger[] values)
        {
            return Lcm((IEnumerable<BigInteger>)values);
        }
    }
}
