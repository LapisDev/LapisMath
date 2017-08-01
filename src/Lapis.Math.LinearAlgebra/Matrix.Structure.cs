/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Matrix
 * Description : Provides methods related to the structures of matrix.
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
    public partial class Matrix
    {
        #region Judgement

        /// <summary>
        /// Returns a value indicating whether the two specified matrices are homotype matrices.
        /// </summary>
        /// <param name="x">The first matrix.</param>
        /// <param name="y">The second matrix.</param>
        /// <returns><see langword="true"/> if <paramref name="x"/> and <paramref name="y"/> are homotype matrices; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsHomotype(Matrix x, Matrix y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            return x.RowCount == y.RowCount && x.ColumnCount == y.ColumnCount;
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is a square.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is a square matrix; otherwise, <see langword="false"/>.</value>
        public bool IsSquare
        {
            get { return RowCount == ColumnCount; }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is symmetric.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is symmetric; otherwise, <see langword="false"/>.</value>
        public bool IsSymmetric
        {
            get
            {
                if (!IsSquare)
                    return false;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = i + 1; j < ColumnCount; j++)
                    {
                        if (_elements[i, j] != _elements[j, i])
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is an upper triangle matrix.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is an upper triangle matrix; otherwise, <see langword="false"/>.</value>
        public bool IsUpperTriangle
        {
            get
            {
                if (!IsSquare)
                    return false;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (_elements[i, j] != Number.Zero)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is a lower triangle matrix.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is a lower triangle matrix; otherwise, <see langword="false"/>.</value>
        public bool IsLowerTriangle
        {
            get
            {
                if (!IsSquare)
                    return false;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = i + 1; j < ColumnCount; j++)
                    {
                        if (_elements[i, j] != Number.Zero)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is a diagonal matrix.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is a diagonal matrix; otherwise, <see langword="false"/>.</value>
        public bool IsDiagonal
        {
            get
            {
                if (!IsSquare)
                    return false;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (i != j && _elements[i, j] != Number.Zero)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the matrix is an identity matrix.
        /// </summary>
        /// <value><see langword="true"/> if the matrix is an identity matrix; otherwise, <see langword="false"/>.</value>
        public bool IsIdentity
        {
            get
            {
                if (!IsSquare)
                    return false;
                for (int i = 0; i < RowCount; i++)
                {
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        if (i != j && _elements[i, j] != Number.Zero)
                            return false;
                        if (i == j && _elements[i, j] != Number.One)
                            return false;
                    }
                }
                return true;
            }
        }

        #endregion


        #region Decompose

        /// <summary>
        /// Returns the row at the specified index.
        /// </summary>
        /// <param name="row">The one-based index of the row to get.</param>
        /// <returns>The <paramref name="row"/>th row.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</exception>
        public Vector Row(int row)
        {
            if (row <= RowCount && row > 0)
            {
                Expression[] r = new Expression[ColumnCount];
                for (int i = 0; i < ColumnCount; i++)
                {
                    r[i] = _elements[row - 1, i];
                }
                return new Vector(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Returns the column at the specified index.
        /// </summary>
        /// <param name="column">The one-based index of the column get.</param>
        /// <returns>The <paramref name="column"/>th column.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</exception>
        public Vector Column(int column)
        {
            if (column <= ColumnCount && column > 0)
            {
                Expression[] c = new Expression[RowCount];
                for (int i = 0; i < RowCount; i++)
                {
                    c[i] = _elements[i, column - 1];
                }
                return new Vector(c);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Returns the upper triangular part of the matrix.
        /// </summary>
        /// <returns>The upper triangular part of the matrix.</returns>
        /// <exception cref="ArgumentException">The matrix is not a square matrix.</exception>
        public Matrix UpperTriangle()
        {
            if (!IsSquare)
                throw new ArgumentException(ExceptionResource.NotSquare);
            Expression[,] r = new Expression[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    if (j >= i)
                        r[i, j] = _elements[i, j];
                    else
                        r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Returns the lower triangular part of the matrix.
        /// </summary>
        /// <returns>The lower triangular part of the matrix.</returns>
        /// <exception cref="ArgumentException">The matrix is not a square matrix.</exception>
        public Matrix LowerTriangle()
        {
            if (!IsSquare)
                throw new ArgumentException(ExceptionResource.NotSquare);
            Expression[,] r = new Expression[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    if (j <= i)
                        r[i, j] = _elements[i, j];
                    else
                        r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Returns the diagonal part of the matrix.
        /// </summary>
        /// <returns>The diagonal part of the matrix.</returns>
        /// <exception cref="ArgumentException">The matrix is not a square matrix.</exception>
        public Matrix Diagonal()
        {
            if (!IsSquare)
                throw new ArgumentException(ExceptionResource.NotSquare);
            Expression[,] r = new Expression[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    if (j == i)
                        r[i, j] = _elements[i, j];
                    else
                        r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Returns the sub-matrix of the matrix with the specified rows and columns.
        /// </summary>
        /// <param name="rowStart">The one-based index of the first row.</param>
        /// <param name="rowEnd">The one-based index of the last row.</param>
        /// <param name="columnStart">The one-based index of the first column.</param>
        /// <param name="columnEnd">The one-based index of the last column.</param>
        /// <returns>The sub-matrix consisted of <paramref name="rowStart"/>-<paramref name="rowEnd"/>th rows and <paramref name="columnStart"/>-<paramref name="columnEnd"/>th columns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="rowStart"/> or <paramref name="rowEnd"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="columnStart"/> or <paramref name="columnEnd"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="rowStart"/> is greater than <paramref name="rowEnd"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="columnStart"/> is greater than <paramref name="columnEnd"/></para>
        /// </exception>
        public Matrix Block(int rowStart, int rowEnd, int columnStart, int columnEnd)
        {
            if (rowStart > 0 && rowStart <= RowCount &&
                rowEnd > 0 && rowEnd <= RowCount &&
                columnStart > 0 && columnStart <= RowCount &&
                columnEnd > 0 && columnEnd <= RowCount &&
                rowStart <= rowEnd && columnStart <= columnEnd)
            {
                Expression[,] r = new Expression[rowEnd - rowStart, columnEnd - columnStart];
                for (var i = rowStart; i < rowEnd; i++)
                {
                    for (var j = columnStart; j < columnEnd; j++)
                    {
                        r[i - rowStart, j - columnStart] = _elements[i - 1, j - 1];
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Returns the sub-matrix by removing one row and one column.
        /// </summary>     
        /// <param name="row">The one-based index of the row to be removed.</param>
        /// <param name="column">The one-based index of the dolumn to be removed.</param>
        /// <returns>The sub-matrix by removing the <paramref name="row"/>th row and the <paramref name="column"/>th column.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        /// </exception>
        public Matrix Submatrix(int row, int column)
        {
            return RemoveRow(row).RemoveColumn(column);
        }

        #endregion


        #region Construction

        /// <summary>
        /// Creates a zero matrix with the specified number of rows and columns.
        /// </summary>
        /// <param name="row">The number of rows.</param>  
        /// <param name="column">The number of columns.</param>
        /// <returns>A zero matrix with <paramref name="row"/> rows and <paramref name="column"/> columns.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The parameter is zero or negative.</exception>
        public static Matrix Zero(int row, int column)
        {
            if (row <= 0 || column <= 0)
                throw new ArgumentOutOfRangeException(ExceptionResource.InvalidRowColumnCountOfMatrix, innerException: null);
            Expression[,] r = new Expression[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Creates an identity matrix with the specified order.
        /// </summary>
        /// <param name="order">An integer specifying the order.</param>  
        /// <returns>An <paramref name="order"/>th-order identity matrix。</returns>
        /// <exception cref="ArgumentOutOfRangeException">The parameter is zero or negative.</exception>
        public static Matrix Identity(int order)
        {
            if (order <= 0)
                throw new ArgumentOutOfRangeException(ExceptionResource.InvalidRowColumnCountOfMatrix, innerException: null);
            Expression[,] r = new Expression[order, order];
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (i == j)
                        r[i, j] = Number.One;
                    else
                        r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Creates a diagonal matrix using the specified <see cref="IEnumerable{Expression}"/> containing the elements.
        /// </summary>
        /// <param name="diagonal">The collection whose elements are copied to the new matrix as diagonal elements.</param>  
        /// <returns>A diagonal matrix whose elements are <paramref name="diagonal"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="diagonal"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="diagonal"/> is empty.</exception>
        public static Matrix Diagonal(IEnumerable<Expression> diagonal)
        {
            if (diagonal == null)
                throw new ArgumentNullException();
            var order = diagonal.Count();
            if (order <= 0)
                throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix);
            Expression[,] r = new Expression[order, order];
            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (i == j)
                    {
                        var ele = diagonal.ElementAt(i);
                        if (ele == null)
                            throw new ArgumentNullException();
                        r[i, j] = diagonal.ElementAt(i);
                    }
                    else
                        r[i, j] = Number.Zero;
                }
            }
            return new Matrix(r);
        }
        /// <summary>
        /// Creates a diagonal matrix using the specified <see cref="Expression"/> array containing the elements.
        /// </summary>
        /// <param name="diagonal">The array whose elements are copied to the new matrix as diagonal elements.</param>  
        /// <returns>A diagonal matrix whose elements are <paramref name="diagonal"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="diagonal"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="diagonal"/> is empty.</exception>
        public static Matrix Diagonal(params Expression[] diagonal)
        {
            return Diagonal((IEnumerable<Expression>)diagonal);
        }

        /// <summary>
        /// Creates a matrix using the specified <see cref="IEnumerable{Vector}"/> containing the rows.
        /// </summary>
        /// <param name="rows">The collection whose elements are copied to the new matrix as rows.</param>  
        /// <returns>A matrix whose rows are <paramref name="rows"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="rows"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        ///   <para><paramref name="rows"/> is empty.</para>
        ///   <para>-or-</para>
        ///   <para>The <see cref="Vector.Dimension"/> of the row vectors are not equal.</para>
        /// </exception>
        public static Matrix FromRows(IEnumerable<Vector> rows)
        {
            if (rows == null)
                throw new ArgumentNullException();
            var row = rows.Count();
            if (row <= 0)
                throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix);
            int column = rows.ElementAt(0).Dimension;
            Expression[,] r = new Expression[row, column];
            for (int i = 0; i < rows.Count(); i++)
            {
                if (rows.ElementAt(i) == null)
                    throw new ArgumentNullException();
                if (rows.ElementAt(i).Dimension == 0)
                    throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix);
                if (rows.ElementAt(i).Dimension != column)
                    throw new ArgumentException(ExceptionResource.RowVectorDimensionNotEqual);
                for (int j = 0; j < rows.ElementAt(i).Dimension; j++)
                {
                    r[i, j] = rows.ElementAt(i).ElementAt(j);
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Creates a matrix using the specified <see cref="Vector"/> array containing the rows.
        /// </summary>
        /// <param name="rows">The array whose elements are copied to the new matrix as rows.</param>  
        /// <returns>A matrix whose rows are <paramref name="rows"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="rows"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        ///   <para><paramref name="rows"/> is empty.</para>
        ///   <para>-or-</para>
        ///   <para>The <see cref="Vector.Dimension"/> of the row vectors are not equal.</para>
        /// </exception>
        public static Matrix FromRows(params Vector[] rows)
        {
            return FromRows((IEnumerable<Vector>)rows);
        }

        /// <summary>
        /// Creates a matrix using the specified <see cref="IEnumerable{Vector}"/> containing the columns.
        /// </summary>
        /// <param name="columns">The collection whose elements are copied to the new matrix as columns.</param>  
        /// <returns>A matrix whose columns are <paramref name="columns"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columns"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        ///   <para><paramref name="columns"/> is empty.</para>
        ///   <para>-or-</para>
        ///   <para>The <see cref="Vector.Dimension"/> of the column vectors are not equal.</para>
        /// </exception>
        public static Matrix FromColumns(IEnumerable<Vector> columns)
        {
            if (columns == null)
                throw new ArgumentNullException();
            var column = columns.Count();
            if (column <= 0)
                throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix);
            int row = columns.ElementAt(0).Dimension;
            Expression[,] r = new Expression[row, column];
            for (int j = 0; j < columns.Count(); j++)
            {
                if (columns.ElementAt(j) == null)
                    throw new ArgumentNullException();
                if (columns.ElementAt(j).Dimension == 0)
                    throw new ArgumentException(ExceptionResource.InvalidRowColumnCountOfMatrix);
                if (columns.ElementAt(j).Dimension != row)
                    throw new ArgumentException(ExceptionResource.ColumnVectorDimensionNotEqual);
                for (int i = 0; i < columns.ElementAt(j).Dimension; i++)
                {
                    r[i, j] = columns.ElementAt(j).ElementAt(i);
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Creates a matrix using the specified <see cref="Vector"/> array containing the columns.
        /// </summary>
        /// <param name="columns">The array whose elements are copied to the new matrix as columns.</param>  
        /// <returns>A matrix whose columns are <paramref name="columns"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="columns"/> is <see langword="null"/> or contains <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        ///   <para><paramref name="columns"/> is empty.</para>
        ///   <para>-or-</para>
        ///   <para>The <see cref="Vector.Dimension"/> of the column vectors are not equal.</para>
        /// </exception>
        public static Matrix FromColumns(params Vector[] columns)
        {
            return FromColumns((IEnumerable<Vector>)columns);
        }

        /// <summary>
        /// Replaces the element at the specified row and column with the new element and returns the result.
        /// </summary>
        /// <param name="row">The one-based index of row of the element.</param>
        /// <param name="column">The one-based index of column of the element.</param>
        /// <param name="element">The element to replace the old element.</param>      
        /// <returns>A matrix that is equivalent to the current matrix except that the element at the <paramref name="row"/>th row and the <paramref name="column"/>th column is replaced with <paramref name="element"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="element"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        /// </exception>
        public Matrix Replace(int row, int column, Expression element)
        {
            if (element == null)
                throw new ArgumentNullException();
            if (row <= RowCount && row > 0 && column <= ColumnCount && column > 0)
            {
                Expression[,] r = (Expression[,])_elements.Clone();
                r[row - 1, column - 1] = element;
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Replaces the sub-matrix at the specified rows and columns with the new sub-matrix and returns the result.
        /// </summary>
        /// <param name="row">The one-based index of the first row.</param>
        /// <param name="column">The one-based index of the first column.</param>
        /// <param name="block">The sub-matrix to replace the old sub-matrix.</param>
        /// <returns>A matrix that is equivalent to the current matrix except that the elements starting at the <paramref name="row"/>th row and the <paramref name="column"/>th column are replaced with <paramref name="block"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="block"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para>The sub-matrix is out of the edge of the matrix.</para>
        /// </exception>
        public Matrix Replace(int row, int column, Matrix block)
        {
            if (block == null)
                throw new ArgumentNullException();
            if (row <= RowCount && row > 0 && column <= ColumnCount && column > 0 &&
                row + block.RowCount - 1 <= RowCount && column + block.ColumnCount - 1 <= ColumnCount)
            {
                Expression[,] r = (Expression[,])_elements.Clone();
                for (int i = 0; i < block.RowCount; i++)
                {
                    for (int j = 0; j < block.ColumnCount; j++)
                    {
                        r[row + i - 1, column + j - 1] = block._elements[i, j];
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Inserts a row at the specified index and returns the result.
        /// </summary>
        /// <param name="rowIndex">The one-based index of the row.</param>
        /// <param name="row">The row to insert.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="row"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Vector.Dimension"/> of <paramref name="row"/> doesn't match <see cref="ColumnCount"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 1 or greater than <see cref="RowCount"/>.</exception>
        public Matrix InsertRow(int rowIndex, Vector row)
        {
            if (row == null)
                throw new ArgumentNullException();
            if (rowIndex < 0 || rowIndex > RowCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            if (row.Dimension != ColumnCount)
                throw new ArgumentException(ExceptionResource.RowVectorDimensionNotEqual);
            Expression[,] r = new Expression[RowCount + 1, ColumnCount];
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < rowIndex - 1; i++)
                {
                    r[i, j] = _elements[i, j];
                }
                r[rowIndex - 1, j] = row.ElementAt(j);
                for (int i = rowIndex - 1; i < RowCount; i++)
                {
                    r[i + 1, j] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Inserts a column at the specified index and returns the result.
        /// </summary>
        /// <param name="columnIndex">The one-based index of the column.</param>
        /// <param name="column">The column to insert.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="column"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Vector.Dimension"/> of <paramref name="column"/> doesn't match <see cref="RowCount"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 1 or greater than <see cref="ColumnCount"/>.</exception>
        public Matrix InsertColumn(int columnIndex, Vector column)
        {
            if (column == null)
                throw new ArgumentNullException();
            if (columnIndex < 0 || columnIndex > ColumnCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            if (column.Dimension != RowCount)
                throw new ArgumentException(ExceptionResource.ColumnVectorDimensionNotEqual);
            Expression[,] r = new Expression[RowCount, ColumnCount + 1];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < columnIndex - 1; j++)
                {
                    r[i, j] = _elements[i, j];
                }
                r[i, columnIndex - 1] = column.ElementAt(i);
                for (int j = columnIndex - 1; j < ColumnCount; i++)
                {
                    r[i, j + 1] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }
        
        /// <summary>
        /// Removes a row at the specified index and returns the result.
        /// </summary>
        /// <param name="rowIndex">The one-based index of the row.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 1 or greater than <see cref="RowCount"/>.</exception>
        public Matrix RemoveRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > RowCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            Expression[,] r = new Expression[RowCount - 1, ColumnCount];
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < rowIndex - 1; i++)
                {
                    r[i, j] = _elements[i, j];
                }
                for (int i = rowIndex; i < RowCount; i++)
                {
                    r[i - 1, j] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Removes a column at the specified index and returns the result.
        /// </summary>
        /// <param name="columnIndex">The one-based index of the column.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 1 or greater than <see cref="ColumnCount"/>.</exception>
        public Matrix RemoveColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > ColumnCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            Expression[,] r = new Expression[RowCount, ColumnCount - 1];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < columnIndex - 1; j++)
                {
                    r[i, j] = _elements[i, j];
                }
                for (int j = columnIndex; j < ColumnCount; i++)
                {
                    r[i, j - 1] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Replaces the elements at the specified row with the new row and returns the result.
        /// </summary>
        /// <param name="rowIndex">The one-based index of the row.</param>
        /// <param name="row">The row to replace the old elements.</param>
        /// <returns>A matrix that is equivalent to the current matrix except that the elements at the <paramref name="rowIndex"/>th row are replaced with <paramref name="row"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="row"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Vector.Dimension"/> of <paramref name="row"/> doesn't match <see cref="ColumnCount"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 1 or greater than <see cref="RowCount"/>.</exception>
        public Matrix ReplaceRow(int rowIndex, Vector row)
        {
            if (row == null)
                throw new ArgumentNullException();
            if (rowIndex < 0 || rowIndex > RowCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            if (row.Dimension != ColumnCount)
                throw new ArgumentException(ExceptionResource.RowVectorDimensionNotEqual);
            Expression[,] r = new Expression[RowCount + 1, ColumnCount];
            for (int j = 0; j < ColumnCount; j++)
            {
                for (int i = 0; i < rowIndex - 1; i++)
                {
                    r[i, j] = _elements[i, j];
                }
                r[rowIndex - 1, j] = row.ElementAt(j);
                for (int i = rowIndex; i < RowCount; i++)
                {
                    r[i, j] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Replaces the elements at the specified column with the new column and returns the result.
        /// </summary>
        /// <param name="columnIndex">The one-based index of the column.</param>
        /// <param name="column">The column to replace the old elements.</param>
        /// <returns>A matrix that is equivalent to the current matrix except that the elements at the <paramref name="columnIndex"/>th column are replaced with <paramref name="column"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="column"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Vector.Dimension"/> of <paramref name="column"/> doesn't match <see cref="RowCount"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 1 or greater than <see cref="ColumnCount"/>.</exception>
        public Matrix ReplaceColumn(int columnIndex, Vector column)
        {
            if (column == null)
                throw new ArgumentNullException();
            if (columnIndex < 0 || columnIndex > ColumnCount)
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
            if (column.Dimension != RowCount)
                throw new ArgumentException(ExceptionResource.ColumnVectorDimensionNotEqual);
            Expression[,] r = new Expression[RowCount, ColumnCount + 1];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < columnIndex - 1; j++)
                {
                    r[i, j] = _elements[i, j];
                }
                r[i, columnIndex - 1] = column.ElementAt(i);
                for (int j = columnIndex; j < ColumnCount; i++)
                {
                    r[i, j] = _elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Concatenates the two matrices horizontally and returns the result.
        /// </summary>
        /// <param name="left">The first matrix.</param>
        /// <param name="right">The second matrix.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="RowCount"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Matrix HorizonalConcat(Matrix left, Matrix right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.RowCount != right.RowCount)
                throw new ArgumentException(ExceptionResource.ColumnVectorDimensionNotEqual);
            Expression[,] r = new Expression[left.RowCount, left.ColumnCount + right.ColumnCount];
            for (int i = 0; i < left.RowCount; i++)
            {
                for (int j = 0; j < left.ColumnCount; j++)
                {
                    r[i, j] = left._elements[i, j];
                }
                for (int j = 0; j < right.ColumnCount; j++)
                {
                    r[i, j + left.ColumnCount] = right._elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Concatenates the two matrices vertically and returns the result.
        /// </summary>
        /// <param name="up">The first matrix.</param>
        /// <param name="down">The second matrix.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="ColumnCount"/> of <paramref name="up"/> and <paramref name="down"/> don't match.</exception>
        public static Matrix VerticalConcat(Matrix up, Matrix down)
        {
            if (up == null || down == null)
                throw new ArgumentNullException();
            if (up.ColumnCount != down.ColumnCount)
                throw new ArgumentException(ExceptionResource.RowVectorDimensionNotEqual);
            Expression[,] r = new Expression[up.RowCount + down.RowCount, up.ColumnCount];
            for (int j = 0; j < up.ColumnCount; j++)
            {
                for (int i = 0; i < up.RowCount; i++)
                {
                    r[i, j] = up._elements[i, j];
                }
                for (int i = 0; i < down.RowCount; i++)
                {
                    r[i + up.RowCount, j] = down._elements[i, j];
                }
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Concatenates the two matrices diagonally and returns the result.
        /// </summary>
        /// <param name="up">The first matrix.</param>
        /// <param name="down">The second matrix.</param>
        /// <returns>The result matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix DiagonalConcat(Matrix up, Matrix down)
        {
            if (up == null || down == null)
                throw new ArgumentNullException();
            Expression[,] r = new Expression[up.RowCount + down.RowCount, up.ColumnCount + down.ColumnCount];
            for (int j = 0; j < up.ColumnCount; j++)
            {
                for (int i = 0; i < up.RowCount; i++)
                {
                    r[i, j] = up._elements[i, j];
                }
                for (int i = 0; i < down.RowCount; i++)
                {
                    r[i + up.RowCount, j] = Number.Zero;
                }
            }
            for (int j = 0; j < down.ColumnCount; j++)
            {
                for (int i = 0; i < up.RowCount; i++)
                {
                    r[i, j + down.ColumnCount] = Number.Zero;
                }
                for (int i = 0; i < down.RowCount; i++)
                {
                    r[i + up.RowCount, j + down.ColumnCount] = down._elements[i, j];
                }
            }
            return new Matrix(r);
        }

        #endregion

        /// <summary>
        /// Maps each element in the matrix into a new element.
        /// </summary>
        /// <param name="func">A transform function to apply to each element.</param>
        /// <returns>A matrix whose elements are the result of invoking the transform function on each element in the current matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public Matrix Map(Func<Expression, Expression> func)
        {
            if (func == null)
                throw new ArgumentNullException();
            return FromArray(_elements.Map(func));
        }
    }
}
