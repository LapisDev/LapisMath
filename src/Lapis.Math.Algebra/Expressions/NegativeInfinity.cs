/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : NegativeInfinity
 * Description : Represents the negative infinity.
 * Created     : 2015/4/5
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    /// <summary>
    /// Represents the negative infinity.
    /// </summary>
    public class NegativeInfinity : Expression
    {
        /// <summary>
        /// Represents the negative infinity.
        /// </summary>
        /// <value>The negative infinity.</value>
        public static readonly NegativeInfinity Instance = new NegativeInfinity();

        #region Convertion

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return IdentifierResource.NegativeInfinity;
        }

        /// <summary>
        /// Converts the <see cref="NegativeInfinity"/> object to <see cref="Double"/>.
        /// </summary>
        /// <returns><see cref="Double.NegativeInfinity"/>.</returns>
        public double ToDouble()
        {
            return double.NegativeInfinity;
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
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="NegativeInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is NegativeInfinity;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see cref="NegativeInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            return value is NegativeInfinity;
        }

        #endregion
        

        #region Private

        private NegativeInfinity() { }

        #endregion
    }
}
