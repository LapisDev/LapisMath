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
    public abstract partial class Real : IComparable<Real>, IEquatable<Real>
    {
        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public abstract override int GetHashCode();

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Real"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Real"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Real)
                return Equals((Real)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Real"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Real"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Real value)
        {
            if (this is Rational && value is Rational)
                return (Rational)this == (Rational)value;
            else if (
                (this is Rational && value is FloatingPoint) ||
                (this is FloatingPoint && value is Rational) ||
                (this is FloatingPoint && value is FloatingPoint)
                )
                return this.ToFloatingPoint().Equals(value.ToFloatingPoint());
            else
                return false;
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Real"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Real left, Real right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Real"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Real left, Real right)
        {
            return Equal(left, right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Real"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Real left, Real right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Compares this instance to a second <see cref="Real"/> and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="value">The <see cref="Real"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of this instance to <paramref name="value"/>. </para>
        /// <para>Less than zero, if the current instance is less than <paramref name="value"/>.</para>
        /// <para>Zero, if current instance equals <paramref name="value"/>.</para>
        /// <para>Greater than zero, if the current instance is greater than <paramref name="value"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public int CompareTo(Real value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (this is Rational && value is Rational)
                return ((Rational)this).CompareTo((Rational)value);
            else if (
                (this is FloatingPoint && value is Rational) ||
                (this is Rational && value is FloatingPoint) ||
                (this is FloatingPoint && value is FloatingPoint)
                )
                return this.ToFloatingPoint().CompareTo(value.ToFloatingPoint());
            else
                throw new ArgumentException(ExceptionResource.InvalidNumber);
        }
        
        /// <summary>
        /// Compares two specified <see cref="Real"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns>
        /// <para>A signed integer value that indicates the relationship of <paramref name="left"/> to <paramref name="right"/>. </para>
        /// <para>Less than zero, if <paramref name="left"/> is less than <paramref name="right"/>.</para>
        /// <para>Zero, if <paramref name="left"/> equals <paramref name="right"/>.</para>
        /// <para>Greater than zero, if <paramref name="left"/> is greater than <paramref name="right"/>.</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static int Compare(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Real"/> is less than another specified <see cref="Real"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) < 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Real"/> is greater than another specified <see cref="Real"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) > 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Real"/> is less than or equal to another specified <see cref="Real"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator <=(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) <= 0;
        }
        
        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Real"/> is greater than or equal to another specified <see cref="Real"/>.
        /// </summary>
        /// <param name="left">The first <see cref="Real"/> object to compare.</param>
        /// <param name="right">The second <see cref="Real"/> object to compare.</param>
        /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>。</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>        
        public static bool operator >=(Real left, Real right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of the current <see cref="Real"/> object.
        /// </summary>
        /// <value>
        /// <para>A number that indicates the sign of the <see cref="Real"/> object.</para>
        /// <para>The value of this object is negative.</para>
        /// <para>The value of this object is 0 (zero).</para>
        /// <para>The value of this object is positive.</para>
        /// </value>
        public virtual int Sign { get { return this.CompareTo(0); } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is 0.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is 0; otherwise, <see langword="false"/>.</value>
        public virtual bool IsZero { get { return this == 0; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is 1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is 1; otherwise, <see langword="false"/>.</value>
        public virtual bool IsOne { get { return this == 1; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is -1.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is -1; otherwise, <see langword="false"/>.</value>
        public virtual bool IsMinusOne { get { return this == -1; } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is <see cref="Double.NaN"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is <see cref="Double.NaN"/>; otherwise, <see langword="false"/>.</value>
        public virtual bool IsNaN { get { return double.IsNaN(this.ToDouble()); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is <see cref="Double.NegativeInfinity"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is <see cref="Double.NegativeInfinity"/>; otherwise, <see langword="false"/>.</value>
        public virtual bool IsNegativeInfinity { get { return double.IsNegativeInfinity(this.ToDouble()); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is <see cref="Double.PositiveInfinity"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is <see cref="Double.PositiveInfinity"/>; otherwise, <see langword="false"/>.</value>
        public virtual bool IsPositiveInfinity { get { return double.IsPositiveInfinity(this.ToDouble()); } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Real"/> object is <see cref="Double.PositiveInfinity"/> or <see cref="Double.NegativeInfinity"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Real"/> object is <see cref="Double.PositiveInfinity"/> or <see cref="Double.NegativeInfinity"/>; otherwise, <see langword="false"/>.</value>
        public virtual bool IsInfinity { get { return double.IsInfinity(this.ToDouble()); } }
    }
}
