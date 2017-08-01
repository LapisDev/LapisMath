/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Fraction
 * Description : Provides methods related to fractions.
 * Created     : 2015/5/16
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.Algebra.Arithmetics;
using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Arithmetics
{
    /// <summary>
    /// Provides methods related to fractions.
    /// </summary> 
    public static class Fraction
    {
        /// <summary>
        /// Returns the numerator and denominator of the fraction in the output parameters.
        /// </summary>
        /// <param name="expression">The fraction expression.</param>
        /// <param name="numerator">When this method returns, contains the numerator of the <paramref name="expression"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="numerator"/> will be overwritten.</param>
        /// <param name="denominator">When this method returns, contains the denominator of the <paramref name="expression"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="denominator"/> will be overwritten.</param>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static void NumeratorDenominator(this Expression expression,
            out Expression numerator, out Expression denominator)
        {
            if (expression == null)
                throw new ArgumentNullException();
            Expression bas;
            Rational exp;
            if (Arithmetics.Pattern.IsNegativeRationalPower(expression, out bas, out exp))
            {
                numerator = Number.One;
                denominator = Expression.Pow(bas, (Number)(-exp));
                return;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression num = Number.One;
                Expression den = Number.One;
                Expression n, d;
                foreach (Expression t in prod)
                {
                    t.NumeratorDenominator(out n, out d);
                    if (n != Number.One)
                        num *= n;
                    if (d != Number.One)
                        den *= d;
                }
                numerator = num;
                denominator = den;       
                return;
            }
            else
            {
                numerator = expression;
                denominator = Number.One;
                return;
            }
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a rational fraction.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> is a rational fraction with respect to <paramref name="symbol"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsRationalFraction(this Expression expression, Symbol symbol)
        {
            if (expression == null)
                throw new ArgumentNullException();
            Expression num, den;
            expression.NumeratorDenominator(out num, out den);
            return Polynomial.IsPolynomial(num, symbol) && Polynomial.IsPolynomial(den, symbol);
        }

        /// <summary>
        /// Returns the sum of the two fractions.
        /// </summary>
        /// <param name="x">The fraction.</param>
        /// <param name="y">The other fraction</param>       
        /// <returns>The sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression CommonDenominatorAdd(Expression x, Expression y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();
            Expression numx, denx, numy, deny;           
            Expression densum = Number.One;
            Expression u = x, v = y;
            while (true)
            {
                u.NumeratorDenominator(out numx, out denx);
                v.NumeratorDenominator(out numy, out deny);
                if (denx == Number.One && deny == Number.One)
                {
                    return (u + v) / densum;
                }
                densum *= denx * deny;
                u = numx * deny;
                v = numy * denx;
            }
        }

        /// <summary>
        /// Converts the algebraic expression to a rational fraction.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The rational fraction converted from <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Rationalize(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            if (expression is Power)
            {
                var pow = (Power)expression;
                return Expression.Pow(Rationalize(pow.Base), pow.Exponent);
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression r = Number.One;
                foreach (Expression t in prod)
                {
                    r *= Rationalize(t);
                }
                return r;
            }
            else if (expression is Sum)
            {
                var sum = (Sum)expression;
                Expression r = Number.Zero;
                foreach (Expression t in sum)
                {
                    r = CommonDenominatorAdd(r, Rationalize(t));
                }
                return r;
            }
            else
                return expression;
        }

        /// <summary>
        /// Expands the fraction expression.
        /// </summary>
        /// <param name="expression">The fraction expression.</param>   
        /// <returns>The expanded expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Expand(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            Expression num, den;
            expression.NumeratorDenominator(out num, out den);
            num = Elementary.Expand(num);
            den = Elementary.Expand(den);
            var r = Rationalize(num / den);
            if (r == expression)
                return r;
            else
                return Expand(r);
        }

        /// <summary>
        /// Simplifies the fraction expression.
        /// </summary>
        /// <param name="expression">The fraction expression.</param> 
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns>The simplified expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Simplify(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            Expression num, den;
            Expand(expression).NumeratorDenominator(out num, out den);           
            var gcd = Polynomial.Gcd(num, den, symbol);
            Expression quonum, quoden, remnum, remden;
            Polynomial.Divide(num, gcd, symbol, out quonum, out remnum);
            Polynomial.Divide(den, gcd, symbol, out quoden, out remden);
            return quonum / quoden;
        }
    }
}
