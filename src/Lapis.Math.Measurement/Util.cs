/********************************************************************************
 * Module      : Lapis.Math.Measurement
 * Class       : Util
 * Description : Provides commonly used methods.
 * Created     : 2015/5/22
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lapis.Math.Measurement
{
    static class Util
    {
        public static bool IsValidIdentifier(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return false; }
            else if (!char.IsLetter(value[0])) { return false; }
            else
            {
                foreach (char c in value)
                {
                    if (!char.IsLetter(c) && !char.IsDigit(c))
                        return false;
                }
            }
            return true;
        }

        public static bool ItemEqual<TKey, TValue>(IDictionary<TKey, TValue> dict1, IDictionary<TKey, TValue> dict2)
            where TKey : IEquatable<TKey>
            where TValue: IEquatable<TValue>
        {
            if (dict1.Count != dict2.Count)
                return false;
            else
            {
                var set1 = new HashSet<TKey>(dict1.Keys);
                var set2 = new HashSet<TKey>(dict2.Keys);
                if (set1.SetEquals(set2) &&
                    set1.All(t => dict1[t].Equals(dict2[t])))
                    return true;
                else
                    return false;
            }
        }
    }
}
