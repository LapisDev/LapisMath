/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Structure
 * Description : Provides methods related to the structures of algebraic expressions.
 * Created     : 2015/4/21
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
    /// Provides methods related to the structures of algebraic expressions.
    /// </summary>
    public static class Structure
    {
        /// <summary>
        /// Returns the symbols in the algebraic expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The set containing the symbols in the algebraic expression.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static ISet<Symbol> Symbols(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            HashSet<Symbol> symbols = new HashSet<Symbol>();
            if (expression is Symbol)
            {
                var sym = (Symbol)expression;
                if (!symbols.Contains(sym))
                    symbols.Add(sym);
            }
            else if (expression is Sum ||
                expression is Product ||
                expression is MultiArgumentFunction)
            {
                var ienum = (IEnumerable<Expression>)expression;
                foreach (Expression t in ienum)
                    symbols.UnionWith(t.Symbols());
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                symbols.UnionWith(pow.Base.Symbols());
                symbols.UnionWith(pow.Exponent.Symbols());
            }
            else if (expression is Function)
            {
                var func = (Function)expression;
                symbols.UnionWith(func.Argument.Symbols());
            }
            else if (expression is Expressions.Number ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
            { }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
            return symbols;
        }

        /// <summary>
        /// Returns a value indicating whether the algebraic expression contains the specified symbol.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> contains <paramref name="symbol"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool Contains(this Expression expression, Symbol symbol)
        {
            if (expression == null || symbol == null)
                throw new ArgumentNullException();
            if (expression is Symbol)
            {
                var sym = (Symbol)expression;
                if (sym == symbol)
                    return true;
                else
                    return false;
            }
            else if (expression is Sum ||
                expression is Product ||
                expression is MultiArgumentFunction)
            {
                var ienum = (IEnumerable<Expression>)expression;
                if (ienum.Any((Expression expr) => Contains(expr, symbol)))
                    return true;
                else
                    return false;
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return Contains(pow.Base, symbol) ||
                    Contains(pow.Exponent, symbol);
            }
            else if (expression is Function)
            {
                var func = (Function)expression;
                return Contains(func.Argument, symbol);
            }
            else if (expression is Expressions.Number ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
                return false;
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        /// <summary>
        /// Returns a value indicating whether the algebraic expression contains any of the specified symbols.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbols">The symbols.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> contains any of <paramref name="symbols"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="symbols"/> is empty.</exception>
        public static bool ContainsAny(this Expression expression, ISet<Symbol> symbols)
        {
            if (expression == null || symbols == null)
                throw new ArgumentNullException();
            if (symbols.Count == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            if (expression is Symbol)
            {
                var sym = (Symbol)expression;
                if (symbols.Contains(sym))
                    return true;
                else
                    return false;
            }
            else if (expression is Sum ||
                expression is Product ||
                expression is MultiArgumentFunction)
            {
                var ienum = (IEnumerable<Expression>)expression;
                if (ienum.Any((Expression expr) => ContainsAny(expr, symbols)))
                    return true;
                else
                    return false;
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return ContainsAny(pow.Base, symbols) ||
                    ContainsAny(pow.Exponent, symbols);
            }           
            else if (expression is Function)
            {
                var func = (Function)expression;
                return ContainsAny(func.Argument, symbols);
            }
            else if (expression is Expressions.Number ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
                return false;
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        /// <summary>
        /// Returns a value indicating whether the algebraic expression contains all of the specified symbols.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="symbols">The symbols.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> contains all of <paramref name="symbols"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="symbols"/> is empty.</exception>
        public static bool ContainsAll(this Expression expression, ISet<Symbol> symbols)
        {
            if (expression == null || symbols == null)
                throw new ArgumentNullException();
            if (symbols.Count == 0)
                throw new ArgumentException(ExceptionResource.CountTooFew);
            return Symbols(expression).IsSupersetOf(symbols);
        }

        /// <summary>
        /// Replaces the specified sub-expression with the new expression and returns the result.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="old">The sub-expression to be replaced.</param>
        /// <param name="new">The expression to replace <paramref name="old"/>.</param>      
        /// <returns>An expression that is equivalent to <paramref name="expression"/> except that all occurrences of <paramref name="old"/> are replaced with <paramref name="new"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Replace(this Expression expression,
            Expression old, Expression @new)
        {
            if (expression == null || old == null || @new == null)
                throw new ArgumentNullException();
            if (expression == old)
                return @new;
            else if (expression is Sum)
            {
                var sum = (Sum)expression;
                return new Sum(sum.Map((Expression expr) => Replace(expr, old, @new)));
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                return new Product(prod.Map((Expression expr) => Replace(expr, old, @new)));
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return Power.Create(Replace(pow.Base, old, @new),
                    Replace(pow.Exponent, old, @new));
            }           
            else if (expression is Function)
            {
                var func = (Function)expression;
                return Function.Create(func.Identifier,
                    Replace(func.Argument, old, @new));
            }
            else if (expression is MultiArgumentFunction)
            {
                var mfunc = (MultiArgumentFunction)expression;
                return new MultiArgumentFunction(mfunc.Identifier,
                    mfunc.Map((Expression expr) => Replace(expr, old, @new)));
            }
            else if (expression is Expressions.Number ||
                expression is Symbol ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
                return expression;
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }
      
        internal static Expression Map(Expression expression, Func<Expression, Expression> func)
        {
            if (expression == null || func == null)
                throw new ArgumentNullException();
            if (expression is Sum)
            {
                var sum = (Sum)expression;
                Expression r = Number.Zero;
                foreach (Expression t in sum)
                {
                    r += func(t);
                }
                return r;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression r = Number.One;
                foreach (Expression t in prod)
                {
                    r *= func(t);
                }
                return r;
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return Expression.Pow(func(pow.Base), func(pow.Exponent));
            }
            else if (expression is Function)
            {
                var fun = (Function)expression;
                return Function.Create(fun.Identifier, func(fun.Argument));
            }
            else if (expression is MultiArgumentFunction)
            {
                var mulfun = (MultiArgumentFunction)expression;
                return new MultiArgumentFunction(mulfun.Identifier, Util.Map(mulfun, func));
            }
            else
                return expression;
        }
    }
}
