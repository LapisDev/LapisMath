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
        /// Returns the arccosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arccosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Acos(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return -ImaginaryOne * Ln(value + ImaginaryOne * Sqrt(One - value * value));
        }

        /// <summary>
        /// Returns the arcsine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arcsine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Asin(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return -ImaginaryOne * Ln(ImaginaryOne * value + Sqrt(One - value * value));
        }

        /// <summary>
        /// Returns the arctangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arctangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Atan(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return ImaginaryOne / new Complex(2, 0) * (Ln(One - ImaginaryOne * value) - Ln(One + ImaginaryOne * value));
        }

        /// <summary>
        /// Computes the conjugate of a complex number and returns the result.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>The conjugate of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Conjugate(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Complex(value.Real, -value.Imaginary);
        }

        /// <summary>
        /// Returns the cosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Cos(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            var a = value.Real;
            var b = value.Imaginary;
            return new Complex(Real.Cos(a) * Real.Cosh(b), -Real.Sin(a) * Real.Sinh(b));
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Cosh(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            var a = value.Real;
            var b = value.Imaginary;
            return new Complex(Real.Cosh(a) * Real.Cos(b), Real.Sinh(a) * Real.Sin(b));
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="value">A number specifying a power.</param>
        /// <returns>The number e raised to the power <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Exp(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Pow(System.Math.E, value);
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="value">The number whose logarithm is to be found.</param>
        /// <returns>The natural logarithm of <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Ln(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Real.Ln(value.Magnitude) + value.Phase * ImaginaryOne;
        }

        /// <summary>
        /// Returns the logarithm of a specified complex number in a specified base.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <param name="base">The base of the logarithm.</param>
        /// <returns>The logarithm of <paramref name="value"/> in base <paramref name="base"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="base"/> is <see langword="null"/>.</exception>
        public static Complex Log(Complex value, Complex @base)
        {
            if (value == null || @base == null)
                throw new ArgumentNullException();
            return Ln(value) / Ln(@base);
        }

        /// <summary>
        /// Returns the base-10 logarithm of a specified complex number.
        /// </summary>
        /// <param name="value">The number whose logarithm is to be found.</param>
        /// <returns>The base-10 logarithm of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Lg(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Log(value, 10);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="left">The number to be raised to a power.</param>
        /// <param name="right">The number that specifies a power.</param>
        /// <returns>The number <paramref name="left"/> raised to the power <paramref name="right"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex Pow(Complex left, Complex right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            var a = Real.Ln(left.Magnitude);
            var theta = left.Phase;
            var m = right.Real;
            var n = right.Imaginary;
            var t = a * n + theta * m;
            var e = Real.Exp(a * m - theta * n);
            var real = Real.Cos(t) * e;
            var imaginary = Real.Sin(t) * e;
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Returns the sine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Sin(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            var a = value.Real;
            var b = value.Imaginary;
            return new Complex(Real.Sin(a) * Real.Cosh(b), Real.Cos(a) * Real.Sinh(b));
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Sinh(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            var a = value.Real;
            var b = value.Imaginary;
            return new Complex(Real.Sinh(a) * Real.Cos(b), Real.Cosh(a) * Real.Sin(b));
        }

        /// <summary>
        /// Returns the square root of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The square root of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Sqrt(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return FromPolarCoordinates(Real.Sqrt(value.Magnitude), value.Phase / 2);
        }

        /// <summary>
        /// Returns the tangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Tan(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Sin(value) / Cos(value);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Complex Tanh(Complex value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Sinh(value) / Cosh(value);
        }
    }
}
