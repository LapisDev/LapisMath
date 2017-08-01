/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigInteger
 * Description : Represents an arbitrarily large interger.
 * Created     : 2015/6/25
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigInteger
    {
        /// <summary>
        /// Gets a value that represents the number zero (0).
        /// </summary>
        /// <value>An object whose value is zero (0).</value>
        public static readonly BigInteger Zero
            = new BigInteger(0, new int[1]);

        /// <summary>
        /// Gets a value that represents the number one (1).
        /// </summary>
        /// <value>An object whose value is one (1).</value>
        public static readonly BigInteger One
            = new BigInteger(1, new int[1] { 1 });

        /// <summary>
        /// Gets a value that represents the number negative one (-1).
        /// </summary>
        /// <value>An object whose value is negative one (-1).</value>
        public static readonly BigInteger MinusOne
            = new BigInteger(-1, new int[1] { 1 });
    }
}
