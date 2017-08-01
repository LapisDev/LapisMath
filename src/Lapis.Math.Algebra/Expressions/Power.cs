/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Power
 * Description : Represents a power expression.
 * Created     : 2015/3/31
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    /// <summary>
    /// Represents a power expression.
    /// </summary>
    public class Power : Expression
    {
        /// <summary>
        /// Gets the expression of the base.
        /// </summary>
        /// <value>The expression of the base.</value>
        public Expression Base { get; private set; }
        
        /// <summary>
        /// Gets the expression of the exponent.
        /// </summary>
        /// <value>The expression of the exponent.</value>
        public Expression Exponent { get; private set; }

        /// <summary>
        /// Creates an instance of the <see cref="Power"/> class using the specified base and exponent.
        /// </summary>
        /// <param name="base">The expression of the base.</param>
        /// <param name="exponent">The expression of the exponent.</param>
        /// <returns>The power expression <paramref name="base"/>^<paramref name="exponent"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Power Create(Expression @base, Expression exponent)
        {
            if (@base == null || exponent == null)
                throw new ArgumentNullException();
            else
                return new Power(@base, exponent);
        }


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return "^".GetHashCode() ^ Base.GetHashCode() ^ Exponent.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Expression"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Expression"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Power)
                return Equals((Power)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            if (value is Power)
                return Equals((Power)value);
            else
                return false;
        }       

        #endregion


        #region Private

        private Power(Expression @base, Expression exponent)
        {
            Base = @base;
            Exponent = exponent;
        }

        private bool Equals(Power value)
        {
            return Base == value.Base &&
                Exponent == value.Exponent;
        }

        #endregion
    }
}
