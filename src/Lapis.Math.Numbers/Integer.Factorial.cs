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
        /// Returns the factorial of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The factorial of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Factorial(Integer value)
        {
            if (value == null)
                throw new ArgumentNullException();  
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (value == 0)
            {
                return Integer.One;
            }
            Integer result = value;
            for (Integer i = 1; i < value; i += 1)
            {
                result *= i;
            }
            return result;
        }

        /// <summary>
        /// Returns the binomial coefficients of the specified two integers.
        /// </summary>
        /// <param name="n">An integer.</param>
        /// <param name="m">The other integer.</param>
        /// <returns>The binomial coefficients of <paramref name="n"/> and <paramref name="m"/>. (<paramref name="n"/>C<paramref name="m"/>)</returns>
        /// <exception cref="ArgumentNullException">The parameter is null.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Binomial(Integer n, Integer m)
        {
            if (n == null || m == null)
                throw new ArgumentNullException();  
            if (m < 0 || n < 0 || m > n)
            {
                return 0;
            }
            Integer result = 1;
            for (Integer i = 1, j = n; i <= m; i += 1, j -= 1)
            {
                result *= j;
                result /= i;
            }
            return result;
        }

        /// <summary>
        /// Returns the factorial of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The factorial of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static int Factorial(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (value == 0)
            {
                return 1;
            }
            int result = value;
            for (int i = 1; i < value; i += 1)
            {
                result *= i;
            }
            return result;
        }

        /// <summary>
        /// Returns the binomial coefficients of the specified two integers.
        /// </summary>
        /// <param name="n">An integer.</param>
        /// <param name="m">The other integer.</param>
        /// <returns>The binomial coefficients of <paramref name="n"/> and <paramref name="m"/>. (<paramref name="n"/>C<paramref name="m"/>)</returns>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static int Binomial(int n, int m)
        {
            if (m < 0 || n < 0 || m > n)
            {
                return 0;
            }
            int result = 1;
            for (int i = 1, j = n; i <= m; i += 1, j -= 1)
            {
                result *= j;
                result /= i;
            }
            return result;
        }
    }
}
