/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigDecimal
 * Description : Represents an arbitrarily large decimal.
 * Created     : 2015/6/28
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigDecimal
    {
        /// <summary>
        /// Converts an <see cref="Int32"/> value to a <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigDecimal"/> object that wraps <paramref name="value"/>.</returns>        
        public static BigDecimal FromInt32(int value)
        {
            return new BigDecimal(BigInteger.FromInt32(value), 0);
        }

        /// <summary>
        /// Converts an <see cref="UInt32"/> value to a <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigDecimal"/> object that wraps <paramref name="value"/>.</returns>        
        [CLSCompliant(false)]
        public static BigDecimal FromUInt32(uint value)
        {
            return new BigDecimal(BigInteger.FromUInt32(value), 0);
        }

        /// <summary>
        /// Converts an <see cref="Int64"/> value to a <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigDecimal"/> object that wraps <paramref name="value"/>.</returns>        
        public static BigDecimal FromInt64(long value)
        {
            return new BigDecimal(BigInteger.FromInt64(value), 0);
        }

        /// <summary>
        /// Converts an <see cref="UInt64"/> value to a <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigDecimal"/> object that wraps <paramref name="value"/>.</returns>        
        [CLSCompliant(false)]
        public static BigDecimal FromUInt64(ulong value)
        {
            return new BigDecimal(BigInteger.FromUInt64(value), 0);
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> object to a <see cref="BigDecimal"/> object.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> object to be wrapped.</param>
        /// <returns>A <see cref="BigDecimal"/> object that wraps <paramref name="value"/>.</returns>        
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal FromBigInteger(BigInteger value)
        {
            if (value == null)
                throw new ArgumentNullException();
            return new BigDecimal(value, 0);
        }

        /// <summary>
        /// Returns the product of the <see cref="BigInteger"/> object multiplied by the specified power of 10.
        /// </summary>
        /// <param name="value">The <see cref="BigInteger"/> object.</param>
        /// <param name="scale">The exponent of the power.</param>
        /// <returns>The product of <paramref name="value"/> multiplied by 10 raised to <paramref name="scale"/>.</returns>
        /// <exception cref="ArgumentNullException">The parameter is <see langword="null"/>.</exception>
        public static BigDecimal MultiplyPowerOfTen(BigInteger value, long scale)
        {
            if (value == null)
                throw new ArgumentNullException();
            int rem = (int)(scale % 9);
            scale = scale / 9;
            if (rem < 0)
            {
                scale--;
                rem += 9;
            }
            value = BigInteger.MultiplyPowerOfTen(value, rem);
            return new BigDecimal(value, scale);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="BigDecimal"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains the number to convert.</param>
        /// <returns>A <see cref="BigDecimal"/> object that is equivalent to the number specified in the <paramref name="value"/> parameter. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in the correct format.</exception>
        public static BigDecimal FromString(string value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(value))
                goto fail;
            value = value.Trim();
            var sb = new StringBuilder(value);
            int e = value.IndexOf('e');
            var scale = 0L;
            int dot = value.IndexOf('.');
            if (e > 0)
            {
                sb.Length = e;
                var exp = value.Substring(e + 1);
                scale = long.Parse(exp);
            }
            else if (e == 0)
                throw new ArgumentException();
            if (dot >= 0)
            {
                sb.Remove(dot, 1);
                dot = sb.Length - dot;
                scale -= dot;
            }
            var rem = scale % 9;
            scale = scale / 9;
            if (rem < 0)
            {
                scale--;
                rem = 9 + rem;
            }
            for (long i = 0; i < rem; i++)
                sb.Append('0');
            return new BigDecimal(BigInteger.FromString(sb.ToString()), scale);
        fail:
            throw new FormatException(ExceptionResource.IncorrectStringFormat);
        }

        /// <summary>
        /// Returns the string representation of the <see cref="BigDecimal"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="BigDecimal"/> object.</returns>
        public override string ToString()
        {
            if (Sign == 0)
                return "0";
            var sb = new StringBuilder();
            if (_scale >= 0)
            {
                for (long i = 0L; i < _scale; i++)
                    sb.Insert(0, 0.ToString("D9"));
                for (long i = 0L; i < _intValue.Length - 1; i++)
                    sb.Insert(0, _intValue.Digit(i).ToString("D9"));
                sb.Insert(0, _intValue.Digit(_intValue.Length - 1).ToString());
            }
            else if (-_scale < _intValue.Length)
            {
                for (long i = -_scale; i < _intValue.Length - 1; i++)
                    sb.Insert(0, 0.ToString("D9"));
                sb.Insert(0, _intValue.Digit(_intValue.Length - 1).ToString());
                sb.Append('.');
                for (long i = -_scale - 1; i >= 1; i--)
                    sb.Append(_intValue.Digit(i).ToString("D9"));
                sb.Append(_intValue.Digit(0).ToString("D9").TrimEnd('0'));
            }
            else
            {
                sb.Append("0.");
                for (long i = -_scale - 1; i > _intValue.Length - 1; i--)
                    sb.Append(0.ToString("D9"));
                for (long i = _intValue.Length - 1; i >= 1; i--)
                    sb.Append(_intValue.Digit(i).ToString("D9"));
                sb.Append(_intValue.Digit(0).ToString("D9").TrimEnd('0'));
            }
            if (Sign < 0)
                sb.Insert(0, "-");
            return sb.ToString();
        }

        private string ToStringSci()
        {
            if (Sign == 0)
                return "0";
            var sb = new StringBuilder();
            var str = _intValue.ToString().TrimStart('-');
            var dot = str.Length - 1;
            long r = dot % 9, q = dot / 9;
            var scale = r + (q + _scale) * 9;
            sb.Append(str = str.TrimEnd('0'));
            if ((dot = str.Length - 1) > 0)
                sb.Insert(1, '.'.ToString());
            if (scale != 0)
                sb.Append('e').Append(scale.ToString());
            if (Sign < 0)
                sb.Insert(0, '-'.ToString());
            return sb.ToString();
        }
    }
}
