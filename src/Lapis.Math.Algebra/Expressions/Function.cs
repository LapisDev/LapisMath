/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Function
 * Description : Represents a function with one argument.
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
    /// Represents a function with one argument.
    /// </summary>
    public class Function : Expression
    {        
        /// <summary>
        /// Gets the expression of the argument.
        /// </summary>
        /// <value>The expression of the argument.</value>
        public Expression Argument { get; private set; }
        
        /// <summary>
        /// Gets the identifier of the function.
        /// </summary>
        /// <value>The identifier of the function.</value>
        public string Identifier { get; private set; }

        /// <summary>
        /// Creates an instance of the <see cref="Function"/> class using the specified identifier and argument.
        /// </summary>
        /// <param name="identifier">The identifier of the function.</param>
        /// <param name="argument">The expression of the argument.</param>
        /// <returns>The function expression of <paramref name="identifier"/>(<paramref name="argument"/>).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid.</exception>
        public static Function Create(string identifier, Expression argument)
        {
            if (argument == null || identifier == null)
                throw new ArgumentNullException();
            else if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            return new Function(identifier, argument);
        }
         

        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Identifier.GetHashCode() ^ Argument.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Expression"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Expression"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Function)
                return Equals((Function)obj);
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
            if (value is Function)
                return Equals((Function)value);
            else
                return false;
        }        

        #endregion


        #region Private

        private Function(string identifier, Expression argument)
        {
            Identifier = identifier;
            Argument = argument;
        }

        private bool Equals(Function value)
        {
            return Identifier == value.Identifier &&
                Argument == value.Argument;
        }

        #endregion
    }
}
