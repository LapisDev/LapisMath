/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Rational
 * Description : Represents a rational number.
 * Created     : 2015/3/29
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public abstract partial class Rational
    {
        /// <summary>
        /// Adds the values of two specified <see cref="Rational"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational operator +(Rational left, Rational right)
        {
            return Add(left, right);
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="Rational"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational Add(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Integer && right is Integer)
                return (Integer)left + (Integer)right;
            else if (
                (left is Fraction && right is Integer) ||
                (left is Integer && right is Fraction) ||
                (left is Fraction && right is Fraction)
                )
                return Normalize((left.ToFraction() + right.ToFraction()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Subtracts a <see cref="Rational"/> object from another <see cref="Rational"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational operator -(Rational left, Rational right)
        {
            return Subtract(left, right);
        }
        
        /// <summary>
        /// Subtracts a <see cref="Rational"/> object from another <see cref="Rational"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational Subtract(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Integer && right is Integer)
                return (Integer)left - (Integer)right;
            else if (
                (left is Fraction && right is Integer) ||
                (left is Integer && right is Fraction) ||
                (left is Fraction && right is Fraction)
                )
                return Normalize((left.ToFraction() - right.ToFraction()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Rational"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational operator *(Rational left, Rational right)
        {
            return Multiply(left, right);
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="Rational"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational Multiply(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Integer && right is Integer)
                return (Integer)left * (Integer)right;
            else if (
                (left is Fraction && right is Integer) ||
                (left is Integer && right is Fraction) ||
                (left is Fraction && right is Fraction)
                )
                return Normalize((left.ToFraction() * right.ToFraction()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Divides a specified <see cref="Rational"/> object by another specified <see cref="Rational"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Rational operator /(Rational left, Rational right)
        {
            return Divide(left, right);
        }
        
        /// <summary>
        /// Divides a specified <see cref="Rational"/> object by another specified <see cref="Rational"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Rational Divide(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (
                (left is Integer && right is Integer) ||
                (left is Fraction && right is Integer) ||
                (left is Integer && right is Fraction) ||
                (left is Fraction && right is Fraction)
                )
                return Normalize((left.ToFraction() / right.ToFraction()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns the value of the <see cref="Rational"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="Rational"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Rational operator +(Rational value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Normalize(value);
        }

        /// <summary>
        /// Negates a specified <see cref="Rational"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational operator -(Rational value)
        {
            return Negate(value);
        }
        
        /// <summary>
        /// Negates a specified <see cref="Rational"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational Negate(Rational value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value is Integer)
                return -(Integer)value;
            else if (value is Fraction)
                return Normalize((-(Fraction)value));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Rational Abs(Rational value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return -value;
            else
                return +value;
        }

        /// <summary>
        /// Returns the multiplicative inverse of a fraction.
        /// </summary>
        /// <param name="value">The fraction.</param>       
        /// <returns>The multiplicative inverse of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Rational Reciprocal(Rational value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value is Integer)
                return Normalize(Fraction.Create(1, value.Numerator));
            else if (value is Fraction)
                return Normalize(Fraction.Reciprocal((Fraction)value));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns a specified number raised to the specified integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="base"/> or <paramref name="exponent"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="base"/> and <paramref name="exponent"/> are 0.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Rational Pow(Rational @base, Integer exponent)
        {
            if (@base == null || exponent == null)
                throw new ArgumentNullException();
            if (@base is Integer && exponent >= 0)
                return Normalize(Integer.PositivePow((Integer)@base, exponent));
            else if (
                @base is Integer && exponent < 0 ||
                @base is Fraction)
                return Normalize(Fraction.Pow(@base.ToFraction(), exponent.ToInt32()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }
    }
}
