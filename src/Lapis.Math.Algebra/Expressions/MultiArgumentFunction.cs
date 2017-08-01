/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : MultiArgumentFunction
 * Description : Represents a function with multiple arguments.
 * Created     : 2015/3/31
 * Note        :
*********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    /// <summary>
    /// Represents a function with multiple arguments.
    /// </summary>
    public class MultiArgumentFunction : Expression, IEnumerable<Expression>
    {
        /// <summary>
        /// Gets the identifier of the function.
        /// </summary>
        /// <value>The identifier of the function.</value>
        public string Identifier { get; private set; }
        
        /// <summary>
        /// Gets the argument at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the argument to get.</param>
        /// <value>The argument at the specified index. </value>
        [System.Runtime.CompilerServices.IndexerName("Argument")]
        public Expression this[int index]
        {
            get { return _innerList[index]; }
        }
        
        /// <summary>
        /// Gets the number of arguments of the function.
        /// </summary>
        /// <value>The number of arguments of the function.</value>
        public int NumberOfArguments
        {
            get
            {
                return _innerList.Count;
            }
        }

        /// <summary>
        /// Creates an instance of the <see cref="MultiArgumentFunction"/> class using the specified identifier and an <see cref="IEnumerable{Expression}"/> containing the arguments.
        /// </summary>
        /// <param name="identifier">The identifier of the function.</param>
        /// <param name="arguments">The collection whose elements are copied to the new <see cref="MultiArgumentFunction"/> as the arguments.</param>
        /// <returns>The function expression of <paramref name="identifier"/>(<paramref name="arguments"/>).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid, or <paramref name="arguments"/> is empty or has only one element.</exception>
        public static MultiArgumentFunction Create(string identifier, IEnumerable<Expression> arguments)
        {
            if (arguments == null || identifier == null)
                throw new ArgumentNullException();
            if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            else if (arguments.Count() <= 1)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            else
            {
                List<Expression> args = new List<Expression>();
                foreach (Expression arg in arguments)
                {
                    if (arg == null)
                        throw new ArgumentException(ExceptionResource.CountTooFew);
                    else
                        args.Add(arg);
                }
                return new MultiArgumentFunction(identifier, args);
            }
        }

        /// <summary>
        /// Creates an instance of the <see cref="MultiArgumentFunction"/> class using the specified identifier and an <see cref="Expression"/> array containing the arguments.
        /// </summary>
        /// <param name="identifier">The identifier of the function.</param>
        /// <param name="arguments">The array whose elements are copied to the new <see cref="MultiArgumentFunction"/> as the arguments.</param>
        /// <returns>The function expression of <paramref name="identifier"/>(<paramref name="arguments"/>).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid, or <paramref name="arguments"/> is empty or has only one element.</exception>
        public static MultiArgumentFunction Create(string identifier, params Expression[] arguments)
        {
            return Create(identifier, (IEnumerable<Expression>)arguments);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the arguments of the <see cref="MultiArgumentFunction"/>.
        /// </summary>
        /// <returns>An enumerator for the arguments of the <see cref="MultiArgumentFunction"/>.</returns>
        public IEnumerator<Expression> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the arguments of the <see cref="MultiArgumentFunction"/>.
        /// </summary>
        /// <returns>An enumerator for the arguments of the <see cref="MultiArgumentFunction"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        
        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hash = Identifier.GetHashCode();
            foreach (Expression expr in this)
                hash ^= expr.GetHashCode();
            return hash;
        }
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Expression"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Expression"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MultiArgumentFunction)
                return Equals((MultiArgumentFunction)obj);
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
            if (value is MultiArgumentFunction)
                return Equals((MultiArgumentFunction)value);
            else
                return false;
        }

        #endregion


        #region Private

        private List<Expression> _innerList;

        internal MultiArgumentFunction(string identifier, List<Expression> arguments)
        {
            Identifier = identifier;
            _innerList = arguments;
        }

        private bool Equals(MultiArgumentFunction value)
        {
            if (Identifier != value.Identifier)
                return false;
            else
                return Util.ItemEqual(this, value);
        }             

        #endregion
    }
}
