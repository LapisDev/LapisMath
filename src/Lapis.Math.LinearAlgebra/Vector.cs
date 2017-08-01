/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Vector
 * Description : Represents a vector.
 * Created     : 2015/5/1
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.LinearAlgebra
{
    /// <summary>
    /// Represents a vector.
    /// </summary>
    public partial class Vector : IEnumerable<Expression>, IEquatable<Vector>
    {
        /// <summary>
        /// Gets the dimension of the vector.
        /// </summary>
        /// <value>The dimension of the vector.</value>
        public int Dimension { get { return _elements.Length; } }
        
        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The one-based index of the element to get.</param>
        /// <value>The element at the specified index.</value>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 1 or greater than <see cref="Dimension"/>.</exception>
        [System.Runtime.CompilerServices.IndexerName("Element")]
        public Expression this[int index]
        {   // one-based index
            get
            {
                if (index <= Dimension && index > 0)
                {
                    return _elements[index - 1];
                }
                else
                    throw new ArgumentException(ExceptionResource.IndexOutOfRange);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the elements of the <see cref="Vector"/>.
        /// </summary>
        /// <returns>An enumerator for the elements of the <see cref="Vector"/>.</returns>
        public IEnumerator<Expression> GetEnumerator()
        {
            return _elements.AsEnumerable().GetEnumerator();
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the elements of the <see cref="Vector"/>.
        /// </summary>
        /// <returns>An enumerator for the elements of the <see cref="Vector"/>.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }


        #region Conversion

        /// <summary>
        /// Creates an instance of the <see cref="Vector"/> class using the specified <see cref="Expression"/> array containing the elements.
        /// </summary>
        /// <param name="elements">The array whose elements are copied to the new <see cref="Vector"/>.</param>
        /// <returns>The <see cref="Vector"/> whose elements are <paramref name="elements"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="elements"/> is empty.</exception>
        public static Vector FromArray(params Expression[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException();     
            if (elements.Length > 0)
            {
                Expression[] r = new Expression[elements.GetLength(0)];
                for (int i = 0; i < elements.GetLength(0); i++)
                {
                    if (elements[i] != null)
                        r[i] = elements[i];
                    else
                        throw new ArgumentNullException();
                }
                return new Vector(r);
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidDimensionOfVector);
        }

        /// <summary>
        /// Defines an explicit conversion of an <see cref="Expression"/> array to a <see cref="Vector"/> object.
        /// </summary>
        /// <param name="value">The array whose elements are copied to the new <see cref="Vector"/>.</param>
        /// <returns>The <see cref="Vector"/> whose elements are <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty or has only one element.</exception>
        public static explicit operator Vector(Expression[] value)
        {
            return FromArray(value);
        }

        /// <summary>
        /// Returns the string representation of the vector.
        /// </summary>
        /// <returns>The string representation of the vector.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < Dimension; i++)
            {
                sb.Append(_elements[i].ToString());
                if (i == Dimension - 1)
                    break;
                else
                    sb.Append(", ");
            }
            sb.Append(" ]");
            return sb.ToString();
        }

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int r = 1;
            foreach (Expression ele in _elements)
                r ^= ele.GetHashCode();
            return r;
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Vector"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Vector"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector)
                return Equals((Vector)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Vector"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Vector"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Vector value)
        {
            if (value.IsNull())
                return false;
            if (this.Dimension == value.Dimension)
            {
                return Util.ItemEqual(this._elements, value._elements);
            }
            else
                return false;
        }

        /// <summary>
        /// Determines whether two specified <see cref="Vector"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Vector"/> object to compare.</param>
        /// <param name="right">The second <see cref="Vector"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Vector left, Vector right)
        {
            return left == right;
        }

        /// <summary>
        /// Determines whether two specified <see cref="Vector"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Vector"/> object to compare.</param>
        /// <param name="right">The second <see cref="Vector"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector left, Vector right)
        {
            if (left.IsNull())
                if (right.IsNull())
                    return true;
                else
                    return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="Vector"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Vector"/> object to compare.</param>
        /// <param name="right">The second <see cref="Vector"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector left, Vector right)
        {
            return !(left == right);
        }

        #endregion
        

        #region Non-public

        internal Vector(Expression[] elements)
        {
            _elements = elements;
        }

        private Expression[] _elements;          

        #endregion
    }
}
