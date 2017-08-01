/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Evaluating
 * Description : Provides methods for evaluation of algebraic expressions.
 * Created     : 2015/9/26
 * Note        :
*********************************************************************************/

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Arithmetics
{
    /// <summary>
    /// Provides methods for evaluation of algebraic expressions.
    /// </summary>
    public static class Evaluating
    {
        /// <summary>
        /// Substitutes the specified value into the symbol in the expression and returns the result of the evaluation.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="parameters">The <see cref="IDictionary{Symbol, Real}"/> containing the symbols and values.</param>
        /// <returns>The result of the evaluation.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException">Some symbol has not been set a value.</exception>
        public static Real Evaluate(this Expression expression, IDictionary<Symbol, Real> parameters)
        {
            if (expression == null || parameters == null)
                throw new ArgumentNullException();
            if (expression is Symbol)
            {
                Symbol symbol = (Symbol)expression;
                Real value;
                if (parameters.TryGetValue(symbol, out value))
                {
                    if (value != null)
                        return value;
                    else
                        throw new ArgumentNullException();
                }
                else
                    throw new ArithmeticException(string.Format(ExceptionResource.ParameterValueExpected,
                        symbol.Identifier));
            }
            else if (expression is Number)
                return (Number)expression;
            else if (expression is Sum)
            {
                Sum sum = (Sum)expression;
                Real r = 0;
                foreach (Expression t in sum)
                {
                    r += t.Evaluate(parameters);
                }
                return r;
            }
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                Real r = 1;
                foreach (Expression t in prod)
                {
                    r *= t.Evaluate(parameters);
                }
                return r;
            }
            else if (expression is Power)
            {
                Power pow = (Power)expression;
                var bas = pow.Base.Evaluate(parameters);
                var exp = pow.Exponent.Evaluate(parameters);
                return Real.Pow(bas, exp);
            }
            else if (expression is Function)
            {
                return Evaluate((Function)expression, parameters);
            }
            else if (expression is MultiArgumentFunction)
            {
                return Evaluate((MultiArgumentFunction)expression, parameters);
            }
            else if (expression is PositiveInfinity)
            {
                return FloatingPoint.PositiveInfinity;
            }
            else if (expression is NegativeInfinity)
            {
                return FloatingPoint.NegativeInfinity;
            }
            else if (expression is ComplexInfinity ||
                expression is Undefined)
            {
                return FloatingPoint.NaN;
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        /// <summary>
        /// Substitutes the specified value into the symbol in the expression and returns the result of the evaluation.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="parameters">The <see cref="IDictionary{Symbol, Real}"/> containing the symbols and values.</param>
        /// <returns>The result of the evaluation.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException">Some symbol has not been set a value.</exception>
        public static double Evaluate(this Expression expression, IDictionary<Symbol, double> parameters)
        {
            if (expression == null || parameters == null)
                throw new ArgumentNullException();
            if (expression is Symbol)
            {
                Symbol symbol = (Symbol)expression;
                double value;
                if (parameters.TryGetValue(symbol, out value))
                {
                    return value;
                }
                else
                    throw new ArithmeticException(string.Format(ExceptionResource.ParameterValueExpected,
                        symbol.Identifier));
            }
            else if (expression is Number)
                return (Real)(Number)expression;
            else if (expression is Sum)
            {
                Sum sum = (Sum)expression;
                double r = 0;
                foreach (Expression t in sum)
                {
                    r += t.Evaluate(parameters);
                }
                return r;
            }
            else if (expression is Product)
            {
                Product prod = (Product)expression;
                double r = 1;
                foreach (Expression t in prod)
                {
                    r *= t.Evaluate(parameters);
                }
                return r;
            }
            else if (expression is Power)
            {
                Power pow = (Power)expression;
                var bas = pow.Base.Evaluate(parameters);
                var exp = pow.Exponent.Evaluate(parameters);
                return System.Math.Pow(bas, exp);
            }
            else if (expression is Function)
            {
                return Evaluate((Function)expression, parameters);
            }
            else if (expression is MultiArgumentFunction)
            {
                return Evaluate((MultiArgumentFunction)expression, parameters);
            }
            else if (expression is PositiveInfinity)
            {
                return double.PositiveInfinity;
            }
            else if (expression is NegativeInfinity)
            {
                return double.NegativeInfinity;
            }
            else if (expression is ComplexInfinity ||
                expression is Undefined)
            {
                return double.NaN;
            }
            else
                throw new ArgumentException(ExceptionResource.InvalidExpression);
        }

        #region Private

        private static Real Evaluate(this Function function, IDictionary<Symbol, Real> parameters)
        {
            Real arg = function.Argument.Evaluate(parameters);
            if (function.Identifier == FunctionIdentifiers.Exp)
            {
                return Real.Exp(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Ln)
            {
                return Real.Ln(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sin)
            {
                return Real.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cos)
            {
                return Real.Cos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tan)
            {
                return Real.Tan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cot)
            {
                return Real.Cos(arg) / Real.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sec)
            {
                return 1 / Real.Cos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csc)
            {
                return 1 / Real.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asin)
            {
                return Real.Asin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acos)
            {
                return Real.Acos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Atan)
            {
                return Real.Atan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acot)
            {
                return System.Math.PI / 2 - Real.Atan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sinh)
            {
                return Real.Sinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cosh)
            {
                return Real.Cosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tanh)
            {
                return Real.Tanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Coth)
            {
                return 1 / Real.Tanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sech)
            {
                return 1 / Real.Cosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csch)
            {
                return 1 / Real.Sinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asinh)
            {
                return Real.Ln(arg + Real.Sqrt(arg * arg + 1));
            }
            else if (function.Identifier == FunctionIdentifiers.Acosh)
            {
                return Real.Ln(arg + Real.Sqrt(arg * arg - 1));
            }
            else if (function.Identifier == FunctionIdentifiers.Atanh)
            {
                return Real.Ln((1 + arg) / (1 - arg)) / 2;
            }
            else if (function.Identifier == FunctionIdentifiers.Abs)
            {
                return Real.Abs(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sgn)
            {
                return arg.Sign;
            }
            else
                throw new ArithmeticException();
        }

        private static Real Evaluate(this MultiArgumentFunction mulFunction, IDictionary<Symbol, Real> parameters)
        {
            if (mulFunction.Identifier == FunctionIdentifiers.Log)
            {
                if (mulFunction.NumberOfArguments != 2)
                    throw new ArithmeticException();
                Real arg0 = mulFunction[0].Evaluate(parameters);
                Real arg1 = mulFunction[1].Evaluate(parameters);
                return Real.Log(arg0, arg1);
            }
            else
                throw new ArithmeticException();
        }

        private static double Evaluate(this Function function, IDictionary<Symbol, double> parameters)
        {
            double arg = function.Argument.Evaluate(parameters);
            if (function.Identifier == FunctionIdentifiers.Exp)
            {
                return System.Math.Exp(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Ln)
            {
                return System.Math.Log(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sin)
            {
                return System.Math.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cos)
            {
                return System.Math.Cos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tan)
            {
                return System.Math.Tan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cot)
            {
                return System.Math.Cos(arg) / System.Math.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sec)
            {
                return 1 / System.Math.Cos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csc)
            {
                return 1 / System.Math.Sin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asin)
            {
                return System.Math.Asin(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acos)
            {
                return System.Math.Acos(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Atan)
            {
                return System.Math.Atan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Acot)
            {
                return System.Math.PI / 2 - System.Math.Atan(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sinh)
            {
                return System.Math.Sinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Cosh)
            {
                return System.Math.Cosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Tanh)
            {
                return System.Math.Tanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Coth)
            {
                return 1 / System.Math.Tanh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sech)
            {
                return 1 / System.Math.Cosh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Csch)
            {
                return 1 / System.Math.Sinh(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Asinh)
            {
                return System.Math.Log(arg + System.Math.Sqrt(arg * arg + 1));
            }
            else if (function.Identifier == FunctionIdentifiers.Acosh)
            {
                return System.Math.Log(arg + System.Math.Sqrt(arg * arg - 1));
            }
            else if (function.Identifier == FunctionIdentifiers.Atanh)
            {
                return System.Math.Log((1 + arg) / (1 - arg)) / 2;
            }
            else if (function.Identifier == FunctionIdentifiers.Abs)
            {
                return System.Math.Abs(arg);
            }
            else if (function.Identifier == FunctionIdentifiers.Sgn)
            {
                return System.Math.Sign(arg);
            }
            else
                throw new ArithmeticException();
        }

        private static double Evaluate(this MultiArgumentFunction mulFunction, IDictionary<Symbol, double> parameters)
        {
            if (mulFunction.Identifier == FunctionIdentifiers.Log)
            {
                if (mulFunction.NumberOfArguments != 2)
                    throw new ArithmeticException();
                double arg0 = mulFunction[0].Evaluate(parameters);
                double arg1 = mulFunction[1].Evaluate(parameters);
                return System.Math.Log(arg0, arg1);
            }
            else
                throw new ArithmeticException();
        }

        #endregion
    }
}
