/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigDecimal
 * Description : Represents an arbitrarily large decimal.
 * Created     : 2015/6/28
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    /// <summary>
    /// Represents an arbitrarily large decimal.
    /// </summary>
    public partial class BigDecimal
    {
        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="BigDecimal"/> object.
        /// </summary>
        /// <value>
        /// <para>A number that indicates the sign of the <see cref="BigDecimal"/> object.</para>
        /// <para>The value of this object is negative.</para>
        /// <para>The value of this object is 0 (zero).</para>
        /// <para>The value of this object is positive.</para>
        /// </value>
        public int Sign { get { return _intValue.Sign; } }


        private BigInteger _intValue;

        private long _scale;

        private const int BaseInt = 1000000000;

        private BigDecimal(BigInteger intValue, long scale)
        {
            if (intValue.Sign == 0)
            {
                _intValue = intValue;
                _scale = 0;
                return;
            }
            long i = 0L;
            while (i < intValue.Length && intValue.Digit(i) == 0)
                i++;
            if (i != 0)
            {
                _intValue = BigInteger.Divide9thPowerOfTen(intValue, i);
                _scale = scale += i;
            }
            else
            {
                _intValue = intValue;
                _scale = scale;
            }
        }

        internal long TenPower
        {
            get
            {
                return _intValue.TenPower + 9 * _scale;
            }
        }
    }
}
