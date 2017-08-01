/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Fraction
 * Description : Represents a fraction number.
 * Created     : 2015/3/28
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class Fraction
    {
        /// <summary>
        /// Adds the values of two specified <see cref="Fraction"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction operator +(Fraction left, Fraction right)
        {
            return Add(left, right);
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="Fraction"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction Add(Fraction left, Fraction right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Normalize(
                left._numerator * right._denominator + left._denominator * right._numerator,
                left._denominator * right._denominator
                );
        }

        /// <summary>
        /// Subtracts a <see cref="Fraction"/> object from another <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction operator -(Fraction left, Fraction right)
        {
            return Subtract(left, right);
        }
        
        /// <summary>
        /// Subtracts a <see cref="Fraction"/> object from another <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction Subtract(Fraction left, Fraction right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Normalize(
                left._numerator * right._denominator - left._denominator * right._numerator,
                left._denominator * right._denominator
                );
        }

        /// <summary>
        /// Multiplies two specified <see cref="Fraction"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction operator *(Fraction left, Fraction right)
        {
            return Multiply(left, right);
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="Fraction"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction Multiply(Fraction left, Fraction right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Normalize(
                left._numerator * right._numerator,
                left._denominator * right._denominator
                );
        }

        /// <summary>
        /// Divides a specified <see cref="Fraction"/> object by another specified <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Fraction operator /(Fraction left, Fraction right)
        {
            return Divide(left, right);
        }
        
        /// <summary>
        /// Divides a specified <see cref="Fraction"/> object by another specified <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Fraction Divide(Fraction left, Fraction right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Normalize(
                left._numerator * right._denominator,
                left._denominator * right._numerator
                );
        }

        /// <summary>
        /// Returns the value of the <see cref="Fraction"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="Fraction"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Fraction operator +(Fraction value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        /// <summary>
        /// Negates a specified <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction operator -(Fraction value)
        {
            return Negate(value);
        }
        
        /// <summary>
        /// Negates a specified <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction Negate(Fraction value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Fraction(-value._numerator, value._denominator);
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        public static Fraction Abs(Fraction value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return -value;
            else
                return value;
        }

        /// <summary>
        /// Returns the multiplicative inverse of a fraction.
        /// </summary>
        /// <param name="value">The fraction.</param>       
        /// <returns>The multiplicative inverse of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Fraction Reciprocal(Fraction value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Normalize(value._denominator, value._numerator);
        }

        /// <summary>
        /// Returns a specified number raised to the specified integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="base"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">The <see cref="Numerator"/> or <see cref="Denominator"/> is greater than <see cref="Int32.MaxValue"/> or less than <see cref="Int32.MinValue"/>.</exception>
        /// <exception cref="ArithmeticException"><paramref name="base"/> and <paramref name="exponent"/> are 0.</exception>
        /// <exception cref="DivideByZeroException">The <see cref="Denominator"/> is 0.</exception>
        public static Fraction Pow(Fraction @base, int exponent)
        {
            if (@base == null)
                throw new ArgumentNullException();       
            if (@base == 0 && exponent == 0)
                throw new ArithmeticException(ExceptionResource.Undefined);
            if (exponent >= 0)
                return Create(Integer.PositivePow(@base._numerator, exponent), Integer.PositivePow(@base._denominator, exponent));
            else
                return Create(Integer.PositivePow(@base._denominator, -exponent), Integer.PositivePow(@base._numerator, -exponent));
        }
    }
}
