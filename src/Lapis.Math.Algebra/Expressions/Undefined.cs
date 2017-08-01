/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Undefined
 * Description : Represents an undefined value.
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
    /// Represents an undefined value.
    /// </summary>
    public class Undefined : Expression
    {

        /// <summary>
        /// Represents an undefined value.
        /// </summary>
        /// <value>A undefined value.</value>
        public static readonly Undefined Instance = new Undefined();


        #region Convertion

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return IdentifierResource.Undefined;
        }

        /// <summary>
        /// Converts the <see cref="Undefined"/> object to <see cref="Double"/>.
        /// </summary>
        /// <returns><see cref="Double.NaN"/>.</returns>
        public double ToDouble()
        {
            return double.NaN;
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
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Undefined"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            return obj is Undefined;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see cref="Undefined"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            return value is Undefined;
        }

        #endregion


        #region Private

        private Undefined() { }

        #endregion
    }
}
