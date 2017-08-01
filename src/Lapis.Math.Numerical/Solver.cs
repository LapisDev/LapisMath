/********************************************************************************
 * Module      : Lapis.Math.Numerical
 * Class       : Solver
 * Description : Provides methods for solving equations.
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
    /// Provides methods for solving equations.
    /// </summary>
    public static partial class Solver
    {
        /// <summary>
        /// Returns the real solutions of a quadratic equation. An exception is thrown if the equation does not have real solutions.
        /// </summary>
        /// <param name="a">The coefficient of the quadratic term.</param>
        /// <param name="b">The coefficient of the linear term.</param>
        /// <param name="c">The constant term.</param>
        /// <returns>The real solutions of the quadratic equation <paramref name="a"/>*x^2 + <paramref name="b"/>*x + <paramref name="c"/> = 0.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The coefficients are invalid.</exception>
        /// <exception cref="ArithmeticException">The equation does not have real solutions.</exception>
        public static Tuple<double, double> Quadratic(double a, double b, double c)
        {
            if (double.IsNaN(a) || double.IsInfinity(a) || a == 0 ||
                double.IsNaN(b) || double.IsInfinity(b) ||
                double.IsNaN(c) || double.IsInfinity(c))
                throw new ArgumentOutOfRangeException();
            var delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                var x1 = (-b + System.Math.Sqrt(delta)) / a / 2;
                var x2 = (-b - System.Math.Sqrt(delta)) / a / 2;
                return new Tuple<double, double>(x1, x2);
            }
            else if (delta == 0)
            {
                var x = -b / a / 2;
                return new Tuple<double, double>(x, x);
            }
            else
                throw new ArithmeticException(ExceptionResource.NoRealRoot);
        }

        /// <summary>
        /// Returns the complex solutions of a quadratic equation.
        /// </summary>
        /// <param name="a">The coefficient of the quadratic term.</param>
        /// <param name="b">The coefficient of the linear term.</param>
        /// <param name="c">The constant term.</param>
        /// <returns>The complex solutions of the quadratic equation <paramref name="a"/>*x^2 + <paramref name="b"/>*x + <paramref name="c"/> = 0.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The coefficients are invalid.</exception>
        public static Tuple<Complex, Complex> Quadratic(Real a, Real b, Real c)
        {
            if (a == null || b == null || c == null)
                throw new ArgumentNullException();
            if (a.IsNaN || a.IsInfinity || a.IsZero ||
                b.IsNaN || b.IsInfinity ||
                c.IsNaN || c.IsInfinity)
                throw new ArgumentOutOfRangeException();
            var delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                var x1 = (-b + Real.Sqrt(delta)) / a / 2;
                var x2 = (-b - Real.Sqrt(delta)) / a / 2;
                return new Tuple<Complex, Complex>(x1, x2);
            }
            else if (delta == 0)
            {
                var x = -b / a / 2;
                return new Tuple<Complex, Complex>(x, x);
            }
            else
            {
                var x1 = (-b + Real.Sqrt(-delta) * Complex.ImaginaryOne) / a / 2;
                var x2 = (-b - Real.Sqrt(-delta) * Complex.ImaginaryOne) / a / 2;
                return new Tuple<Complex, Complex>(x1, x2);
            }
        }


        /// <summary>
        /// Returns the complex solutions of a cubic equation.
        /// </summary>
        /// <param name="a">The coefficient of the cubic term.</param>
        /// <param name="b">The coefficient of the quadratic term.</param>
        /// <param name="c">The coefficient of the linear term.</param>
        /// <param name="d">The constant term.</param>
        /// <returns>The complex solutions of the cubic equation <paramref name="a"/>*x^3 + <paramref name="b"/>*x^2 + <paramref name="c"/>*x + <paramref name="d"/> = 0.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The coefficients are invalid.</exception>
        public static Tuple<Complex, Complex, Complex> Cubic(Real a, Real b, Real c, Real d)
        {
            if (a == null || b == null || c == null || d == null)
                throw new ArgumentNullException();
            if (a.IsNaN || a.IsInfinity || a.IsZero ||
                b.IsNaN || b.IsInfinity ||
                c.IsNaN || c.IsInfinity ||
                d.IsNaN || d.IsInfinity)
                throw new ArgumentOutOfRangeException();
            var delta1 = b * c / (6 * a * a) - b * b * b / (27 * a * a * a) - d / a / 2;
            var delta2 = c / a / 3 - b * b / (9 * a * a);
            var delta = delta1 * delta1 + delta2 * delta2 * delta2;
            if (delta > 0)
            {
                var t0 = -b / a / 3;
                var t1 = Real.Root(delta1 + Real.Sqrt(delta), 3);
                var t2 = Real.Root(delta1 - Real.Sqrt(delta), 3);
                var x1 = t0 + t1 + t2;
                var x2 = t0 + w1 * t1 + w2 * t2;
                var x3 = t0 + w2 * t1 + w1 * t2;
                return new Tuple<Complex, Complex, Complex>(x1, x2, x3);
            }
            else if (delta == 0)
            {
                if (delta1 == 0)
                {
                    var x = -b / a / 3;
                    return new Tuple<Complex, Complex, Complex>(x, x, x);
                }
                else
                {
                    var t0 = -b / a / 3;
                    var t1 = Real.Pow(delta1, (Real)1 / 3);
                    var x1 = t0 + 2 * t1;    
                    var x2 = t0 - t1;
                    return new Tuple<Complex, Complex, Complex>(x1, x2, x2);
                }
            }
            else
            {
                var t0 = -b / a / 3;
                var t1 = Complex.Pow(delta1 + Complex.Sqrt(delta), (Real)1 / 3);
                var t2 = Complex.Pow(delta1 - Complex.Sqrt(delta), (Real)1 / 3);
                var x1 = t0 + t1 + t2;
                var x2 = t0 + w1 * t1 + w2 * t2;
                var x3 = t0 + w2 * t1 + w1 * t2;
                return new Tuple<Complex, Complex, Complex>(x1, x2, x3);
            }
        }
        static readonly Complex w1 = Complex.FromRectangularCoordinates(-0.5, Real.Sqrt(3) / 2);
        static readonly Complex w2 = Complex.FromRectangularCoordinates(-0.5, -Real.Sqrt(3) / 2);
    }
}
