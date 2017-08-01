/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : Util
 * Description : Provides commonly used methods.
 * Created     : 2015/3/31
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Algebra
{
    internal static class Util
    {
        #region String

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

        #endregion


        #region List and Enumerable               

        public static bool ItemEqual<T>(IEnumerable<T> items1, IEnumerable<T> items2)
            where T : IEquatable<T>
        {
            if (items1.Count() != items2.Count())
                return false;
            else
            {
                for (int i = 0; i < items1.Count(); i++)
                {
                    if (!items1.ElementAt(i).Equals(items2.ElementAt(i)))
                        return false;
                }
                return true;
            }
        }

        public static int ItemCompare<T>(IEnumerable<T> items1, IEnumerable<T> items2)
          where T : IComparable<T>
        {
            int cmp;
            for (int i = 0; i < items1.Count() && i < items2.Count(); i++)
            {
                cmp = items1.ElementAt(i).CompareTo(items2.ElementAt(i));
                if (cmp != 0)
                    return cmp;
            }
            return items1.Count().CompareTo(items2.Count());
        }
        public static int ItemCompare<T>(IEnumerable<T> items1, IEnumerable<T> items2, IComparer<T> comparer)
        {
            int cmp;
            for (int i = 0; i < items1.Count() && i < items2.Count(); i++)
            {
                cmp = comparer.Compare(items1.ElementAt(i), items2.ElementAt(i));
                if (cmp != 0)
                    return cmp;
            }
            return items1.Count().CompareTo(items2.Count());
        }

        public static bool Decompose<T>(this IEnumerable<T> items, out T head, out List<T> tail)
        {
            if (items != null && items.Count() > 0)
            {
                head = items.ElementAt(0);
                var list = new List<T>();
                for (int i = 1; i < items.Count(); i++)
                    list.Add(items.ElementAt(i));
                tail = list;
                return true;
            }
            else
            {
                head = default(T);
                tail = null;
                return false;
            }
        }

        public static List<T> Map<T>(this IEnumerable<T> items, Func<T, T> func)
        {
            if (items == null)
                throw new ArgumentNullException();
            else
            {
                var list = new List<T>();
                foreach (T item in items)
                {
                    list.Add(func(item));
                }
                return list;
            }
        }

        public static void Separate<T>(this IEnumerable<T> items, Predicate<T> predicate, out List<T> trueItems, out List<T> falseItems)
        {
            if (items == null)
                throw new ArgumentNullException();
            else
            {
                trueItems = new List<T>();
                falseItems = new List<T>();
                foreach (T item in items)
                {
                    if (predicate(item))
                    {
                        trueItems.Add(item);
                    }
                    else
                    {
                        falseItems.Add(item);
                    }
                }                
            }
        }

        #endregion


        public static bool IsNull(this object obj)
        {
            return object.ReferenceEquals(obj, null);
        }
        public static bool NullCompare<T>(T left, T right)
        {
            return object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null);
        }
    }
}
