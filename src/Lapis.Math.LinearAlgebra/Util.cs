/********************************************************************************
 * Module      : Lapis.Math.LinearAlgebra
 * Class       : Util
 * Description : Provides commonly used methods.
 * Created     : 2015/5/1
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.LinearAlgebra
{
    internal static class Util
    {
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

        public static bool ItemEqual<T>(T[,] items1, T[,] items2)
          where T : IEquatable<T>
        {
            if (items1.GetLength(0) != items2.GetLength(0) ||
                items1.GetLength(1) != items2.GetLength(1))
                return false;
            else
            {
                for (int i = 0; i < items1.GetLength(0); i++)
                {
                    for (int j = 0; j < items1.GetLength(1); j++)
                    {
                        if (!items1[i, j].Equals(items2[i, j]))
                            return false;
                    }
                }
                return true;
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
        public static T[] Map<T>(this T[] items, Func<T, T> func)
        {
            if (items == null)
                throw new ArgumentNullException();
            else
            {
                T[] r = new T[items.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    r[i] = func(items[i]);
                }
                return r;
            }
        }

        public static T[,] Map<T>(this T[,] items, Func<T, T> func)
        {
            if (items == null)
                throw new ArgumentNullException();
            else
            {
                T[,] r = new T[items.GetLength(0), items.GetLength(1)];
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    for (int j = 0; j < items.GetLength(1); j++)
                    {
                        r[i, j] = func(items[i, j]);                        
                    }
                }
                return r;
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
