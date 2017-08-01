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
        #region Plus

        /// <summary>
        /// Adds the values of two specified <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Add(BigInteger left, BigInteger right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Sign == 0)
                return right;
            else if (right.Sign == 0)
                return left;
            else if (left.Sign == right.Sign)
            {
                var r = PosAdd(left, right);
                r.Sign *= left.Sign;
                return r;
            }
            else
            {
                var r = PosSubtract(left, right);
                r.Sign *= left.Sign;
                return r;
            }
        }

        /// <summary>
        /// Adds the values of two specified <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger operator +(BigInteger left, BigInteger right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Subtracts a <see cref="BigInteger"/> object from another <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Subtract(BigInteger left, BigInteger right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.Sign == 0)
                return left;
            else if (left.Sign == right.Sign)
            {
                var r = PosSubtract(left, right);
                r.Sign *= left.Sign;
                return r;
            }
            else
            {
                var r = PosAdd(left, right);
                r.Sign *= -right.Sign;
                return r;
            }
        }

        /// <summary>
        /// Subtracts a <see cref="BigInteger"/> object from another <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger operator -(BigInteger left, BigInteger right)
        {
            return Subtract(left, right);
        }

        #region Private

        private static BigInteger PosAdd(BigInteger x, BigInteger y)
        {
            if (x.Length < y.Length)
            {
                BigInteger temp = x;
                x = y;
                y = temp;
            }
            int[] data = new int[x.Length + 1];
            int sum = 0, carry = 0;
            for (long i = 0L; i < y.Length; i++)
            {
                sum = x._data[i] + y._data[i] + carry;
                if (sum >= BaseInt)
                {
                    carry = 1;
                    data[i] = sum - BaseInt;
                }
                else
                {
                    carry = 0;
                    data[i] = sum;
                }
            }
            for (long i = y.Length; i < x.Length; i++)
            {
                sum = x._data[i] + carry;
                if (sum >= BaseInt)
                {
                    carry = 1;
                    data[i] = sum - BaseInt;
                }
                else
                {
                    carry = 0;
                    data[i] = sum;
                }
            }
            if (carry > 0)
                data[x.Length] = carry;
            return new BigInteger(1, data);
        }

        private static BigInteger PosSubtract(BigInteger x, BigInteger y)
        {
            int sign = 1;
            if (x.Length < y.Length)
            {
                BigInteger temp = x;
                x = y;
                y = temp;
                sign = -sign;
            }
            int[] data = new int[x.Length];
            int diff = 0, borrow = 0;
            for (long i = 0L; i < y.Length; i++)
            {
                diff = x._data[i] - y._data[i] - borrow;
                if (diff < 0)
                {
                    borrow = 1;
                    data[i] = diff + BaseInt;
                }
                else
                {
                    borrow = 0;
                    data[i] = diff;
                }
            }
            for (long i = y.Length; i < x.Length; i++)
            {
                diff = x._data[i] - borrow;
                if (diff < 0)
                {
                    borrow = 1;
                    data[i] = diff + BaseInt;
                }
                else
                {
                    borrow = 0;
                    data[i] = diff;
                }
            }
            if (borrow != 0)
            {
                for (long i = 0L; i < x.Length; i++)
                    data[i] = BaseInt - 1 - data[i];
                data[0]++;
                sign = -sign;
            }
            return new BigInteger(sign, data);
        }

        #endregion

        #endregion

        #region Multiply

        /// <summary>
        /// Multiplies two specified <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Multiply(BigInteger left, BigInteger right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Sign == 0)
                return left;
            else if (right.Sign == 0)
                return right;
            else
            {
                var r = PosMultiply(left, right);
                r.Sign = left.Sign * right.Sign;
                return r;
            }
        }

        /// <summary>
        /// Multiplies two specified <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger operator *(BigInteger left, BigInteger right)
        {
            return Multiply(left, right);
        }

        #region Private

        private static BigInteger PosMultiply(BigInteger x, BigInteger y)
        {
            int[] data = new int[x.Length + y.Length];
            long prod = 0L, carry = 0L;
            for (long i = 0L; i < x.Length; i++)
            {
                if (x._data[i] == 0)
                    continue;
                carry = 0L;
                for (long j = 0L; j < y.Length; j++)
                {
                    prod = (long)x._data[i] * (long)y._data[j] + data[i + j] + carry;
                    carry = prod / BaseInt;
                    data[i + j] = (int)(prod % BaseInt);
                }
                if (carry > 0)
                    data[i + y.Length] = (int)carry;
            }
            return new BigInteger(1, data);
        }

        #endregion

        #endregion

        #region Divide

        /// <summary>
        /// Divides a specified <see cref="BigInteger"/> object by another specified <see cref="BigInteger"/> object, returns the result, and returns the remainder in an output parameter.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <param name="reminder">When this method returns, contains a <see cref="BigInteger"/> object that represents the remainder from the division. This parameter is passed uninitialized..</param>
        /// <returns>The quotient of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static BigInteger DivideRemainder(BigInteger left, BigInteger right, out BigInteger reminder)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.Sign == 0)
            {
                throw new DivideByZeroException();
            }
            else if (left.Sign == 0)
            {
                reminder = left;
                return left;
            }
            else
            {
                BigInteger r, q;
                q = PosDivide(left, right, out r);
                r.Sign *= left.Sign;
                q.Sign = left.Sign * right.Sign;
                reminder = r;
                return q;
            }
        }

        /// <summary>
        /// Divides a specified <see cref="BigInteger"/> object by another specified <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static BigInteger operator /(BigInteger left, BigInteger right)
        {
            BigInteger reminder;
            return DivideRemainder(left, right, out reminder);
        }

        /// <summary>
        /// Returns the remainder that results from division with two specified <see cref="BigInteger"/> objects.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static BigInteger operator %(BigInteger left, BigInteger right)
        {
            BigInteger reminder;
            DivideRemainder(left, right, out reminder);
            return reminder;
        }

        #region Private

        // Assumes x.Length <= y.Length + 1
        private static int OneDigitDivide(BigInteger x, BigInteger y, out BigInteger remainder)
        {
            //if (x.Length - y.Length > 1)
            //    throw new Exception();
            if (x.Length < y.Length)
            {
                remainder = x;
                return 0;
            }
            if (x.Length == y.Length)
            {
                if (x.Length == 1)
                {
                    remainder = FromInt32(x._data[0] % y._data[0]);
                    return x._data[0] / y._data[0];
                }
                if (x.Length == 2)
                {
                    long x1 = ((long)x._data[1] * BaseInt) + (long)x._data[0];
                    long y1 = ((long)y._data[1] * BaseInt) + (long)y._data[0];
                    remainder = FromInt64(x1 % y1);
                    return (int)(x1 / y1);
                }
            }
            else
            {   // x.Length > y.Length
                if (x.Length == 2)
                {
                    long x1 = ((long)x._data[1] * BaseInt) + (long)x._data[0];
                    long y1 = (long)y._data[0];
                    remainder = FromInt64(x1 % y1);
                    return (int)(x1 / y1);
                }
            }
            long a2, a1, a0;
            BigInteger xx, yy;
            if (y._data[y.Length - 1] < BaseInt >> 1)
            {
                var z = FromInt32(BaseInt / (y._data[y.Length - 1] + 1));
                xx = PosMultiply(x, z);
                yy = PosMultiply(y, z);
            }
            else
            {
                xx = x;
                yy = y;
            }
            if (xx.Length == yy.Length)
            {
                a2 = 0;
                a1 = xx._data[x.Length - 1];
                a0 = xx._data[x.Length - 2];
            }
            else
            {   // xx.Length > yy.Length
                a2 = xx._data[xx.Length - 1];
                a1 = xx._data[xx.Length - 2];
                a0 = xx._data[xx.Length - 3];
            }
            // xx.Length == yy.Length + 1 && yy.Length >= 2
            long b1 = yy._data[yy.Length - 1],
                b0 = yy._data[yy.Length - 2];
            long a = (a2 * BaseInt) + a1;
            long q = a / b1;
            if (q > BaseInt - 1)
                q = BaseInt - 1;
            long t = a - q * b1;
            if (t * BaseInt < q * b0 - a0)
                q--;
            remainder = Subtract(x, Multiply(y, FromInt64(q)));
            if (remainder.Sign < 0)
            {
                q--;
                remainder = Subtract(x, Multiply(y, FromInt64(q)));
            }
            return (int)q;
        }

        private static BigInteger PosDivide(BigInteger x, BigInteger y, out BigInteger remainder)
        {
            long k = x.Length - y.Length + 1;
            if (k <= 1 || x.Length == 1)
                return FromInt32(OneDigitDivide(x, y, out remainder));
            int[] q = new int[k];
            BigInteger r;
            OneDigitDivide(new BigInteger(1, DigitShiftRight(x, k)), y, out r);
            int[] t;
            for (long i = k - 1; i >= 0; i--)
            {
                t = DigitShiftLeft(r, 1);
                t[0] = x._data[i];
                q[i] = OneDigitDivide(new BigInteger(1, t), y, out r);
            }
            remainder = r;
            return new BigInteger(1, q);
        }

        #endregion

        #endregion

        #region DigitShift

        /// <summary>
        /// Returns the product of the <see cref="BigInteger"/> object multiplied by the specified power of 10.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> object.</param>
        /// <param name="num">The exponent of the power.</param>
        /// <returns>The product of <paramref name="value"/> multiplied by 10 raised to <paramref name="num"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger MultiplyPowerOfTen(BigInteger value, long num)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (num == 0)
                return value;
            else if (num < 0)
                return DividePowerOfTen(value, -num);
            long digit = num / 9;
            int rem = (int)(num % 9);
            int[] data = DigitShiftLeft(value, digit);
            var r = new BigInteger(value.Sign, data);
            if (rem > 0)
            {
                int m = 1;
                for (int i = 0; i < rem; i++)
                    m *= 10;
                r = PosMultiply(r, FromInt32(m));
            }
            return r;
        }

        /// <summary>
        /// Returns the result of the <see cref="BigInteger"/> object divided by the specified power of 10.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> object.</param>
        /// <param name="num">The exponent of the power.</param>
        /// <returns>The result of <paramref name="value"/> divided by 10 raised to <paramref name="num"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger DividePowerOfTen(BigInteger value, long num)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (num == 0)
                return value;
            else if (num < 0)
                return MultiplyPowerOfTen(value, -num);
            long digit = num / 9;
            int rem = (int)(num % 9);
            int[] data = DigitShiftRight(value, digit);
            var r = new BigInteger(value.Sign, data);
            if (rem > 0)
            {
                int m = 1;
                for (int i = 0; i < rem; i++)
                    m *= 10;
                BigInteger rd;
                r = PosDivide(r, FromInt32(m), out rd);
            }
            return r;
        }

        #region Internal

        internal static BigInteger Multiply9thPowerOfTen(BigInteger value, long num)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (num == 0)
                return value;
            else if (num < 0)
                return Divide9thPowerOfTen(value, -num);
            long digit = num;
            int[] data = DigitShiftLeft(value, digit);
            var r = new BigInteger(value.Sign, data);
            return r;
        }

        internal static BigInteger Divide9thPowerOfTen(BigInteger value, long num)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (num == 0)
                return value;
            else if (num < 0)
                return Multiply9thPowerOfTen(value, -num);
            long digit = num;
            int[] data = DigitShiftRight(value, digit);
            var r = new BigInteger(value.Sign, data);
            return r;
        }

        #endregion

        #region Private

        private static int[] DigitShiftRight(BigInteger value, long num)
        {
            long count = value.Length - num;
            int[] data = count > 0 ? new int[count] : new int[1];
            for (long i = 0, j = num; i < count; i++, j++)
                data[i] = value._data[j];
            return data;
        }

        private static int[] DigitShiftLeft(BigInteger value, long num)
        {
            long count = value.Length + num;
            int[] data = count > 0 ? new int[count] : new int[1];
            for (long i = 0, j = num; j < count; i++, j++)
                data[j] = value._data[i];
            return data;
        }

        #endregion

        #endregion


        #region Unary

        /// <summary>
        /// Negates a specified <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger Negate(BigInteger value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new BigInteger(-value.Sign, (int[])value._data.Clone());
        }

        /// <summary>
        /// Negates a specified <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger operator -(BigInteger value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Returns the value of the <see cref="BigInteger"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="BigInteger"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigInteger operator +(BigInteger value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        #endregion


        #region Pow

        /// <summary>
        /// Returns a specified number raised to the specified positive integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="base"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exponent"/> is negative.</exception>
        /// <exception cref="ArithmeticException"><paramref name="base"/> and <paramref name="exponent"/> are zero.</exception>
        public static BigInteger PositivePow(BigInteger @base, long exponent)
        {
            if (@base == null)
                throw new ArgumentNullException();
            if (exponent < 0)
                throw new ArgumentOutOfRangeException();
            if (exponent == 0 && @base.IsZero)
                throw new ArithmeticException(ExceptionResource.Undefined);
            BigInteger r = FromInt32(1);
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    r = PosMultiply(r, @base);
                @base = PosMultiply(@base, @base);
                exponent >>= 1;
            }
            return r;
        }

        #region Private


        #endregion

        #endregion

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static BigInteger Abs(BigInteger value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return Negate(value);
            else
                return value;
        }
    }
}
