/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Pattern
 * Description : Provides methods for pattern matching for algebraic expressions.
 * Created     : 2015/5/20
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Algebra.Arithmetics
{
    public static partial class Pattern
    {
        /// <summary>
        /// Returns a value that indicates whether the algebraic expression matches the specified pattern.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="pattern">The expression of the pattern.</param>
        /// <param name="result">When this method returns, contains the symbols in the pattern and the matched sub-expressions. This parameter is passed uninitialized; any value originally supplied in <paramref name="result"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> is matched with <pararefm name="pattern"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool Match(this Expression expression, Expression pattern, out IDictionary<Symbol, Expression> result)
        {
            if (expression == null || pattern == null)
                throw new ArgumentNullException();
            var dict = new Dictionary<Symbol, Expression>();
            if (MatchInner(expression, pattern, dict))
            {
                result = dict;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        private static bool MatchInner(Expression expression, Expression pattern, Dictionary<Symbol, Expression> dictionary)
        {
            if (pattern is Symbol)
            {
                Symbol sym = (Symbol)pattern;
                Expression expr;
                if (dictionary.TryGetValue(sym, out expr))
                {
                    if (expr == expression)
                        return true;
                    else
                        return false;
                }
                else
                {
                    dictionary.Add(sym, expression);
                    return true;
                }
            }
            else if (pattern is Number ||
                pattern is Undefined ||
                pattern is PositiveInfinity ||
                pattern is NegativeInfinity ||
                pattern is ComplexInfinity)
            {
                return expression == pattern;
            }
            else if (pattern is Sum && expression is Sum)
            {
                var p = (Sum)pattern;
                var e = (Sum)expression;
                if (p.NumberOfAddends == e.NumberOfAddends)
                {
                    for (int i = 0; i < p.NumberOfAddends; i++)
                    {
                        if (!MatchInner(e[i], p[i], dictionary))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            else if (pattern is Product && expression is Product)
            {
                var p = (Product)pattern;
                var e = (Product)expression;
                if (p.NumberOfFactors == e.NumberOfFactors)
                {
                    for (int i = 0; i < p.NumberOfFactors; i++)
                    {
                        if (!MatchInner(e[i], p[i], dictionary))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            else if (pattern is Power && expression is Power)
            {
                var p = (Power)pattern;
                var e = (Power)expression;
                if (MatchInner(e.Base, p.Base, dictionary) &&
                    MatchInner(e.Exponent, p.Exponent, dictionary))
                    return true;
                else
                    return false;
            }
            else if (pattern is Function && expression is Function)
            {
                var p = (Function)pattern;
                var e = (Function)expression;
                if (e.Identifier == p.Identifier &&
                    MatchInner(e.Argument, p.Argument, dictionary))
                    return true;
                else
                    return false;
            }
            else if (pattern is MultiArgumentFunction && expression is MultiArgumentFunction)
            {
                var p = (MultiArgumentFunction)pattern;
                var e = (MultiArgumentFunction)expression;
                if (p.Identifier == e.Identifier &&
                    p.NumberOfArguments == e.NumberOfArguments)
                {
                    for (int i = 0; i < p.NumberOfArguments; i++)
                    {
                        if (!MatchInner(e[i], p[i], dictionary))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }     
}
