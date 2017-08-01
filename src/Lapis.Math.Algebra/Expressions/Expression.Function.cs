/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Expression
 * Description : Provides basic elementary functions of algebraic expressions.
 * Created     : 2015/5/16
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Algebra.Arithmetics;
using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Expressions
{
    public partial class Expression
    {
        /// <summary>
        /// Returns e raised to the specified power expression.
        /// </summary>
        /// <param name="value">The expression specifying a power.</param>
        /// <returns>The expression of e raised to <paramref name="value"/>.</returns>       
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Exp(Expression value)
        {
            if (value == Number.Zero)
                return Number.One;
            var func = value as Function;
            if (func != null && func.Identifier == FunctionIdentifiers.Ln)
            {
                return func.Argument;
            }
            var sum = value as Sum;
            if (sum != null)
            {
                Expression a = Number.One;
                Expression e = Number.Zero;
                foreach (var addend in sum)
                {
                    var f = addend as Function;
                    if (f != null && f.Identifier == FunctionIdentifiers.Ln)
                    {
                        a *= f.Argument;
                    }
                    else
                    {
                        e += addend;
                    }
                }
                return a * Function.Create(FunctionIdentifiers.Exp, e);
            }
            var prod = value as Product;
            if (prod != null)
            {
                Expression a = null;
                Expression e = Number.One;
                foreach (var factor in prod)
                {
                    if (a == null)
                    {
                        var f = factor as Function;
                        if (f != null && f.Identifier == FunctionIdentifiers.Ln)
                        {
                            a = f.Argument;
                            continue;
                        }
                    }
                    e *= factor;
                }
                if (a != null)
                    return Expression.Pow(a, e);
            }
            return Function.Create(FunctionIdentifiers.Exp, value);
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of the specified expression.
        /// </summary>
        /// <param name="value">The expression whose logarithm is to be found.</param>
        /// <returns>The expression of the natural logarithm of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Ln(Expression value)
        {
            if (value == Number.One)
                return Number.Zero;
            var num = value as Number;
            if (num != null && (Real)num <= Number.Zero)
            {
                return Undefined.Instance;
            }
            var func = value as Function;
            if (func != null && func.Identifier == FunctionIdentifiers.Exp)
            {
                return func.Argument;
            }
            var prod = value as Product;
            if (prod != null)
            {
                Expression a = null;
                Expression e = Number.One;
                foreach (var factor in prod)
                {
                    if (a == null)
                    {
                        var f = factor as Function;
                        if (f != null && f.Identifier == FunctionIdentifiers.Exp)
                        {
                            a = f.Argument;
                            continue;
                        }
                    }
                    e *= factor;
                }
                if (a != null)
                    return a + Function.Create(FunctionIdentifiers.Ln, e);
            }
            return Function.Create(FunctionIdentifiers.Ln, value);
        }

        /// <summary>
        /// Returns the base 10 logarithm of the specified expression.
        /// </summary>
        /// <param name="value">The expression whose logarithm is to be found.</param>
        /// <returns>The expression of the base 10 logarithm of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Lg(Expression value)
        {
            if (value == Number.One)
                return Number.Zero;
            var num = value as Number;
            if (num != null && (Real)num <= Number.Zero)
            {
                return Undefined.Instance;
            }
            return Function.Create(FunctionIdentifiers.Lg, value);
        }

        /// <summary>
        /// Returns the logarithm of the specified expression in the specified base.
        /// </summary>
        /// <param name="value">The expression whose logarithm is to be found.</param>
        /// <param name="base">The base of the logarithm.</param>
        /// <returns>The expression of the logarithm of <paramref name="value"/> in <paramref name="base"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Log(Expression value, Expression @base)
        {
            var num = value as Number;
            if (num != null && (Real)num <= Number.Zero)
            {
                return Undefined.Instance;
            }
            num = @base as Number;
            if (num != null && (Real)num <= Number.Zero)
            {
                return Undefined.Instance;
            }
            return Ln(value) / Ln(@base);
            // return MultiArgumentFunction.Create(FunctionIdentifiers.Log, value, 10);
        }

        /// <summary>
        /// Returns the absolute value of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the absolute value of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Abs(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign < 0)
                    return (Number)(-num);
                else
                    return value;
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                Expression head;
                List<Expression> list;
                Integer num;
                prod.Decompose(out head, out list);
                if (head.IsInteger(out num))
                {
                    if (list.Count == 1)
                        return Abs(head) * Function.Create(FunctionIdentifiers.Abs, list[0]);
                    else
                        return Abs(head) * Function.Create(FunctionIdentifiers.Abs, new Product(list));
                }
                else
                    return Function.Create(FunctionIdentifiers.Abs, value);
            }
            else
                return Function.Create(FunctionIdentifiers.Abs, value);
        }

        /// <summary>
        /// Returns the sign of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the sign of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sgn(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign < 0)
                    return Number.MinusOne;
                else if (num.Sign == 0)
                    return Number.Zero;
                else
                    return Number.One;
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                Expression head;
                List<Expression> list;
                Integer num;
                prod.Decompose(out head, out list);
                if (head.IsInteger(out num))
                {
                    if (list.Count == 1)
                        return Sgn(head) * Function.Create(FunctionIdentifiers.Sgn, list[0]);
                    else
                        return Sgn(head) * Function.Create(FunctionIdentifiers.Sgn, new Product(list));
                }
                else
                    return Function.Create(FunctionIdentifiers.Sgn, value);
            }
            else
                return Function.Create(FunctionIdentifiers.Sgn, value);
        }

        /// <summary>
        /// Returns the sine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sin(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Sin, -value);
                else
                    return Function.Create(FunctionIdentifiers.Sin, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Sin, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Sin,
                            new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Sin, value);
        }

        /// <summary>
        /// Returns the cosine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Cos(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign == 0)
                    return Number.One;
                else if (num.Sign < 0)
                    return Function.Create(FunctionIdentifiers.Cos, -value);
                else
                    return Function.Create(FunctionIdentifiers.Cos, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return Function.Create(FunctionIdentifiers.Cos, list[0]);
                    else
                        return Function.Create(FunctionIdentifiers.Cos, new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Cos, value);
        }

        /// <summary>
        /// Returns the tangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Tan(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Tan, -value);
                else
                    return Function.Create(FunctionIdentifiers.Tan, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Tan, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Tan, new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Tan, value);
        }

        /// <summary>
        /// Returns the cotangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the cotangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Cot(Expression value)
        {
            return 1 / Tan(value);
            // return Function.Create(FunctionIdentifiers.Cot, value);
        }

        /// <summary>
        /// Returns the secant of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the secant of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sec(Expression value)
        {
            return 1 / Cos(value);
            // return Function.Create(FunctionIdentifiers.Sec, value);
        }

        /// <summary>
        /// Returns the cosecant of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the cosecant of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Csc(Expression value)
        {
            return 1 / Sin(value);
            // return Function.Create(FunctionIdentifiers.Csc, value);
        }

        /// <summary>
        /// Returns the arcsine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the arcsine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Asin(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num <= Number.MinusOne || num >= Number.One)
                {
                    return Undefined.Instance;
                }
                if (num == Number.MinusOne)
                {
                    return NegativeInfinity.Instance;
                }
                if (num == Number.One)
                {
                    return PositiveInfinity.Instance;
                }
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Asin, -value);
                else
                    return Function.Create(FunctionIdentifiers.Asin, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Asin, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Asin,
                            new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Asin, value);
        }

        /// <summary>
        /// Returns the arccosine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the arccosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Acos(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num <= Number.MinusOne || num >= Number.One)
                {
                    return Undefined.Instance;
                }
                if (num == Number.MinusOne)
                {
                    return NegativeInfinity.Instance;
                }
                if (num == Number.One)
                {
                    return PositiveInfinity.Instance;
                }
            }          
            return Function.Create(FunctionIdentifiers.Acos, value);
        }

        /// <summary>
        /// Returns the arctangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the arctangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Atan(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;                
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Atan, -value);
                else
                    return Function.Create(FunctionIdentifiers.Atan, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Atan, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Atan,
                            new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Atan, value);
        }

        /// <summary>
        /// Returns the arccotangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the arccotangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Acot(Expression value)
        {
            if (value is PositiveInfinity)
            {
                return Number.Zero;
            }
            return Function.Create(FunctionIdentifiers.Acot, value);
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sinh(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;              
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Sinh, -value);
                else
                    return Function.Create(FunctionIdentifiers.Sinh, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Sinh, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Sinh,
                            new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Sinh, value);
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Cosh(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign == 0)
                    return Number.One;
                else if (num.Sign < 0)
                    return Function.Create(FunctionIdentifiers.Cosh, -value);
                else
                    return Function.Create(FunctionIdentifiers.Cosh, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return Function.Create(FunctionIdentifiers.Cosh, list[0]);
                    else
                        return Function.Create(FunctionIdentifiers.Cosh, new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Cosh, value);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Tanh(Expression value)
        {
            if (value is Number)
            {
                var num = (Real)(Number)value;
                if (num.Sign == 0)
                    return Number.Zero;
                else if (num.Sign < 0)
                    return -Function.Create(FunctionIdentifiers.Tanh, -value);
                else
                    return Function.Create(FunctionIdentifiers.Tanh, value);
            }
            else if (value is Product)
            {
                var prod = (Product)value;
                List<Expression> list = prod.ToList();
                Expression head = list[0];
                if (head is Number && (Real)(Number)head < 0)
                {
                    if (head == Number.MinusOne)
                        list.RemoveAt(0);
                    else
                        list[0] = -head;
                    if (list.Count == 1)
                        return -Function.Create(FunctionIdentifiers.Tanh, list[0]);
                    else
                        return -Function.Create(FunctionIdentifiers.Tanh, new Product(list));
                }
            }
            return Function.Create(FunctionIdentifiers.Tanh, value);
        }

        /// <summary>
        /// Returns the hyperbolic cotangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic cotangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Coth(Expression value)
        {
            return 1 / Tanh(value);
            // return Function.Create(FunctionIdentifiers.Coth, value);
        }

        /// <summary>
        /// Returns the hyperbolic secant of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic secant of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sech(Expression value)
        {
            return 1 / Cosh(value);
            // return Function.Create(FunctionIdentifiers.Sech, value);
        }

        /// <summary>
        /// Returns the hyperbolic cosecant of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the hyperbolic cosecant of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Csch(Expression value)
        {
            return 1 / Sinh(value);
            // return Function.Create(FunctionIdentifiers.Csch, value);
        }

        /// <summary>
        /// Returns the inverse hyperbolic sine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the inverse hyperbolic sine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Asinh(Expression value)
        {
            return Ln(value + Sqrt(Pow(value, 2) + Number.One));
            // return Function.Create(FunctionIdentifiers.Asinh, value);
        }

        /// <summary>
        /// Returns the inverse hyperbolic cosine of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the inverse hyperbolic cosine of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Acosh(Expression value)
        {
            return Ln(value + Sqrt(Pow(value, 2) - Number.One));
            // return Function.Create(FunctionIdentifiers.Acosh, value);
        }

        /// <summary>
        /// Returns the inverse hyperbolic tangent of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the inverse hyperbolic tangent of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Atanh(Expression value)
        {
            return Ln((value + Number.One) / (value - Number.One)) / 2;
            // return Function.Create(FunctionIdentifiers.Atanh, value);
        }

        /// <summary>
        /// Returns the square root of the specified expression.
        /// </summary>
        /// <param name="value">The expression.</param>
        /// <returns>The expression of the square root of <paramref name="value"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Sqrt(Expression value)
        {
            return Pow(value, 0.5);
        }
    }

    static class FunctionIdentifiers
    {
        public const string Exp = "exp";

        public const string Log = "log";

        public const string Lg = "lg";

        public const string Ln = "ln";

        public const string Abs = "abs";

        public const string Sgn = "sgn";

        public const string Sin = "sin";

        public const string Cos = "cos";

        public const string Tan = "tan";

        public const string Sec = "sec";

        public const string Csc = "csc";

        public const string Cot = "cot";

        public const string Asin = "asin";

        public const string Acos = "acos";

        public const string Atan = "atan";

        public const string Acot = "acot";

        public const string Sinh = "sinh";

        public const string Cosh = "cosh";

        public const string Tanh = "tanh";

        public const string Sech = "sech";

        public const string Csch = "csch";

        public const string Coth = "coth";

        public const string Asinh = "asinh";

        public const string Acosh = "acosh";

        public const string Atanh = "atanh";
    }
}
