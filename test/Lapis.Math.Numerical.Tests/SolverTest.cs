using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lapis.Math.Numbers;

namespace Lapis.Math.Numerical.Test
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void QuadraticTest()
        {
            Assert.AreEqual(Tuple.Create(1.0, -3.0), Solver.Quadratic(1.0, 2.0, -3.0), "x^2 + 2*x - 3 => 1, -3");
            Assert.AreEqual(Tuple.Create(1.0, 1.0), Solver.Quadratic(1.0, -2.0, 1.0), "x^2 - 2*x + 1 => 1, 1");
            Assert.AreEqual(Tuple.Create(Complex.FromRectangularCoordinates(-1, 1), Complex.FromRectangularCoordinates(-1, -1)),
                Solver.Quadratic((Real)1.0, (Real)2.0, (Real)2.0), "x^2 + 2*x + 2 => -1+i, -1-i");
        }

        [TestMethod]
        public void CubicTest()
        {
            Assert.AreEqual(Tuple.Create<Complex, Complex, Complex>(9.0, 4.0, 4.0), Solver.Cubic(-1, 17, -88, 144), "-x^3 + 17*x^2 - 88*x + 144 => 9, 4, 4");
            Assert.AreEqual(Tuple.Create<Complex, Complex, Complex>(-1.0, -1.0, -1.0), Solver.Cubic(1.0, 3.0, 3.0, 1.0), "x^3 + 3*x^2 + 3*x + 1 => -1, -1, -1");
            // Assert.AreEqual(Tuple.Create(Complex.FromInt32(-3), Real.Sqrt(2) * Complex.ImaginaryOne, -Real.Sqrt(2) * Complex.ImaginaryOne),
            //     Solver.Cubic(1, 3, 2, 6), "x^3 + 3*x^2 + 2*x + 6 => -3, 1.414i, 1.414i");
        }

        [TestMethod]
        public void BisectionTest()
        {
            Assert.AreEqual(1, Solver.Bisection(x => System.Math.Log(x), 0, 10), "ln(x) => 1");
            Assert.AreEqual(1, Solver.Bisection(x => System.Math.Pow(x, x) - 1, 0.1, 10), "x^x => 1");
        }

        [TestMethod]
        public void SecantTest()
        {
            Assert.AreEqual(1, Solver.Secant(x => System.Math.Log(x), 0.1), "ln(x) => 1");
            Assert.AreEqual(1, Solver.Secant(x => System.Math.Pow(x, x) - 1, 10), "x^x => 1");
        }
    }
}
