/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Elementary
 * Description : Provides methods for elementary arithmetics of algebraic expressions.
 * Created     : 2015/4/5
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Arithmetics
{
    /// <summary>
    /// Provides methods for elementary arithmetics of algebraic expressions.
    /// </summary> 
    public static class Elementary
    {
        /// <summary>
        /// Expands the specified algebraic expression.
        /// </summary>
        /// <param name="value">The algebraic expression.</param>
        /// <returns>The expanded expression of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Expand(this Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value is Sum)
                return SumExpand((Sum)value);
            else if (value is Product)
                return ProdExpand((Product)value);

            Expression b;
            Integer exp;
            if (value.IsPositiveIntegerPower(out b, out exp))
            {
                return ExpandPow(b, exp);
            }
            else
                return value;
        }

        #region Private

        private static Expression SumExpand(Sum value)
        {
            Expression r = Number.Zero;
            foreach (Expression expr in value)
            {
                Expression t = Expand(expr);
                r += t;
            }
            return r;
        }

        private static Expression ProdExpand(Product value)
        {
            Expression r = Number.One;
            foreach (Expression expr in value)
            {
                Expression t = Expand(expr);
                r = ExpandMul(r, t);
            }
            return r;

        }

        private static Expression ExpandMul(Expression left, Expression right)
        {
            if (left is Sum)
            {
                Expression r = Number.Zero;
                foreach (Expression expr in (Sum)left)
                {
                    r += ExpandMul(expr, right);
                }
                return r;
            }
            else if (right is Sum)
                return ExpandMul(right, left);
            else
                return left * right;
        }

        private static Expression ExpandPow(Expression @base, Integer exponent)
        {
            if (@base is Sum && exponent > 1)
            {
                Sum sum = (Sum)@base;
                Expression r = Number.Zero;
                for (int i = 0; i < sum.Count(); i++)
                {
                    r += Expression.Pow(sum[i], (Number)exponent);
                    for (int j = i + 1; j < sum.Count(); j++)
                    {
                        for (Integer k = 1; k < exponent; k += 1)
                        {
                            r += (Number)Numbers.Integer.Binomial(exponent, k) * Expression.Pow(sum[i], (Number)(exponent - k)) * Expression.Pow(sum[j], (Number)k);
                        }
                    }
                }
                return r;
            }
            else
                return Expression.Pow(@base, (Number)exponent);
        }

        #endregion


        /// <summary>
        /// Simplifies the specified algebraic expression.
        /// </summary>
        /// <param name="value">The algebraic expression.</param>
        /// <returns>The simplified expression of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Simplify(this Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();
            else if (value is Sum)
            {
                Sum sum = (Sum)value;
                Expression r = Number.Zero;
                foreach (Expression t in sum)
                {
                    r += Simplify(t);
                }
                return r;
            }
            else if (value is Product)
            {
                Product prod = (Product)value;
                Expression r = Number.One;
                foreach (Expression t in prod)
                {
                    r *= Simplify(t);
                }
                return r;
            }
            else if (value is Power)
            {
                Power pow = (Power)value;
                var bas = Simplify(pow.Base);
                var exp = Simplify(pow.Exponent);
                return Expression.Pow(bas, exp);
            }
            else if (value is Function)
            {
                return Simplify((Function)value);
            }
            else if (value is MultiArgumentFunction)
            {
                return Simplify((MultiArgumentFunction)value);
            }
            else if (value is Number ||
                value is Symbol ||
                value is PositiveInfinity ||
                value is NegativeInfinity ||
                value is ComplexInfinity ||
                value is Undefined)
            {
                return value;
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        #region Private

        private static Expression Simplify(this Function function)
        {
            Expression arg = Simplify(function.Argument);
            if (function.Identifier == FunctionIdentifiers.Exp)
            {
                return Expression.Exp(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Ln)
            {
                return Expression.Ln(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sin)
            {
                return Expression.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cos)
            {
                return Expression.Cos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tan)
            {
                return Expression.Tan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cot)
            {
                return Expression.Tan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sec)
            {
                return Expression.Sec(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csc)
            {
                return Expression.Csc(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asin)
            {
                return Expression.Asin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acos)
            {
                return Expression.Acos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Atan)
            {
                return Expression.Atan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acot)
            {
                return Expression.Acot(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sinh)
            {
                return Expression.Sinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cosh)
            {
                return Expression.Cosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tanh)
            {
                return Expression.Tanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Coth)
            {
                return Expression.Coth(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sech)
            {
                return Expression.Sech(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csch)
            {
                return Expression.Csch(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asinh)
            {
                return Expression.Asinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acosh)
            {
                return Expression.Acosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Atanh)
            {
                return Expression.Atanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Abs)
            {
                return Expression.Abs(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sgn)
            {
                return Expression.Sgn(arg);
            }
            else
                throw new ArithmeticException();
        }

        private static Expression Simplify(this MultiArgumentFunction mulFunction)
        {
            if (mulFunction.Identifier == FunctionIdentifiers.Log)
            {
                if (mulFunction.NumberOfArguments != 2)
                    throw new ArithmeticException();
                Expression arg0 = Simplify(mulFunction[0]);
                Expression arg1 = Simplify(mulFunction[1]);
                return Expression.Log(arg0, arg1);
            }
            else
                throw new ArithmeticException();
        }

        #endregion
    }
}
