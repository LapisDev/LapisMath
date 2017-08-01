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
        /// Converts a <see cref="Int32"/> value to a <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Real"/> object that wraps <paramref name="value"/>.</returns>        
        public static Real FromInt32(int value)
        {
            return Rational.FromInt32(value);
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Int32"/> value to a <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Real"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Real(int value)
        {
            return FromInt32(value);
        }

        /// <summary>
        /// Converts the <see cref="Real"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <returns>The <see cref="Double"/> value that the <see cref="Real"/> object wraps.</returns>        
        public abstract double ToDouble();

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="Real"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Real"/> object to be converted.</param>
        /// <returns>The <see cref="Double"/> value that <paramref name="value"/> wraps.</returns>
        public static implicit operator double(Real value)
        {
            if (value == null)
                return 0;
            return value.ToDouble();
        }

        /// <summary>
        /// Converts a <see cref="Double"/> value to a <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Real"/> object that wraps <paramref name="value"/>.</returns>        
        public static Real FromDouble(double value)
        {
            return Normalize(FloatingPoint.FromDouble(value));
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Double"/> value to a <see cref="Real"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Real"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Real(double value)
        {
            return FromDouble(value);
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Real"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Real"/> object.</returns>
        public abstract override string ToString();
    }
}
