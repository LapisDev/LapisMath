/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Exponential
 * Description : Provides methods related to exponential and logarithmic functions.
 * Created     : 2015/5/17
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
    /// Provides methods related to exponential and logarithmic functions.
    /// </summary>
    public static class Exponential
    {
        /// <summary>
        /// Expands the specified exponential or logarithmic expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The expanded expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Expand(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            if (expression is Number ||
                expression is Symbol ||
                expression is Undefined ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity)
            {
                return expression;
            }
            var m = Structure.Map(expression, Expand);
            if (m is Function)
            {
                var fun = (Function)m;
                if (fun.Identifier == FunctionIdentifiers.Exp)
                    return ApplyExp(Elementary.Expand(fun.Argument));
                else if (fun.Identifier == FunctionIdentifiers.Ln)
                    return ApplyLn(Elementary.Expand(fun.Argument));
                else
                    return fun;
            }
            else
                return m;
        }

        #region Private

        private static Expression ApplyExp(Expression expression)
        {
            if (expression is Sum)
            {
                var sum = (Sum)expression;
                Expression r = Number.One;
                foreach (Expression t in sum) 
                { 
                    r *= ApplyExp(t);
                }
                return r;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression head;
                List<Expression> list;
                Integer num;
                prod.Decompose(out head, out list);
                if (head.IsInteger(out num))
                {
                    if (list.Count == 1)
                        return Expression.Pow(ApplyExp(list[0]), head);
                    else
                        return Expression.Pow(ApplyExp(new Product(list)), head);
                }
                else
                    return Expression.Exp(expression);
            }
            else
                return Expression.Exp(expression);
        }

        private static Expression ApplyLn(Expression expression)
        {
            if (expression is Product)
            {
                var prod = (Product)expression;
                Expression r = Number.Zero;
                foreach (Expression t in prod)
                {
                    r += ApplyLn(t);
                }
                return r;
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return Elementary.Expand(pow.Exponent * ApplyLn(pow.Base));
            }
            else
                return Expression.Ln(expression);
        }

        #endregion

        /// <summary>
        /// Contracts the specified exponential or logarithmic expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The contracted expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Contract(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            var m = Structure.Map(expression, Contract);
            if (m is Product || m is Power)
            {
                return ConstractInner(m);
            }
            else
                return m;
        }

        #region Private

        private static Expression ConstractInner(Expression expression)
        {
            Expression r = Elementary.Expand(expression);
            if (r is Power)
            {
                var pow = (Power)r;
                if (pow.Base is Function)
                {
                    var fun = (Function)pow.Base;
                    if (fun.Identifier == FunctionIdentifiers.Exp)
                    {
                        var p = fun.Argument * pow.Exponent;
                        if (p is Product || p is Power)
                            return Expression.Exp(ConstractInner(p));
                        else
                            return Expression.Exp(p);
                    }                    
                }
            }
            if (r is Product)
            {
                Expression c = Number.One;
                Expression a = Number.Zero;
                foreach (Expression t in (Product)r)
                {
                    if (t is Function)
                    {
                        var fun = (Function)t;
                        if (fun.Identifier == FunctionIdentifiers.Exp)
                        {
                            a += fun.Argument;
                            continue;
                        }
                    }
                    c *= t;
                }
                return c * Expression.Exp(a);
            }
            if (r is Sum)
            {
                Expression a = Number.Zero;
                foreach (Expression t in (Sum)r)
                {
                    if (t is Product || t is Power)
                    {
                        a += ConstractInner(t);
                    }
                    else
                    {
                        a += t;
                    }
                }
                return a;
            }
            return r;
        }

        #endregion

        /// <summary>
        /// Simplifies the specified exponential or logarithmic expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The simplified expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Simplify(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            var r = Fraction.Rationalize(expression);
            Expression num, den;
            Fraction.NumeratorDenominator(r, out num, out den);
            return Contract(num) / Contract(den);
        }
    }
}
