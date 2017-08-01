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
    /// <summary>
    /// Represents an arbitrarily large interger.
    /// </summary>
    public partial class BigInteger
    {
        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="BigInteger"/> object.
        /// </summary>
        /// <value>
        /// <para>A number that indicates the sign of the <see cref="BigInteger"/> object.</para>
        /// <para>The value of this object is negative.</para>
        /// <para>The value of this object is 0 (zero).</para>
        /// <para>The value of this object is positive.</para>
        /// </value>
        public int Sign { get; private set; }


        private int[] _data;      

        internal long Length { get; private set; }

        internal long TenPower
        {
            get
            {
                var head = _data[Length - 1];
                int count = 0;
                while ((head /= 10) != 0)
                    count++;
                return (Length - 1) * 9 + count;
            }
        }

        internal int Digit(long index)
        {
            return _data[index];
        }

        private const int BaseInt = 1000000000;

        private BigInteger(int sign, int[] data)
        {
            _data = data;
            Sign = sign;
            long length = data.Length;
            while (length > 0 && data[length - 1] == 0)
                length--;
            if (length == 0)
            {
                Sign = 0;
                length++;
            }
            Length = length;       
        }      
    }
}
