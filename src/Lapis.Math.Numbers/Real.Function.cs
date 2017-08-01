/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Real
 * Description : Represents a real number.
 * Created     : 2015/8/19
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
        /// Returns the arccosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arccosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Acos(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Acos(value.ToDouble());
        }

        /// <summary>
        /// Returns the arcsine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arcsine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Asin(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Asin(value.ToDouble());
        }

        /// <summary>
        /// Returns the arctangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The arctangent of <paramref name="value"/>.</returns> 
        public static Real Atan(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Atan(value.ToDouble());
        }

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">A <see cref="Real"/> object.</param>
        /// <returns>The smallest integral value that is greater than or equal to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Ceiling(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Ceiling(value.ToDouble());
        }

        /// <summary>
        /// Returns the cosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Cos(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Cos(value.ToDouble());
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Cosh(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Cosh(value.ToDouble());
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="value">A number specifying a power.</param>
        /// <returns>The number e raised to the power <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Exp(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Exp(value.ToDouble());
        }

        /// <summary>
        /// Returns the largest integral value that is less than or equal to the <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">A <see cref="Real"/> object.</param>
        /// <returns>The largest integral value that is less than or equal to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Floor(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Floor(value.ToDouble());
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="value">The number whose logarithm is to be found.</param>
        /// <returns>The natural logarithm of <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Ln(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Log(value.ToDouble());
        }

        /// <summary>
        /// Returns the logarithm of a specified real number in a specified base.
        /// </summary>
        /// <param name="value">A real number.</param>
        /// <param name="base">The base of the logarithm.</param>
        /// <returns>The logarithm of <paramref name="value"/> in base <paramref name="base"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="base"/> is <see langword="null"/>.</exception>
        public static Real Log(Real value, Real @base)
        {
            if (value == null || @base == null)
                throw new ArgumentNullException();
            return System.Math.Log(value.ToDouble(), @base.ToDouble());
        }

        /// <summary>
        /// Returns the base-10 logarithm of a specified real number.
        /// </summary>
        /// <param name="value">The number whose logarithm is to be found.</param>
        /// <returns>The base-10 logarithm of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Lg(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Log10(value.ToDouble());
        }

        /// <summary>
        /// Returns the larger of two numbers
        /// </summary>
        /// <param name="x">The first of two numbers to compare.</param>
        /// <param name="y">The second of two numbers to compare.</param>
        /// <returns>Parameter <paramref name="x"/> or <paramref name="y"/>, whichever is larger.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="x"/> or <paramref name="y"/> is <see langword="null"/>.</exception>
        public static Real Max(Real x, Real y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            return x.CompareTo(y) >= 0 ? x : y;
        }

        /// <summary>
        /// Returns the smaller of two numbers
        /// </summary>
        /// <param name="x">The first of two numbers to compare.</param>
        /// <param name="y">The second of two numbers to compare.</param>
        /// <returns>Parameter <paramref name="x"/> or <paramref name="y"/>, whichever is smaller.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="x"/> or <paramref name="y"/> is <see langword="null"/>.</exception>
        public static Real Min(Real x, Real y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            return x.CompareTo(y) <= 0 ? x : y;
        }

        /// <summary>
        /// Rounds a <see cref="Real"/> object to an integral number.
        /// </summary>
        /// <param name="value">A <see cref="Real"/> object to be rounded.</param>
        /// <returns>The integral number nearest to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Real Round(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Round(value.ToDouble());
        }

        /// <summary>
        /// Returns the sine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Sin(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Sin(value.ToDouble());
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Sinh(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Sinh(value.ToDouble());
        }

        /// <summary>
        /// Returns the square root of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The square root of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Sqrt(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Sqrt(value.ToDouble());
        }

        /// <summary>
        /// Returns the tangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Tan(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Tan(value.ToDouble());
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified number.
        /// </summary>
        /// <param name="value">The number.</param>
        /// <returns>The hyperbolic tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Real Tanh(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return System.Math.Tanh(value.ToDouble());
        }   
    }
}
