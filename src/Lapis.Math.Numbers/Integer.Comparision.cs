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
    public partial class Integer : IComparable<Integer>, IEquatable<Integer>
    {
        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Integer"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Integer"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Integer)
                return Equals((Integer)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Integer"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Integer"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Integer value)
        {
            if (object.ReferenceEquals(value, null))
                return false;
            return this.value.Equals(value.value);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Integer"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Integer left, Integer right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Integer"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Integer left, Integer right)
        {
            return Equal(left, right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Integer"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Integer left, Integer right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares this instance to a second <see cref="Integer"/> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="value">The <see cref="Integer"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of this instance to <paramref name="value"/>. </para>
        /// <para>Less than zero, if the current instance is less than <paramref name="value"/>.</para>
        /// <para>Zero, if current instance equals <paramref name="value"/>.</para>
        /// <para>Greater than zero, if the current instance is greater than <paramref name="value"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public int CompareTo(Integer value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return this.value.CompareTo(value.value);
        }

        /// <summary>
        /// Compares two specified <see cref="Integer"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of <paramref name="left"/> to <paramref name="right"/>. </para>
        /// <para>Less than zero, if <paramref name="left"/> is less than <paramref name="right"/>.</para>
        /// <para>Zero, if <paramref name="left"/> equals <paramref name="right"/>.</para>
        /// <para>Greater than zero, if <paramref name="left"/> is greater than <paramref name="right"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static int Compare(Integer left, Integer right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Integer"/> is less than another specified <see cref="Integer"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <(Integer left, Integer right)
        {
            return Compare(left, right) < 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Integer"/> is greater than another specified <see cref="Integer"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >(Integer left, Integer right)
        {
            return Compare(left, right) > 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Integer"/> is less than or equal to another specified <see cref="Integer"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <=(Integer left, Integer right)
        {
            return Compare(left, right) <= 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Integer"/> is greater than or equal to another specified <see cref="Integer"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Integer"/> object to compare.</param>
        /// <param name="right">The second <see cref="Integer"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >=(Integer left, Integer right)
        {
            return Compare(left, right) >= 0;
        }

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="Integer"/> object.
        /// </summary>
        /// <value>
        /// <para>A number that indicates the sign of the <see cref="Integer"/> object.</para>
        /// <para>The value of this object is negative.</para>
        /// <para>The value of this object is 0 (zero).</para>
        /// <para>The value of this object is positive.</para>
        /// </value>
        public override int Sign { get { return this.value.CompareTo(0); } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is 0.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Integer"/> object is 0; otherwise, <see langword="false"/>.</value>
        public override bool IsZero { get { return this.value == 0; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is 1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Integer"/> object is 1; otherwise, <see langword="false"/>.</value>
        public override bool IsOne { get { return this.value == 1; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Integer"/> object is -1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Integer"/> object is -1; otherwise, <see langword="false"/>.</value>
        public override bool IsMinusOne { get { return this.value == -1; } }
    }
}
