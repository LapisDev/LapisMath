/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Product
 * Description : Represents a product expression.
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
    /// Represents a product expression.
    /// </summary>
    public class Product : Expression, IEnumerable<Expression>
    {
        /// <summary>
        /// Gets the factor at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the factor to get.</param>
        /// <value>The factor at the specified index. </value>
        [System.Runtime.CompilerServices.IndexerName("Factor")]
        public Expression this[int index]
        {
            get { return _innerList[index]; }
        }

        /// <summary>
        /// Gets the number of factors of the product.
        /// </summary>
        /// <value>The number of factors of the product.</value>
        public int NumberOfFactors
        {
            get { return _innerList.Count; }
        }

        /// <summary>
        /// Creates an instance of the <see cref="Product"/> class using the specified <see cref="IEnumerable{Expression}"/> containing the factors.
        /// </summary>
        /// <param name="expressions">The collection whose elements are copied to the new <see cref="Product"/> as the factors.</param>
        /// <returns>The <see cref="Product"/> whose factors are <paramref name="expressions"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="expressions"/> is empty or has only one element.</exception>
        public static Product Create(IEnumerable<Expression> expressions)
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
            Product prod = new Product(list);
            return prod;
        }
        
        /// <summary>
        /// Creates an instance of the <see cref="Product"/> class using the specified <see cref="Expression"/> array containing the factors.
        /// </summary>
        /// <param name="expressions">The array whose elements are copied to the new <see cref="Product"/> as the factors.</param>
        /// <returns>The <see cref="Product"/> whose factors are <paramref name="expressions"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="expressions"/> is empty or has only one element.</exception>
        public static Product Create(params Expression[] expressions)
        {
            return Create((IEnumerable<Expression>)expressions);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the factors of the <see cref="Product"/>.
        /// </summary>
        /// <returns>An enumerator for the factors of the <see cref="Product"/>.</returns>
        public IEnumerator<Expression> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the factors of the <see cref="Product"/>.
        /// </summary>
        /// <returns>An enumerator for the factors of the <see cref="Product"/>.</returns>
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
            if (obj is Product)
            {
                return Equals((Product)obj);
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
            if (value is Product)
            {
                return Equals((Product)value);
            }
            else { return false; }
        }

        #endregion


        #region Private

        private List<Expression> _innerList;        

        internal Product(List<Expression> expressions)
        {
            _innerList = expressions;
        }

        private bool Equals(Product value)
        {
            return Util.ItemEqual(this, value);
        }            

        #endregion
    }
}
