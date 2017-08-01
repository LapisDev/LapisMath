/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Complex
 * Description : Represents a complex number.
 * Created     : 2015/8/19
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class Complex
    {
        /// <summary>
        /// Adds the values of two specified <see cref="Complex"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator +(Complex left, Complex right)
        {
            return Add(left, right);
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="Complex"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Add(Complex left, Complex right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            var real = left.Real + right.Real;
            var imaginary = left.Imaginary + right.Imaginary;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Subtracts a <see cref="Complex"/> object from another <see cref="Complex"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator -(Complex left, Complex right)
        {
            return Subtract(left, right);
        }
        
        /// <summary>
        /// Subtracts a <see cref="Complex"/> object from another <see cref="Complex"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Subtract(Complex left, Complex right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            var real = left.Real - right.Real;
            var imaginary = left.Imaginary - right.Imaginary;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Complex"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator *(Complex left, Complex right)
        {
            return Multiply(left, right);
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="Complex"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Multiply(Complex left, Complex right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            var real = left.Real * right.Real - left.Imaginary * right.Imaginary;
            var imaginary = left.Real * right.Imaginary + left.Imaginary * right.Real;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Divides a specified <see cref="Complex"/> object by another specified <see cref="Complex"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator /(Complex left, Complex right)
        {
            return Divide(left, right);
        }
        
        /// <summary>
        /// Divides a specified <see cref="Complex"/> object by another specified <see cref="Complex"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Divide(Complex left, Complex right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            var d = right.Real * right.Real + right.Imaginary * right.Imaginary;
            var real = (left.Real * right.Real + left.Imaginary * right.Imaginary) / d;
            var imaginary = (left.Imaginary * right.Real - left.Real * right.Imaginary) / d;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Returns the multiplicative inverse of a complex number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The multiplicative inverse of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Reciprocal(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return One / value;
        }

        /// <summary>
        /// Returns the value of the <see cref="Complex"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="Complex"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator +(Complex value)
        {
            return value;
        }

        /// <summary>
        /// Negates a specified <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex operator -(Complex value)
        {
            return Negate(value);
        }
        
        /// <summary>
        /// Negates a specified <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Negate(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Complex(-value.Real, -value.Imaginary);
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Abs(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value.Magnitude;
        }

        /// <summary>
        /// Returns a specified number raised to the specified integral power.
        /// </summary>
        /// <param name="base">The number to be raised to a power.</param>
        /// <param name="exponent">The number that specifies a power.</param>
        /// <returns>The number <paramref name="base"/> raised to the power <paramref name="exponent"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="base"/> is <see langword="null"/>.</exception>
        public static Complex Pow(Complex @base, int exponent)
        {
            if (@base == null)
                throw new ArgumentNullException();
            var magnitude = Real.Pow(@base.Magnitude, exponent);
            var phase = @base.Phase * exponent;
            return FromPolarCoordinates(magnitude, phase);
        }
    }
}
