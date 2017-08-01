/********************************************************************************
 * Module      : Lapis.Math.Numbers.BigNumbers
 * Class       : BigInteger
 * Description : Represents an arbitrarily large interger.
 * Created     : 2015/6/25
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Numbers.BigNumbers
{
    public partial class BigInteger
    {
        /// <summary>
        /// Converts an <see cref="Int32"/> value to a <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigInteger"/> object that wraps <paramref name="value"/>.</returns>        
        public static BigInteger FromInt32(int value)
        {
            var sign = value.CompareTo(0);
            if (sign == 0)
                return Zero;
            var n = value > 0 ? value : -value;
            var digit0 = n % BaseInt;
            var digit1 = n / BaseInt;
            int[] data;
            if (digit1 == 0)
                data = new int[1] { digit0 };
            else
                data = new int[2] { digit0, digit1 };
            return new BigInteger(sign, data);
        }

        /// <summary>
        /// Converts an <see cref="UInt32"/> value to a <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigInteger"/> object that wraps <paramref name="value"/>.</returns>        
        [CLSCompliant(false)]
        public static BigInteger FromUInt32(uint value)
        {
            var sign = value.CompareTo(0);
            if (sign == 0)
                return Zero;
            var n = value;
            var digit0 = n % BaseInt;
            var digit1 = n / BaseInt;
            int[] data;
            if (digit1 == 0)
                data = new int[1] { (int)digit0 };
            else
                data = new int[2] { (int)digit0, (int)digit1 };
            return new BigInteger(sign, data);
        }

        /// <summary>
        /// Converts an <see cref="Int64"/> value to a <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigInteger"/> object that wraps <paramref name="value"/>.</returns>        
        public static BigInteger FromInt64(long value)
        {
            var sign = value.CompareTo(0);
            if (sign == 0)
                return Zero;
            var n = value > 0 ? value : -value;
            var digit0 = n % BaseInt;
            n = n / BaseInt;
            var digit1 = n % BaseInt;
            n = n / BaseInt;
            var digit2 = n % BaseInt;
            var digit3 = n / BaseInt;
            int[] data;
            if (digit3 != 0)
                data = new int[4] { (int)digit0, (int)digit1, (int)digit2, (int)digit3 };
            else if (digit2 != 0)
                data = new int[3] { (int)digit0, (int)digit1, (int)digit2 };
            else if (digit1 != 0)
                data = new int[2] { (int)digit0, (int)digit1 };
            else
                data = new int[1] { (int)digit0 };          
            return new BigInteger(sign, data);
        }

        /// <summary>
        /// Converts an <see cref="UInt64"/> value to a <see cref="BigInteger"/> object.
        /// </summary>
        /// <param name="value">The value to be wrapped.</param>
        /// <returns>A <see cref="BigInteger"/> object that wraps <paramref name="value"/>.</returns>        
        [CLSCompliant(false)]
        public static BigInteger FromUInt64(ulong value)
        {
            var sign = value.CompareTo(0);
            if (sign == 0)
                return Zero;
            var n = value;
            var digit0 = n % BaseInt;
            n = n / BaseInt;
            var digit1 = n % BaseInt;
            n = n / BaseInt;
            var digit2 = n % BaseInt;
            var digit3 = n / BaseInt;
            int[] data;
            if (digit3 != 0)
                data = new int[4] { (int)digit0, (int)digit1, (int)digit2, (int)digit3 };
            else if (digit2 != 0)
                data = new int[3] { (int)digit0, (int)digit1, (int)digit2 };
            else if (digit1 != 0)
                data = new int[2] { (int)digit0, (int)digit1 };
            else
                data = new int[1] { (int)digit0 };      
            return new BigInteger(sign, data);
        }

        /// <summary>
        /// Returns the string representation of the <see cref="BigInteger"/> object.
        /// </summary>
        /// <returns>The string representation of the <see cref="BigInteger"/> object.</returns>
        public override string ToString()
        {
            if (Sign == 0)
                return "0";
            var sb = new StringBuilder();
            for (long i = 0; i < Length - 1; i++)
                sb.Insert(0, _data[i].ToString("D9"));
            sb.Insert(0, _data[Length - 1].ToString());
            if (Sign < 0)
                sb.Insert(0, "-");           
            return sb.ToString();
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="BigInteger"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains the number to convert.</param>
        /// <returns>A <see cref="BigInteger"/> object that is equivalent to the number specified in the <paramref name="value"/> parameter. </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in the correct format.</exception>
        public static BigInteger FromString(string value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(value))
                goto fail;
            value = value.Trim();
            int sign, start;
            if (value[0] == '-')
            {
                sign = -1;
                start = 1;
            }
            else
            {
                sign = 1;
                start = 0;
            }
            int[] data = new int[value.Length / 9 + 1];
            var sb = new StringBuilder();
            char c;
                     
            int count = 0;
            for (int i = value.Length - 1; i >= start; i--)
            {
                c = value[i];
                if (char.IsDigit(c))
                    sb.Insert(0, c.ToString());
                else
                    goto fail;
                if (sb.Length == 9)
                {
                    data[count] = int.Parse(sb.ToString());
                    sb.Clear();
                    count++;
                }
            }
            if (sb.Length > 0)
            {
                data[count] = int.Parse(sb.ToString());
                sb.Clear();               
            }         
            return new BigInteger(sign, data);
        fail:
            throw new FormatException(ExceptionResource.IncorrectStringFormat);
        }
    }
}
