/********************************************************************************
 * Module      : Lapis.Math.Numerical
 * Class       : Integral
 * Description : Provides methods for calculation of numerical integrals.
 * Created     : 2015/10/22
 * Note        : 
*********************************************************************************/

using Lapis.Math.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numerical
{
    /// <summary>
    /// Provides methods for calculation of numerical integrals.
    /// </summary>
    public static class Integral
    {
        /// <summary>
        /// Returns the definite integral of a function within the specified range.
        /// </summary>
        /// <param name="func">The integrand.</param>
        /// <param name="lower">The lower limit.</param>
        /// <param name="upper">The upper limit.</param>
        /// <param name="accuracy">The accuracy of the integral.</param>
        /// <returns>The definite integral of <paramref name="func"/> within the range (<paramref name="lower"/>, <paramref name="upper"/>).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lower"/> or <paramref name="upper"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">The maximum iterations were reached.</exception>
        public static double Romberg(Func<double, double> func, double lower, double upper, double accuracy = 1e-8)
        {
            if (func == null)
                throw new ArgumentNullException();
            if (double.IsNaN(lower) || double.IsInfinity(lower) ||
                double.IsNaN(upper) || double.IsInfinity(upper) ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 100;
            double x;
            double p, s;
            double e = accuracy + 1;
            double r = 0;
            double h = upper - lower;
            int m = 1, n = 1;
            double[] y = new double[MaxIterationCount];
            y[0] = (func(lower) + func(upper)) / 2 * h;
            while (e >= accuracy && m < MaxIterationCount)
            {
                p = 0;
                for (int i = 0; i < n; i++)
                {
                    x = lower + (i + 0.5) * h;
                    p += func(x);
                }
                p = (y[0] + h * p) / 2;
                s = 1;
                for (int k = 1; k < m; k++)
                {
                    s *= 4;
                    r = (s * p - y[k - 1]) / (s - 1);
                    y[k - 1] = p;
                    p = r;
                }
                e = System.Math.Abs(r - y[m - 1]);
                m += 1;
                y[m - 1] = r;
                n *= 2;
                h /= 2;
            }
            if (e >= accuracy)
                throw new ArithmeticException(ExceptionResource.MaxIteration);
            return r;
        }

        /// <summary>
        /// Returns the definite integral of a function within the specified range.
        /// </summary>
        /// <param name="func">The integrand.</param>
        /// <param name="lower">The lower limit.</param>
        /// <param name="upper">The upper limit.</param>
        /// <param name="accuracy">The accuracy of the integral.</param>
        /// <returns>The definite integral of <paramref name="func"/> within the range (<paramref name="lower"/>, <paramref name="upper"/>).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> or <paramref name="lower"/> or <paramref name="upper"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lower"/> or <paramref name="upper"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">The maximum iterations were reached.</exception>
        public static Real Romberg(Func<Real, Real> func, Real lower, Real upper, double accuracy = 1e-8)
        {
            if (func == null ||
                lower == null || upper == null)
                throw new ArgumentNullException();
            if (lower.IsNaN || lower.IsInfinity ||
                upper.IsNaN || upper.IsInfinity ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 10;
            Real x;
            Real p, s;
            double e = accuracy + 1;
            Real r = 0;
            Real h = upper - lower;
            int m = 1, n = 1;
            Real[] y = new Real[MaxIterationCount];
            y[0] = (func(lower) + func(upper)) / 2 * h;
            while (e >= accuracy && m < MaxIterationCount)
            {
                p = 0;
                for (int i = 0; i < n; i++)
                {
                    x = lower + (i + 0.5) * h;
                    p += func(x);
                }
                p = (y[0] + h * p) / 2;
                s = 1;
                for (int k = 1; k < m; k++)
                {
                    s *= 4;
                    r = (s * p - y[k - 1]) / (s - 1);
                    y[k - 1] = p;
                    p = r;
                }
                e = System.Math.Abs(r - y[m - 1]);
                m += 1;
                y[m - 1] = r;
                n *= 2;
                h /= 2;
            }
            if (e >= accuracy)
                throw new ArithmeticException(ExceptionResource.MaxIteration);
            return r;
        }

    }
}
