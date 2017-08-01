/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Matrix
 * Description : Provides elementary arithmetics of matrices.
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
        #region Unary

        /// <summary>
        /// Returns the value of the <see cref="Matrix"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">The matrix.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix operator +(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            return value;
        }

        /// <summary>
        /// Negates the <see cref="Matrix"/> by multiplying all its values by -1.
        /// </summary>
        /// <param name="value">The matrix to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix operator -(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            return new Matrix(value._elements.
                Map((Expression expr) => -expr));
        }

        #endregion


        #region Add and Subtract

        /// <summary>
        /// Adds each element in one matrix with its corresponding element in a second matrix.
        /// </summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix  to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="RowCount"/> and <see cref="ColumnCount"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Matrix operator +(Matrix left, Matrix right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            if (IsHomotype(left, right))
            {
                Expression[,] r = new Expression[left._elements.GetLength(0), left._elements.GetLength(1)];
                for (int i = 0; i < left._elements.GetLength(0); i++)
                {
                    for (int j = 0; j < left._elements.GetLength(1); j++)
                    {
                        r[i, j] = left._elements[i, j] + right._elements[i, j];
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentException(ExceptionResource.AddOrSubtractNotHomotype);
        }

        /// <summary>
        /// Subtracts each element in a second matrix from its corresponding element in a first matrix.
        /// </summary>
        /// <param name="left">The first matrix to add.</param>
        /// <param name="right">The second matrix  to add.</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="RowCount"/> and <see cref="ColumnCount"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            if (IsHomotype(left, right))
            {
                Expression[,] r = new Expression[left._elements.GetLength(0), left._elements.GetLength(1)];
                for (int i = 0; i < left._elements.GetLength(0); i++)
                {
                    for (int j = 0; j < left._elements.GetLength(1); j++)
                    {
                        r[i, j] = left._elements[i, j] - right._elements[i, j];
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentException(ExceptionResource.AddOrSubtractNotHomotype);
        }

        #endregion


        #region Multiply and Divide

        /// <summary>
        /// Returns the matrix that results from multiplying two matrices together.
        /// </summary>
        /// <param name="left">The first matrix to multiply.</param>
        /// <param name="right">The second matrix to multiply.</param>
        /// <returns>The product matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="ColumnCount"/> of <paramref name="left"/> and the <see cref="RowCount"/> of <paramref name="right"/> don't match.</exception>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            if (left.ColumnCount == right.RowCount)
            {
                Expression[,] r = new Expression[left._elements.GetLength(0), right._elements.GetLength(1)];
                for (int i = 0; i < left._elements.GetLength(0); i++)
                {
                    for (int j = 0; j < right._elements.GetLength(1); j++)
                    {
                        r[i, j] = Number.Zero;
                        for (int k = 0; k < left._elements.GetLength(1);k++ )
                            r[i, j] += left._elements[i, k] * right._elements[k, j];
                    }
                }
                return new Matrix(r);
            }
            else
                throw new ArgumentException(ExceptionResource.MutiplyRowColumnNotMatch);
        }

        /// <summary>
        /// Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.
        /// </summary>
        /// <param name="left">The scaling value to use.</param>
        /// <param name="right">The matrix to scale.</param>
        /// <returns>The scaled matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix operator *(Expression left, Matrix right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            return new Matrix(right._elements.Map((Expression expr) => left * expr));
        }

        /// <summary>
        /// Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The scaling value to use.</param>
        /// <returns>The scaled matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix operator *(Matrix left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            return new Matrix(left._elements.Map((Expression expr) => expr * right));
        }

        /// <summary>
        /// Multiplies the first matrix by the inverse of the second matrix.
        /// </summary>
        /// <param name="left">The first matrix.</param>
        /// <param name="right">The second matrix.</param>
        /// <returns>The product matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        ///   <para><paramref name="right"/> is not a square matrix, or it is a singular matrix.</para>
        ///   <para>-or-</para>
        ///   <para>The <see cref="ColumnCount"/> of <paramref name="left"/> doesn't match the <see cref="RowCount"/> or <see cref="ColumnCount"/> of <paramref name="right"/>.</para>
        /// </exception>
        public static Matrix operator /(Matrix left, Matrix right)
        {
            return left * Inverse(right);
        }

        /// <summary>
        /// Returns the matrix that results from dividing all the elements of a specified matrix by a scalar factor.
        /// </summary>
        /// <param name="left">The matrix to scale.</param>
        /// <param name="right">The scalar factor to use.</param>
        /// <returns>The scaled matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix operator /(Matrix left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();     
            return new Matrix(left._elements.Map((Expression expr) => expr / right));
        }

        #endregion


        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="value">The matrix to transpose.</param>       
        /// <returns>The transposed matrix.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Matrix Transpose(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException();
            Expression[,] r = new Expression[value._elements.GetLength(1), value._elements.GetLength(0)];
            for (int i = 0; i < value._elements.GetLength(0); i++)
            {
                for (int j = 0; j < value._elements.GetLength(1); j++)
                {
                    r[j, i] = value._elements[i, j];
                }
            }
            return new Matrix(r);
        }


        #region Elementary Transformation

        /// <summary>
        /// Exchanges two rows in the matrix.
        /// </summary>
        /// <param name="matrix">The matrix to transform.</param>
        /// <param name="row1">The one-based index of the first row.</param>
        /// <param name="row2">The one-based index of the second row.</param>
        /// <returns>The transformed matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="row1"/> or <paramref name="row2"/> is less than 1 or greater that <see cref="RowCount"/>.</exception>
        public static Matrix ExchangeRows(Matrix matrix, int row1, int row2)
        {
            if (matrix == null)
                throw new ArgumentNullException(); 
            if (row1 <= matrix.RowCount && row1 > 0 &&
                row2 <= matrix.RowCount && row2 > 0)
            {
                Expression[,] r = (Expression[,])matrix._elements.Clone();
                ExchangeRows(r, row1 - 1, row2 - 1);
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Multiplies a row in the matrix by a scalar factor.
        /// </summary>
        /// <param name="matrix">The matrix to transform.</param>
        /// <param name="row">The one-based index of the row.</param>
        /// <param name="multiplier">The scalar factor to use.</param>
        /// <returns>The transformed matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="row"/> or <paramref name="row"/> is less than 1 or greater that <see cref="RowCount"/>.</exception>
        public static Matrix RowMultiply(Matrix matrix, int row, Expression multiplier)
        {
            if (matrix == null)
                throw new ArgumentNullException(); 
            if (row <= matrix.RowCount && row > 0)
            {
                Expression[,] r = (Expression[,])matrix._elements.Clone();
                RowMultiply(r, row - 1, multiplier);
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        /// <summary>
        /// Multiplies a row in the matrix by a scalar factor, and adds the result to another row.
        /// </summary>
        /// <param name="matrix">The matrix to transform.</param>
        /// <param name="row1">The one-based index of the first row.</param>
        /// <param name="row2">The one-based index of the second row.</param>
        /// <param name="multiplier">The scalar factor to use.</param>
        /// <returns>The transformed matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="row1"/> or <paramref name="row2"/> is less than 1 or greater that <see cref="RowCount"/>.</exception>
        public static Matrix RowMultiplyAdd(Matrix matrix, int row1, int row2, Expression multiplier)
        {
            if (matrix == null)
                throw new ArgumentNullException(); 
            if (row1 <= matrix.RowCount && row1 > 0 &&
                row2 <= matrix.RowCount && row2 > 0)
            {
                Expression[,] r = (Expression[,])matrix._elements.Clone();
                RowMultiplyAdd(r, row1 - 1, row2 - 1, multiplier);
                return new Matrix(r);
            }
            else
                throw new ArgumentOutOfRangeException(ExceptionResource.RowColumnOutOfRange, innerException: null);
        }

        #region Private

        private static void ExchangeRows(Expression[,] elements, int row1, int row2)
        {
            Expression t;
            for (int i = 0; i < elements.GetLength(1); i++)
            {
                t = elements[row1, i];
                elements[row1, i] = elements[row2, i];
                elements[row2, i] = t;
            }
        }

        private static void RowMultiply(Expression[,] elements, int row, Expression multiplier)
        {
            for (int i = 0; i < elements.GetLength(1); i++)
            {
                elements[row, i] *= multiplier;
            }
        }

        private static void RowMultiplyAdd(Expression[,] elements, int row1, int row2, Expression multiplier)
        {
            for (int i = 0; i < elements.GetLength(1); i++)
            {
                elements[row2, i] += elements[row1, i] * multiplier;
            }
        }


        #endregion

        #endregion


        #region Echelon

        /// <summary>
        /// Transform the matrix to a row echelon form.
        /// </summary>
        /// <param name="value">The matrix to transform.</param>
        /// <returns>The transformed matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Matrix RowEchelon(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            Expression[,] r = (Expression[,])value._elements.Clone();
            int row = 0;
            for (int i = 0; i < value.ColumnCount; i++)
            {
                int notZeroRow = FindNotZeroElement(r, i, row);
                if (notZeroRow == -1)
                    continue;
                else if (notZeroRow != 0)
                    ExchangeRows(r, row, notZeroRow);
                EliminateNotZeroElement(r, i, row, row + 1);
                row++;
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Transform the matrix to a row simplest form.
        /// </summary>
        /// <param name="value">The matrix to transform.</param>
        /// <returns>The transformed matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static Matrix RowSimplest(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            Expression[,] r = (Expression[,])value._elements.Clone();
            int row = 0;
            for (int i = 0; i < value.ColumnCount; i++)
            {
                int notZeroRow = FindNotZeroElement(r, i, row);
                if (notZeroRow == -1)
                    continue;
                else if (notZeroRow != 0)
                    ExchangeRows(r, row, notZeroRow);
                RowMultiply(r, row, Number.One / r[row, i]);
                EliminateNotZeroElement(r, i, row, 0);
                row++;
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Returns the rank of the specified matrix.
        /// </summary>
        /// <param name="value">The matrix.</param>       
        /// <returns>The rank of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static int Rank(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            Expression[,] e = RowEchelon(value)._elements;
            int rank = 0;
            bool isZeroRow;
            for (int i = 0; i < e.GetLength(0); i++)
            {
                isZeroRow = true;
                for (int j = 0; j < e.GetLength(1); j++)
                {
                    if (e[i, j] != Number.Zero)
                    {
                        isZeroRow = false;
                        break;
                    }
                }
                if (isZeroRow)
                    break;
                else
                    rank++;
            }
            return rank;
        }

        #region Private

        private static int FindNotZeroElement(Expression[,] elements, int column, int row)
        {
            for (int j = row; j < elements.GetLength(0); j++)
            {
                if (elements[j, column] != Number.Zero)
                    return j;
            }
            return -1;
        }
        private static void EliminateNotZeroElement(Expression[,] elements, int column, int row, int startRow)
        {
            Expression multiplier;
            for (int j = startRow; j < elements.GetLength(0); j++)
            {
                if (j == row)
                    continue;
                multiplier = -elements[j, column] / elements[row, column];
                RowMultiplyAdd(elements, row, j, multiplier);
            }          
        }

		#endregion

        #endregion


        /// <summary>
        /// Inverts the specified matrix.
        /// </summary>
        /// <param name="value">The matrix to invert.</param>       
        /// <returns>The inverted matrix.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a square matrix, or it is a singular matrix.</exception>
        public static Matrix Inverse(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            if (!value.IsSquare)
            {
                throw new ArgumentException(ExceptionResource.InverseNotSquare);
            }
            Matrix ret = Matrix.Identity(value.RowCount);
            Expression[,] e = (Expression[,])value._elements.Clone();
            Expression[,] r = ret._elements;
            int row = 0;
            for (int i = 0; i < value.ColumnCount; i++)
            {
                int notZeroRow = FindNotZeroElement(e, i, row);
                if (notZeroRow == -1)
                    throw new ArgumentException(ExceptionResource.InverseNotFullRank);
                else if (notZeroRow != 0)
                {
                    ExchangeRows(e, row, notZeroRow);
                    ExchangeRows(r, row, notZeroRow);
                }
                Expression multiplier = Number.One / e[row, i];
                RowMultiply(e, row, multiplier);
                RowMultiply(r, row, multiplier);
                for (int j = 0; j < e.GetLength(0); j++)
                {
                    if (j == row)
                        continue;
                    multiplier = -e[j, i] / e[row, i];
                    RowMultiplyAdd(e, row, j, multiplier);
                    RowMultiplyAdd(r, row, j, multiplier);
                }   
                row++;
            }
            return new Matrix(r);
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <param name="value">The matrix.</param>       
        /// <returns>The determinant of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a square matrix.</exception>
        public static Expression Determinant(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            if (!value.IsSquare)
            {
                throw new ArgumentException(ExceptionResource.DeterminantNotSquare);
            }
            Expression[,] e = (Expression[,])value._elements.Clone();
            bool sign = true;
            int row = 0;
            for (int i = 0; i < value.ColumnCount; i++)
            {
                int notZeroRow = FindNotZeroElement(e, i, row);
                if (notZeroRow == -1)
                    return Number.Zero;
                else if (notZeroRow != 0)
                {        
                    ExchangeRows(e, row, notZeroRow);
                    sign = !sign;
                }                
                EliminateNotZeroElement(e, i, row, row + 1);
                row++;
            }
            Expression r = Number.One;
            for (int i = 0; i < e.GetLength(0); i++)
                r *= e[i, i];
            if (!sign)
                r = -r;
            return r;
        }

        /// <summary>
        /// Returns the minor of the matrix by removing one row and one column.
        /// </summary>
        /// <param name="matrix">The matrix.</param>       
        /// <param name="row">The one-based index of the row to be removed.</param>
        /// <param name="column">The one-based index of the dolumn to be removed.</param>
        /// <returns>The minor of <paramref name="matrix"/> by removing the <paramref name="row"/>th row and the <paramref name="column"/>th column.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="matrix"/> is not a square matrix.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        /// </exception>
        public static Expression Minor(Matrix matrix, int row, int column)
        {
            if (matrix == null)
                throw new ArgumentNullException();
            if (!matrix.IsSquare)
            {
                throw new ArgumentException(ExceptionResource.DeterminantNotSquare);
            }
            return Determinant(matrix.Submatrix(row,column));
        }

        /// <summary>
        /// Returns the cofactor of the matrix by removing one row and one column.
        /// </summary>
        /// <param name="matrix">The matrix.</param>       
        /// <param name="row">The one-based index of the row to be removed.</param>
        /// <param name="column">The one-based index of the dolumn to be removed.</param>
        /// <returns>The cofactor of <paramref name="matrix"/> by removing the <paramref name="row"/>th row and the <paramref name="column"/>th column.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="matrix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="matrix"/> is not a square matrix.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   <para><paramref name="row"/> is less than 1 or greater than <see cref="RowCount"/>.</para>
        ///   <para>-or-</para>
        ///   <para><paramref name="column"/> is less than 1 or greater than <see cref="ColumnCount"/>.</para>
        /// </exception>
       public static Expression Cofactor(Matrix matrix, int row, int column)
        {
            if ((row + column) % 2 == 0)
                return Minor(matrix, row, column);
            else
                return -Minor(matrix, row, column);
        }

        /// <summary>
        /// Returns the trace of the matrix.
        /// </summary>
        /// <param name="value">The matrix.</param>       
        /// <returns>The trace of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is not a square matrix, or it is a singular matrix.</exception>
        public static Expression Trace(Matrix value)
        {
            if (value == null)
                throw new ArgumentNullException(); 
            if (!value.IsSquare)
            {
                throw new ArgumentException(ExceptionResource.TraceNotSquare);
            }
            Expression r = Number.Zero;
            for (int i = 0; i < value._elements.GetLength(0); i++)
                r += value._elements[i, i];           
            return r;
        }
    }
}
