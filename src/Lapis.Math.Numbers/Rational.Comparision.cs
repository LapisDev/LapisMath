/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Rational
 * Description : Represents a rational number.
 * Created     : 2015/3/29
 * Note        : 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public abstract partial class Rational : IComparable<Rational>, IEquatable<Rational>
    {
        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public abstract override int GetHashCode();

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Rational"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Rational"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Rational)
                return Equals((Rational)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Rational"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Rational"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Rational value)
        {
            if (this is Integer && value is Integer)
                return Integer.Equal((Integer)this, (Integer)value);
            else if (
                (this is Fraction && value is Integer) ||
                (this is Integer && value is Fraction) ||
                (this is Fraction && value is Fraction)
                )
                return Fraction.Equal(this.ToFraction(), value.ToFraction());
            else
                return false;
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Rational"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Rational left, Rational right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Rational"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Rational left, Rational right)
        {
            return Equal(left, right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Rational"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Rational left, Rational right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares this instance to a second <see cref="Rational"/> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="value">The <see cref="Rational"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of this instance to <paramref name="value"/>. </para>
        /// <para>Less than zero, if the current instance is less than <paramref name="value"/>.</para>
        /// <para>Zero, if current instance equals <paramref name="value"/>.</para>
        /// <para>Greater than zero, if the current instance is greater than <paramref name="value"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public int CompareTo(Rational value)
        {
            if (value == null)
                throw new ArgumentNullException();  
            if (this is Integer && value is Integer)
                return ((Integer)this).CompareTo((Integer)value);
            else if (
                (this is Fraction && value is Integer) ||
                (this is Integer && value is Fraction) ||
                (this is Fraction && value is Fraction)
                )
                return this.ToFraction().CompareTo(value.ToFraction());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }
        
        /// <summary>
        /// Compares two specified <see cref="Rational"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of <paramref name="left"/> to <paramref name="right"/>. </para>
        /// <para>Less than zero, if <paramref name="left"/> is less than <paramref name="right"/>.</para>
        /// <para>Zero, if <paramref name="left"/> equals <paramref name="right"/>.</para>
        /// <para>Greater than zero, if <paramref name="left"/> is greater than <paramref name="right"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static int Compare(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();  
            return left.CompareTo(right);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Rational"/> is less than another specified <see cref="Rational"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();  
            return left.CompareTo(right) < 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Rational"/> is greater than another specified <see cref="Rational"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();  
            return left.CompareTo(right) > 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Rational"/> is less than or equal to another specified <see cref="Rational"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <=(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) <= 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Rational"/> is greater than or equal to another specified <see cref="Rational"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Rational"/> object to compare.</param>
        /// <param name="right">The second <see cref="Rational"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >=(Rational left, Rational right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="Rational"/> object.
        /// </summary>
        /// <value>
        /// <para>A number that indicates the sign of the <see cref="Rational"/> object.</para>
        /// <para>The value of this object is negative.</para>
        /// <para>The value of this object is 0 (zero).</para>
        /// <para>The value of this object is positive.</para>
        /// </value>
        public override int Sign { get { return this.CompareTo(0); } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Rational"/> object is 0.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Rational"/> object is 0; otherwise, <see langword="false"/>.</value>
        public override bool IsZero { get { return this == 0; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Rational"/> object is 1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Rational"/> object is 1; otherwise, <see langword="false"/>.</value>
        public override bool IsOne { get { return this == 1; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Rational"/> object is -1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Rational"/> object is -1; otherwise, <see langword="false"/>.</value>
        public override bool IsMinusOne { get { return this == -1; } }
    }
}
