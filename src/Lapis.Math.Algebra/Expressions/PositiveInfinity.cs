/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : PositiveInfinity
 * Description : Represents the positive infinity.
 * Created     : 2015/3/29
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    /// <summary>
    /// Represents the positive infinity.
    /// </summary>
    public class PositiveInfinity : Expression
    {
        
        /// <summary>
        /// Represents the positive infinity.
        /// </summary>
        /// <value>The positive infinity.</value>
        public static readonly PositiveInfinity Instance = new PositiveInfinity();

        #region Convertion

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return IdentifierResource.PositiveInfinity;
        }

        /// <summary>
        /// Converts the <see cref="PositiveInfinity"/> object to <see cref="Double"/>.
        /// </summary>
        /// <returns><see cref="Double.PositiveInfinity"/>.</returns>
        public double ToDouble()
        {
            return double.PositiveInfinity;
        }

        #endregion


        #region Comparision

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="PositiveInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is PositiveInfinity;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see cref="PositiveInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            return value is PositiveInfinity;
        }

        #endregion


        #region Private

        private PositiveInfinity() { }

        #endregion
    }
}
