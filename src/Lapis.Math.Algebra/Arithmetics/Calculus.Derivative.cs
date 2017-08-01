/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Calculus
 * Description : Provides methods related to calculus.
 * Created     : 2015/5/16
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Algebra.Arithmetics
{
    /// <summary>
    /// Provides methods related to calculus.
    /// </summary>
    public static class Calculus
    {

        /// <summary>
        /// Returns the (partial) derivative of the specified algebraic expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The variable that the (partial) derivative is with respect to.</param>
        /// <returns>The partial derivative of <paramref name="expression"/> with respect to <paramref name="symbol"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Derivative(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            if (expression == symbol)
                return Number.One;
            else if (expression is Number || expression is Symbol)
                return Number.Zero;
            else if (expression is Sum)
            {
                Sum sum = (Sum)expression;
                Expression r = Number.Zero;
                foreach (Expression t in sum)
                {
                    r += t.Derivative(symbol);
                }
                return r;
            }
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                Expression r = Number.Zero;
                Expression t;
                for (int i = 0; i < prod.NumberOfFactors; i++)
                {
                    t = Number.One;
                    for (int j = 0; j < prod.NumberOfFactors; j++)
                    {
                        if (j == i)
                            t *= prod[j].Derivative(symbol);
                        else
                            t *= prod[j];
                    }
                    r += t;
                }
                return r;
            }
            else if (expression is Power)
            {
                Power pow = (Power)expression;
                var dbas = pow.Base.Derivative(symbol);
                var dexp = pow.Exponent.Derivative(symbol);
                return dexp * Expression.Ln(pow.Base) * expression + pow.Exponent * dbas * Expression.Pow(pow.Base, pow.Exponent - 1);
            }
            else if (expression is Function)
            {
                return Derivative((Function)expression, symbol);
            }
            else if (expression is MultiArgumentFunction)
            {
                return Derivative((MultiArgumentFunction)expression, symbol);
            }
            else if (expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
            {
                return expression;
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        #region Private

        private static Expression Derivative(this Function function, Symbol symbol)
        {
            if (function.Identifier == FunctionIdentifiers.Exp)
            {
                return Derivative(function.Argument, symbol) * function;
            }
            else if (function.Identifier == FunctionIdentifiers.Ln)
            {
                return Derivative(function.Argument, symbol) / function.Argument;
            }
            else if (function.Identifier == FunctionIdentifiers.Sin)
            {
                return Derivative(function.Argument, symbol) * Expression.Cos(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Cos)
            {
                return -Derivative(function.Argument, symbol) * Expression.Sin(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Tan)
            {
                return 2 * Derivative(function.Argument, symbol) / (Expression.Cos(2 * function.Argument) + 1);
            }
            else if (function.Identifier == FunctionIdentifiers.Cot)
            {
                return 2 * Derivative(function.Argument, symbol) / (Expression.Cos(2 * function.Argument) + 1);
            }
            else if (function.Identifier == FunctionIdentifiers.Sec)
            {
                return Derivative(function.Argument, symbol) * function * Expression.Tan(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Csc)
            {
                return -Derivative(function.Argument, symbol) * function * Expression.Cot(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Asin)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(1 - Expression.Pow(function.Argument, 2), -0.5);
            }
            else if (function.Identifier == FunctionIdentifiers.Acos)
            {
                return -Derivative(function.Argument, symbol) * Expression.Pow(1 - Expression.Pow(function.Argument, 2), -0.5);
            }
            else if (function.Identifier == FunctionIdentifiers.Atan)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(1 + Expression.Pow(function.Argument, 2), -1);
            }
            else if (function.Identifier == FunctionIdentifiers.Acot)
            {
                return -Derivative(function.Argument, symbol) * Expression.Pow(1 + Expression.Pow(function.Argument, 2), -1);
            }
            else if (function.Identifier == FunctionIdentifiers.Sinh)
            {
                return Derivative(function.Argument, symbol) * Expression.Cosh(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Cosh)
            {
                return Derivative(function.Argument, symbol) * Expression.Sinh(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Tanh)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(Expression.Sech(function.Argument), 2);
            }
            else if (function.Identifier == FunctionIdentifiers.Coth)
            {
                return -Derivative(function.Argument, symbol) * Expression.Pow(Expression.Csch(function.Argument), 2);
            }
            else if (function.Identifier == FunctionIdentifiers.Sech)
            {
                return -Derivative(function.Argument, symbol) * function * Expression.Tanh(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Csch)
            {
                return -Derivative(function.Argument, symbol) * function * Expression.Coth(function.Argument);
            }
            else if (function.Identifier == FunctionIdentifiers.Asinh)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(1 + Expression.Pow(function.Argument, 2), -0.5);
            }
            else if (function.Identifier == FunctionIdentifiers.Acosh)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(-1 + Expression.Pow(function.Argument, 2), -0.5);
            }
            else if (function.Identifier == FunctionIdentifiers.Atanh)
            {
                return Derivative(function.Argument, symbol) * Expression.Pow(1 - Expression.Pow(function.Argument, 2), -1);
            }
            else if (function.Identifier == FunctionIdentifiers.Abs)
            {
                return Derivative(function.Argument, symbol) * function / function.Argument;
            }
            else if (function.Identifier == FunctionIdentifiers.Sgn)
            {
                return 0;
            }
            else
                throw new ArithmeticException();
        }

        private static Expression Derivative(this MultiArgumentFunction mulFunction, Symbol symbol)
        {
            if (mulFunction.Identifier == FunctionIdentifiers.Log)
            {
                if (mulFunction.NumberOfArguments != 2)
                    throw new ArithmeticException();
                return Derivative(mulFunction[0], symbol) / (mulFunction[0] * Expression.Ln(mulFunction[1]));
            }
            else
                throw new ArithmeticException();
        }

        #endregion

        /// <summary>
        /// Returns the derivative of the algebraic expression at the specified point.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The variable that the derivative is with respect to.</param>
        /// <param name="point">The point that the derivative is at.</param>
        /// <returns>The derivative of <paramref name="expression"/> with respect to <paramref name="symbol"/> at <paramref name="point"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Derivative(this Expression expression, Symbol symbol, Expression point)
        {
            return Elementary.Simplify(
                Structure.Replace(
                    expression.Derivative(symbol),
                    symbol, point));
        }

        /// <summary>
        /// Returns the Taylor expansion of the algebraic expression.
		/// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The variable that the Taylor expansion is with respect to.</param>
        /// <param name="point">The point that the Taylor expansion is at.</param>
        /// <param name="degree">The degree of the Taylor expansion.</param>
        /// <returns>The <paramref name="degree"/>th degree Taylor expansion of <paramref name="expression"/> with respect to <paramref name="symbol"/> at <paramref name="point"/>. </returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="degree"/> is less than 1.</exception>
        public static Expression Taylor(this Expression expression, Symbol symbol,
            Expression point, int degree)
        {
            if (expression == null || symbol == null || point == null)
                throw new ArgumentNullException();
            if (degree < 1)
                throw new ArgumentOutOfRangeException();
            int n = 0;
            int nf = 1;
            Expression acc = Number.Zero;
            Expression dxn = expression;
            while (true)
            {
                if (n == degree)
                    break;
                acc += Elementary.Simplify(Structure.Replace(dxn, symbol, point)) / nf * Expression.Pow(symbol - point, n);
                dxn = Derivative(dxn, symbol);
                nf *= n + 1;
                n++;
            }
            return Elementary.Expand(acc);
        }
    }
}
