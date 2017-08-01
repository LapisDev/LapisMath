/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Expression
 * Description : Provides elementary arithmetics of algebraic expressions.
 * Created     : 2015/4/5
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    public partial class Expression
    {      
        #region Unary

        /// <summary>
        /// Returns the value of the <see cref="Expression"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">The expression.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator +(Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();  
            return value;
        }

        /// <summary>
        /// Negates the <see cref="Expression"/> value.
        /// </summary>
        /// <param name="value">The expression to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator -(Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();  
            if (value is Sum)
                return new Sum(((Sum)value).
                    Map((Expression expr) => -expr));
            else
                return Number.MinusOne * value;
        }
        
        #endregion


        #region Add and Subtract

        /// <summary>
        /// Adds the values of two specified <see cref="Expression"/> objects.
        /// </summary>
        /// <param name="left">The first expression to add.</param>
        /// <param name="right">The second expression to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator +(Expression left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            if (left is Undefined || right is Undefined)
            {
                return Undefined.Instance;
            }                              

            if (left is Sum && right is Sum)
            {
                List<Expression> list = ((Sum)left).Concat((Sum)right).ToList();                
                return SumNormalize(list);
            }
            else if (left is Sum)
            {
                List<Expression> list = ((Sum)left).ToList();
                list.Add(right);
                return SumNormalize(list);
            }
            else if (right is Sum)
            {
                return right + left;
            }

            Expression r = TermAdd(left, right);
            if (r != null)
            {
                return r;
            }
            else
            {
                if (ExpressionComparer.Instance.Compare(left, right) < 0)
                    return Sum.Create(left, right);
                else
                    return Sum.Create(right, left);
            }
        }

        /// <summary>
        /// Subtracts an <see cref="Expression"/> object from another <see cref="Expression"/> object.
        /// </summary>
        /// <param name="left">The expression to subtract from (the minuend).</param>
        /// <param name="right">The expression to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator -(Expression left, Expression right)
        {
            return left + (-right);
        }

        #region Private

        // Converts to c*x 
        private bool IsTermOfSum(out Number coefficient, out Expression expression)
        {
            if (this is Number)
            {
                coefficient = null;
                expression = null;
                return false;
            }
            else if (this is Product)
            {
                var prod = (Product)this;
                Expression head;
                List<Expression> tail;
                prod.Decompose(out head, out tail);
                if (head is Number)
                {
                    coefficient = (Number)head;
                    if (tail.Count > 1)
                        expression = Product.Create(tail);
                    else
                        expression = tail[0];
                    return true;
                }
            }
            coefficient = Number.One;
            expression = this;
            return true;
        }

        // a*x + b*x => (a+b)*x
        private static Expression TermAdd(Expression left, Expression right)
        {
            if (left == Number.Zero)
                return right;
            else if (right == Number.Zero)
                return left;
            else if (left is Number && right is Number)
                return (Number)left + (Number)right;

            Number num1, num2;
            Expression expr1, expr2;
            if (left.IsTermOfSum(out num1, out expr1) &&
                right.IsTermOfSum(out num2, out expr2) &&
                expr1 == expr2)
            {
                return (num1 + num2) * expr1;
            }
            else
                return null;
        }
        
        private static Expression SumNormalize(List<Expression> list)
        {
            if (list.Count == 0)
                return Number.Zero;
            else if (list.Count == 1)
                return list[0];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    Expression t = TermAdd(list[i], list[j]);
                    if (t != null)
                    {
                        list[i] = t;
                        list[j] = Number.Zero;
                        if (list[i] == Number.Zero)
                            break;
                    }
                }
                
            }
            for (int i = 0; i < list.Count; )
            {
                if (list[i] == Number.Zero)
                    list.RemoveAt(i);
                else
                    i++;
            }

            list.Sort(ExpressionComparer.Instance);

            if (list.Count == 0)
                return Number.Zero;
            else if (list.Count == 1)
                return list[0];
            else
                return new Sum(list);
        }

        #endregion

        #endregion


        #region Multiply and Divide

        /// <summary>
        /// Multiplies two specified <see cref="Expression"/> objects.
        /// </summary>
        /// <param name="left">The first expression to multiply.</param>
        /// <param name="right">The second expression to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator *(Expression left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            if (left is Undefined || right is Undefined)
            {
                return Undefined.Instance;
            }           
            
            if (left is Product && right is Product)
            {
                List<Expression> list = ((Product)left).Concat((Product)right).ToList();
                return ProdNormalize(list);
            }
            else if (left is Product)
            {
                List<Expression> list = ((Product)left).ToList();
                list.Add(right);
                return ProdNormalize(list);
            }
            else if (right is Product)
            {
                return right * left;
            }

            Expression r = TermMultiply(left, right);
            if (r != null)
            {
                return r;
            }
            else
            {
                if (ExpressionComparer.Instance.Compare(left, right) < 0)
                    return Product.Create(left, right);
                else
                    return Product.Create(right, left);
            }
        }

        /// <summary>
        /// Divides a specified <see cref="Expression"/> object by another specified <see cref="Expression"/> object.
        /// </summary>
        /// <param name="left">The expression to be divided.</param>
        /// <param name="right">The expression to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator /(Expression left, Expression right)
        {
            return left * Reciprocal(right);
        }

        /// <summary>
        /// Returns the reciprocal of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>       
        /// <returns>The reciprocal of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Reciprocal(Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();  
            if (value is Undefined)
                return Undefined.Instance;
            else if (value is PositiveInfinity || value is NegativeInfinity)
                return Number.Zero;
            else if (value is Number)
            {
                if (value == Number.Zero)
                    return ComplexInfinity.Instance;
                else
                    return Number.One / (Number)value;
            }
            else if (value is Product)
            {
                return ProdNormalize(((Product)value).Map(Reciprocal));
            }
            else if (value is Power)
            {
                var pow = (Power)value;
                return Pow(pow.Base, -pow.Exponent);
            }
            else
                return Pow(value, Number.MinusOne);
        }

        #region Private

        // Converts to x^a
        private bool IsTermOfProduct(out Expression @base, out Expression exponent)
        {
            if (this is Number)
            {
                @base = null;
                exponent = null;
                return false;
            }
            else if (this is Power)
            {
                var pow = (Power)this;
                @base = pow.Base;
                exponent = pow.Exponent;
                return true;
            }
            exponent = Number.One;
            @base = this;
            return true;
        }

        // x^a * x^b => x^(a+b)
        private static Expression TermMultiply(Expression left, Expression right)
        {
            if (left == Number.Zero)
                return Number.Zero;
            else if (right == Number.Zero)
                return Number.Zero;
            else if (left == Number.One)
                return right;
            else if (right == Number.One)
                return left;
            else if (left is Number && right is Number)
                return (Number)left * (Number)right;           

            Expression base1, base2;
            Expression exp1, exp2;
            if (left.IsTermOfProduct(out base1, out exp1) &&
                right.IsTermOfProduct(out base2, out exp2) &&
                base1 == base2)
            {
                return Pow(base1, exp1 + exp2);
            }
            else
                return null;
        }

        private static Expression ProdNormalize(List<Expression> list)
        {
            if (list.Count == 0)
                return Number.One;
            else if (list.Count == 1)
                return list[0];

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == Number.Zero)
                    return Number.Zero;

                for (int j = i + 1; j < list.Count; j++)
                {
                    Expression t = TermMultiply(list[i], list[j]);
                    if (t != null)
                    {
                        list[i] = t;
                        list[j] = Number.One;
                        if (list[i] == Number.One)
                            break;
                        else if (list[i] == Number.Zero)
                            return Number.Zero;
                    }
                }
            }
            for (int i = 0; i < list.Count; )
            {
                if (list[i] == Number.One)
                    list.RemoveAt(i);
                else
                    i++;
            }

            list.Sort(ExpressionComparer.Instance);
            if (list.Count == 0)
                return Number.One;
            else if (list.Count == 1)
                return list[0];
            else
                return new Product(list);
        }

        #endregion

        #endregion


        #region Pow

        /// <summary>
        /// Returns the specified expression raised to the specified power.
        /// </summary>
        /// <param name="left">The expression to be raised to a power.</param>
        /// <param name="right">The expression that specifies the power.</param>
        /// <returns>The expression of <paramref name="left"/> raised to the power <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Pow(Expression left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            if (left == Number.Zero && right == Number.Zero)
                return Undefined.Instance;
            else if (right == Number.Zero)
                return Number.One;
            else if (right == Number.One)
                return left;
            else if (left == Number.One)
                return Number.One;
            else if (left is Number && right is Number)
            {
                return Number.Pow((Number)left, (Number)right);
            }
            else if (left is Product)
            {
                return ProdNormalize(((Product)left).
                    Map((Expression expr) => Pow(expr, right)));
            }
            else if (left is Power)
            {
                var pow = (Power)left;
                return Pow(pow.Base, pow.Exponent * right);
            }
            else
                return Power.Create(left, right);
        }         

        #endregion
    }
}
