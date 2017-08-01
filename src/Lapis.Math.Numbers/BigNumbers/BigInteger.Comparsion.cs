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
    public partial class BigInteger : IComparable<BigInteger>, IEquatable<BigInteger>
    {
        /// <summary>
        /// Compares this instance to a second <see cref="BigInteger"/> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="other">The <see cref="BigInteger"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of this instance to <paramref name="other"/>. </para>
        /// <para>Less than zero, if the current instance is less than <paramref name="other"/>.</para>
        /// <para>Zero, if current instance equals <paramref name="other"/>.</para>
        /// <para>Greater than zero, if the current instance is greater than <paramref name="other"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public int CompareTo(BigInteger other)
        {
            if (object.ReferenceEquals(other, null))
                throw new ArgumentNullException();
            else if (Sign != other.Sign)
                return Sign.CompareTo(other.Sign);
            else if (Sign > 0)
            {
                if (Length != other.Length)
                    return Length.CompareTo(other.Length);
                else
                {
                    for (long i = Length - 1; i >= 0; i--)
                        if (_data[i] != other._data[i])
                            return _data[i].CompareTo(other._data[i]);
                    return 0;
                }
            }
            else if (Sign < 0)
            {
                if (Length != other.Length)
                    return -Length.CompareTo(other.Length);
                else
                {
                    for (long i = Length - 1; i >= 0; i--)
                        if (_data[i] != other._data[i])
                            return -_data[i].CompareTo(other._data[i]);
                    return 0;
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this.Sign.GetHashCode() ^ this.Length.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="BigInteger"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="BigInteger"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BigInteger)
                return Equals((BigInteger)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="BigInteger"/> object have the same value.
        /// </summary>
        /// <param name="other">The <see cref="BigInteger"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="other"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(BigInteger other)
        {
            if(object.ReferenceEquals(other, null))
                return false;
            else if (Sign != other.Sign)
                return false;
            else if (Length != other.Length)
                return false;
            else
            {
                for (long i = 0; i < Length; i++)
                    if (_data[i] != other._data[i])
                        return false;
                return true;
            }           
        }              

        /// <summary>
        /// Indicates whether the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.Zero"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.Zero"/>; otherwise, <see langword="false"/>.</value>
        public bool IsZero { get { return this.Equals(Zero); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.One"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.One"/>; otherwise, <see langword="false"/>.</value>
        public bool IsOne { get { return this.Equals(One); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.MinusOne"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="BigInteger"/> object is <see cref="BigInteger.MinusOne"/>; otherwise, <see langword="false"/>.</value>
        public bool IsMinusOne { get { return this.Equals(MinusOne); } }
    }
}
