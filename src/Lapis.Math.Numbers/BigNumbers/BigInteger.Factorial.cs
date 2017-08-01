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
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigInteger
    {
        /// <summary>
        /// Returns the factorial of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The factorial of <paramref name="value"/>.</returns>
        public static BigInteger Factorial(long value)
        {
            long h = value >> 1;
            BigInteger q = BigInteger.FromInt64(h);
            q = PosMultiply(q, q);
            BigInteger n = FromInt64(value);
            BigInteger two = FromInt32(2);
            BigInteger r = (value & 1) == 1 ?
                PosMultiply(PosMultiply(two, q), n)
                : PosMultiply(two, q);
            for (long d = 1; d < value - 2; d += 2)
            {
                q = Subtract(q, FromInt64(d));
                r = PosMultiply(r, q);
            }
            return r;
        }

        /// <summary>
        /// Returns the binomial coefficients of the specified two integers.
        /// </summary>
        /// <param name="n">An integer.</param>
        /// <param name="m">The other integer.</param>
        /// <returns>The binomial coefficients of <paramref name="n"/> and <paramref name="m"/>. (<paramref name="n"/>C<paramref name="m"/>)</returns>
        public static BigInteger Binomial(long n, long m)
        {
            if (m < 0 || n < 0 || m > n)
            {
                return Zero;
            }
            BigInteger result = One;
            for (long i = 1, j = n; i <= m; i += 1, j -= 1)
            {
                result = BigInteger.Multiply(result, BigInteger.FromInt64(j));
                BigInteger r;
                result = BigInteger.DivideRemainder(result, BigInteger.FromInt64(i), out r);
            }
            return result;
        }       
    }
}
