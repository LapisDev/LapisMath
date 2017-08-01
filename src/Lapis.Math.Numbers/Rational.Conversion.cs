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
        /// Converts a <see cref="Int32"/> value to a <see cref="Rational"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Rational"/> object that wraps <paramref name="value"/>.</returns>        
        public new static Rational FromInt32(int value)
        {
            return Integer.FromInt32(value);
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Int32"/> value to a <see cref="Rational"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Rational"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Rational(int value)
        {
            return FromInt32(value);
        }

        /// <summary>
        /// Converts the <see cref="Rational"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <returns>The <see cref="Double"/> value that is equal to the <see cref="Rational"/> object.</returns>        
        public override double ToDouble()
        {
            return (double)this.Numerator / this.Denominator;
        }

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="Rational"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Rational"/> object to be converted.</param>
        /// <returns>The <see cref="Double"/> value that is equal to <paramref name="value"/>.</returns>
        public static implicit operator double(Rational value)
        {
            if (value == null)
                return 0;
            return value.ToDouble();
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Rational"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Rational"/> object.</returns>
        public abstract override string ToString();
    }
}
