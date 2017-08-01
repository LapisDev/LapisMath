/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Polynomial
 * Description : Provides methods related to polynomials.
 * Created     : 2015/5/15
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
    /// Provides methods related to polynomials.
    /// </summary> 
    public static class Polynomial
    {
        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a momomial.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> is a monomial with respect to <paramref name="symbol"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsMonomial(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            if (expression == symbol)
                return true;
            else if (expression is Number)
                return true;
            Expression bas;
            Integer exp;
            if (expression.IsPositiveIntegerPower(out bas, out exp) &&
                bas == symbol)
                return true;
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                return prod.All((Expression t) => t.IsMonomial(symbol));
            }
            else
                return !expression.Contains(symbol);
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a polynomial.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> is a polynomial with respect to <paramref name="symbol"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsPolynomial(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            if (expression.IsMonomial(symbol))
                return true;
            else if (expression is Sum)
            {
                Sum sum = (Sum)expression;
                return sum.All((Expression t) => t.IsMonomial(symbol));
            }
            else
                return false;
        }


        /// <summary>
        /// Returns the degree of the polynomial expression.
        /// </summary>
        /// <param name="expression">The polynomial expression.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns>The degree of <paramref name="expression"/> with respect to <paramref name="symbol"/>, or <see langword="Undefined"/> if <paramref name="expression"/> is not a polynomial with respect to <paramref name="symbol"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Degree(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            var deg = expression.DegreeOfMonomial(symbol);
            if (deg is Undefined)
            {
                if (expression is Sum)
                {
                    Sum sum = (Sum)expression;
                    Expression max = null;
                    foreach (Expression t in sum)
                    {
                        if (max == null ||
                            ExpressionComparer.Instance.Compare(max, t) < 0)
                            max = t;
                    }
                    return max;
                }
                else
                    return Undefined.Instance;
            }
            else
                return deg;
        }

        /// <summary>
        /// Returns the leading coefficient and degree of the polynomial expression in the output parameters.
        /// </summary>
        /// <param name="expression">The polynomial expression.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <param name="coefficent">When this method returns, contains the leading coefficient of <paramref name="expression"/>, or <see langword="Undefined"/> if <paramref name="expression"/> is not a polynomial with respect to <paramref name="symbol"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="coefficent"/> will be overwritten.</param>
        /// <param name="degree">When this method returns, contains the degree of <paramref name="expression"/> with respect to <paramref name="symbol"/>, or <see langword="Undefined"/> if <paramref name="expression"/> is not a polynomial with respect to <paramref name="symbol"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="degree"/> will be overwritten.</param>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static void LeadingCoefficientDegree(this Expression expression, Symbol symbol, out Expression coefficent, out Expression degree)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            Expression coe;
            Expression deg;
            expression.CoefficientDegreeOfMonomial(symbol, out coe, out deg);
            if (deg is Undefined)
            {
                if (expression is Sum)
                {
                    Sum sum = (Sum)expression;
                    coe = Number.Zero;
                    deg = Number.Zero;
                    foreach (Expression t in sum)
                    {
                        Expression c;
                        Expression d;
                        t.CoefficientDegreeOfMonomial(symbol, out c, out d);
                        var cmp= ExpressionComparer.Instance.Compare(deg, d);
                        if (cmp < 0)
                        {
                            deg = d;
                            coe = c;
                        }
                        else if (cmp == 0)
                        {
                            coe += c;
                        }
                    }
                    coefficent = coe;
                    degree = deg;
                    return;
                }
                else
                {
                    coefficent = Undefined.Instance;
                    degree = Undefined.Instance;
                    return;
                }
            }
            else
            {
                coefficent = coe;
                degree = deg;
                return;
            }
        }


        #region Private

        private static Expression DegreeOfMonomial(this Expression expression, Symbol symbol)
        {
            if (expression == Number.Zero)
                return NegativeInfinity.Instance;
            else if (expression == symbol)
                return Number.One;
            else if (expression is Number)
                return Number.Zero;
            Expression bas;
            Integer exp;
            if (expression.IsPositiveIntegerPower(out bas, out exp) &&
                bas == symbol)
                return (Number)exp;
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                Expression deg = Number.Zero;
                foreach (Expression t in prod)
                    deg += t.DegreeOfMonomial(symbol);
                return deg;
            }
            else if (!expression.Contains(symbol))
                return Number.Zero;
            else
                return Undefined.Instance;
        }

        private static void CoefficientDegreeOfMonomial(this Expression expression, Symbol symbol, out Expression coefficent, out Expression degree)
        {
            if (expression == symbol)
            {
                coefficent = Number.One;
                degree = Number.One;
                return;
            }
            else if (expression is Number)
            {
                coefficent = expression;
                degree = Number.Zero;
                return;
            }
            Expression bas;
            Integer exp;
            if (expression.IsPositiveIntegerPower(out bas, out exp) &&
                bas == symbol)
            {
                coefficent = Number.One;
                degree = (Number)exp;
            }
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                Expression coe = Number.One;
                Expression deg = Number.Zero;
                foreach (Expression t in prod)
                {
                    Expression c;
                    Expression d;
                    t.CoefficientDegreeOfMonomial(symbol, out c, out d);
                    coe *= c;
                    deg += d;
                }
                coefficent = coe;
                degree = deg;
                return;
            }
            else if (!expression.Contains(symbol))
            {
                coefficent = expression;
                degree = Number.Zero;
                return;
            }
            else
            {
                coefficent = Undefined.Instance;
                degree = Undefined.Instance;
                return;
            }
        }

        #endregion


        /// <summary>
        /// Calculates the quotient and remainder of two polynomials and returns them in output parameters.
        /// </summary>
        /// <param name="numerator">The dividend.</param>
        /// <param name="denominator">The divisor.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <param name="quotient">When this method returns, contains the quotient of <paramref name="numerator"/> and <paramref name="denominator"/> with respect to <paramref name="symbol"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="quotient"/> will be overwritten.</param>
        /// <param name="remainder">When this method returns, contains the remainder of <paramref name="numerator"/> and <paramref name="denominator"/> with respect to <paramref name="symbol"/>. This parameter is passed uninitialized; any value originally supplied in <paramref name="remainder"/> will be overwritten.</param>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static void Divide(Expression numerator, Expression denominator,
            Symbol symbol, out Expression quotient, out Expression remainder)
        {
            if (numerator == null || denominator == null || symbol == null)
                throw new ArgumentNullException();
            Expression leaCoeDen;
            Expression degDen;
            denominator.LeadingCoefficientDegree(symbol, out leaCoeDen, out degDen);
            if (ExpressionComparer.Instance.Compare(degDen, Number.One) < 0)
            {
                quotient = numerator / denominator;
                remainder = Number.Zero;
                return;
            }
            else
            {
                var u = denominator - leaCoeDen * Expression.Pow(symbol, degDen);
                Expression v;
                var rem = numerator;
                Expression quo = Number.Zero;
                Expression leaCoeRem;
                Expression degRem;
                while (true)
                {
                    rem.LeadingCoefficientDegree(symbol, out leaCoeRem, out degRem);
                    if (ExpressionComparer.Instance.Compare(degRem, degDen) < 0)
                    {
                        quotient = quo;
                        remainder = rem;
                        return;
                    }
                    v = leaCoeRem / leaCoeDen * Expression.Pow(symbol, degRem - degDen);
                    quo += v;
                    rem = Elementary.Expand(rem - leaCoeRem * Expression.Pow(symbol, degRem) - u * v);
                }
            }
        }

        /// <summary>
        /// Returns the greatest common divisor of the two polynomials.
        /// </summary>
        /// <param name="x">The polynomial.</param>
        /// <param name="y">The other polynomial.</param>
        /// <param name="symbol">The symbol of the pivot.</param>
        /// <returns>The greatest common divisor of <paramref name="x"/> and <paramref name="y"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Gcd(Expression x, Expression y, Symbol symbol)
        {
            if (x == null || y == null || symbol == null)
                throw new ArgumentNullException();
            if (x == Number.Zero && y == Number.Zero)
                return Number.Zero;
            Expression u = x;
            Expression v = y;
            Expression q, r;
            while (true)
            {
                if (v == Number.Zero)
                {
                    r = u;
                    break;
                }
                else
                {
                    Divide(u, v, symbol, out q, out r);
                    u = v;
                    v = r;
                }
            }
            Expression coe, deg;
            r.LeadingCoefficientDegree(symbol, out coe, out deg);
            return Elementary.Expand(r / coe);
        }
    }
}
