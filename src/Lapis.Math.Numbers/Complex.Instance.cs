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
    public partial class Complex 
    {
        /// <summary>
        /// Gets a value that represents the number zero (0).
        /// </summary>
        /// <value>An object whose value is zero (0).</value>
        public static readonly Complex Zero = new Complex(0, 0);
        
        /// <summary>
        /// Gets a value that represents the number one (1).
        /// </summary>
        /// <value>An object whose value is one (1).</value>
        public static readonly Complex One = new Complex(1,0);
        
        /// <summary>
        /// Gets a value that represents the number negative one (-1).
        /// </summary>
        /// <value>An object whose value is negative one (-1).</value>
        public static readonly Complex MinusOne = new Complex(-1,0);

        /// <summary>
        /// Gets a value with a real number equal to zero and an imaginary number equal to one.
        /// </summary>
        /// <value>An object whose value is imaginary one (i).</value>
        public static readonly Complex ImaginaryOne = new Complex(0, 1);
        
        /// <summary>
        /// Gets a value with a real number equal to zero and an imaginary number equal to negative one.
        /// </summary>
        /// <value>An object whose value is negative imaginary one (-i).</value>
        public static readonly Complex MinusImaginaryOne = new Complex(0, -1);

        /// <summary>
        /// Gets a value that represents the complex infinity.
        /// </summary>
        /// <value>An object that represents the complex infinity.</value>
        public static readonly Complex ComplexInfinity = new Complex(FloatingPoint.PositiveInfinity, FloatingPoint.PositiveInfinity);

        /// <summary>
        /// Gets a value that represents undefined.
        /// </summary>
        /// <value>An object that represents undefined.</value>
        public static readonly Complex Undefined = new Complex(FloatingPoint.NaN, FloatingPoint.NaN);
    }
}
