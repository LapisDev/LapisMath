/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Vector
 * Description : Provides elementary arithmetics of vectors.
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
    public partial class Vector
    {
        #region Unary

        /// <summary>
        /// Returns the value of the <see cref="Vector"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">The vector.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Vector operator +(Vector value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        /// <summary>
        /// Negates the <see cref="Vector"/> by multiplying all its values by -1.
        /// </summary>
        /// <param name="value">The vector to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Vector operator -(Vector value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Vector(value._elements.
                Map((Expression expr) => -expr));
        }

        #endregion


        #region Add and Subtract

        /// <summary>
        /// Adds each element in one vector with its corresponding element in a second vector.
        /// </summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector  to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Dimension"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Vector operator +(Vector left, Vector right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Dimension == right.Dimension)
            {
                Expression[] r = new Expression[left.Dimension];
                for (int i = 0; i < left.Dimension; i++)
                {
                    r[i] = left._elements[i] + right._elements[i];
                }
                return new Vector(r);
            }
            else
                throw new ArgumentException(ExceptionResource.AddOrSubtractDimensionNotEqual);
        }

        /// <summary>
        /// Subtracts each element in a second vector from its corresponding element in a first vector.
        /// </summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Dimension"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Vector operator -(Vector left, Vector right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Dimension == right.Dimension)
            {
                Expression[] r = new Expression[left.Dimension];
                for (int i = 0; i < left.Dimension; i++)
                {
                    r[i] = left._elements[i] - right._elements[i];
                }
                return new Vector(r);
            }
            else
                throw new ArgumentException(ExceptionResource.AddOrSubtractDimensionNotEqual);
        }

        #endregion


        #region Multiply and Divide

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second product.</param>
        /// <returns>The dot product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Dimension"/> of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Expression operator *(Vector left, Vector right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (left.Dimension == right.Dimension)
            {
                Expression r = Number.Zero;
                for (int i = 0; i < left.Dimension; i++)
                {
                    r += left._elements[i] * right._elements[i];
                }
                return r;
            }
            else
                throw new ArgumentException(ExceptionResource.MultiplyDimensionNotEqual);
        }

        /// <summary>
        /// Returns the vector that results from scaling all the elements of a specified vector by a scalar factor.
        /// </summary>
        /// <param name="left">The scaling value to use.</param>
        /// <param name="right">The vector to scale.</param>
        /// <returns>The scaled vector.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Vector operator *(Expression left, Vector right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return new Vector(right._elements.Map(
                (Expression expr) => left * expr));
        }

        /// <summary>
        /// Returns the vector that results from scaling all the elements of a specified vector by a scalar factor.
        /// </summary>
        /// <param name="left">The vector to scale.</param>
        /// <param name="right">The scaling value to use.</param>
        /// <returns>The scaled vector.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Vector operator *(Vector left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return new Vector(left._elements.Map(
                (Expression expr) => expr * right));
        }

        #endregion

        /// <summary>
        /// Returns the modulus of the specified vector.
        /// </summary>
        /// <param name="value">The vector</param>
        /// <returns>The modulus of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Norm(Vector value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return Expression.Pow(value * value, Number.FromDouble(0.5));
        }

        /// <summary>
        /// Returns the cosine of the angle of the two specified vectors.
        /// </summary>
        /// <param name="x">The first vector.</param>
        /// <param name="y">The second vector.</param>
        /// <returns>The cosine of the angle of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The <see cref="Dimension"/> of <paramref name="x"/> and <paramref name="y"/> don't match.</exception>
        public static Expression CosAngle(Vector x, Vector y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            return x * y / Norm(x) / Norm(y);
        }

        /// <summary>
        /// Maps each element in the vector into a new element.
        /// </summary>
        /// <param name="func">A transform function to apply to each element.</param>
        /// <returns>A vector whose elements are the result of invoking the transform function on each element in the current vector.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public Vector Map(Func<Expression, Expression> func)
        {
            if (func == null)
                throw new ArgumentNullException();
            return FromArray(_elements.Map(func));
        }
    }
}
