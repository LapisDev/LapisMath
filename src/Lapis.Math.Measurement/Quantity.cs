/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : Quantity
 * Description : Represents a number with a unit of measure.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.Numbers;

namespace Lapis.Math.Measurement
{

    /// <summary>
    /// Represents a number with a unit of measure.
    /// </summary>
    public class Quantity : IEquatable<Quantity>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public Expression Value { get; private set; }

        /// <summary>
        /// Gets the unit of measure.
        /// </summary>
        /// <value>The unit of measure.</value>
        public Unit Unit { get; private set; }

        /// <summary>
        /// Creates a <see cref="Quantity"/> object using the specified value and unit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>A <see cref="Quantity"/> object of <paramref name="value"/> with the unit <paramref name="unit"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity Create(Expression value, Unit unit)
        {
            if (value == null || unit == null)
                throw new ArgumentNullException();
            Expression v;
            Unit u;
            unit.Normalize(out v, out u);
            return new Quantity(value * v, u);
        }

        #region Conversion

        /// <summary>
        /// Returns the string representation of the <see cref="Quantity"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Quantity"/> object.</returns>
        public override string ToString()
        {
            if (Unit == Unit.None)
                return Value.ToString();
            if (Value is Number)
            {
                var num = (Real)(Number)Value;
                if (num is Fraction)
                    return string.Format("({0}) {1}", Value.ToString(), Unit.ToString());
                else
                    return string.Format("{0} {1}", Value.ToString(), Unit.ToString());
            }
            else if (Value is Symbol)
                return string.Format("{0} {1}", Value.ToString(), Unit.ToString());
            else
                return string.Format("({0}) {1}", Value.ToString(), Unit.ToString());
        }

        /// <summary>
        /// Defines an implicit conversion of a dimensionless value to a <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="Quantity"/> object that wraps <paramref name="value"/>.</returns>
        public static implicit operator Quantity(Expression value)
        {
            if (value == null)
                return null;
            return Quantity.Create(value, Unit.None);
        }

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Unit.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Quantity"/> object, have the same value and unit.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Quantity"/> and its value and unit are the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Quantity)
            {
                return Equals((Quantity)obj);
            }
            else if (obj is Expression)
                return Equals((Quantity)obj);
            else { return false; }
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Quantity"/> object have the same value and unit.
        /// </summary>
        /// <param name="value">The <see cref="Quantity"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value and unit of <paramref name="value"/> are the same as the value and unit of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Quantity value)
        {
            if (object.ReferenceEquals(value, null))
                return false;
            if (Value == value.Value && Unit == value.Unit)
                return true;
            else
            {
                Expression r;
                return Unit.ConvertTo(value.Unit, out r) &&
                   r * Value == value.Value;
            }
        }

        /// <summary>
        /// Determines whether two specified <see cref="Quantity"/> objects have the same value and unit.
        /// </summary>
        /// <param name="left">The first <see cref="Quantity"/> object to compare.</param>
        /// <param name="right">The second <see cref="Quantity"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value and unit of <paramref name="left"/> are the same as the value and unit of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Quantity left, Quantity right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="Quantity"/> objects don't have the same value and unit.
        /// </summary>
        /// <param name="left">The first <see cref="Quantity"/> object to compare.</param>
        /// <param name="right">The second <see cref="Quantity"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value and unit of <paramref name="left"/> are not the same as the value and unit of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Quantity left, Quantity right)
        {
            return !(left == right);
        }

        #endregion


        #region Operation

        /// <summary>
        /// Returns the value of the <see cref="Quantity"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">The quantity.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator +(Quantity value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value;
        }

        /// <summary>
        /// Negates the <see cref="Quantity"/> value.
        /// </summary>
        /// <param name="value">The quantity to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator -(Quantity value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Quantity(-value.Value, value.Unit);
        }

        /// <summary>
        /// Adds the values of two specified <see cref="Quantity"/> objects.
        /// </summary>
        /// <param name="left">The first quantity to add.</param>
        /// <param name="right">The second quantity to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException">The dimensions of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Quantity operator +(Quantity left, Quantity right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            Expression rate;
            if (left.Unit.ConvertTo(right.Unit, out rate))
            {
                return new Quantity(left.Value * rate + right.Value, right.Unit);
            }
            throw new ArithmeticException(ExceptionResource.DimensionNotMatch);
        }

        /// <summary>
        /// Subtracts an <see cref="Quantity"/> object from another <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="left">The quantity to subtract from (the minuend).</param>
        /// <param name="right">The quantity to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArithmeticException">The dimensions of <paramref name="left"/> and <paramref name="right"/> don't match.</exception>
        public static Quantity operator -(Quantity left, Quantity right)
        {
            return left + (-right);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Quantity"/> objects.
        /// </summary>
        /// <param name="left">The first quantity to multiply.</param>
        /// <param name="right">The second quantity to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator *(Quantity left, Quantity right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            Expression value;
            Unit unit;
            (left.Unit * right.Unit).Normalize(out value, out unit);
            return new Quantity(left.Value * right.Value * value, unit);
        }

        /// <summary>
        /// Divides a specified <see cref="Quantity"/> object by another specified <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="left">The quantity to be divided.</param>
        /// <param name="right">The quantity to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator /(Quantity left, Quantity right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            Expression value;
            Unit unit;
            (left.Unit / right.Unit).Normalize(out value, out unit);
            return new Quantity(left.Value / right.Value * value, unit);
        }

        /// <summary>
        /// Returns the specified quantity raised to the specified power.
        /// </summary>
        /// <param name="left">The quantity to be raised to a power.</param>
        /// <param name="right">The expression that specifies the power.</param>
        /// <returns>The quantity of <paramref name="left"/> raised to the power <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity Pow(Quantity left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            Expression value;
            Unit unit;
            Unit.Pow(left.Unit, right).Normalize(out value, out unit);
            return new Quantity(Expression.Pow(left.Value, right) * value, unit);
        }

        #endregion


        internal Quantity(Expression value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}
