/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Matrix
 * Description : Represents a matrix.
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
    /// Represents a matrix.
    /// </summary>
    public partial class Matrix : IEquatable<Matrix>
    {
        
        /// <summary>
        /// Gets the number of rows of the matrix.
        /// </summary>
        /// <value>The number of rows of the matrix.</value>
        public int RowCount { get { return _elements.GetLength(0); } }

        /// <summary>
        /// Gets the number of columns of the matrix.
        /// </summary>
        /// <value>The number of columns of the matrix.</value>
        public int ColumnCount { get { return _elements.GetLength(1); } }

        /// <summary>
        /// Gets the element at the specified row and column.
        /// </summary>
        /// <param name="row">The one-based index of row of the element to get.</param>
        /// <param name="column">The one-based index of column of the element to get.</param>
        /// <value>The element at the specified row and column.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        /// </exception>
        [System.Runtime.CompilerServices.IndexerName("Element")]
        public Expression this[int row, int column]
        {   // one-based index
            get 
            {
                if (row <= RowCount && row > 0 && column <= ColumnCount && column > 0)
                {
                    return _elements[row - 1, column - 1];
                }
                else
                    throw new ArgumentException(ExceptionResource.RowColumnOutOfRange);
            }
        }
        

        #region Conversion

        /// <summary>
        /// Creates a matrix from a two-dimensional array of <see cref="Expression"/>.
        /// </summary>
        /// <param name="elements">The array whose elements are copied to the new <see cref="Matrix"/> as the elements.</param>
        /// <returns>The <see cref="Matrix"/> object containing <paramref name="elements"/>.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="elements"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="elements"/> is empty.</exception>
        public static Matrix FromArray(Expression[,] elements)
        {
            if (elements == null)
                throw new ArgumentNullException();            
            if (elements.GetLength(0) > 1 && elements.GetLength(1) > 1)
            {
                Expression[,] r = new Expression[elements.GetLength(0), elements.GetLength(1)];
                for (int i = 0; i < elements.GetLength(0); i++)
                {
                    for (int j = 0; j < elements.GetLength(1); j++)
                    {
                        if (elements[i, j] != null)
                            r[i, j] = elements[i, j];
                        else
                            throw new ArgumentNullException();        
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix); 
        }

        /// <summary>
        /// Defines an explicit conversion of a two-dimensional array of <see cref="Expression"/> to a <see cref="Matrix"/> object.
        /// </summary>
        /// <param name="value">The array whose elements are copied to the new <see cref="Matrix"/> as the elements.</param>
        /// <returns>The <see cref="Matrix"/> object containing <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>, or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is empty.</exception>
        public static explicit operator Matrix(Expression[,] value)
        {
            return FromArray(value);
        }

        /// <summary>
        /// Returns the string representation of the matrix.
        /// </summary>
        /// <returns>The string representation of the matrix.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
            for (int i = 0; i < RowCount;i++ )
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    sb.Append(_elements[i, j].ToString());

                    if (j == ColumnCount - 1)
                    {
                        if (i == RowCount - 1)
                            break;
                        else
                            sb.Append(";\n  ");
                    }
                    else
                        sb.Append(", ");
                }
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
        /// Determines whether this instance and a specified object, which must also be an <see cref="Matrix"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Matrix"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix)
                return Equals((Matrix)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Matrix"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Matrix"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Matrix value)
        {
            if (value.IsNull())
                return false;
            return Util.ItemEqual(this._elements, value._elements);
        }

        /// <summary>
        /// Determines whether two specified <see cref="Matrix"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Matrix"/> object to compare.</param>
        /// <param name="right">The second <see cref="Matrix"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Matrix left, Matrix right)
        {
            return left == right;
        }

        /// <summary>
        /// Determines whether two specified <see cref="Matrix"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Matrix"/> object to compare.</param>
        /// <param name="right">The second <see cref="Matrix"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Matrix left, Matrix right)
        {
            if (left.IsNull())
                if (right.IsNull())
                    return true;
                else
                    return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="Matrix"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Matrix"/> object to compare.</param>
        /// <param name="right">The second <see cref="Matrix"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Matrix left, Matrix right)
        {
            return !(left == right);
        }

        #endregion


        #region Non-public

        internal Matrix(Expression[,] elements)
        {
             _elements = elements;           
        }

        private Expression[,] _elements;       

        #endregion
    }
}
