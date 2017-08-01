/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Integer
 * Description : Represents a 32-bit signed integer.
 * Created     : 2015/3/28
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class Integer
    {
        /// <summary>
        /// Converts a <see cref="Int32"/> value to a <see cref="Integer"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Integer"/> object that wraps <paramref name="value"/>.</returns>        
        public new static Integer FromInt32(int value)
        {
            return new Integer(value);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Int32"/> value to a <see cref="Integer"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Integer"/> object that wraps <paramref name="value"/>.</returns>        
        public static implicit operator Integer(int value)
        {
            return FromInt32(value);
        }

        /// <summary>
        /// Converts the <see cref="Integer"/> object to a <see cref="Int32"/> value.
        /// </summary>
        /// <returns>The <see cref="Int32"/> value that the <see cref="Integer"/> object wraps.</returns>        
        public int ToInt32()
        {
            return this.value;
        }

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="Integer"/> object to a <see cref="Int32"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Integer"/> object to be converted.</param>
        /// <returns>The <see cref="Int32"/> value that <paramref name="value"/> wraps.</returns>
        public static explicit operator int(Integer value)
        {
            if (value == null)
                return 0;
            return value.ToInt32();
        }

        /// <summary>
        /// Converts the <see cref="Integer"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <returns>The <see cref="Double"/> value that is equal to the <see cref="Integer"/> object.</returns>        
        public override double ToDouble()
        {
            return this.value;
        }

        /// <summary>
        /// Defines an implicit conversion of ta <see cref="Integer"/> object to a <see cref="Double"/> value.
        /// </summary>
        /// <param name="value">The <see cref="Integer"/> object to be converted.</param>
        /// <returns>The <see cref="Double"/> value that is equal to <paramref name="value"/>.</returns>
        public static implicit operator double(Integer value)
        {
            if (value == null)
                return 0;
            return value.ToDouble();
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Integer"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Integer"/> object.</returns>
        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
