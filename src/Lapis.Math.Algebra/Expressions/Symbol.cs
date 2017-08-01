/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Symbol
 * Description : Represents an algebraic symbol.
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
    /// Represents an algebraic symbol.
    /// </summary>
    public class Symbol : Expression
    {
        /// <summary>
        /// Gets the identifier of the algebraic symbol.
        /// </summary>
        /// <value>The identifier of the algebraic symbol.</value>
        public string Identifier { get; private set; }


        #region Convertion

        /// <summary>
        /// Creates an instance of the <see cref="Symbol"/> class using the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the algebraic symbol.</param>
        /// <returns>The algebraic symbol <paramref name="identifier"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid.</exception>
        public static Symbol FromString(string identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException();
            if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            return new Symbol(identifier);
        }

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return Identifier;
        }

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Expression"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Symbol"/> and its <see cref="Identifier"/> is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Symbol)
            {
                return Equals((Symbol)obj);
            }
            else { return false; }
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> is <see cref="Symbol"/> and its <see cref="Identifier"/> is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            if (value is Symbol)
            {
                return Equals((Symbol)value);
            }
            else { return false; }
        }

        #endregion


        #region Private

        private Symbol(string identifier)
        {
            Identifier = identifier;
        }

        private bool Equals(Symbol value)
        {
            return Identifier == value.Identifier;
        }

        #endregion
    }
}
