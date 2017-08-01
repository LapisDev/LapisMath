/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Expression
 * Description : Represents an algebraic expression.
 * Created     : 2015/3/31
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra.Expressions
{
    /// <summary>
    /// Represents an algebraic expression. This class is abstract.
    /// </summary>
    public abstract partial class Expression : IEquatable<Expression>
    {
        #region Convertion

        /// <summary>
        /// Returns the string representation of the algebraic expression.
        /// </summary>
        /// <returns>The string representation of the algebraic expression.</returns>
        public override string ToString()
        {
            return ExpressionWriter.WriteFriendly(this);
            // return ExpressionWriter.WriteStrict(this);
        }

        /// <summary>
        /// Returns the string representation of the algebraic expression in the specified format.
        /// </summary>
        /// <param name="format">The format in which the algebraic expression is converted to a string.</param>
        /// <returns>The string representation of the algebraic expression.</returns>
        public string ToString(ExpressionFormat format)
        {
            return ExpressionWriter.Write(this, format);
        }

        /// <summary>
        /// Defines an implicit conversion of <see cref="Int32"/> to <see cref="Expression"/>.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> to be wrapped.</param>
        /// <returns>An <see cref="Expression"/> that wraps <paramref name="value"/>.</returns>
        public static implicit operator Expression(int value)
        {            
            return new Number(value);
        }

        /// <summary>
        /// Defines an implicit conversion of <see cref="Double"/> to <see cref="Expression"/>.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> to be wrapped.</param>
        /// <returns>An <see cref="Expression"/> that wraps <paramref name="value"/>.</returns>
        public static implicit operator Expression(double value)
        {
            if (double.IsNaN(value))
                return Undefined.Instance;
            if (double.IsPositiveInfinity(value))
                return PositiveInfinity.Instance;
            if (double.IsNegativeInfinity(value))
                return NegativeInfinity.Instance;
            return Number.FromDouble(value);
        }

        /// <summary>
        /// Defines an implicit conversion of <see cref="Lapis.Math.Numbers.Real"/> to <see cref="Expression"/>.
        /// </summary>
        /// <param name="value">The <see cref="Numbers.Real"/> to be wrapped.</param>
        /// <returns>An <see cref="Expression"/> that wraps <paramref name="value"/>.</returns>
        public static implicit operator Expression(Numbers.Real value)
        {
            if (value == null)
                return null;
            if (value is Numbers.FloatingPoint)
            {
                var fl = (Numbers.FloatingPoint)value;
                if (fl.IsNaN)
                    return Undefined.Instance;
                if (fl.IsPositiveInfinity)
                    return PositiveInfinity.Instance;
                if (fl.IsNegativeInfinity)
                    return NegativeInfinity.Instance;
            }
            return (Number)value;
        }

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public abstract override int GetHashCode();
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="Expression"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Expression"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Expression)
                return Equals((Expression)obj);
            else
                return false;
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Expression"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Expression"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public abstract bool Equals(Expression value);

        /// <summary>
        /// Determines whether two specified <see cref="Expression"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Expression"/> object to compare.</param>
        /// <param name="right">The second <see cref="Expression"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Equal(Expression left,Expression right)
        {
            return left == right;
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Expression"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Expression"/> object to compare.</param>
        /// <param name="right">The second <see cref="Expression"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Expression left, Expression right)
        {
            if (left.IsNull())
                if (right.IsNull())
                    return true;
                else
                    return false;
            
            return left.Equals(right);
        }
        
        /// <summary>
        /// Determines whether two specified <see cref="Expression"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Expression"/> object to compare.</param>
        /// <param name="right">The second <see cref="Expression"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Expression left, Expression right)
        {
            return !(left == right);           
        }

        #endregion


        internal Expression() { }                
    }
}
