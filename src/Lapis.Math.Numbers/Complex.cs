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
    /// <summary>
    /// Represents a complex number.
    /// </summary>
    public partial class Complex
    {
        /// <summary>
        /// Gets the imaginary component of the current <see cref="Complex"/> object.
        /// </summary>
        /// <value>The imaginary component of the current <see cref="Complex"/> object.</value>
        public Real Imaginary { get; private set; }

        /// <summary>
        /// Gets the real component of the current <see cref="Complex"/> object.
        /// </summary>
        /// <value>The real component of the current <see cref="Complex"/> object.</value>
        public Real Real { get; private set; }

        /// <summary>
        /// Gets the magnitude (or absolute value) of the current <see cref="Complex"/> object.
        /// </summary>
        /// <value>Tets the magnitude (or absolute value) of the current <see cref="Complex"/> object.</value>
        public Real Magnitude
        {
            get 
            {
                if (Real.IsInfinity ||
                    Imaginary.IsInfinity)
                    return FloatingPoint.PositiveInfinity;
                return Real.Sqrt(Real.Pow(Real, 2) + Real.Pow(Imaginary, 2));
            }
        }

        /// <summary>
        /// Gets the phase of the current <see cref="Complex"/> object.
        /// </summary>
        /// <value>Gets the phase of the current <see cref="Complex"/> object.</value>
        public Real Phase
        {
            get
            {
                if (Real.IsInfinity ||
                    Imaginary.IsInfinity)
                    return FloatingPoint.NaN;
                return System.Math.Atan2(Imaginary, Real);
            }
        }


        private Complex(Real real, Real imaginary)
        {
            if (real == null || imaginary == null)
                throw new ArgumentNullException();
            if (real.IsNaN ||
                imaginary.IsNaN)
            {
                Real = FloatingPoint.NaN;
                Imaginary = FloatingPoint.NaN;
                return;
            }
            if (real.IsInfinity ||
                imaginary.IsInfinity)
            {
                Real = FloatingPoint.PositiveInfinity;
                Imaginary = FloatingPoint.PositiveInfinity;
                return;
            }
            Real = real;
            Imaginary = imaginary;
        }
    }
}
