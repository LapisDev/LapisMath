/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Number
 * Description : Represents a number.
 * Created     : 2015/3/31
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
    /// Represents a number. This class is a wrapper of <see cref="Lapis.Math.Numbers.Real"/>.
    /// </summary>
    public class Number : Expression
    {        
        #region Convertion

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Number"/> object to a <see cref="Lapis.Math.Numbers.Real"/> object.
        /// </summary>
        /// <param name="value">The <see cref="Number"/> to be converted.</param>
        /// <returns>The <see cref="Lapis.Math.Numbers.Real"/> wrapped by <paramref name="value"/>.</returns>
        public static implicit operator Real(Number value)
        {
            if (value == null)
                return null;
            return value._innerNumber;
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Lapis.Math.Numbers.Real"/> to a <see cref="Number"/> object.
        /// </summary>
        /// <param name="value">The <see cref="Numbers.Real"/> to be wrapped.</param>
        /// <returns>A <see cref="Number"/> object that wraps <paramref name="value"/>.</returns>
        /// <exception cref="ArithmeticException"><paramref name="value"/> is <see cref="Numbers.FloatingPoint.NaN"/>, <see cref="Numbers.FloatingPoint.PositiveInfinity"/> or <see cref="Numbers.FloatingPoint.NegativeInfinity"/>.</exception>
        public static explicit operator Number(Numbers.Real value)
        {
            if (value == null)
                return null;
            if (value is Numbers.FloatingPoint)
            {
                var fl = (Numbers.FloatingPoint)value;
                if (fl.IsNaN || fl.IsInfinity)
                    throw new ArithmeticException();
            }
            return new Number(value);
        }
 
        /// <summary>
        /// Converts an <see cref="Int32"/> value to a <see cref="Number"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Number"/> object that wraps <paramref name="value"/>.</returns>
        public static Number FromInt32(int value)
        {
            return (Number)Real.FromInt32(value);
        }
        
        /// <summary>
        /// Converts an <see cref="Double"/> value to a <see cref="Number"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="Number"/> object that wraps <paramref name="value"/>.</returns>
        ///<exception cref="ArithmeticException"><paramref name="value"/> is <see cref="Double.NaN"/>, <see cref="Double.PositiveInfinity"/> or <see cref="Double.NegativeInfinity"/>.</exception>
        public static Number FromDouble(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArithmeticException();
            return (Number)Real.FromDouble(value);
        }

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {            
            return _innerNumber.ToString();
        }

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {            
            return _innerNumber.GetHashCode();
        }
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Number"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Number"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Number)
            {
                return Equals((Number)obj);
            }
            else { return false; }
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(Expression value)
        {
            if (value is Number)
            {
                return Equals((Number)value);
            }
            else { return false; }
        }

        #endregion
            

        #region Instance

        /// <summary>
        /// Represents the number zero (0).
        /// </summary>
        /// <value>The number zero (0).</value>
        public static readonly Number Zero = (Number)Integer.Zero;
        
        /// <summary>
        /// Represents the number one (1).
        /// </summary>
        /// <value>The number one (1).</value>
        public static readonly Number One = (Number)Integer.One;
        
        /// <summary>
        /// Represents the number negative one (-1).
        /// </summary>
        /// <value>The number negative one (-1).</value>
        public static readonly Number MinusOne = (Number)Integer.MinusOne;

        #endregion


        #region Operation

        /// <summary>
        /// Adds the values of two specified <see cref="Number"/> objects.
        /// </summary>
        /// <param name="left">The first number to add.</param>
        /// <param name="right">The second number to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator +(Number left, Number right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            return left._innerNumber + right._innerNumber;
        }

        /// <summary>
        /// Subtracts an <see cref="Number"/> object from another <see cref="Number"/> object.
        /// </summary>
        /// <param name="left">The number to subtract from (the minuend).</param>
        /// <param name="right">The number to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator -(Number left, Number right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            return left._innerNumber - right._innerNumber;
        }

        /// <summary>
        /// Multiplies two specified <see cref="Number"/> objects.
        /// </summary>
        /// <param name="left">The first number to multiply.</param>
        /// <param name="right">The second number to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator *(Number left, Number right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            return left._innerNumber * right._innerNumber;
        }

        /// <summary>
        /// Divides a specified <see cref="Number"/> object by another specified <see cref="Number"/> object.
        /// </summary>
        /// <param name="left">The number to be divided.</param>
        /// <param name="right">The number to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression operator /(Number left, Number right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            return left._innerNumber / right._innerNumber;
        }

        /// <summary>
        /// Returns the specified number raised to the specified power.
        /// </summary>
        /// <param name="left">The number to be raised to a power.</param>
        /// <param name="right">The number that specifies the power.</param>
        /// <returns>The result of <paramref name="left"/> raised to the power <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Expression Pow(Number left, Number right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();      
            return Real.Pow(left._innerNumber, right._innerNumber);
        }

        #endregion


        #region Private

        private readonly Real _innerNumber;

        internal Number(Real number)
        {
            _innerNumber = number;
        }

        private bool Equals(Number value)
        {
            return _innerNumber == value._innerNumber;
        }

        #endregion
    }
}
