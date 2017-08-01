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
    /// <summary>
    /// Represents a 32-bit signed integer. This class is a wrapper of <see cref="System.Int32"/>.
    /// </summary>
    public partial class Integer : Rational
    {
        /// <summary>
        /// Gets an integer <c>1</c>.
        /// </summary>
        /// <value><c>1</c>.</value>
        public override int Denominator { get { return 1; } }

        /// <summary>
        /// Gets the value of the <see cref="Integer"/> object.
        /// </summary>
        /// <value>The value of the <see cref="Integer"/> object.</value>
        public override int Numerator { get { return this.value; } }

        #region Private

        private Integer(int value)
        {
            this.value = value;
        }

        private readonly int value;

        #endregion
    }
}
