/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Pattern
 * Description : Provides methods for pattern matching for algebraic expressions.
 * Created     : 2015/4/6
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
    /// Provides methods for pattern matching for algebraic expressions.
    /// </summary>
    public static partial class Pattern
    {        
        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is an integer.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="integer">When this method returns, contains the integer that <paramref name="expression"/> represents. This parameter is passed uninitialized; any value originally supplied in <paramref name="integer"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> represents an integer; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsInteger(this Expression expression, out Integer integer)
        {
            if (expression == null)
                throw new ArgumentNullException();  
            if (expression is Number)
            {
                Real num = (Real)(Number)expression;
                if (num is Integer)
                {
                    integer = (Integer)num;
                    return true;
                }
            }
            integer = null;
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a rational number.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="rational">When this method returns, contains the rational number that <paramref name="expression"/> represents. This parameter is passed uninitialized; any value originally supplied in <paramref name="rational"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> represents a rational number; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsRational(this Expression expression, out Rational rational)
        {
            if (expression == null)
                throw new ArgumentNullException(); 
            if (expression is Number)
            {
                Real num = (Real)(Number)expression;
                if (num is Rational)
                {
                    rational = (Rational)num;
                    return true;
                }
            }
            rational = null;
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a positive integer exponentiation.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="base">When this method returns, contains the base of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="base"/> will be overwritten.</param>
        /// <param name="exponent">When this method returns, contains the exponent of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="exponent"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> represents a positive integer exponentiation; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsPositiveIntegerPower(this Expression expression,
            out Expression @base, out Integer exponent)
        {
            if (expression == null)
                throw new ArgumentNullException(); 
            if (expression is Power)
            {
                var power = (Power)expression;
                Integer integer;
                if (power.Exponent.IsInteger(out integer))
                {                    
                    if (integer.Sign > 0)
                    {
                        @base = power.Base;
                        exponent = integer;
                        return true;
                    }
                }
            }
            @base = null;
            exponent = null;
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a negative integer exponentiation.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="base">When this method returns, contains the base of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="base"/> will be overwritten.</param>
        /// <param name="exponent">When this method returns, contains the exponent of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="exponent"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> represents a negative integer exponentiation; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsNegativeIntegerPower(this Expression expression,
            out Expression @base, out Integer exponent)
        {
            if (expression == null)
                throw new ArgumentNullException(); 
            if (expression is Power)
            {
                var power = (Power)expression;
                Integer integer;
                if (power.Exponent.IsInteger(out integer))
                {
                    if (integer.Sign < 0)
                    {
                        @base = power.Base;
                        exponent = integer;
                        return true;
                    }
                }
            }
            @base = null;
            exponent = null;
            return false;
        }

        /// <summary>
        /// Returns a value that indicates whether the algebraic expression is a negative rational exponentiation.
        /// </summary>
        /// <param name="expression">The algebraic expression.</param>
        /// <param name="base">When this method returns, contains the base of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="base"/> will be overwritten.</param>
        /// <param name="exponent">When this method returns, contains the exponent of the exponential expression. This parameter is passed uninitialized; any value originally supplied in <paramref name="exponent"/> will be overwritten.</param>
        /// <returns><see langword="true"/> if <paramref name="expression"/> represents a negative rational exponentiation; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static bool IsNegativeRationalPower(this Expression expression,
            out Expression @base, out Rational exponent)
        {
            if (expression == null)
                throw new ArgumentNullException(); 
            if (expression is Power)
            {
                var power = (Power)expression;
                Rational rational;
                if (power.Exponent.IsRational(out rational))
                {                    
                    if (rational.Sign < 0)
                    {
                        @base = power.Base;
                        exponent = rational;
                        return true;
                    }
                }
            }
            @base = null;
            exponent = null;
            return false;
        }
    }
}
