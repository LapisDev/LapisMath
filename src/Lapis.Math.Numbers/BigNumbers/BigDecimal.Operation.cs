/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigDecimal
 * Description : Represents an arbitrarily large decimal.
 * Created     : 2015/6/28
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigDecimal
    {
        #region Plus

        /// <summary>
        /// Adds the values of two specified <see cref="BigDecimal"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Add(BigDecimal left, BigDecimal right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Sign == 0)
                return right;
            else if (right.Sign == 0)
                return left;
            long s1 = left._scale, s2 = right._scale;
            long s = s1 > s2 ? s2 : s1;
            BigInteger int1 = BigInteger.Multiply9thPowerOfTen(left._intValue, s1 - s);
            BigInteger int2 = BigInteger.Multiply9thPowerOfTen(right._intValue, s2 - s);
            BigInteger intValue = BigInteger.Add(int1, int2);
            return new BigDecimal(intValue, s);
        }

        /// <summary>
        /// Adds the values of two specified <see cref="BigDecimal"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Subtracts a <see cref="BigDecimal"/> object from another <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Subtract(BigDecimal left, BigDecimal right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.Sign == 0)
                return left;
            long s1 = left._scale, s2 = right._scale;
            long s = s1 > s2 ? s2 : s1;
            BigInteger int1 = BigInteger.Multiply9thPowerOfTen(left._intValue, s1 - s);
            BigInteger int2 = BigInteger.Multiply9thPowerOfTen(right._intValue, s2 - s);
            BigInteger intValue = BigInteger.Subtract(int1, int2);
            return new BigDecimal(intValue, s);
        }

        /// <summary>
        /// Subtracts a <see cref="BigDecimal"/> object from another <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            return Subtract(left, right);
        }

        #endregion

        #region Multiply

        /// <summary>
        /// Multiplies two specified <see cref="BigDecimal"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Multiply(BigDecimal left, BigDecimal right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Sign == 0)
                return left;
            else if (right.Sign == 0)
                return right;
            long s1 = left._scale, s2 = right._scale;
            long s = s1 + s2;
            BigInteger int1 = left._intValue;
            BigInteger int2 = right._intValue;
            BigInteger intValue = BigInteger.Multiply(int1, int2);
            return new BigDecimal(intValue, s);
        }

        /// <summary>
        /// Multiplies two specified <see cref="BigDecimal"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            return Multiply(left, right);
        }

        #endregion

        #region Divide

        /// <summary>
        /// Divides a specified <see cref="BigDecimal"/> object by another specified <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static BigDecimal Divide(BigDecimal left, BigDecimal right, long precision)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.Sign == 0)
                throw new DivideByZeroException();
            if (left.Sign == 0)
                return left;
            int re = (int)(precision % 9);
            precision = precision / 9;
            long s1 = left._scale, s2 = right._scale;
            long s = s1 - s2;
            BigInteger int1 = left._intValue;
            BigInteger int2 = right._intValue;
            if (-s < precision + 1)
                int1 = BigInteger.Multiply9thPowerOfTen(int1, precision + 1 + s);
            else if (-s > precision + 1)
                int2 = BigInteger.Multiply9thPowerOfTen(int2, -precision - 1 - s);
            BigInteger rem;
            BigInteger intValue = BigInteger.DivideRemainder(int1, int2, out rem);
            if (re == 0)
                if (intValue.Digit(0) >= BaseInt / 2)
                    return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(intValue, 1), BigInteger.One), -precision);
                else
                    return new BigDecimal(BigInteger.Divide9thPowerOfTen(intValue, 1), -precision);
            else
            {
                int p = 1;
                for (int i = re; i < 9; i++)
                    p *= 10;
                int r = intValue.Digit(0) % p;
                if (r > p >> 1)
                    p = p - r;
                else
                    p = -r;
                return new BigDecimal(BigInteger.Add(intValue, BigInteger.FromInt32(p)), -precision - 1);
            }
        }

        #endregion


        #region Unary

        /// <summary>
        /// Negates a specified <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Negate(BigDecimal value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new BigDecimal(BigInteger.Negate(value._intValue), value._scale);
        }

        /// <summary>
        /// Negates a specified <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal operator -(BigDecimal value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Returns the value of the <see cref="BigDecimal"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="BigDecimal"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal operator +(BigDecimal value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        #endregion

        #region Round

        /// <summary>
        /// Rounds a <see cref="BigDecimal"/> object to a specified number of fractional digits.
        /// </summary>
        /// <param name="value">A <see cref="BigDecimal"/> object to be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>The number nearest to <paramref name="value"/> that contains a number of fractional digits equal to <paramref name="digits"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Round(BigDecimal value, long digits)
        {
            if (value == null)
                throw new ArgumentNullException();
            var count = -value._scale * 9 - digits;
            if (count <= 0)
                return value;
            var rem = (int)(count % 9);
            count = count / 9;
            if (rem == 0)
            {
                if (value.Sign > 0)
                    if (value._intValue.Digit(count - 1) >= BaseInt / 2)
                        return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                    else
                        return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
                else
                    if (value._intValue.Digit(count - 1) > BaseInt / 2)
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                else
                    return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
            }
            else
            {
                int p = 1;
                for (int i = 0; i < rem; i++)
                    p *= 10;
                int r = value._intValue.Digit(count) % p;
                if (value.Sign > 0)
                {
                    if (r >= p >> 1)
                        p = p - r;
                    else
                        p = -r;
                    return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
                else
                {
                    if (r > p >> 1)
                        p = p - r;
                    else
                        p = -r;
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
            }
        }

        /// <summary>
        /// Returns the smallest value containing the specified number of fractional digits that is greater than or equal to the <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">A <see cref="BigDecimal"/> object.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>The smallest value containing <paramref name="digits"/> fractional digits that is greater than or equal to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Ceiling(BigDecimal value, long digits)
        {
            if (value == null)
                throw new ArgumentNullException();
            var count = -value._scale * 9 - digits;
            if (count <= 0)
                return value;
            var rem = (int)(count % 9);
            count = count / 9;
            if (rem == 0)
            {
                if (value.Sign > 0)
                    if (value._intValue.Digit(count - 1) > 0 || count > 1)
                        return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                    else
                        return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
                else
                    return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
            }
            else
            {
                int p = 1;
                for (int i = 0; i < rem; i++)
                    p *= 10;
                int r = value._intValue.Digit(count) % p;
                if (value.Sign > 0)
                {
                    if (r > 0 || count > 0)
                        p = p - r;
                    else
                        p = 0;
                    return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
                else
                {
                    if (r > 0 || count > 0)
                        p = r;
                    else
                        p = 0;
                    return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
            }
        }

        /// <summary>
        /// Returns the largest value containing the specified number of fractional digits that is less than or equal to the <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">A <see cref="BigDecimal"/> object.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>The largest value containing <paramref name="digits"/> fractional digits that is less than or equal to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal Floor(BigDecimal value, long digits)
        {
            if (value == null)
                throw new ArgumentNullException();
            var count = -value._scale * 9 - digits;
            if (count <= 0)
                return value;
            var rem = (int)(count % 9);
            count = count / 9;
            if (rem == 0)
            {
                if (value.Sign > 0)
                    return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
                else
                    if (value._intValue.Digit(count - 1) > 0 || count > 1)
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                else
                    return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
            }
            else
            {
                int p = 1;
                for (int i = 0; i < rem; i++)
                    p *= 10;
                int r = value._intValue.Digit(count) % p;
                if (value.Sign > 0)
                {
                    if (r > 0 || count > 0)
                        p = r;
                    else
                        p = 0;
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
                else
                {
                    if (r > 0 || count > 0)
                        p = p - r;
                    else
                        p = 0;
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
            }
        }

        /// <summary>
        /// Rounds a <see cref="BigDecimal"/> object to a specified number of fractional digits. When a number is halfway between two others, it is rounded toward the nearest even number.
        /// </summary>
        /// <param name="value">A <see cref="BigDecimal"/> object to be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        /// <returns>The number nearest to <paramref name="value"/> that contains a number of fractional digits equal to <paramref name="digits"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal RoundHalfEven(BigDecimal value, long digits)
        {
            if (value == null)
                throw new ArgumentNullException();
            var count = -value._scale * 9 - digits;
            if (count <= 0)
                return value;
            var rem = (int)(count % 9);
            count = count / 9;
            if (rem == 0)
            {
                if (value.Sign > 0)
                {
                    var d = value._intValue.Digit(count - 1);
                    if (d > BaseInt / 2 ||
                        (d == BaseInt / 2 &&
                            (count > 1 ||
                                (value._intValue.Length > count &&
                                    (value._intValue.Digit(count) & 1) == 1
                                )
                            )
                        )
                       )
                        return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                    else
                        return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
                }
                else
                {
                    var d = value._intValue.Digit(count - 1);
                    if (d > BaseInt / 2 ||
                        (d == BaseInt / 2 &&
                            (count > 1 ||
                                (value._intValue.Length > count &&
                                    (value._intValue.Digit(count) & 1) == 0
                                )
                            )
                        )
                       )
                        return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.One), value._scale + count);
                    else
                        return new BigDecimal(BigInteger.Divide9thPowerOfTen(value._intValue, count), value._scale + count);
                }
            }
            else
            {
                int p = 1;
                for (int i = 0; i < rem; i++)
                    p *= 10;
                int d = value._intValue.Digit(count) / p;
                int r = value._intValue.Digit(count) % p;
                if (value.Sign > 0)
                {
                    if (r > p >> 1 ||
                          (r == p >> 1 &&
                            (count > 0 ||
                                (d & 1) == 1
                            )
                          )
                        )
                        p = p - r;
                    else
                        p = -r;
                    return new BigDecimal(BigInteger.Add(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
                else
                {
                    if (r > p >> 1 ||
                          (r == p >> 1 &&
                            (count > 0 ||
                                (d & 1) == 0
                            )
                          )
                        )
                        p = p - r;
                    else
                        p = -r;
                    return new BigDecimal(BigInteger.Subtract(BigInteger.Divide9thPowerOfTen(value._intValue, count), BigInteger.FromInt32(p)), value._scale + count);
                }
            }
        }

        #endregion
    }
}
