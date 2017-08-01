/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Real
 * Description : Represents a real number.
 * Created     : 2015/3/29
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public abstract partial class Real
    {
        /// <summary>
        /// Adds the values of two specified <see cref="Real"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator +(Real left, Real right)
        {
            return Add(left, right);
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="Real"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Add(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Rational && right is Rational)
            {
                try
                {
                    return (Rational)left + (Rational)right;
                }
                catch (OverflowException)
                {
                    return Normalize(left.ToFloatingPoint() + right.ToFloatingPoint());
                }
            }
            else if (
                (left is Rational && right is FloatingPoint) ||
                (left is FloatingPoint && right is Rational) ||
                (left is FloatingPoint && right is FloatingPoint)
                )
                return Normalize(left.ToFloatingPoint() + right.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Subtracts a <see cref="Real"/> object from another <see cref="Real"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator -(Real left, Real right)
        {
            return Subtract(left, right);
        }
        
        /// <summary>
        /// Subtracts a <see cref="Real"/> object from another <see cref="Real"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Subtract(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Rational && right is Rational)
                try
                {
                    return (Rational)left - (Rational)right;
                }
                catch (OverflowException)
                {
                    return Normalize(left.ToFloatingPoint() - right.ToFloatingPoint());
                }
            else if (
                (left is Rational && right is FloatingPoint) ||
                (left is FloatingPoint && right is Rational) ||
                (left is FloatingPoint && right is FloatingPoint)
                )
                return Normalize(left.ToFloatingPoint() - right.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Real"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator *(Real left, Real right)
        {
            return Multiply(left, right);
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="Real"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Multiply(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Rational && right is Rational)
                try
                {
                    return (Rational)left * (Rational)right;
                }
                catch (OverflowException)
                {
                    return Normalize(left.ToFloatingPoint() * right.ToFloatingPoint());
                }
            else if (
                (left is Rational && right is FloatingPoint) ||
                (left is FloatingPoint && right is Rational) ||
                (left is FloatingPoint && right is FloatingPoint)
                )
                return Normalize(left.ToFloatingPoint() * right.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Divides a specified <see cref="Real"/> object by another specified <see cref="Real"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator /(Real left, Real right)
        {
            return Divide(left, right);
        }
        
        /// <summary>
        /// Divides a specified <see cref="Real"/> object by another specified <see cref="Real"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Divide(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Rational && right is Rational)
                try
                {
                    return (Rational)left / (Rational)right;
                }
                catch (OverflowException)
                {
                    return Normalize(left.ToFloatingPoint() / right.ToFloatingPoint());
                }
                catch (DivideByZeroException)
                {
                    return Normalize(left.ToFloatingPoint() / right.ToFloatingPoint());
                }
            else if (
                (left is Rational && right is FloatingPoint) ||
                (left is FloatingPoint && right is Rational) ||
                (left is FloatingPoint && right is FloatingPoint)
                )
                return Normalize(left.ToFloatingPoint() / right.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns the value of the <see cref="Real"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="Real"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator +(Real value)
        {
            return Normalize(value);
        }

        /// <summary>
        /// Negates a specified <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real operator -(Real value)
        {
            return Negate(value);
        }
        
        /// <summary>
        /// Negates a specified <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Negate(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value is Rational)
                try
                {
                    return -(Rational)value;
                }
                catch (OverflowException)
                {
                    return Normalize(-value.ToFloatingPoint());
                }
            else if (value is FloatingPoint)
                return Normalize(-value.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Abs(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value.Sign < 0)
                return -value;
            else
                return +value;
        }

        /// <summary>
        /// Returns a specified number raised to the specified integral power.
        /// </summary>
        /// <param name="left">The number to be raised to a power.</param>
        /// <param name="right">The number that specifies a power.</param>
        /// <returns>The number <paramref name="left"/> raised to the power <paramref name="right"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Pow(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left is Rational && right is Integer)
                try
                {
                    return Rational.Pow((Rational)left, (Integer)right);
                }
                catch (ArithmeticException)
                {
                    return Normalize(FloatingPoint.Pow(left.ToFloatingPoint(), right.ToFloatingPoint()));
                }                
            else if (
                left is Rational && right is Rational ||
                left is Rational && right is FloatingPoint ||
                left is FloatingPoint && right is Rational ||
                left is FloatingPoint && right is FloatingPoint)
                return Normalize(FloatingPoint.Pow(left.ToFloatingPoint(), right.ToFloatingPoint()));
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        /// <summary>
        /// Returns the specified root of the number.
        /// </summary>
        /// <param name="value">The number whose root is to be found</param>
        /// <param name="index">The order of the root.</param>
        /// <returns>The <paramref name="index"/>th root of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Root(Real value, int index)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (index == 0)
                return FloatingPoint.NaN;
            if (index < 0)
                return 1 / Root(value, -index);
            if ((index & 0x1) == 0)
                return Pow(value, 1.0 / index);
            else
            {
                if (value.Sign > 0)
                    return Pow(value, 1.0 / index);
                else if (value.Sign == 0)
                    return 0;
                else
                    return -Pow(-value, 1.0 / index);
            }
        }
    }
}
