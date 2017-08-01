/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : ComplexInfinity
 * Description : Represents the complex infinity.
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
    /// Represents the complex infinity.
    /// </summary>
    public class ComplexInfinity : Expression
    {
        /// <summary>
        /// Represents the complex infinity.
        /// </summary>
        /// <value>The complex infinity.</value>
        public static readonly ComplexInfinity Instance = new ComplexInfinity();


        #region Convertion

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return IdentifierResource.ComplexInfinity;
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
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="ComplexInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is ComplexInfinity;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see cref="ComplexInfinity"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            return value is ComplexInfinity;
        }

        #endregion
        

        #region Private

        private ComplexInfinity() { }

        #endregion
    }
}