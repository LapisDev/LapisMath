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
    public partial class BigDecimal
    {
        /// <summary>
        /// Gets a value that represents the number zero (0).
        /// </summary>
        /// <value>An object whose value is zero (0).</value>
        public static readonly BigDecimal Zero
            = new BigDecimal(BigInteger.Zero, 0);

        /// <summary>
        /// Gets a value that represents the number one (1).
        /// </summary>
        /// <value>An object whose value is one (1).</value>
        public static readonly BigDecimal One
            = new BigDecimal(BigInteger.One, 0);

        /// <summary>
        /// Gets a value that represents the number negative one (-1).
        /// </summary>
        /// <value>An object whose value is negative one (-1).</value>
        public static readonly BigDecimal MinusOne
           = new BigDecimal(BigInteger.MinusOne, 0);
    }
}
