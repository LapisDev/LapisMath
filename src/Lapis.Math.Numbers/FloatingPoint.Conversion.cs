/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : FloatingPoint
 * Description : Represents a double-precision floating-point number.
 * Created     : 2015/3/28
 * Note        : Formerly named Decimal, 2015/7/30
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class FloatingPoint
    {
        /// <summary>
        /// Converts a <see cref="Double"/> value to a <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="FloatingPoint"/> object that wraps <paramref name="value"/>.</returns>        
        public new static FloatingPoint FromDouble(double value)
        {
            return new FloatingPoint(value);
        }
        
        /// <summary>
        /// Defines an implicit conversion of a <see cref="Double"/> value to a <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="FloatingPoint"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator FloatingPoint(double value)
        {
            return FromDouble(value);
        }

        /// <summary>
        /// Converts the <see cref="FloatingPoint"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <returns>The <see cref="Double"/> value that the <see cref="FloatingPoint"/> object wraps.</returns>        
        public override double ToDouble()
        {
            return this.value;
        }

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="FloatingPoint"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="FloatingPoint"/> object to be converted.</param>
        /// <returns>The <see cref="Double"/> value that <paramref name="value"/> wraps.</returns>
        public static implicit operator double(FloatingPoint value)
        {
            if (value == null)
                return 0;
            return value.ToDouble();
        }

        /// <summary>
        /// Returns the string representation of the <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="FloatingPoint"/> object.</returns>
        public override string ToString()
        {
            return this.value.ToString();
        }       
    }
}
