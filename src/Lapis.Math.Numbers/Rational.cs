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
    /// <summary>
    /// Represents a rational number. This class is abstract.
    /// </summary>
    public abstract partial class Rational : Real
    {

        /// <summary>
        /// Creates a <see cref="Rational"/> object using the specified numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator of the rational number.</param>
        /// <param name="denominator">The denominator of the rational number.</param>
        /// <returns>The rational number <paramref name="numerator"/>/<paramref name="denominator"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="denominator"/> is 0.</exception>
        public static Rational Create(Integer numerator, Integer denominator)
        {
            return Normalize(Fraction.Create(numerator, denominator));
        }

        /// <summary>
        /// Creates a <see cref="Rational"/> object using the specified numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator of the rational number.</param>
        /// <param name="denominator">The denominator of the rational number.</param>
        /// <returns>The rational number <paramref name="numerator"/>/<paramref name="denominator"/>.</returns>
        /// <exception cref="DivideByZeroException"><paramref name="denominator"/> is 0.</exception>
        public static Rational Create(int numerator, int denominator)
        {
            return Normalize(Fraction.Create(numerator, denominator));
        }

        /// <summary>
        /// Gets the numerator of the rational number.
        /// </summary>
        /// <value>The numerator of the rational number.</value>
        public abstract int Numerator { get; }
        
        /// <summary>
        /// Gets the denominator of the rational number.
        /// </summary>
        /// <value>The denominator of the rational number.</value>
        public abstract int Denominator { get; }


        #region Non-public

        internal Rational() { }

        private static Rational Normalize(Rational value)
        {
            if (value is Integer)
                return value;
            else if (value is Fraction)
            {
                var frac = (Fraction)value;
                if (frac.Denominator == 1)
                    return frac.Numerator;
                else if (frac.Numerator == 0)
                    return Integer.Zero;
                else
                    return frac;
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        private Fraction ToFraction()
        {
            if (this is Integer)
                return Fraction.FromInt32((int)(Integer)this);
            else if (this is Fraction)
                return (Fraction)this;
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        #endregion
    }
}
