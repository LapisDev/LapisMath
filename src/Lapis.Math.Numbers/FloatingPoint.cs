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
    /// <summary>
    /// Represents a double-precision floating-point number. This class is a wrapper of <see cref="System.Double"/>.
    /// </summary>
    public partial class FloatingPoint : Real
    {
        #region Private

        private FloatingPoint(double value)
        {
            this.value = value;
        }

        private readonly double value;

        #endregion
    }
}
