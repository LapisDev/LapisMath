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
        /// Converts a <see cref="Int32"/> value to a <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <returns>A <see cref="Fraction"/> object that is equal to <paramref name="value"/>.</returns>        
        public new static Fraction FromInt32(int value)
        {
            return new Fraction(value, 1);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Int32"/> value to a <see cref="Fraction"/> object.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <returns>A <see cref="Fraction"/> object that is equal to <paramref name="value"/>.</returns>        
        public static implicit operator Fraction(int value)
        {
            return FromInt32(value);
        }

        /// <summary>
        /// Converts the <see cref="Fraction"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <returns>The <see cref="Double"/> value that is equal to the <see cref="Fraction"/> object.</returns>        
        public override double ToDouble()
        {
            return (double)_numerator / _denominator;
        }

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="Fraction"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Fraction"/> object to be converted.</param>
        /// <returns>The <see cref="Double"/> value that is equal to <paramref name="value"/>.</returns>
        public static implicit operator double(Fraction value)
        {
            if (value == null)
                return 0;
            return value.ToDouble();
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Fraction"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Fraction"/> object.</returns>
        public override string ToString()
        {
            return _numerator.ToString() + "/" + _denominator.ToString();
        }        
    }
}
