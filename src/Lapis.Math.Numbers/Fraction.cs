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
    /// <summary>
    /// Represents a fraction number.
    /// </summary>
    public partial class Fraction : Rational
    {

        /// <summary>
        /// Gets the numerator of the fraction.
        /// </summary>
        /// <value>The numerator of the fraction.</value>
        public override int Numerator { get { return _numerator; } }

        /// <summary>
        /// Gets the denominator of the fraction.
        /// </summary>
        /// <value>The denominator of the fraction.</value>
        public override int Denominator { get { return _denominator; } }
            
        /// <summary>
        /// Creates a <see cref="Fraction"/> object using the specified numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>The fraction <paramref name="numerator"/>/<paramref name="denominator"/>.</returns>
        /// <exception cref="DivideByZeroException"><paramref name="denominator"/> is 0.</exception>
        public new static Fraction Create(int numerator, int denominator)
        { 
            return Normalize(numerator, denominator);
        }
        
        /// <summary>
        /// Creates a <see cref="Fraction"/> object using the specified numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <returns>The fraction <paramref name="numerator"/>/<paramref name="denominator"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="DivideByZeroException"><paramref name="denominator"/> is 0.</exception>
        public new static Fraction Create(Integer numerator, Integer denominator)
        {
            if (numerator == null || denominator == null)
                throw new ArgumentNullException();
            return Normalize((int)numerator, (int)denominator);
        }


        #region Private

        private int _numerator;

        private int _denominator;

        private Fraction(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        private static Fraction Normalize(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException();
            else if (denominator == 1)
                return new Fraction(numerator, denominator);
            else
            {
                var gcd = Integer.Gcd(numerator, denominator);
                numerator = numerator / gcd;
                denominator = denominator / gcd;
                if (denominator < 0)
                    return new Fraction(-numerator, -denominator);
                else
                    return new Fraction(numerator, denominator);
            }
        }

        #endregion
    }
}
