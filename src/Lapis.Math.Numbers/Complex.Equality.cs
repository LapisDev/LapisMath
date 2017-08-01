/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Complex
 * Description : Represents a complex number.
 * Created     : 2015/8/19
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class Complex : IEquatable<Complex>
    {
        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Complex"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Complex"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Complex)
                return Equals((Complex)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Complex"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Complex"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Complex value)
        {
            if (value == null)
                return false;
            return Real == value.Real && Imaginary == value.Imaginary;
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Complex"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Complex"/> object to compare.</param>
        /// <param name="right">The second <see cref="Complex"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Complex left, Complex right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Complex"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Complex"/> object to compare.</param>
        /// <param name="right">The second <see cref="Complex"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Complex left, Complex right)
        {
            return Equal(left, right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Complex"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Complex"/> object to compare.</param>
        /// <param name="right">The second <see cref="Complex"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Complex left, Complex right)
        {
            return !(left == right);
        }
       
        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.Zero"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.Zero"/>; otherwise, <see langword="false"/>.</value>
        public bool IsZero { get { return this == Zero; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.One"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.One"/>; otherwise, <see langword="false"/>.</value>
        public bool IsOne { get { return this == One; } }
        
        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.MinusOne"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.MinusOne"/>; otherwise, <see langword="false"/>.</value>
        public bool IsMinusOne { get { return this == MinusOne; } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.ImaginaryOne"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.ImaginaryOne"/>; otherwise, <see langword="false"/>.</value>
        public bool IsImaginaryOne { get { return this == ImaginaryOne; } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.MinusImaginaryOne"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.MinusImaginaryOne"/>; otherwise, <see langword="false"/>.</value>
        public bool IsMinusImaginaryOne { get { return this == MinusImaginaryOne; } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.ComplexInfinity"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.ComplexInfinity"/>; otherwise, <see langword="false"/>.</value>
        public bool IsComplexInfinity { get { return this == ComplexInfinity; } }

        /// <summary>
        /// Indicates whether the value of the current <see cref="Complex"/> object is <see cref="Complex.Undefined"/>.
        /// </summary>
        /// <value><see langword="true"/> if the value of the current <see cref="Complex"/> object is <see cref="Complex.Undefined"/>; otherwise, <see langword="false"/>.</value>
        public bool IsUndefined { get { return Real.IsNaN || Imaginary.IsNaN; } }    
    }
}
