/********************************************************************************
 * Module      : Lapis.Math.Statistical
 * Class       : Statistics
 * Description : Provides methods for statistics.
 * Created     : 2015/8/29
 * Note        : 
*********************************************************************************/

using Lapis.Math.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Statistical
{
    /// <summary>
    /// Provides methods for statistics.
    /// </summary>
    public static partial class Statistics
    {

        #region double

		/// <summary>
        /// Returns the maximum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The maximum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>       
        public static double Maximum(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var max = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d > max)
                    max = d;
            }
            enumerator.Dispose();
            return max;
        }

        /// <summary>
        /// Returns the minimum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The miniimum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double Minimum(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d < min)
                    min = d;
            }
            enumerator.Dispose();
            return min;
        }

		/// <summary>
        /// Returns the range of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The range of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double Range(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var max = enumerator.Current;
			var min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d > max)
                    max = d;
				if (d < min)
                    min = d;
            }
            enumerator.Dispose();
            return max - min;
        }

		/// <summary>
        /// Returns the sum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The sum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double Sum(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var sum = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                sum += d;
            }
            enumerator.Dispose();
            return sum;
        }

        /// <summary>
        /// Returns the mean of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double Mean(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d - mean) / n;
            }
            enumerator.Dispose();
            return mean;
        }


        /// <summary>
        /// Returns the variance of the samples.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The variance of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static double SampleVariance(IEnumerable<double> data)
        {
            return Variance(data, true);
        }
        /// <summary>
        /// Returns the variance of the population.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The variance of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static double PopulationVariance(IEnumerable<double> data)
        {
            return Variance(data, false);
        }
        private static double Variance(IEnumerable<double> data, bool isSample)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d - mean) / n;
            }
            if (n == 1)
                throw new ArgumentException(ExceptionResource.CountToFew);
            enumerator.Reset();
            enumerator.MoveNext();
            var variance = enumerator.Current - mean;
            variance *= variance;
            while (enumerator.MoveNext())
            {
                var diff = enumerator.Current - mean;
                variance += diff * diff;
            }
            enumerator.Dispose();
            int freeDegree = isSample ? n - 1 : n;
            variance /= freeDegree;
            return variance;
        }

        /// <summary>
        /// Returns the standard deviation of the samples.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The standard deviation of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static double SampleStandardDeviation(IEnumerable<double> data)
        {
            return System.Math.Sqrt(Variance(data, true));
        }
        /// <summary>
        /// Returns the standard deviation of the population.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The standard deviation of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static double PopulationStandardDeviation(IEnumerable<double> data)
        {
            return System.Math.Sqrt(Variance(data, false));
        }


        /// <summary>
        /// Returns the root mean square of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double RootMeanSquare(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d * d - mean) / n;
            }
            enumerator.Dispose();
            return System.Math.Sqrt(mean);
        }


        /// <summary>
        /// Returns the median of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static double Median(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var count = data.Count();
            if (count == 0)
                throw new ArgumentException(ExceptionResource.CountToFew);
            var ordered = data.OrderBy(d => d);
            if ((count & 1) == 0)
                return (ordered.ElementAt(count / 2) + ordered.ElementAt(count / 2 - 1)) / 2;
            else
                return ordered.ElementAt((count - 1)/ 2);
        }

        /// <summary>
        /// Returns the mode of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static IEnumerable<double> Mode(IEnumerable<double> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            int count = 0;
            var dict = new Dictionary<double, int>();
            foreach (var d in data)
            {
                count += 1;
                if (dict.ContainsKey(d))
                    dict[d] += 1;
                else
                    dict.Add(d, 1);
            }
            if (count == 0)
                throw new ArgumentException(ExceptionResource.CountToFew);
            List<double> list = new List<double>();
            var ordered = dict.OrderByDescending(d => d.Value);
            count = 0;
            int value = 0;
            foreach (var d in ordered)
            {
                count++;
                if (d.Value > value)
                    value = d.Value;
                if (d.Value < value)
                    break;
                list.Add(d.Key);
            }
            if (list.Count() == count)
                list.Clear();
            return list;
        }


        /// <summary>
        /// Returns the covariance of the samples.
        /// </summary>
        /// <param name="data1">The collection containing the values of the first variable.</param>
        /// <param name="data2">The collection containing the values of the second variable.</param>
        /// <returns>The covariance of <paramref name="data1"/> and <paramref name="data2"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="data1"/> or <paramref name="data2"/> is empty or contains only one element.</para>
        /// <para>-or-</para>
        /// <para>The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</para>
        /// </exception>
        public static double SampleCovariance(IEnumerable<double> data1, IEnumerable<double> data2)
        {
            return Covariance(data1, data2, true);
        }
        /// <summary>
        /// Returns the covariance of the population.
        /// </summary>
        /// <param name="data1">The collection containing the values of the first variable.</param>
        /// <param name="data2">The collection containing the values of the second variable.</param>
        /// <returns>The covariance of <paramref name="data1"/> and <paramref name="data2"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="data1"/> or <paramref name="data2"/> is empty or contains only one element.</para>
        /// <para>-or-</para>
        /// <para>The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</para>
        /// </exception>
        public static double PopulationCovariance(IEnumerable<double> data1, IEnumerable<double> data2)
        {
            return Covariance(data1, data2, false);
        }
        private static double Covariance(IEnumerable<double> data1, IEnumerable<double> data2, bool isSample)
        {
            if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var enum1 = data1.GetEnumerator();
            var enum2 = data2.GetEnumerator();
            if (!enum1.MoveNext() || !enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var mean1 = enum1.Current;
            var mean2 = enum2.Current;
            int n = 1;
            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                    throw new ArgumentException(ExceptionResource.CountNotEqual);
                n += 1;
                var d1 = enum1.Current;               
                mean1 += (d1 - mean1) / n;
                var d2 = enum2.Current;
                mean2 += (d2 - mean2) / n;
            }
            if (enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountNotEqual);   
            if (n == 1)
                throw new ArgumentException(ExceptionResource.CountToFew);         
            enum1.Reset();
            enum2.Reset();
            enum1.MoveNext();
            enum2.MoveNext();
            var covariance = (enum1.Current - mean1) * (enum2.Current - mean2);
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                var d1 = enum1.Current;
                var diff1 = d1 - mean1;
                var d2 = enum2.Current;
                var diff2 = d2 - mean2;
                covariance += diff1 * diff2;
            }          
            enum1.Dispose();
            enum2.Dispose();
            int freeDegree = isSample ? n - 1 : n;
            covariance /= freeDegree;
            return covariance;
        }

		#endregion

        #region Real

		/// <summary>
        /// Returns the maximum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The maximum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Maximum(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var max = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d > max)
                    max = d;
            }
            enumerator.Dispose();
            return max;
        }

        /// <summary>
        /// Returns the minimum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The miniimum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Minimum(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d < min)
                    min = d;
            }
            enumerator.Dispose();
            return min;
        }

		/// <summary>
        /// Returns the range of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The range of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Range(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var max = enumerator.Current;
			var min = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                if (d > max)
                    max = d;
				if (d < min)
                    min = d;
            }
            enumerator.Dispose();
            return max - min;
        }

		/// <summary>
        /// Returns the sum of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The sum of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Sum(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var sum = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var d = enumerator.Current;
                sum += d;
            }
            enumerator.Dispose();
            return sum;
        }

        /// <summary>
        /// Returns the mean of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Mean(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d - mean) / n;
            }
            enumerator.Dispose();
            return mean;
        }


        /// <summary>
        /// Returns the variance of the samples.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The variance of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static Real SampleVariance(IEnumerable<Real> data)
        {
            return Variance(data, true);
        }
        /// <summary>
        /// Returns the variance of the population.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The variance of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static Real PopulationVariance(IEnumerable<Real> data)
        {
            return Variance(data, false);
        }
        private static Real Variance(IEnumerable<Real> data, bool isSample)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d - mean) / n;
            }
            if (n == 1)
                throw new ArgumentException(ExceptionResource.CountToFew);
            enumerator.Reset();
            enumerator.MoveNext();
            var variance = enumerator.Current - mean;
            variance *= variance;
            while (enumerator.MoveNext())
            {
                var diff = enumerator.Current - mean;
                variance += diff * diff;
            }
            enumerator.Dispose();
            int freeDegree = isSample ? n - 1 : n;
            variance /= freeDegree;
            return variance;
        }

        /// <summary>
        /// Returns the standard deviation of the samples.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The standard deviation of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static Real SampleStandardDeviation(IEnumerable<Real> data)
        {
            return System.Math.Sqrt(Variance(data, true));
        }
        /// <summary>
        /// Returns the standard deviation of the population.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The standard deviation of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty or contains only one element.</exception>
        public static Real PopulationStandardDeviation(IEnumerable<Real> data)
        {
            return System.Math.Sqrt(Variance(data, false));
        }


        /// <summary>
        /// Returns the root mean square of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real RootMeanSquare(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var enumerator = data.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            int n = 1;
            var mean = enumerator.Current;
            while (enumerator.MoveNext())
            {
                n += 1;
                var d = enumerator.Current;
                mean += (d * d - mean) / n;
            }
            enumerator.Dispose();
            return System.Math.Sqrt(mean);
        }


        /// <summary>
        /// Returns the median of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static Real Median(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            var count = data.Count();
            if (count == 0)
                throw new ArgumentException(ExceptionResource.CountToFew);
            var ordered = data.OrderBy(d => d);
            if ((count & 1) == 0)
                return (ordered.ElementAt(count / 2) + ordered.ElementAt(count / 2 - 1)) / 2;
            else
                return ordered.ElementAt((count - 1)/ 2);
        }

        /// <summary>
        /// Returns the mode of a collection of numbers.
        /// </summary>
        /// <param name="data">The collection containing the numbers.</param>
        /// <returns>The mean of <paramref name="data"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="data"/> is empty.</exception>
        public static IEnumerable<Real> Mode(IEnumerable<Real> data)
        {
            if (data == null)
                throw new ArgumentNullException();
            int count = 0;
            var dict = new Dictionary<Real, int>();
            foreach (var d in data)
            {
                count += 1;
                if (dict.ContainsKey(d))
                    dict[d] += 1;
                else
                    dict.Add(d, 1);
            }
            if (count == 0)
                throw new ArgumentException(ExceptionResource.CountToFew);
            List<Real> list = new List<Real>();
            var ordered = dict.OrderByDescending(d => d.Value);
            count = 0;
            int value = 0;
            foreach (var d in ordered)
            {
                count++;
                if (d.Value > value)
                    value = d.Value;
                if (d.Value < value)
                    break;
                list.Add(d.Key);
            }
            if (list.Count() == count)
                list.Clear();
            return list;
        }


        /// <summary>
        /// Returns the covariance of the samples.
        /// </summary>
        /// <param name="data1">The collection containing the values of the first variable.</param>
        /// <param name="data2">The collection containing the values of the second variable.</param>
        /// <returns>The covariance of <paramref name="data1"/> and <paramref name="data2"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="data1"/> or <paramref name="data2"/> is empty or contains only one element.</para>
        /// <para>-or-</para>
        /// <para>The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</para>
        /// </exception>
        public static Real SampleCovariance(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
            return Covariance(data1, data2, true);
        }
        /// <summary>
        /// Returns the covariance of the population.
        /// </summary>
        /// <param name="data1">The collection containing the values of the first variable.</param>
        /// <param name="data2">The collection containing the values of the second variable.</param>
        /// <returns>The covariance of <paramref name="data1"/> and <paramref name="data2"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="data1"/> or <paramref name="data2"/> is empty or contains only one element.</para>
        /// <para>-or-</para>
        /// <para>The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</para>
        /// </exception>
        public static Real PopulationCovariance(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
            return Covariance(data1, data2, false);
        }
        private static Real Covariance(IEnumerable<Real> data1, IEnumerable<Real> data2, bool isSample)
        {
            if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var enum1 = data1.GetEnumerator();
            var enum2 = data2.GetEnumerator();
            if (!enum1.MoveNext() || !enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountToFew);
            var mean1 = enum1.Current;
            var mean2 = enum2.Current;
            int n = 1;
            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                    throw new ArgumentException(ExceptionResource.CountNotEqual);
                n += 1;
                var d1 = enum1.Current;               
                mean1 += (d1 - mean1) / n;
                var d2 = enum2.Current;
                mean2 += (d2 - mean2) / n;
            }
            if (enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountNotEqual);   
            if (n == 1)
                throw new ArgumentException(ExceptionResource.CountToFew);         
            enum1.Reset();
            enum2.Reset();
            enum1.MoveNext();
            enum2.MoveNext();
            var covariance = (enum1.Current - mean1) * (enum2.Current - mean2);
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                var d1 = enum1.Current;
                var diff1 = d1 - mean1;
                var d2 = enum2.Current;
                var diff2 = d2 - mean2;
                covariance += diff1 * diff2;
            }          
            enum1.Dispose();
            enum2.Dispose();
            int freeDegree = isSample ? n - 1 : n;
            covariance /= freeDegree;
            return covariance;
        }

		#endregion

    }
}
