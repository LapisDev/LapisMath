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
    public partial class BigDecimal : IComparable<BigDecimal>, IEquatable<BigDecimal>
    {
        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this._intValue.GetHashCode() ^ this._scale.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="BigDecimal"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="BigDecimal"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BigDecimal)
                return Equals((BigDecimal)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="BigDecimal"/> object have the same value.
        /// </summary>
        /// <param name="other">The <see cref="BigDecimal"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="other"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(BigDecimal other)
        {
            if (object.ReferenceEquals(other, null))
                return false;
            return _scale == other._scale && _intValue.Equals(other._intValue);
        }

        /// <summary>
        /// Compares this instance to a second <see cref="BigDecimal"/> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="other">The <see cref="BigDecimal"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of this instance to <paramref name="other"/>. </para>
        /// <para>Less than zero, if the current instance is less than <paramref name="other"/>.</para>
        /// <para>Zero, if current instance equals <paramref name="other"/>.</para>
        /// <para>Greater than zero, if the current instance is greater than <paramref name="other"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public int CompareTo(BigDecimal other)
        {
            if (object.ReferenceEquals(other, null))
                throw new ArgumentNullException();
            else if (Sign != other.Sign)
                return Sign.CompareTo(other.Sign);
			else
            {
                long s1 = _scale, s2 = other._scale;
                long s = s1 > s2 ? s2 : s1;
                BigInteger int1 = BigInteger.Multiply9thPowerOfTen(_intValue, s1 - s);
                BigInteger int2 = BigInteger.Multiply9thPowerOfTen(other._intValue, s2 - s);
                return int1.CompareTo(int2);
            }
        }

        /// <summary>
        /// Indicates whether the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.Zero"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.Zero"/>; otherwise, <see langword="false"/>.</value>
        public bool IsZero { get { return this.Equals(Zero); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.One"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.One"/>; otherwise, <see langword="false"/>.</value>
        public bool IsOne { get { return this.Equals(One); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.MinusOne"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigDecimal"/> object is <see cref="BigDecimal.MinusOne"/>; otherwise, <see langword="false"/>.</value>
        public bool IsMinusOne { get { return this.Equals(MinusOne); } }
    }
}
