/********************************************************************************
 * 模块名称 : Lapis.Math.Numbers.BigNumbers
 * 类 名 称 : BigDecimal
 * 类 描 述 : 用于大小数的表示。
 * 创建日期 : 2015/6/28
 * 备    注 :
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
        #region Exp

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="value">A number specifying a power.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The number e raised to the power <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Exp(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            return ExpTaylor(value, precision);
        }

        private static BigDecimal ExpTaylor(BigDecimal value, long precision)
        {
            if (value.Sign == 0)
                return One;
            BigDecimal factorial = BigDecimal.One;
            BigDecimal power = value;
            BigDecimal sumPrev, sum;
            sum = BigDecimal.Add(BigDecimal.One, value);
            int i = 2;
            BigDecimal term;
            do
            {
                power = BigDecimal.Multiply(power, value);
                factorial = BigDecimal.Multiply(factorial, BigDecimal.FromInt32(i));
                term = BigDecimal.Divide(power, factorial, precision);
                sumPrev = sum;
                sum = BigDecimal.Add(sum, term);
                i++;
            } while (!sum.Equals(sumPrev));
            return sum;
        }

        #endregion

        #region Log

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="value">The number whose logarithm is to be found.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The natural logarithm of <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="value"/> is zero or negative.</exception>
        public static BigDecimal Ln(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign <= 0)
                throw new ArithmeticException(ExceptionResource.Undefined);
            long magnitude = value.TenPower;
            if (magnitude < 2 && magnitude > -2)
                return LnArctanh(value, precision);
            else
            {
                long scale = 9 * value._scale - magnitude;
                BigDecimal n = MultiplyPowerOfTen(value._intValue, scale);
                BigDecimal ln = LnArctanh(n, precision);
                BigDecimal ten = FromInt32(10);
                BigDecimal ln10 = LnArctanh(ten, precision);
                return Add(ln, RoundHalfEven(Multiply(ln10, FromInt64(magnitude)), precision));
            }
        }                

        private static BigDecimal LnArctanh(BigDecimal value, long precision)
        {
            long pre = precision + 1;
            BigDecimal x = value,
                tolerance = MultiplyPowerOfTen(BigInteger.FromInt32(5), -pre);
            BigDecimal y = Divide(Subtract(x, One), Add(x, One), pre);
            BigDecimal two = FromInt32(2);
            x = Multiply(y, two);
            BigDecimal y2 = Multiply(y, y);
            BigDecimal power = y;
            BigDecimal term;
            int i = 3;
            do
            {
                power = Multiply(power, y2);
                term = Multiply(power, two);
                term = Divide(term, FromInt32(i), pre);
                x = Add(x, term);
                i += 2;
            } while (Abs(term).CompareTo(tolerance) > 0);
            return Round(x, precision);
        }

        private static BigDecimal LnNewton(BigDecimal value, long precision)
        {
            long pre = precision + 1;
            BigDecimal x = value,
                tolerance = MultiplyPowerOfTen(BigInteger.FromInt32(5), -pre);
            BigDecimal power;
            BigDecimal term;
            do
            {
                power = Exp(x, pre);
                term = Subtract(power, value);
                term = Divide(term, power, pre);
                x = Subtract(x, term);
            } while (Abs(term).CompareTo(tolerance) > 0);
            return Round(x, precision);
        }

        #endregion

        /// <summary>
        /// Returns the specified root of the number.
        /// </summary>
        /// <param name="value">The number whose root is to be found</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <param name="index">The order of the root.</param>
        /// <returns>The <paramref name="index"/>th root of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="index"/> is zero.</exception>
        public static BigDecimal Root(BigDecimal value, long index, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (index == 0)
                throw new ArithmeticException(ExceptionResource.Undefined);
            if (index < 0)
                return Divide(One, Root(value, -index, precision), precision);
            long pre = precision + 1;
            BigDecimal x = value,
                i = BigDecimal.FromBigInteger(BigInteger.FromInt64(index)),
                j = BigDecimal.FromBigInteger(BigInteger.FromInt64(index - 1)),
                tolerance = MultiplyPowerOfTen(BigInteger.FromInt32(5), -pre);
            BigDecimal xPrev, powi, powj, num, den, eps;
            x = Divide(x, i, precision);
            do
            {
                powj = Pow(x, index - 1, pre);
                powi = Multiply(powj, x);
                num = Add(value, Multiply(powi, j));
                den = Multiply(powj, i);
                xPrev = x;
                x = Divide(num, den, pre);
                eps = Abs(Subtract(x, xPrev));
            } while (eps.CompareTo(tolerance) > 0);
            return Round(x, precision);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="value">The number to be raised to a power.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="value"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="value"/> and <paramref name="exponent"/> are zero.</exception>
        public static BigDecimal Pow(BigDecimal value, long exponent, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (exponent == 0 && value.IsZero)
                throw new ArithmeticException(ExceptionResource.Undefined);
            if (exponent < 0)
                return Divide(One, Pow(value, -exponent, precision), precision);

            BigDecimal x = value;
            BigDecimal pow = One;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    pow = Multiply(pow, x);
                x = Multiply(x, x);
                exponent >>= 1;
            }
            return Round(pow, precision);
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Abs(BigDecimal value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return Negate(value);
            else
                return value;
        }

        /// <summary>
        /// Gets the ratio of the circumference of a circle to its diameter, π.
        /// </summary>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The value of π, accurate to <paramref name="precision"/> decimal places.</returns>
        public static BigDecimal Pi(long precision)
        {
            BigDecimal sixteen = FromInt32(16),
                two = FromInt32(2),
                four = FromInt32(4),
                five = FromInt32(5),
                six = FromInt32(6),
                eight = FromInt32(8);
            BigDecimal pi = Zero;
            BigDecimal term, eightk = Zero, pow16 = One;
            int k = 0;
            do
            {
                term = Divide(four, Add(eightk, One), precision);
                term = Subtract(term, Divide(two, Add(eightk, four), precision));
                term = Subtract(term, Divide(One, Add(eightk, five), precision));
                term = Subtract(term, Divide(One, Add(eightk, six), precision));
                term = Divide(term, pow16, precision);
                pi = Add(pi, term);
                pow16 = Multiply(pow16, sixteen);
                eightk = Add(eightk, eight);
                k++;
            } while (term.Sign != 0);
            return pi;
        }

        #region Trigonometric

        /// <summary>
        /// Returns the sine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Sin(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.TenPower < 2)
                return SinTaylor(value, precision);
            else
            {
                BigDecimal pi = Pi(precision + 9);
                BigDecimal twoPi = Multiply(pi, FromInt32(2));
                BigDecimal n = Divide(value, twoPi, 0);
                BigDecimal x = Subtract(value, Multiply(n, twoPi));
                return SinTaylor(x, precision);
            }
        }

        /// <summary>
        /// Returns the cosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Cos(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.TenPower < 2)
                return CosTaylor(value, precision);
            else
            {
                BigDecimal pi = Pi(precision + 9);
                BigDecimal twoPi = Multiply(pi, FromInt32(2));
                BigDecimal n = Divide(value, twoPi, 0);
                BigDecimal x = Subtract(value, Multiply(n, twoPi));
                return CosTaylor(x, precision);
            }
        }

        /// <summary>
        /// Returns the tangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException">The cosine of <paramref name="value"/> is 0.</exception>
        public static BigDecimal Tan(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            var sin = Sin(value, precision + 1);
            var cos = Cos(value, precision + 1);
            return Divide(sin, cos, precision);
        }

        /// <summary>
        /// Returns the cotangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The cotangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException">The sine of <paramref name="value"/> is 0.</exception>
        public static BigDecimal Cot(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            var sin = Sin(value, precision + 1);
            var cos = Cos(value, precision + 1);
            return Divide(cos, sin, precision);
        }

        private static BigDecimal SinTaylor(BigDecimal value, long precision)
        {
            BigDecimal y = value;
            BigDecimal pow = value;
            BigDecimal x2 = Multiply(value, value);
            BigDecimal term;
            BigDecimal factorial = One;
            int i = 1;
            do
            {
                pow = Multiply(pow, x2);
                factorial = Multiply(factorial, FromInt64((long)(2 * i) * (long)(2 * i + 1)));
                term = Divide(pow, factorial, precision);
                if ((i & 1) == 1)
                    y = Subtract(y, term);
                else
                    y = Add(y, term);
                i++;
            } while (term.Sign != 0);
            return y;
        }

        private static BigDecimal CosTaylor(BigDecimal value, long precision)
        {
            BigDecimal y = One;
            BigDecimal pow = One;
            BigDecimal x2 = Multiply(value, value);
            BigDecimal term;
            BigDecimal factorial = One;
            int i = 1;
            do
            {
                pow = Multiply(pow, x2);
                factorial = Multiply(factorial, FromInt64((long)(2 * i - 1) * (long)(2 * i)));
                term = Divide(pow, factorial, precision);
                if ((i & 1) == 1)
                    y = Subtract(y, term);
                else
                    y = Add(y, term);
                i++;
            } while (term.Sign != 0);
            return y;
        }

        #endregion

        #region Arc Trigonometric

        /// <summary>
        /// Returns the arctangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The arctangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Arctan(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return Negate(Arctan(Negate(value), precision));
            else if (value.CompareTo(One) <= 0)
                return ArctanTaylor(value, precision);
            else
            {
                BigDecimal pi = Pi(precision);
                BigDecimal halfPi = Divide(pi, FromInt32(2), precision);
                BigDecimal x = Divide(One, value, precision);
                BigDecimal arctan = ArctanTaylor(x, precision);
                if (value.Sign > 0)
                    return Subtract(halfPi, arctan);
                else
                    return Subtract(Negate(halfPi), arctan);
            }
        }

        /// <summary>
        /// Returns the arccotangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The arccotangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigDecimal Arccot(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            BigDecimal pi = Pi(precision);
            BigDecimal halfPi = Divide(pi, FromInt32(2), precision);            
            BigDecimal arctan = Arctan(value, precision);
            return Subtract(halfPi, arctan);
        }

        private static BigDecimal ArctanTaylor(BigDecimal value, long precision)
        {
            BigDecimal y = value;
            BigDecimal pow = value;
            BigDecimal x2 = Multiply(value, value);
            BigDecimal term, termPrev = Zero;
            int i = 1;
            do
            {
                pow = Multiply(pow, x2);
                term = Divide(pow, FromInt32(2 * i + 1), precision);
                if (term.Equals(termPrev))
                {
                    term = Divide(term, FromInt32(2), precision);
                    if ((i & 1) == 1)
                        y = Subtract(y, term);
                    else
                        y = Add(y, term);
                    break;
                }
                else
                    if ((i & 1) == 1)
                        y = Subtract(y, term);
                    else
                        y = Add(y, term);
                termPrev = term;
                i++;
            } while (term.Sign != 0);
            return y;
        }

        /// <summary>
        /// Returns the arcsine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The arcsine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="value"/> is greater than 1, or less than -1.</exception>
        public static BigDecimal Arcsin(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return Negate(Arcsin(Negate(value), precision));
            else if (value.CompareTo(One) < 0)
            {
                BigDecimal x = Multiply(value, value);
                x = Subtract(One, x);
                x = Root(x, 2, precision);
                x = Divide(value, x, precision);
                BigDecimal arctan = Arctan(x, precision);
                return arctan;
            }
            else if (value.CompareTo(One) == 0)
                return Divide(Pi(precision), FromInt32(2), precision);
            else
                throw new ArithmeticException(ExceptionResource.Undefined);
        }

        /// <summary>
        /// Returns the arccosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <param name="precision">The number of decimal places in the return value.</param>
        /// <returns>The arccosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="value"/> is greater than 1, or less than -1.</exception>
        public static BigDecimal Arccos(BigDecimal value, long precision)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.CompareTo(MinusOne) == 0)
                return Pi(precision);
            if (value.Sign < 0)
            {
                BigDecimal x = Multiply(value, value);
                x = Subtract(One, x);
                x = Root(x, 2, precision);
                x = Divide(x, Negate(value), precision);
                BigDecimal arctan = Arctan(x, precision);
                return Subtract(Pi(precision), arctan);
            }
            else if (value.Sign == 0)
                return Divide(Pi(precision), FromInt32(2), precision);
            else if (value.CompareTo(One) < 0)
            {
                BigDecimal x = Multiply(value, value);
                x = Subtract(One, x);
                x = Root(x, 2, precision);
                x = Divide(x, value, precision);
                BigDecimal arctan = Arctan(x, precision);
                return arctan;
            }
            else if (value.CompareTo(One) == 0)
                return Zero;
            else
                throw new ArithmeticException(ExceptionResource.Undefined);
        }

        #endregion
    }
}
