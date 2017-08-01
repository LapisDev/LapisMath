/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : Unit
 * Description : Represents a unit of measure.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Measurement
{
    /// <summary>
    /// Represents a unit of measure.
    /// </summary>
    public partial class Unit : IEquatable<Unit>
    {
        /// <summary>
        /// Represents the unit of a dimensionless value (1).
        /// </summary>
        /// <value>The unit of a dimensionless value (1).</value>
        public static readonly Unit None = new Unit(Number.One, new Dictionary<Unit, Expression>());

        /// <summary>
        /// Creates a basic unit with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>The unit <paramref name="identifier"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid.</exception>  
        public static Unit Base(string identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException();
            if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            return new Unit(identifier);
        }

        /// <summary>
        /// Create a unit with the specified identifier and make the unit equal to another unit.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="unit">The other unit that the new unit equals to.</param>
        /// <returns>The unit <paramref name="identifier"/> that equals to <paramref name="unit"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid.</exception>  
        public static Unit Create(string identifier, Unit unit)
        {
            if (identifier == null || unit == null)
                throw new ArgumentNullException();
            if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            return new Unit(identifier, unit._multiple, unit._innerDictionary);
        }

        /// <summary>
        /// Create a unit with the specified identifier and make the unit equal to a <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="quantity">The <see cref="Quantity"/> object that the new unit equals to.</param>
        /// <returns>The unit <paramref name="identifier"/> that equals to <paramref name="quantity"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="identifier"/> is invalid.</exception>  
        public static Unit Create(string identifier, Quantity quantity)
        {
            if (identifier == null || quantity == null)
                throw new ArgumentNullException();
            if (!Util.IsValidIdentifier(identifier))
                throw new ArgumentException(ExceptionResource.InvalidIdentifier);
            return Create(identifier, FromExpression(quantity.Value) * quantity.Unit);
        }


        #region Conversion

        /// <summary>
        /// Returns the string representation of the <see cref="Unit"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="Unit"/> object.</returns>
        public override string ToString()
        {
            if (HasIdentifier)
            {
                return _identifier;
            }
            else if (this == None)
                return "1";          
            else
            {
                var sb = new StringBuilder();
                if (_multiple != Number.One)
                {
                    sb.Append(_multiple).Append("*");
                }
                foreach (var t in _innerDictionary)
                {
                    sb.Append(t.Key);
                    if (t.Value == Number.One)
                    { }
                    else if (t.Value is Number || t.Value is Symbol)
                    {
                        sb.Append("^").Append(t.Value);
                    }
                    else
                    {
                        sb.Append("^(").Append(t.Value).Append(")");
                    }
                    sb.Append("*");
                }
                sb.Length -= 1;
                return sb.ToString();
            }
        }

        #region Private

        private static Unit FromExpression(Expression value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new Unit(value, None._innerDictionary);
        }

        #endregion

        #endregion


        #region Equality

        /// <summary>
        /// Returns the hash code for the current object. 
        /// </summary>
        /// <returns>The hash code for the current object.</returns>
        public override int GetHashCode()
        {
            Unit u;
            Expression r;
            ToBase(out r, out u);
            return r.GetHashCode();
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Unit"/> object, have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> is <see cref="Unit"/> and its value is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Unit)
            {
                return Equals((Unit)obj);
            }
            else { return false; }
        }

        /// <summary>
        /// Determines whether this instance and a specified <see cref="Unit"/> object have the same value.
        /// </summary>
        /// <param name="value">The <see cref="Unit"/> object to compare to this instance.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="value"/> is the same as the value of this instance; otherwise, <see langword="false"/>.</returns>
        public bool Equals(Unit value)
        {
            if (object.ReferenceEquals(value, null))
                return false;
            Unit u, v;
            Expression r, s;
            ToBase(out r, out u);
            value.ToBase(out s, out v);
            if (u._innerDictionary.Count == 0 && v._innerDictionary.Count == 0)
                return u._identifier == v._identifier && r == s;
            if (Util.ItemEqual(u._innerDictionary, v._innerDictionary) && r == s)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines whether two specified <see cref="Unit"/> objects have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> object to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Unit left, Unit right)
        {
            if (object.ReferenceEquals(left, null))
                if (object.ReferenceEquals(right, null))
                    return true;
                else
                    return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="Unit"/> objects don't have the same value.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> object to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> object to compare.</param>
        /// <returns><see langword="true"/> if the value of <paramref name="left"/> is not the same as the value of <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Unit left, Unit right)
        {
            return !(left == right);
        }

        #endregion
        

        #region Relation

        #region Internal

        internal bool ConvertTo(Unit unit, out Expression value)
        {
            Unit u, v;
            Expression r, s;
            ToBase(out r, out u);
            unit.ToBase(out s, out v);
            if (u == v)
            {
                value = r / s;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        internal void ToBase(out Expression value, out Unit unit)
        {
            if (_innerDictionary.Count == 0)
            {
                Normalize(out value, out unit);
                return;
            }
            var rate = _multiple;
            var dict = new Dictionary<Unit, Expression>();
            Expression v, vv;
            Unit u, uu;
            foreach (var t in _innerDictionary)
            {
                u = t.Key;
                v = t.Value;
                u.ToBase(out vv, out uu);
                rate *= Expression.Pow(vv, v);
                if (uu._innerDictionary.Count == 0)
                {
                    if (dict.ContainsKey(uu))
                    {
                        dict[uu] += v;
                        if (dict[uu] == Number.Zero)
                        {
                            dict.Remove(uu);
                        }
                    }
                    else
                        dict.Add(uu, v);
                }
                foreach (var tt in uu._innerDictionary)
                {
                    if (dict.ContainsKey(tt.Key))
                    {
                        dict[tt.Key] += tt.Value * v;
                        if (dict[tt.Key] == Number.Zero)
                        {
                            dict.Remove(tt.Key);
                        }
                    }
                    else
                        dict.Add(tt.Key, tt.Value * v);
                }
            }
            if (dict.Count == 1 && dict.ElementAt(0).Value == Number.One)
                unit = dict.ElementAt(0).Key;
            else
                unit = new Unit(Number.One, dict);
            value = rate;
        }

        internal void Normalize(out Expression value, out Unit unit)
        {
            if (HasIdentifier)
            {
                value = Number.One;
                unit = this;
            }
            else if (_innerDictionary.Count == 0)
            {
                value = _multiple;
                unit = new Unit(_identifier, Number.One, _innerDictionary);
            }
            else
            {
                value = _multiple;
                unit = new Unit(Number.One, _innerDictionary);
            }
        }

        #endregion

        #region Private

        private Expression _multiple;

        private Dictionary<Unit, Expression> _innerDictionary;

        private void ToBaseInner(out Dictionary<Unit, Expression> units, out Expression multiple)
        {           
            
            var uni = new Dictionary<Unit, Expression>();
            var mul = _multiple;
            Unit unit;
            Expression exp;
            Dictionary<Unit, Expression> dict;
            Expression e;
            foreach (var t in _innerDictionary)
            {
                unit = t.Key;
                exp = t.Value;
                if (unit._innerDictionary.Count == 0)
                {
                    mul *= unit._multiple * exp;
                    if (uni.ContainsKey(unit))
                    {
                        uni[unit] += exp;
                        if (uni[unit] == Number.Zero)
                        {
                            uni.Remove(unit);
                        }
                    }
                    else
                        uni.Add(unit, exp);
                    continue;
                }
                unit.ToBaseInner(out dict, out e);
                mul *= Expression.Pow(e, exp);
                foreach (var u in dict)
                {
                    if (uni.ContainsKey(u.Key))
                    {
                        uni[u.Key] += u.Value * exp;
                        if (uni[u.Key] == Number.Zero)
                        {
                            uni.Remove(u.Key);
                        }
                    }
                    else
                        uni.Add(u.Key, u.Value * exp);
                }
            }
            units = uni;
            multiple = mul;
        }

        #endregion      

        #endregion


        private string _identifier;
        
        internal bool HasIdentifier { get { return _identifier != null; } }

        
        #region Constructors

        private Unit(string identifier)
        {
            _multiple = 1;
            _identifier = identifier;
            _innerDictionary = new Dictionary<Unit, Expression>();
        }

        private Unit(Expression multiple, Dictionary<Unit, Expression> dict)
        {           
            _multiple = multiple;
            _innerDictionary = dict;
        }

        private Unit(string identifier, Expression multiple, Dictionary<Unit, Expression> dict)
        {
            _identifier = identifier;
            _multiple = multiple;
            _innerDictionary = dict;
        }

        #endregion
    }
}
