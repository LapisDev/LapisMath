/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Trigonometric
 * Description : Provides methods related to trigonometric functions.
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
    /// Provides methods related to trigonometric functions.
    /// </summary>
    public static class Trigonometric
    {
        /// <summary>
        /// Expands the specified trigonometric expression.
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
                if (fun.Identifier == FunctionIdentifiers.Sin)
                {
                    Expression s, c;
                    ApplySinCos(Elementary.Expand(fun.Argument), out s, out c);
                    return s;
                }
                else if (fun.Identifier == FunctionIdentifiers.Cos)
                {
                    Expression s, c;
                    ApplySinCos(Elementary.Expand(fun.Argument), out s, out c);
                    return c;
                }
                else
                    return fun;
            }
            else
                return m;
            
        }

        #region Private

        private static void ApplySinCos(Expression expression, out Expression sin,out Expression cos)
        {
            if (expression is Sum)
            {
                var sum = (Sum)expression;
                Expression s;
                Expression c;
                Expression s1, c1, s2, c2;
                ApplySinCos(sum[0], out s, out c);
                for (int i = 1; i < sum.NumberOfAddends;i++ )
                {
                    s1 = s;
                    c1 = c;
                    ApplySinCos(sum[i], out s2, out c2);
                    s = s1 * c2 + c1 * s2;
                    c = c1 * c2 - s1 * s2;
                }
                sin = s;
                cos = c;
                return;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression head;
                List<Expression> list;
                Integer num;
                prod.Decompose(out head, out list);
                if (head.IsInteger(out num) && num.Sign > 0)
                {
                    int n = num.ToInt32();
                    Expression tail;
                    if (list.Count == 1)
                        tail = list[0];
                    else
                        tail = new Product(list);
                    var sint = Expression.Sin(tail);
                    var cost = Expression.Cos(tail);
                    Expression s = Number.Zero;
                    Expression c = Number.Zero;
                    Expression s1, c1, s2, c2;
                    for (int i = 1, j = 0; i <= n || j <= n; i += 2, j += 2)
                    {
                        if (i <= n)
                        {
                            s1 = i;
                            c1 = (Integer.FromInt32((i - 1) / 2).IsEven ? Number.One : Number.MinusOne) * Integer.Binomial(n, i);
                            s += c1 * Expression.Pow(cost, n - s1) * Expression.Pow(sint, s1);
                        }
                        if (j <= n)
                        {
                            s2 = j;
                            c2 = (Integer.FromInt32(j / 2).IsEven ? Number.One : Number.MinusOne) * Integer.Binomial(n, j);
                            c += c2 * Expression.Pow(cost, n - s2) * Expression.Pow(sint, s2);
                        }                        
                    }
                    sin = s;
                    cos = c;
                    return;
                }
                else
                {
                    sin = Expression.Sin(expression);
                    cos = Expression.Cos(expression);
                    return;
                }
            }
            else
            {
                sin = Expression.Sin(expression);
                cos = Expression.Cos(expression);
                return;
            }    
        }

        #endregion


        /// <summary>
        /// Contracts the specified trigonometric expression.
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
                return ContractInner(m);
            else
                return m;
        }

        #region Private

        private static Expression ContractInner(Expression expression)
        {
            var r = Elementary.Expand(expression);
            if (r is Product)
            {
                Expression sc, rest;
                Separate(r, out sc, out rest);
                if (sc == Number.One)
                    return r;
                else if (sc is Function)
                {
                    var fun = (Function)sc;
                    if (fun.Identifier == FunctionIdentifiers.Sin ||
                        fun.Identifier == FunctionIdentifiers.Cos)
                        return r;
                }               
                if (sc is Power)
                {
                    var pow = (Power)sc;
                    return Elementary.Expand(rest * ApplyPow(pow.Base, pow.Exponent));
                }
                if (sc is Product)
                {
                    var prod = (Product)sc;
                    return Elementary.Expand(rest * ApplyProduct(prod));
                }
                return rest * sc;
            }
            else if (r is Sum)
            {
                Expression s = Number.Zero;
                foreach (Expression t in (Sum)r)
                {
                    if (t is Product || t is Power)
                    {
                        s += ContractInner(t);
                    }
                    else
                    {
                        s += t;
                    }
                }
                return s;
            }
            Expression bas;
            Integer exp;
            if (r.IsPositiveIntegerPower(out bas, out exp) && bas is Function)
            {
                var fun = (Function)bas;
                if (fun.Identifier == FunctionIdentifiers.Sin ||
                    fun.Identifier == FunctionIdentifiers.Cos)
                {
                    return ApplyPow(bas, (Number)exp);
                }
            }
            return r;
        }
        private static void Separate(Expression expression, out Expression sinCosPart, out Expression rest)
        {
            if (expression is Product)
            {
                var prod = (Product)expression;
                List<Expression> s, r;
                prod.Separate((Expression t) => IsSinCosPart(t), out s, out r);
                if (s.Count == 0)
                    sinCosPart = Number.One;
                else if (s.Count == 1)
                    sinCosPart = s[0];
                else
                    sinCosPart = new Product(s);
                if (r.Count == 0)
                    rest = Number.One;
                else if (r.Count == 1)
                    rest = r[0];
                else
                    rest = new Product(r);
                return;
            }
            else if (IsSinCosPart(expression))
            {
                sinCosPart = expression;
                rest = Number.One;
                return;
            }
            else
            {
                sinCosPart = Number.One;
                rest = expression;
                return;
            }
        }
        private static bool IsSinCosPart(Expression expression)
        {
            if (expression is Function)
            {
                var fun = (Function)expression;
                return fun.Identifier == FunctionIdentifiers.Sin ||
                    fun.Identifier == FunctionIdentifiers.Cos;
            }
            Expression bas;
            Integer exp;
            if (expression.IsPositiveIntegerPower(out bas, out exp))
            {
                return IsSinCosPart(bas);
            }
            return false;
        }

        private static Expression ApplyPow(Expression bas, Expression exp)
        {
            Integer num;
            if (exp.IsInteger(out num) && num.Sign >0)
            {
                if (bas is Function)
                {
                    var fun = (Function)bas;
                    if (fun.Identifier == FunctionIdentifiers.Sin)
                    {
                        var n = num.ToInt32();
                        if (num.IsEven)
                        {
                            var u = ((num / 2).IsEven ? Number.One : Number.MinusOne) * Expression.Pow(2, 1 - n);
                            Expression s = Number.Zero;
                            for (int i = 0; i <= n / 2 - 1; i++)
                            {
                                s += (Integer.FromInt32(i).IsEven ? Number.One : Number.MinusOne) * u *
                                    (Number)Integer.Binomial(num, i) * Expression.Cos((Number)(num - 2 * i) * fun.Argument);
                            }
                            return (num.IsEven ? Number.One : Number.MinusOne) * (Number)Integer.Binomial(n, n / 2) / Expression.Pow(2, n) + s;
                        }
                        else
                        {
                            var u = ((num -1 / 2).IsEven ? Number.One : Number.MinusOne) * Expression.Pow(2, 1 - n);
                            Expression s = Number.Zero;
                            for (int i = 0; i <= n / 2; i++)
                            {
                                s += (Integer.FromInt32(i).IsEven ? Number.One : Number.MinusOne) * u *
                                    (Number)Integer.Binomial(num, i) * Expression.Sin((Number)(num - 2 * i) * fun.Argument);
                            }
                            return s;
                        }
                    }
                    else if (fun.Identifier == FunctionIdentifiers.Cos)
                    {
                        var n = num.ToInt32();
                        if (num.IsEven)
                        {
                            var u = Expression.Pow(2, 1 - n);
                            Expression s = Number.Zero;
                            for (int i = 0; i <= n / 2 - 1; i++)
                            {
                                s +=  u * (Number)Integer.Binomial(num, i) * Expression.Cos((Number)(num - 2 * i) * fun.Argument);
                            }
                            return (Number)Integer.Binomial(n, n / 2) / Expression.Pow(2, n) + s;
                        }
                        else
                        {
                            var u = Expression.Pow(2, 1 - n);
                            Expression s = Number.Zero;
                            for (int i = 0; i <= n / 2; i++)
                            {
                                s +=  u * (Number)Integer.Binomial(num, i) * Expression.Cos((Number)(num - 2 * i) * fun.Argument);
                            }
                            return s;
                        }
                    }
                }
            }
            return Expression.Pow(bas, exp);
        }

        private static Expression ApplyProduct(Product product)
        {
            if (product.NumberOfFactors == 2)
            {
                var u = product[0];
                var v = product[1];
                if (u is Power)
                {
                    var pow = (Power)u;
                    return ContractInner(v * ApplyPow(pow.Base, pow.Exponent));
                }
                else if (v is Power)
                {
                    var pow = (Power)v;
                    return ContractInner(u * ApplyPow(pow.Base, pow.Exponent));
                }
                else if (u is Function && v is Function)
                {
                    var fun1 = (Function)u;
                    var fun2 = (Function)v;
                    if (fun1.Identifier == FunctionIdentifiers.Sin &&
                       fun2.Identifier == FunctionIdentifiers.Sin)
                    {
                        return Expression.Cos(fun1.Argument - fun2.Argument) / 2 -
                            Expression.Cos(fun1.Argument + fun2.Argument) / 2;
                    }
                    else if (fun1.Identifier == FunctionIdentifiers.Cos &&
                      fun2.Identifier == FunctionIdentifiers.Cos)
                    {
                        return Expression.Cos(fun1.Argument + fun2.Argument) / 2 +
                            Expression.Cos(fun1.Argument - fun2.Argument) / 2;
                    }
                    else if (fun1.Identifier == FunctionIdentifiers.Sin &&
                      fun2.Identifier == FunctionIdentifiers.Cos)
                    {
                        return Expression.Sin(fun1.Argument + fun2.Argument) / 2 +
                            Expression.Sin(fun1.Argument - fun2.Argument) / 2;
                    }
                    else if (fun1.Identifier == FunctionIdentifiers.Cos &&
                     fun2.Identifier == FunctionIdentifiers.Sin)
                    {
                        return Expression.Sin(fun1.Argument + fun2.Argument) / 2 -
                            Expression.Sin(fun1.Argument - fun2.Argument) / 2;
                    }
                }
                throw new Exception();
            }
            else 
            {
                Expression head;
                List<Expression> list;
                product.Decompose(out head, out list);
                if (list.Count == 1)
                    return ContractInner(head * list[0]);
                else
                    return ContractInner(head * ApplyProduct(new Product(list)));
            }            
        }

        #endregion


        /// <summary>
        /// Simplifies the specified trigonometric expression.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <returns>The simplified expression of <paramref name="expression"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Simplify(this Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(); 
            var r = Fraction.Rationalize(SubstituteToSinCos(expression));
            Expression num, den;
            Fraction.NumeratorDenominator(r, out num, out den);
            return Contract(Expand(num)) / Contract(Expand(den));
        }

        #region Private

        private static Expression SubstituteToSinCos(Expression expression)
        {
            if (expression is Function)
            {
                var fun = (Function)expression;
                var arg = SubstituteToSinCos(fun.Argument);
                if (fun.Identifier == FunctionIdentifiers.Tan)
                {
                    return Expression.Sin(arg) / Expression.Cos(arg);
                }
                else
                    return Function.Create(fun.Identifier, arg);
            }
            else if (expression is Sum)
            {
                var sum = (Sum)expression;
                Expression s = Number.Zero;
                foreach (Expression t in sum)
                {
                    s += SubstituteToSinCos(t);
                }
                return s;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                Expression p = Number.One;
                foreach (Expression t in prod)
                {
                    p *= SubstituteToSinCos(t);
                }
                return p;
            }
            else if (expression is Power)
            {
                var pow = (Power)expression;
                return Expression.Pow(SubstituteToSinCos(pow.Base), SubstituteToSinCos(pow.Exponent));
            }
            else if (expression is MultiArgumentFunction)
            {
                var mulfun = (MultiArgumentFunction)expression;
                return MultiArgumentFunction.Create(mulfun.Identifier, mulfun.Map((Expression t) => SubstituteToSinCos(t)));
            }
            else
                return expression;
        }

        #endregion
    }
}
