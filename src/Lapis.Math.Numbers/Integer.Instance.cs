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
        /// Gets a value that represents the number zero (0).
        /// </summary>
        /// <value>An object whose value is zero (0).</value>
        public static readonly Integer Zero = new Integer(0);
        
        /// <summary>
        /// Gets a value that represents the number one (1).
        /// </summary>
        /// <value>An object whose value is one (1).</value>
        public static readonly Integer One = new Integer(1);
        
        /// <summary>
        /// Gets a value that represents the number negative one (-1).
        /// </summary>
        /// <value>An object whose value is negative one (-1).</value>
        public static readonly Integer MinusOne = new Integer(-1);

        /// <summary>
        /// Represents the largest possible value of a <see cref="Integer"/> object. 
        /// </summary>
        /// <value>The largest possible value of a <see cref="Integer"/> object. </value>
        /// <seealso cref="System.Int32.MaxValue"/>
        public static readonly Integer MaxValue = new Integer(int.MaxValue);
        
        /// <summary>
        /// Represents the smallest possible value of a <see cref="Integer"/> object. 
        /// </summary>
        /// <value>The smallest possible value of a <see cref="Integer"/> object</value>
        /// <seealso cref="System.Int32.MinValue"/>
        public static readonly Integer MinValue = new Integer(int.MinValue);
    }
}
