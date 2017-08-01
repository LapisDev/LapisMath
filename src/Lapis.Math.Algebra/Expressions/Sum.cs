/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Sum
 * Description : Represents a sum expression.
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
    /// Represents a sum expression.
    /// </summary>
    public class Sum : Expression, IEnumerable<Expression>
    {
        /// <summary>
        /// Gets the addend at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the addend to get.</param>
        /// <value>The addend at the specified index. </value>
        [System.Runtime.CompilerServices.IndexerName("Addend")]
        public Expression this[int index]
        {
            get { return _innerList[index]; }
        }

        /// <summary>
        /// Gets the number of faddends of the sum.
        /// </summary>
        /// <value>The number of addends of the sum.</value>
        public int NumberOfAddends
        {
            get { return _innerList.Count; }
        }

        /// <summary>
        /// Creates an instance of the <see cref="Sum"/> class using the specified <see cref="IEnumerable{Expression}"/> containing the addends.
        /// </summary>
        /// <param name="expressions">The collection whose elements are copied to the new <see cref="Sum"/> as the addends.</param>
        /// <returns>The <see cref="Sum"/> whose addends are <paramref name="expressions"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="expressions"/> is empty or has only one element.</exception>
        public static Sum Create(IEnumerable<Expression> expressions)
        {
            if (expressions == null)
                throw new ArgumentNullException();
            if (expressions.Count() <= 1)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            List<Expression> list = new List<Expression>();
            foreach (Expression expr in expressions)
            {
                if (expr == null)
                    throw new ArgumentException(ExceptionResource.CountTooFew);
                else
                    list.Add(expr);
            }
            Sum sum = new Sum(list);
            return sum;
        }
        
        /// <summary>
        /// Creates an instance of the <see cref="Sum"/> class using the specified <see cref="Expression"/> array containing the addends.
        /// </summary>
        /// <param name="expressions">The array whose elements are copied to the new <see cref="Sum"/> as the addends.</param>
        /// <returns>The <see cref="Sum"/> whose addends are <paramref name="expressions"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="expressions"/> is empty or has only one element.</exception>
        public static Sum Create(params Expression[] expressions)
        {
            return Create((IEnumerable<Expression>)expressions);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the addends of the <see cref="Sum"/>.
        /// </summary>
        /// <returns>An enumerator for the addends of the <see cref="Sum"/>.</returns>
        public IEnumerator<Expression> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the addends of the <see cref="Sum"/>.
        /// </summary>
        /// <returns>An enumerator for the addends of the <see cref="Sum"/>.</returns>
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
            var hash = "+".GetHashCode();
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
            if (obj is Sum)
            {
                return Equals((Sum)obj);
            }
            else { return false; }
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            if (value is Sum)
            {
                return Equals((Sum)value);
            }
            else { return false; }
        }

        #endregion


        #region Private

        private List<Expression> _innerList;        

        internal Sum(List<Expression> expressions)
        {
            _innerList = expressions;
        }

        private bool Equals(Sum value)
        {
            return Util.ItemEqual(this, value);
        }

        #endregion
    }
}
