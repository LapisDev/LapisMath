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
        /// Adds the values of two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Add(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            long r = (long)left.value + right.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer operator +(Integer left, Integer right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Subtracts a <see cref="Integer"/> object from another <see cref="Integer"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Subtract(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            long r = (long)left.value - right.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Subtracts a <see cref="Integer"/> object from another <see cref="Integer"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer operator -(Integer left, Integer right)
        {
            return Subtract(left, right);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Multiply(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            long r = (long)left.value * right.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer operator *(Integer left, Integer right)
        {
            return Multiply(left, right);
        }

        /// <summary>
        /// Divides a specified <see cref="Integer"/> object by another specified <see cref="Integer"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static Integer Divide(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.value == 0)
                throw new DivideByZeroException();
            long r = (long)left.value / right.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Divides a specified <see cref="Integer"/> object by another specified <see cref="Integer"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static Integer operator /(Integer left, Integer right)
        {
            return Divide(left, right);
        }

        /// <summary>
        /// Returns the remainder that results from division with two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static Integer Remainder(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right.value == 0)
                throw new DivideByZeroException();
            long r = (long)left.value % right.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Returns the remainder that results from division with two specified <see cref="Integer"/> objects.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The remainder that results from the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="right"/> is 0.</exception>
        public static Integer operator %(Integer left, Integer right)
        {
            return Remainder(left, right);
        }

        /// <summary>
        /// Negates a specified <see cref="Integer"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Negate(Integer value)
        {
            if (value == null)
                throw new ArgumentNullException();
            long r = -(long)value.value;
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }
        
        /// <summary>
        /// Negates a specified <see cref="Integer"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer operator -(Integer value)
        {
            return Negate(value);
        }

        /// <summary>
        /// Returns the value of the <see cref="Integer"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="Integer"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Integer operator +(Integer value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer Abs(Integer value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value < 0)
                return Negate(value);
            else
                return value;
        }

        /// <summary>
        /// Returns a specified number raised to the specified positive integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="base"/> or <paramref name="exponent"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exponent"/> is negative.</exception>
        /// <exception cref="ArithmeticException"><paramref name="base"/> and <paramref name="exponent"/> are zero.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="MaxValue"/> or less than <see cref="MinValue"/>.</exception>
        public static Integer PositivePow(Integer @base, Integer exponent)
        {
            if (@base == null || exponent == null)
                throw new ArgumentNullException();
            if (exponent < 0)
                throw new ArgumentOutOfRangeException();
            if (@base == 0 && exponent == 0)
                throw new ArithmeticException(ExceptionResource.Undefined);
            double r = System.Math.Pow(@base.value, exponent.value);
            if (r <= int.MaxValue && r >= int.MinValue)
                return new Integer((int)r);
            else
                throw new OverflowException();
        }

        /// <summary>
        /// Returns a specified number raised to the specified positive integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="exponent"/> is negative.</exception>
        /// <exception cref="ArithmeticException"><paramref name="base"/> and <paramref name="exponent"/> are zero.</exception>
        /// <exception cref="OverflowException">The value is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static int PositivePow(int @base, int exponent)
        {          
            if (exponent < 0)
                throw new ArgumentOutOfRangeException();
            if (@base == 0 && exponent == 0)
                throw new ArithmeticException(ExceptionResource.Undefined);
            double r = System.Math.Pow(@base, exponent);
            if (r <= int.MaxValue && r >= int.MinValue)
                return (int)r;
            else
                throw new OverflowException();
        }

    }
}
