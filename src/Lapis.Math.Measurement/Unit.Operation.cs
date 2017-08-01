/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : Unit
 * Description : Provides methods for operations of units.
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
    public partial class Unit
    {
        /// <summary>
        /// Multiplies two specified <see cref="Unit"/> objects.
        /// </summary>
        /// <param name="left">The first unit to multiply.</param>
        /// <param name="right">The second unit to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Unit operator *(Unit left, Unit right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            Expression rate;
            Dictionary<Unit, Expression> dic;
            if (left.HasIdentifier)
            {
                dic = new Dictionary<Unit, Expression>() { { left, 1 } };
                rate = Number.One;
                if (dic.ContainsKey(right))
                {
                    dic[right] += 1;
                    if (dic[right] == Number.Zero)
                    {
                        dic.Remove(right);
                    }
                }
                else if (right.HasIdentifier)
                    dic.Add(right, 1);
                else
                {
                    rate *= right._multiple;
                    foreach (var t in right._innerDictionary)
                    {
                        if (dic.ContainsKey(t.Key))
                        {
                            dic[t.Key] += t.Value;
                            if (dic[t.Key] == Number.Zero)
                            {
                                dic.Remove(t.Key);
                            }
                        }
                        else
                            dic.Add(t.Key, t.Value);
                    }
                }
            }
            else if (right.HasIdentifier)
                return right * left;
            else
            {
                dic = new Dictionary<Unit, Expression>(left._innerDictionary);
                rate = left._multiple * right._multiple;
                foreach (var t in right._innerDictionary)
                {
                    if (dic.ContainsKey(t.Key))
                    {
                        dic[t.Key] += t.Value;
                        if (dic[t.Key] == Number.Zero)
                        {
                            dic.Remove(t.Key);
                        }
                    }
                    else
                        dic.Add(t.Key, t.Value);
                }
            }
            if (rate == 1 && dic.Count == 1 && dic.ElementAt(0).Value == Number.One)
                return dic.ElementAt(0).Key;
            else
                return new Unit(rate, dic);
        }

        /// <summary>
        /// Divides a specified <see cref="Unit"/> object by another specified <see cref="Unit"/> object.
        /// </summary>
        /// <param name="left">The unit to be divided.</param>
        /// <param name="right">The unit to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Unit operator /(Unit left, Unit right)
        {
            return left * Reciprocal(right);
        }

        private static Unit Reciprocal(Unit value)
        {
            if (value == null)
                throw new ArgumentNullException();
            Dictionary<Unit, Expression> dic = new Dictionary<Unit, Expression>();
            Expression rate;
            if (value.HasIdentifier)
            { 
                dic.Add(value, -1);
                rate = Number.One;
            }
            else
            {
                rate = Expression.Reciprocal(value._multiple);
                foreach (var t in value._innerDictionary)
                    dic.Add(t.Key, -t.Value);
            }
            if (rate == 1 && dic.Count == 1 && dic.ElementAt(0).Value == Number.One)
                return dic.ElementAt(0).Key;
            else
                return new Unit(rate, dic);
        }


        /// <summary>
        /// Multiplies a specified <see cref="Unit"/> object by a specified value.
        /// </summary>
        /// <param name="left">The multiplier.</param>
        /// <param name="right">The unit.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator *(Expression left, Unit right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Quantity.Create(left, right);
        }

        /// <summary>
        /// Multiplies a specified <see cref="Quantity"/> object by a specified unit.
        /// </summary>
        /// <param name="left">The <see cref="Quantity"/> object.</param>
        /// <param name="right">The unit.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator *(Quantity left, Unit right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left * Quantity.Create(1, right);
        }

        /// <summary>
        /// Multiplies a specified <see cref="Unit"/> object by a specified value.
        /// </summary>
        /// <param name="left">The unit.</param>
        /// <param name="right">The multiplier.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator *(Unit left, Expression right)
        {
            return right * left;
        }

        /// <summary>
        /// Multiplies a specified unit by a specified <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="left">The unit.</param>
        /// <param name="right">The <see cref="Quantity"/> object.</param>
        /// <returns>The result.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator *(Unit left, Quantity right)
        {
            return right * left;
        }

        /// <summary>
        /// Divides a specified value by another specified <see cref="Unit"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The unit to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator /(Expression left, Unit right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Quantity.Create(left, Reciprocal(right));
        }

        /// <summary>
        /// Divides a specified <see cref="Quantity"/> object by another specified <see cref="Unit"/> object.
        /// </summary>
        /// <param name="left">The <see cref="Quantity"/> object to be divided.</param>
        /// <param name="right">The unit to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator /(Quantity left, Unit right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return left / Quantity.Create(1, right);
        }

        /// <summary>
        /// Divides a specified <see cref="Unit"/> object by another specified value.
        /// </summary>
        /// <param name="left">The unit to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator /(Unit left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Quantity.Create(Expression.Reciprocal(right), left);
        }

        /// <summary>
        /// Divides a specified <see cref="Unit"/> object by another specified <see cref="Quantity"/> object.
        /// </summary>
        /// <param name="left">The unit to be divided.</param>
        /// <param name="right">The <see cref="Quantity"/> object to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Quantity operator /(Unit left, Quantity right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return Quantity.Create(1, left) / right;
        }

        /// <summary>
        /// Returns the specified <see cref="Unit"/> object raised to the specified power.
        /// </summary>
        /// <param name="left">The <see cref="Unit"/> object to be raised to a power.</param>
        /// <param name="right">The expression that specifies the power.</param>
        /// <returns>The unit of <paramref name="left"/> raised to the power <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static Unit Pow(Unit left, Expression right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            if (right == Number.Zero)
                return None;
            else if (right == Number.One)
                return left;
            Expression rate;
            Dictionary<Unit, Expression> dic;
            if (left.HasIdentifier)
            {
                dic = new Dictionary<Unit, Expression>() { { left, right } };
                rate = Number.One;
            }
            else
            {
                dic = new Dictionary<Unit, Expression>();
                rate = Expression.Pow(left._multiple, right);
                foreach (var t in left._innerDictionary)
                {
                    dic.Add(t.Key, t.Value * right);
                }
            }
            if (rate == 1 && dic.Count == 1 && dic.ElementAt(0).Value == Number.One)
                return dic.ElementAt(0).Key;
            else
                return new Unit(rate, dic);
        }
    }
}
