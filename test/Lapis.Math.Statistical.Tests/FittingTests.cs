using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lapis.Math.Statistical;
using Lapis.Math.Numbers;

namespace Lapis.Math.Statistical.Tests
{
    [TestClass]
    public class FittingTests
    {
        [TestMethod]
        public void LinearTest()
        {
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 3, 3, 3, 5 };
            double r2;
            var r = Fitting.Linear(x, y, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r2);
            Assert.AreEqual(0.6, r[0], 1e-4);
            Assert.AreEqual(0.8, r[1], 1e-4);
            Assert.AreEqual(0.8, r2, 1e-4);
            x = new double[] { 0, 0.6931, 1.0986, 1.3863, 1.6094 };
            y = new double[] { 0, 1.0986, 1.0986, 1.0986, 1.6094 };
            r = Fitting.Linear(x, y, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r2);
            Assert.AreEqual(0.1602, r[0], 1e-4);
            Assert.AreEqual(0.8573, r[1], 1e-4);
        }

        [TestMethod]
        public void PolynomialTest()
        {
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 3, 3, 3, 5 };
            double r2;
            var r = Fitting.Polynomial(x, y, 2, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r[2]);
            Console.WriteLine(r2);
            Assert.AreEqual(0.6, r[0], 1e-4);
            Assert.AreEqual(0.8, r[1], 1e-4);
            Assert.AreEqual(0, r[2], 1e-4);
            Assert.AreEqual(0.8, r2, 1e-4);
            r = Fitting.Polynomial(x, y, 3, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r[2]);
            Console.WriteLine(r[3]);
            Console.WriteLine(r2);
            Assert.AreEqual(-5, r[0], 1e-4);
            Assert.AreEqual(26.0 / 3, r[1], 1e-4);
            Assert.AreEqual(-3, r[2], 1e-4);
            Assert.AreEqual(1.0 / 3, r[3], 1e-4);
            Assert.AreEqual(1, r2, 1e-4);
        }

        [TestMethod]
        public void ExponentialTest()
        {
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 3, 3, 3, 5 };
            double r2;
            var r = Fitting.Exponential(x, y, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r2);
            Assert.AreEqual(1.0155112783974817, r[0], 1e-4);
            Assert.AreEqual(0.3218875824868199, r[1], 1e-4);
        }

        [TestMethod]
        public void LogarithmTest()
        {
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 3, 3, 3, 5 };
            double r2;
            var r = Fitting.Logarithm(x, y, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r2);
            Assert.AreEqual(1.9925086774872764, r[0], 1e-4);
            Assert.AreEqual(1.092176231821617, r[1], 1e-4);
        }

        [TestMethod]
        public void PowerTest()
        {
            double[] x = { 1, 2, 3, 4, 5 };
            double[] y = { 1, 3, 3, 3, 5 };
            double r2;
            var r = Fitting.Power(x, y, out r2);
            Console.WriteLine(r[0]);
            Console.WriteLine(r[1]);
            Console.WriteLine(r2);
            // Assert.AreEqual(1.335, r[0], 1e-4);
            // Assert.AreEqual(0.759, r[1], 1e-4);
        }

    }
}
