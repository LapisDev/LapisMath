/********************************************************************************
 * 模块名称 : Lapis.Math.Numbers
 * 类 名 称 : FloatingPoint
 * 类 描 述 : 用于小数的表示和运算。
 * 创建日期 : 2015/3/28
 * 备    注 : 原名 Decimal，2015/7/30 修改。
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    public partial class FloatingPoint
    {
        /// <summary>
        /// Adds the values of two specified <see cref="FloatingPoint"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator +(FloatingPoint left, FloatingPoint right)
        {
            return Add(left, right);
        }
        
        /// <summary>
        /// Adds the values of two specified <see cref="FloatingPoint"/> objects.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Add(FloatingPoint left, FloatingPoint right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return new FloatingPoint(left.value + right.value);
        }

        /// <summary>
        /// Subtracts a <see cref="FloatingPoint"/> object from another <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator -(FloatingPoint left, FloatingPoint right)
        {
            return Subtract(left, right);
        }
        
        /// <summary>
        /// Subtracts a <see cref="FloatingPoint"/> object from another <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Subtract(FloatingPoint left, FloatingPoint right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return new FloatingPoint(left.value - right.value);
        }

        /// <summary>
        /// Multiplies two specified <see cref="FloatingPoint"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator *(FloatingPoint left, FloatingPoint right)
        {
            return Multiply(left, right);
        }
        
        /// <summary>
        /// Multiplies two specified <see cref="FloatingPoint"/> objects.
        /// </summary>
        /// <param name="left">The first value to multiply.</param>
        /// <param name="right">The second value to multiply.</param>
        /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Multiply(FloatingPoint left, FloatingPoint right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return new FloatingPoint(left.value * right.value);
        }

        /// <summary>
        /// Divides a specified <see cref="FloatingPoint"/> object by another specified <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator /(FloatingPoint left, FloatingPoint right)
        {
            return Divide(left, right);
        }
        
        /// <summary>
        /// Divides a specified <see cref="FloatingPoint"/> object by another specified <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="left">The value to be divided.</param>
        /// <param name="right">The value to divide by.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Divide(FloatingPoint left, FloatingPoint right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return FloatingPoint.FromDouble(left.value / right.value);
        }

        /// <summary>
        /// Negates a specified <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator -(FloatingPoint value)
        {
            return Negate(value);
        }
        
        /// <summary>
        /// Negates a specified <see cref="FloatingPoint"/> object.
        /// </summary>
        /// <param name="value">The value to negate.</param>       
        /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Negate(FloatingPoint value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new FloatingPoint(-value.value);
        }
        
        /// <summary>
        /// Returns the value of the <see cref="FloatingPoint"/> operand. (The sign of the operand is unchanged.)
        /// </summary>
        /// <param name="value">A <see cref="FloatingPoint"/> object.</param>       
        /// <returns>The value of the <paramref name="value"/> operand.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint operator +(FloatingPoint value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return value.value;
        }

        /// <summary>
        /// Returns the absolute value of a number.
        /// </summary>
        /// <param name="value">The number.</param>       
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static FloatingPoint Abs(FloatingPoint value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (value < 0)
                return -value;
            else
                return value;
        }

        /// <summary>
        /// Returns a specified number raised to the specified integral power.
        /// </summary>
        /// <param name="left">The number to be raised to a power.</param>
        /// <param name="right">The number that specifies a power.</param>
        /// <returns>The number <paramref name="left"/> raised to the power <paramref name="right"/>.</returns> 
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static FloatingPoint Pow(FloatingPoint left, FloatingPoint right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();
            return FloatingPoint.FromDouble(System.Math.Pow(left.value, right.value));
        }
    }
}
