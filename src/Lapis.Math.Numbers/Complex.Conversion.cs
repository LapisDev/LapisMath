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
        /// Converts an <see cref="Int32"/> value to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        public static Complex FromInt32(int value)
        {
            return new Complex(value, Integer.Zero);
        }
        
        /// <summary>
        /// Defines an implicit conversion of an <see cref="Int32"/> value to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Complex(int value)
        {
            return FromInt32(value);
        }
      
        /// <summary>
        /// Converts a <see cref="Double"/> value to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        public static Complex FromDouble(double value)
        {
            return new Complex(value, Integer.Zero);
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Double"/> value to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Complex(double value)
        {
            return FromDouble(value);
        }

        /// <summary>
        /// Converts a <see cref="Real"/> object to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex FromReal(Real value)
        {
            return new Complex(value, Integer.Zero);
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Real"/> object to a <see cref="Complex"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Complex"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Complex(Real value)
        {
            if (value == null)
                return null;
            return FromReal(value);
        }

        /// <summary>
        /// Creates a complex number from a point's rectangular coordinates.
        /// </summary>
        /// <param name="real">The real part of the complex number.</param>
        /// <param name="imaginary">The imaginary part of the complex number.</param>
        /// <returns>A complex number.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex FromRectangularCoordinates(Real real, Real imaginary)
        {
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Creates a complex number from a point's polar coordinates.
        /// </summary>
        /// <param name="magnitude">The magnitude, which is the distance from the origin (the intersection of the x-axis and the y-axis) to the number.</param>
        /// <param name="phase">The phase, which is the angle from the line to the horizontal axis, measured in radians.</param>
        /// <returns>A complex number.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Complex FromPolarCoordinates(Real magnitude, Real phase)
        {
            if (magnitude.IsInfinity)
            {
                return ComplexInfinity;
            }
            if (magnitude == 0)
            {
                return Zero;
            }
            if (magnitude.IsNaN ||
                phase.IsNaN ||
                phase.IsInfinity)
            {
                return Undefined;
            }            
            var real = magnitude * Real.Cos(phase);
            var imaginary = magnitude * Real.Sin(phase);
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Complex"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Complex"/> object.</returns>
        public override string ToString()
        {
            if (IsUndefined)
                return IdentifierResource.Undefined;
            if (IsComplexInfinity)
                return IdentifierResource.ComplexInfinity;
            if (Real.Sign == 0)
                if (Imaginary.Sign < 0)
                    return string.Format("-{0}*i", -Imaginary);
                else if (Imaginary.Sign == 0)
                    return Real.ToString();
                else
                    return string.Format("{0}*i", Imaginary);
            else
                if (Imaginary.Sign < 0)
                    return string.Format("{0}-{1}*i", Real, -Imaginary);
                else if (Imaginary.Sign == 0)
                    return Real.ToString();
                else
                    return string.Format("{0}+{1}*i", Real, Imaginary);
        }
    }
}
