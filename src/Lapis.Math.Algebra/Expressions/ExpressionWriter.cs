/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : ExpressionWriter
 * Description : Provides methods for conversion of an algebraic expression to a string.
 * Created     : 2015/5/16
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Expressions
{

    /// <summary>
    /// Represents the format in which an algebraic expression is converted to a string.
    /// </summary>
    public enum ExpressionFormat
    {
        /// <summary>
        /// Represents the human friendly format.
        /// </summary>
        Friendly,
        /// <summary>
        /// Represents the strict format.
        /// </summary>
        Strict
    }

    static class ExpressionWriter
    {
        public static string Write(this Expression expression, ExpressionFormat format)
        {
            if (format == ExpressionFormat.Strict)
                return expression.WriteStrict();
            else if (format == ExpressionFormat.Friendly)
                return expression.WriteFriendly();
            else
                return expression.Write(default(ExpressionFormat));
        }


        #region Friendly

        public static string WriteFriendly(this Expression expression)
        {        
            if (expression is Number ||
                expression is Symbol ||
                expression is Undefined ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity)
            {
                return expression.ToString();
            }
            else if (expression is Sum)
            {
                return WriteFriendly((Sum)expression);
            }
            else if (expression is Product)
            {
                return WriteFriendly((Product)expression);
            }
            else if (expression is Power)
            {
                return WriteFriendly((Power)expression);
            }
            else if (expression is Function)
            {
                return WriteFriendly((Function)expression);
            }
            else if (expression is MultiArgumentFunction)
            {
                return WriteFriendly((MultiArgumentFunction)expression);
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);            
        }



        internal static string WriteFriendly(this Sum sum)
        {
            var sb = new StringBuilder();  
            
            var head = sum[0];
            if (head is Number)
                sb.Append(head.WriteFriendly());
            else if (head is Product)
            {
                var prod = (Product)head;
                if (prod[0] is Number &&
                    ((Real)(Number)prod[0]).Sign < 0)
                {
                    var list = ((Product)prod).ToList();
                    list[0] = -list[0];
                    var p = new Product(list);
                    sb.Append("-").Append(p.WriteFriendly());
                }
                else
                    sb.Append(prod.WriteFriendly());
            }
            else if (head.Priority() <= sum.Priority())
                sb.Append("(").Append(head.WriteFriendly()).Append(")");
            else
                sb.Append(head.WriteFriendly());
            for (int i = 1; i < sum.NumberOfAddends; i++)
            {
                if (sum[i] is Number)
                {
                    var num = (Real)(Number)sum[i];
                    if (num.Sign < 0)
                        sb.Append(" - ").Append((-num).ToString());
                    else
                        sb.Append(" + ").Append((num).ToString());
                }
                else if (sum[i] is Product)
                {
                    var prod = (Product)sum[i];
                    if (prod[0] is Number &&
                        ((Real)(Number)prod [0]).Sign<0 )
                    {
                        var list = ((Product)prod).ToList();
                        list[0] = -list[0];
                        var p = new Product(list);
                        sb.Append(" - ").Append(p.WriteFriendly());
                    }
                    else
                        sb.Append(" + ").Append(prod.WriteFriendly());
                }
                else if (sum[i].Priority() <= sum.Priority())
                    sb.Append(" + ").Append("(").Append(sum[i].WriteFriendly()).Append(")");
                else
                    sb.Append(" + ").Append(sum[i].WriteFriendly());
            }
            return sb.ToString();
        }

        private static void NumDen(this Expression expression, 
            out Expression numerator, out Expression denominator)
        {
            Expression bas;
            Rational exp;
            if (Arithmetics.Pattern.IsNegativeRationalPower(expression,out bas,out exp))
            {
                numerator = Number.One;
                denominator = Expression.Pow(bas, (Number)(-exp));                
                return;
            }
            else if (expression is Product)
            {
                var prod = (Product)expression;
                var listn = new List<Expression>();
                var listd = new List<Expression>();
                Expression n, d;
                foreach (Expression t in prod)
                {
                    t.NumDen(out n, out d);
                    if (n != Number.One)
                        listn.Add(n);
                    if (d != Number.One)
                        listd.Add(d);
                }
                if (listn.Count == 0)
                    numerator = Number.One;
                else if (listn.Count == 1)
                    numerator = listn[0];
                else
                    numerator = new Product(listn);
                if (listd.Count == 0)
                    denominator = Number.One;
                else if (listd.Count == 1)
                    denominator = listd[0];
                else
                    denominator = new Product(listd);
                return;
            }
            else
            {
                numerator = expression;
                denominator = Number.One;
                return;
            }
        }

        internal static string WriteFriendly(this Product product)
        {            
            var sb = new StringBuilder();         

            Expression num, den;
            product.NumDen(out num, out den);
            if (den == Number.One)
            {
                if (num is Product)
                {
                    var prod = (Product)num;
                    if (prod[0] is Number &&
                        ((Real)(Number)prod[0]).Sign < 0)
                    {
                        var list = ((Product)prod).ToList();
                        list[0] = -list[0];
                        var p = new Product(list);
                        sb.Append("-").Append(p.WriteFriendly());
                    }
                    else
                    {
                        foreach (Expression expr in prod)
                        {
                            if (expr.Priority() <= product.Priority())
                                sb.Append("(").Append(expr.WriteFriendly()).Append(")");
                            else
                                sb.Append(expr.WriteFriendly());
                            sb.Append(" * ");
                        }
                        sb.Length -= 3;
                        return sb.ToString();
                    }
                }
                else
                    return num.WriteFriendly();
            }
            else
            {
                if (num.Priority() <= product.Priority())
                    sb.Append("(").Append(num.WriteFriendly()).Append(")");
                else
                    sb.Append(num.WriteFriendly());
                sb.Append(" / ");
                if (den.Priority() <= product.Priority())
                    sb.Append("(").Append(den.WriteFriendly()).Append(")");
                else
                    sb.Append(den.WriteFriendly());
            }
            return sb.ToString();
        }

        internal static string WriteFriendly(this Power power)
        {
            var sb = new StringBuilder();
            Expression bas;
            Integer exp;
            if (Arithmetics.Pattern.IsNegativeIntegerPower(power, out bas, out exp))
            {
                sb.Append(Integer.One.ToString()).Append(" / ");
                if (exp == Integer.MinusOne)
                {
                    if (bas.Priority() <= 2)
                        sb.Append("(").Append(bas.WriteFriendly()).Append(")");
                    else
                        sb.Append(bas.WriteFriendly());
                }
                else
                {
                    if (bas.Priority() <= 3)
                        sb.Append("(").Append(bas.WriteFriendly()).Append(")");
                    else
                        sb.Append(bas.WriteFriendly());
                    sb.Append(" ^ ").Append((-exp).ToString());
                }
            }
            else
            {
                if (power.Base.Priority() <= power.Priority())
                    sb.Append("(").Append(power.Base.WriteFriendly()).Append(")");
                else
                    sb.Append(power.Base.WriteFriendly());
                sb.Append(" ^ ");
                if (power.Exponent.Priority() <= power.Priority())
                    sb.Append("(").Append(power.Exponent.WriteFriendly()).Append(")");
                else
                    sb.Append(power.Exponent.WriteFriendly());
            }
            return sb.ToString();
        }

        internal static string WriteFriendly(this Function function)
        {
            var sb = new StringBuilder();
            sb.Append(function.Identifier).
                Append("(").
                Append(function.Argument.WriteFriendly()).
                Append(")");
            return sb.ToString();
        }

        internal static string WriteFriendly(this MultiArgumentFunction mulFunction)
        {
            var sb = new StringBuilder();
            sb.Append(mulFunction.Identifier).Append("(");
            foreach (Expression expr in mulFunction)
            {
                sb.Append(expr.WriteFriendly()).Append(", ");
            }
            sb.Length -= 2;
            sb.Append(")");
            return sb.ToString();
        }

        #endregion
                
        #region Strict

        public static string WriteStrict(this Expression expression)
        {
            if (expression is Number ||
                expression is Symbol ||
                expression is Undefined ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity)
            {
                return expression.ToString();
            }
            else if (expression is Sum)
            {
                return WriteStrict((Sum)expression);
            }
            else if (expression is Product)
            {
                return WriteStrict((Product)expression);
            }
            else if (expression is Power)
            {
                return WriteStrict((Power)expression);
            }
            else if (expression is Function)
            {
                return WriteStrict((Function)expression);
            }
            else if (expression is MultiArgumentFunction)
            {
                return WriteStrict((MultiArgumentFunction)expression);
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }
               

        internal static string WriteStrict(this Sum sum)
        {
            var sb = new StringBuilder();
            foreach (Expression expr in sum)
            {
                if (expr.Priority() <= sum.Priority())
                    sb.Append("(").Append(expr.WriteStrict()).Append(")");
                else
                    sb.Append(expr.WriteStrict());
                sb.Append(" + ");
            }
            sb.Length -= 3;
            return sb.ToString();
        }

        internal static string WriteStrict(this Product product)
        {
            var sb = new StringBuilder();
            foreach (Expression expr in product)
            {
                if (expr.Priority() <= product.Priority())
                    sb.Append("(").Append(expr.WriteStrict()).Append(")");
                else
                    sb.Append(expr.WriteStrict());
                sb.Append(" * ");
            }
            sb.Length -= 3;
            return sb.ToString();
        }

        internal static string WriteStrict(this Power power)
        {
            var sb = new StringBuilder();
            if (power.Base.Priority() <= power.Priority())
                sb.Append("(").Append(power.Base.WriteStrict()).Append(")");
            else
                sb.Append(power.Base.WriteStrict());
            sb.Append(" ^ ");
            if (power.Exponent.Priority() <= power.Priority())
                sb.Append("(").Append(power.Exponent.WriteStrict()).Append(")");
            else
                sb.Append(power.Exponent.WriteStrict());
            return sb.ToString();
        }

        internal static string WriteStrict(this Function function)
        {
            var sb = new StringBuilder();
            sb.Append(function.Identifier).
                Append("(").
                Append(function.Argument.WriteStrict()).
                Append(")");
            return sb.ToString();
        }

        internal static string WriteStrict(this MultiArgumentFunction mulFunction)
        {
            var sb = new StringBuilder();
            sb.Append(mulFunction.Identifier).Append("(");
            foreach (Expression expr in mulFunction)
            {
                sb.Append(expr.WriteStrict()).Append(", ");
            }
            sb.Length -= 2;
            sb.Append(")");
            return sb.ToString();
        }

        #endregion


        internal static int Priority(this Expression expression)
        {
            if (expression is Number)
            {
                var num = (Real)(Number)expression;
                if (num.Sign < 0)
                    return 1;
                else if (num is Fraction)
                    return 2;
                else
                    return 4;
            }
            else if (expression is Sum)
                return 1;
            else if (expression is Product)
                return 2;
            else if (expression is Power)
                return 3;
            else if (expression is Function ||
                expression is MultiArgumentFunction ||                
                expression is Symbol ||
                expression is PositiveInfinity ||
                expression is NegativeInfinity ||
                expression is ComplexInfinity ||
                expression is Undefined)
                return 4;
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }  
    }
}
