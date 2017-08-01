/********************************************************************************
 * Module      : Lapis.Math.Numbers
 * Class       : Util
 * Description : Provides commonly used methods.
 * Created     : 2015/3/29
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numbers
{
    internal static class Util
    {
        public static bool IsInteger(double value, out int integer)
        {
            if (value <= int.MaxValue && value >= int.MinValue)
            {
                int i = (int)value;
                if (i == value)
                {
                    integer = i;
                    return true;
                }
            }
            integer = default(int);
            return false;
        }

        public static bool IsRational(double value, out int numerator, out int denominator)
        {
            double num = value;
            if (num > int.MaxValue || num < int.MinValue)
                goto fail;
            long den = 1;
            int i = 0;
            while (num % 1 != 0)
            {
                num *= 10;
                if (num > int.MaxValue && num < int.MinValue)
                    goto fail;
                den *= 10;
                if (den > int.MaxValue && den < int.MinValue)
                    goto fail;
                i += 1;
                // Only converts a decimal with fewer than 4 decimal places to a fraction
                if (i > 4)
                    goto fail;
            }
            numerator = (int)num;
            denominator = (int)den;
            return true;
            fail:
            numerator = denominator = default(int);
            return false;
        }
    }
}
