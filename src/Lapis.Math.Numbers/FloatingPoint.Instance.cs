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
        /// Represents a value that is not a number (NaN).
        /// </summary>
        /// <value>The value that is not a number (NaN).</value>
        /// <seealso cref="double.NaN"/>
        public static FloatingPoint NaN = new FloatingPoint(double.NaN);
        
        /// <summary>
        /// Represents negative infinity. 
        /// </summary>
        /// <value>The negative infinity. </value>
        /// <seealso cref="double.NegativeInfinity"/>
        public static FloatingPoint NegativeInfinity = new FloatingPoint(double.NegativeInfinity);
        
        /// <summary>
        /// Represents positive infinity.
        /// </summary>
        /// <value>The positive infinity.</value>
        /// <seealso cref="double.PositiveInfinity"/>
        public static FloatingPoint PositiveInfinity = new FloatingPoint(double.PositiveInfinity);
        
        /// <summary>
        /// Represents the largest possible value of a <see cref="FloatingPoint"/> object. 
        /// </summary>
        /// <value>The largest possible value of a <see cref="FloatingPoint"/> object. </value>
        /// <seealso cref="double.MaxValue"/>
        public static FloatingPoint MaxValue = new FloatingPoint(double.MaxValue);
        
        /// <summary>
        /// Represents the smallest possible value of a <see cref="FloatingPoint"/> object. 
        /// </summary>
        /// <value>The smallest possible value of a <see cref="FloatingPoint"/> object</value>
        /// <seealso cref="double.MinValue"/>
        public static FloatingPoint MinValue = new FloatingPoint(double.MinValue);
        
        /// <summary>
        /// Represents the smallest positive <see cref="FloatingPoint"/> value that is greater than zero. 
        /// </summary>
        /// <value>The smallest positive <see cref="FloatingPoint"/> value that is greater than zero. </value>
        /// <seealso cref="double.Epsilon"/>
        public static FloatingPoint Epsilon = new FloatingPoint(double.Epsilon);
    }
}
