/********************************************************************************
 * Module      : Lapis.Math.Numerical
 * Class       : Solver
 * Description : Provides methods for solving equations.
 * Created     : 2015/10/23
 * Note        : 
*********************************************************************************/

using Lapis.Math.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lapis.Math.Numerical
{
    public static partial class Solver
    {
        /// <summary>
        /// Solve the equation in the specified range with the bisection method.
        /// </summary>
        /// <param name="func">The equation to solve.</param>
        /// <param name="lower">The lower limit.</param>
        /// <param name="upper">The upper limit.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>A solution of <paramref name="func"/> within the range [<paramref name="lower"/>, <paramref name="upper"/>].</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lower"/> or <paramref name="upper"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">Cannot find the solution, or the maximum iterations were reached.</exception>
        public static double Bisection(Func<double, double> func, double lower, double upper, double accuracy = 1e-8)
        {
            if (func == null)
                throw new ArgumentNullException();
            if (double.IsNaN(lower) || double.IsInfinity(lower) ||
                double.IsNaN(upper) || double.IsInfinity(upper) ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 100;
            double f1 = func(lower);
            if (System.Math.Abs(f1) < accuracy)
                return lower;
            double f2 = func(upper);
            if (System.Math.Abs(f2) < accuracy)
                return upper;
            if (System.Math.Sign(f1) == System.Math.Sign(f2))
                throw new ArithmeticException(ExceptionResource.CannotFindRoot);
            double x = (lower + upper) / 2;
            for (int i = 0; i <= MaxIterationCount; i++)
            {
                if (System.Math.Abs(f1 - f2) < accuracy / 2 &&
                    upper == lower)
                    return x;
                if (lower == x || upper == x)
                    throw new ArithmeticException(ExceptionResource.CannotFindRoot);
                double fmid = func(x);
                if (System.Math.Sign(f1) == System.Math.Sign(fmid))
                {
                    lower = x;
                    f1 = fmid;
                }
                else if (System.Math.Sign(f2) == System.Math.Sign(fmid))
                {
                    upper = x;
                    f2 = fmid;
                }
                else
                    return x;
                x = (lower + upper) / 2;
            }
            throw new ArithmeticException(ExceptionResource.MaxIteration);
        }

        /// <summary>
        /// Solve the equation in the specified range with the bisection method.
        /// </summary>
        /// <param name="func">The equation to solve.</param>
        /// <param name="lower">The lower limit.</param>
        /// <param name="upper">The upper limit.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>A solution of <paramref name="func"/> within the range [<paramref name="lower"/>, <paramref name="upper"/>].</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> or <paramref name="lower"/> or <paramref name="upper"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="lower"/> or <paramref name="upper"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">Cannot find the solution, or the maximum iterations were reached.</exception>
        public static Real Bisection(Func<Real, Real> func, Real lower, Real upper, double accuracy = 1e-8)
        {
            if (func == null ||
                lower == null || upper == null)
                throw new ArgumentNullException();
            if (lower.IsNaN || lower.IsInfinity ||
                upper.IsNaN || upper.IsInfinity ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 100;
            var f1 = func(lower);
            if (System.Math.Abs(f1) < accuracy)
                return lower;
            var f2 = func(upper);
            if (System.Math.Abs(f2) < accuracy)
                return upper;
            if (f1.Sign == f2.Sign)
                throw new ArithmeticException(ExceptionResource.CannotFindRoot);
            Real x = (lower + upper) / 2;
            for (int i = 0; i <= MaxIterationCount; i++)
            {
                if (System.Math.Abs(f1 - f2) < accuracy / 2 &&
                    upper == lower)
                    return x;
                if (lower == x || upper == x)
                    throw new ArithmeticException(ExceptionResource.CannotFindRoot);
                var fmid = func(x);
                if (f1.Sign == fmid.Sign)
                {
                    lower = x;
                    f1 = fmid;
                }
                else if (f2.Sign == fmid.Sign)
                {
                    upper = x;
                    f2 = fmid;
                }
                else
                    return x;
                x = (lower + upper) / 2;
            }
            throw new ArithmeticException(ExceptionResource.MaxIteration);
        }

        /// <summary>
        /// Solve the equation with the secant method.
        /// </summary>
        /// <param name="func">The equation to solve.</param>
        /// <param name="initial">An initial value.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>A solution of <paramref name="func"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initial"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">Cannot find the solution, or the maximum iterations were reached.</exception>
        public static double Secant(Func<double, double> func, double initial, double accuracy = 1e-8)
        {
            if (func == null)
                throw new ArgumentNullException();
            if (double.IsNaN(initial) || double.IsInfinity(initial) ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 100;
            const double InitialStep = 1e-4;
            double x0 = initial;
            double x1 = initial + InitialStep;
            double f0 = func(x0);
            double f1 = func(x1);
            for (int i = 0; i < MaxIterationCount; i++)
            {
                double step = f1 * (x1 - x0) / (f1 - f0);
                x0 = x1;
                f0 = f1;
                x1 -= step;
                f1 = func(x1);
                if (System.Math.Abs(step) < accuracy && System.Math.Abs(f1) < accuracy)
                    return x1;
            }
            throw new ArithmeticException(ExceptionResource.MaxIteration);
        }

        /// <summary>
        /// Solve the equation with the secant method.
        /// </summary>
        /// <param name="func">The equation to solve.</param>
        /// <param name="initial">An initial value.</param>
        /// <param name="accuracy">The accuracy.</param>
        /// <returns>A solution of <paramref name="func"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> or <paramref name="initial"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initial"/> or <paramref name="accuracy"/> is invalid.</exception>
        /// <exception cref="ArithmeticException">Cannot find the solution, or the maximum iterations were reached.</exception>
        public static Real Secant(Func<Real, Real> func, Real initial, double accuracy = 1e-8)
        {
            if (func == null || initial == null)
                throw new ArgumentNullException();
            if (initial.IsNaN || initial.IsInfinity ||
                double.IsNaN(accuracy) || double.IsInfinity(accuracy) ||
                accuracy <= 0)
                throw new ArgumentOutOfRangeException();
            const int MaxIterationCount = 100;
            const double InitialStep = 1e-4;
            var x0 = initial;
            var x1 = initial + InitialStep;
            var f0 = func(x0);
            var f1 = func(x1);
            for (int i = 0; i < MaxIterationCount; i++)
            {
                var step = f1 * (x1 - x0) / (f1 - f0);
                x0 = x1;
                f0 = f1;
                x1 -= step;
                f1 = func(x1);
                if (System.Math.Abs(step) < accuracy && System.Math.Abs(f1) < accuracy)
                    return x1;
            }
            throw new ArithmeticException(ExceptionResource.MaxIteration);
        }
    }
}
