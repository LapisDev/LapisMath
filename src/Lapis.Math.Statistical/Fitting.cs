/********************************************************************************
 * Module      : Lapis.Math.Statistical
 * Class       : Fitting
 * Description : Provides methods for fitting.
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
    /// Provides methods for fitting.
    /// </summary>
	public static partial class Fitting
    {

        #region double

        /// <summary>
        /// Applies the linear (y = p1 * x + p0) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (p0 and p1).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Linear(IEnumerable<double> data1, IEnumerable<double> data2)
        {
            return Polynomial(data1, data2, 1);
        }
        
        /// <summary>
        /// Applies the linear (y = p1 * x + p0) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared value.</param>
        /// <returns>An array that contains the fitting parameters (p0 and p1).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Linear(IEnumerable<double> data1, IEnumerable<double> data2, out double rSquare)
        {
            return Polynomial(data1, data2, 1, out rSquare);
        }

        /// <summary>
        /// Applies the polynomial (y = pn * x ^ n + ... + p1 * x + p0) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="degree">The degree of the polynomial.</param>
        /// <returns>An array that contains the fitting parameters (p0, p1, ... and pn).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Polynomial(IEnumerable<double> data1, IEnumerable<double> data2, int degree)
        {
		    if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            double[,] gauss = new double[degree + 1, degree + 2];
            for (int i = 0; i < degree + 1; i++)
            {
                int j;
                for (j = 0; j < degree + 1; j++)
                {
                    gauss[i, j] = PowerSum(data1, j + i);
                }
                gauss[i, j] = PowerSum(data1, i, data2);
            }
            return Gauss(gauss, degree + 1);
        }
        /// <summary>
        /// Applies the polynomial (y = pn * x ^ n + ... + p1 * x + p0) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <param name="degree">The degree of the polynomial.</param>
        /// <returns>An array that contains the fitting parameters (p0, p1, ... and pn).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Polynomial(IEnumerable<double> data1, IEnumerable<double> data2, int degree, out double rSquare)
        {
            var r = Polynomial(data1, data2, degree);
            rSquare = RSquare(data2, data1.Select(d =>
            {
                double fit = r[0];
                for (int i = 1; i <= degree; i++)
                    fit += r[i] * System.Math.Pow(d, i);
                return fit;
            }));
            return r;
        }
        private static double PowerSum(IEnumerable<double> data1, int n1)
        {
            double sum = 0;
            var enum1 = data1.GetEnumerator();
            while (enum1.MoveNext())
            {
                var d1 = enum1.Current;
                if (n1 == 0)
                    sum += 1;
                else
                    sum += System.Math.Pow(d1, n1);
            }
            enum1.Dispose();
            return sum;
        }
        private static double PowerSum(IEnumerable<double> data1, int n1, IEnumerable<double> data2)
        {
            double sum = 0;
            var enum1 = data1.GetEnumerator();
            var enum2 = data2.GetEnumerator();
            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                    throw new ArgumentException(ExceptionResource.CountNotEqual);
                var d1 = enum1.Current;
                var d2 = enum2.Current;
                if (n1 == 0)
                    sum += d2;
                else
                    sum += System.Math.Pow(d1, n1) * d2;
            }
            if (enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountNotEqual);
            enum1.Dispose();
            enum2.Dispose();
            return sum;
        }
        private static double[] Gauss(double[,] gauss, int n)
        {
            int i, j;
            int k, m;
            double temp;
            double max;
            double s;
            double[] x = new double[n];
            for (j = 0; j < n; j++)
            {
                max = gauss[j, j];
                k = j;
                for (i = j; i < n; i++)
                {
                    if (System.Math.Abs(gauss[i, j]) > max)
                    {
                        max = gauss[i, j];
                        k = i;
                    }
                }
                if (k != j)
                {
                    for (m = j; m < n + 1; m++)
                    {
                        temp = gauss[j, m];
                        gauss[j, m] = gauss[k, m];
                        gauss[k, m] = temp;
                    }
                }
				if (max == 0)
                    throw new ArithmeticException(ExceptionResource.FailedToFit);
                for (i = j + 1; i < n; i++)
                {
                    s = gauss[i, j] / gauss[j, j];
                    for (m = j; m <= n; m++)
                        gauss[i, m] -= gauss[j, m] * s;
                }
            }
            for (i = n - 1; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j < n; j++)
                    s = s + gauss[i, j] * x[j];
                x[i] = (gauss[i, n] - s) / gauss[i, i];
            }
            return x;
        }
        
        /// <summary>
        /// Applies the logarithm (y = a * ln(x) + b) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Logarithm(IEnumerable<double> data1, IEnumerable<double> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1.Select(d => (double)System.Math.Log(d)), data2);
            var temp = r[0];
            r[0] = r[1];
            r[1] = temp;
            return r;
        }        
        /// <summary>
        /// Applies the logarithm (y = a * ln(x) + b) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Logarithm(IEnumerable<double> data1, IEnumerable<double> data2, out double rSquare)
        {
            var r = Logarithm(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[1] * System.Math.Log(d) + r[0]));
            return r;
        }
        
        /// <summary>
        /// Applies the power (y = a * x ^ b) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Power(IEnumerable<double> data1, IEnumerable<double> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1.Select(d => (double)System.Math.Log(d)), data2.Select(d => (double)System.Math.Log(d)));
            r[0] = System.Math.Exp(r[0]);
            return r;
        }        
        /// <summary>
        /// Applies the power (y = a * x ^ b) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Power(IEnumerable<double> data1, IEnumerable<double> data2, out double rSquare)
        {
            var r = Power(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[0] * System.Math.Pow(d, r[1])));
            return r;
        }
        
        /// <summary>
        /// Applies the exponential (y = a * exp(b * x)) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Exponential(IEnumerable<double> data1, IEnumerable<double> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1, data2.Select(d => (double)System.Math.Log(d)));
            r[0] = System.Math.Exp(r[0]);
            return r;
        }
        /// <summary>
        /// Applies the exponential (y = a * exp(b * x)) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static double[] Exponential(IEnumerable<double> data1, IEnumerable<double> data2, out double rSquare)
        {
            var r = Exponential(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[0] * System.Math.Exp(r[1] * d)));
            return r;
        }

        private static double RSquare(IEnumerable<double> data2, IEnumerable<double> data2Fit)
        {
            double avg = Statistics.Mean(data2);
            double sst = 0;
            double sse = 0;
            var enum1 = data2.GetEnumerator();
            var enum2 = data2Fit.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                var d1 = enum1.Current;
                var d2 = enum2.Current;
                var difft = d1 - avg;
                var diffe = d1 - d2;
                sst += difft * difft;
                sse += diffe * diffe;
            }
            enum1.Dispose();
            enum2.Dispose();
            return 1 - sse / sst;
        }

		#endregion
    
        #region Real

        /// <summary>
        /// Applies the linear (y = p1 * x + p0) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (p0 and p1).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Linear(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
            return Polynomial(data1, data2, 1);
        }
        /// <summary>
        /// Applies the linear (y = p1 * x + p0) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared value.</param>
        /// <returns>An array that contains the fitting parameters (p0 and p1).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Linear(IEnumerable<Real> data1, IEnumerable<Real> data2, out Real rSquare)
        {
            return Polynomial(data1, data2, 1, out rSquare);
        }

        /// <summary>
        /// Applies the polynomial (y = pn * x ^ n + ... + p1 * x + p0) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="degree">The degree of the polynomial.</param>
        /// <returns>An array that contains the fitting parameters (p0, p1, ... and pn).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Polynomial(IEnumerable<Real> data1, IEnumerable<Real> data2, int degree)
        {
		    if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            Real[,] gauss = new Real[degree + 1, degree + 2];
            for (int i = 0; i < degree + 1; i++)
            {
                int j;
                for (j = 0; j < degree + 1; j++)
                {
                    gauss[i, j] = PowerSum(data1, j + i);
                }
                gauss[i, j] = PowerSum(data1, i, data2);
            }
            return Gauss(gauss, degree + 1);
        }
        /// <summary>
        /// Applies the polynomial (y = pn * x ^ n + ... + p1 * x + p0) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <param name="degree">The degree of the polynomial.</param>
        /// <returns>An array that contains the fitting parameters (p0, p1, ... and pn).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Polynomial(IEnumerable<Real> data1, IEnumerable<Real> data2, int degree, out Real rSquare)
        {
            var r = Polynomial(data1, data2, degree);
            rSquare = RSquare(data2, data1.Select(d =>
            {
                Real fit = r[0];
                for (int i = 1; i <= degree; i++)
                    fit += r[i] * System.Math.Pow(d, i);
                return fit;
            }));
            return r;
        }
        private static Real PowerSum(IEnumerable<Real> data1, int n1)
        {
            Real sum = 0;
            var enum1 = data1.GetEnumerator();
            while (enum1.MoveNext())
            {
                var d1 = enum1.Current;
                if (n1 == 0)
                    sum += 1;
                else
                    sum += System.Math.Pow(d1, n1);
            }
            enum1.Dispose();
            return sum;
        }
        private static Real PowerSum(IEnumerable<Real> data1, int n1, IEnumerable<Real> data2)
        {
            Real sum = 0;
            var enum1 = data1.GetEnumerator();
            var enum2 = data2.GetEnumerator();
            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                    throw new ArgumentException(ExceptionResource.CountNotEqual);
                var d1 = enum1.Current;
                var d2 = enum2.Current;
                if (n1 == 0)
                    sum += d2;
                else
                    sum += System.Math.Pow(d1, n1) * d2;
            }
            if (enum2.MoveNext())
                throw new ArgumentException(ExceptionResource.CountNotEqual);
            enum1.Dispose();
            enum2.Dispose();
            return sum;
        }
        private static Real[] Gauss(Real[,] gauss, int n)
        {
            int i, j;
            int k, m;
            Real temp;
            Real max;
            Real s;
            Real[] x = new Real[n];
            for (j = 0; j < n; j++)
            {
                max = gauss[j, j];
                k = j;
                for (i = j; i < n; i++)
                {
                    if (System.Math.Abs(gauss[i, j]) > max)
                    {
                        max = gauss[i, j];
                        k = i;
                    }
                }
                if (k != j)
                {
                    for (m = j; m < n + 1; m++)
                    {
                        temp = gauss[j, m];
                        gauss[j, m] = gauss[k, m];
                        gauss[k, m] = temp;
                    }
                }
				if (max == 0)
                    throw new ArithmeticException(ExceptionResource.FailedToFit);
                for (i = j + 1; i < n; i++)
                {
                    s = gauss[i, j] / gauss[j, j];
                    for (m = j; m <= n; m++)
                        gauss[i, m] -= gauss[j, m] * s;
                }
            }
            for (i = n - 1; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j < n; j++)
                    s = s + gauss[i, j] * x[j];
                x[i] = (gauss[i, n] - s) / gauss[i, i];
            }
            return x;
        }

        /// <summary>
        /// Applies the logarithm (y = a * ln(x) + b) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Logarithm(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1.Select(d => (Real)System.Math.Log(d)), data2);
            var temp = r[0];
            r[0] = r[1];
            r[1] = temp;
            return r;
        }
        /// <summary>
        /// Applies the logarithm (y = a * ln(x) + b) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Logarithm(IEnumerable<Real> data1, IEnumerable<Real> data2, out Real rSquare)
        {
            var r = Logarithm(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[1] * System.Math.Log(d) + r[0]));
            return r;
        }

        /// <summary>
        /// Applies the power (y = a * x ^ b) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Power(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1.Select(d => (Real)System.Math.Log(d)), data2.Select(d => (Real)System.Math.Log(d)));
            r[0] = System.Math.Exp(r[0]);
            return r;
        }
        /// <summary>
        /// Applies the power (y = a * x ^ b) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Power(IEnumerable<Real> data1, IEnumerable<Real> data2, out Real rSquare)
        {
            var r = Power(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[0] * System.Math.Pow(d, r[1])));
            return r;
        }

        /// <summary>
        /// Applies the exponential (y = a * exp(b * x)) fitting on a set of values and returns the parameters.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Exponential(IEnumerable<Real> data1, IEnumerable<Real> data2)
        {
			if (data1 == null || data2 == null)
                throw new ArgumentNullException();
            var r = Linear(data1, data2.Select(d => (Real)System.Math.Log(d)));
            r[0] = System.Math.Exp(r[0]);
            return r;
        }
        /// <summary>
        /// Applies the exponential (y = a * exp(b * x)) fitting on a set of values and returns the parameters, and also returns the R-squared in an output parameter.
        /// </summary>
        /// <param name="data1">The values of the independent variable.</param>
        /// <param name="data2">The values of the dependent variable.</param>
        /// <param name="rSquare">When this method returns, contains the R-squared.</param>
        /// <returns>An array that contains the fitting parameters (a and b).</returns>
        /// <exception cref="ArgumentNullException"><paramref name="data1"/> or <paramref name="data2"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">The lengths of <paramref name="data1"/> and <paramref name="data2"/> don't match.</exception>
        /// <exception cref="ArithmeticException">Cannot apply fitting on the data.</exception>
        public static Real[] Exponential(IEnumerable<Real> data1, IEnumerable<Real> data2, out Real rSquare)
        {
            var r = Exponential(data1, data2);
            rSquare = RSquare(data2, data1.Select(d => r[0] * System.Math.Exp(r[1] * d)));
            return r;
        }

        private static Real RSquare(IEnumerable<Real> data2, IEnumerable<Real> data2Fit)
        {
            Real avg = Statistics.Mean(data2);
            Real sst = 0;
            Real sse = 0;
            var enum1 = data2.GetEnumerator();
            var enum2 = data2Fit.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                var d1 = enum1.Current;
                var d2 = enum2.Current;
                var difft = d1 - avg;
                var diffe = d1 - d2;
                sst += difft * difft;
                sse += diffe * diffe;
            }
            enum1.Dispose();
            enum2.Dispose();
            return 1 - sse / sst;
        }

		#endregion
    
    }
}