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
    /// <summary>
    /// Represents a real number. This class is abstract.
    /// </summary>
    public abstract partial class Real
    {
        #region Non-public

        internal Real() { }

        private static Real Normalize(Real value)
        {
            if (value is Rational)
                return value;
            else if (value is FloatingPoint)
            {
                var dbl = ((FloatingPoint)value).ToDouble();
                int i;
                if (Util.IsInteger(dbl, out i))
                    return Rational.FromInt32(i);
                int num, den;
                if (Util.IsRational(dbl, out num, out den))
                    return Fraction.Create(num, den);
                else
                    return value;                
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        private FloatingPoint ToFloatingPoint()
        {
            if (this is Rational)
                return FloatingPoint.FromDouble(this.ToDouble());
            else if (this is FloatingPoint)
                return (FloatingPoint)this;
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }

        #endregion
    }
}
