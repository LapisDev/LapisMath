using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lapis.Math.Numbers;

namespace Lapis.Math.Numbers.Tests
{
    [TestClass()]
    public class RealTests
    {
        [TestMethod()]
        public void EqualTest()
        {
            Assert.AreEqual(true, Fraction.Create(1, 2) == FloatingPoint.FromDouble(0.5), "0.5 == 1/2");
            Assert.AreEqual(true, Fraction.Create(25, 8) == FloatingPoint.FromDouble(3.125), "3.125 == 25/8");
        }

        [TestMethod()]
        public void FromDoubleTest()
        {
            Assert.AreEqual(Fraction.Create(1, 2), (Real)(0.5), "0.5 => 1/2");
            Assert.AreEqual(Fraction.Create(25, 8), (Real)(3.125), "3.125 => 25/8");
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(Fraction.Create(5, 6), (Real)(0.5) + Fraction.Create(1, 3), "0.5 + 1/3 => 5/6");
            Assert.AreEqual(Fraction.Create(-1, 6), (Real)(-0.5) + Fraction.Create(1, 3), "-0.5 + 1/3 => -1/6");
        }

        [TestMethod()]
        public void SubtractTest()
        {
            Assert.AreEqual(Fraction.Create(1, 6), (Real)(0.5) - Fraction.Create(1, 3), "0.5 - 1/3 => 1/6");
            Assert.AreEqual(Fraction.Create(-5, 6), (Real)(-0.5) - Fraction.Create(1, 3), "-0.5 - 1/3 => -5/6");
        }

        [TestMethod()]
        public void MutiplyTest()
        {
            Assert.AreEqual(Fraction.Create(1, 6), (Real)(0.5) * Fraction.Create(1, 3), "0.5 * 1/3 => 1/6");
            Assert.AreEqual(Fraction.Create(-1, 6), (Real)(-0.5) + Fraction.Create(1, 3), "-0.5 * 1/3 => -1/6");
        }

        [TestMethod()]
        public void DivideTest()
        {
            Assert.AreEqual(Fraction.Create(3, 2), (Real)(0.5) / Fraction.Create(1, 3), "0.5 / (1/3) => 3/2");
            Assert.AreEqual(Fraction.Create(-3, 2), (Real)(-0.5) / Fraction.Create(1, 3), "-0.5 / (1/3) => -3/2");
        }             

        [TestMethod()]
        public void PowTest()
        {
            Assert.AreEqual(Fraction.Create(1, 4), Real.Pow((Real)(0.5), Integer.FromInt32(2)), "0.5 ^ 2 => 1/4");
            Assert.AreEqual((Real)(System.Math.Sqrt(2)), Real.Pow(Integer.FromInt32(2), Fraction.Create(1, 2)), "2 ^ (1/2) => 1.4142135623731");
            Assert.AreEqual(Integer.FromInt32(4), Real.Pow((Real)(0.5), Integer.FromInt32(-2)), "0.5 ^ -2 => 4");
            Assert.AreEqual(Fraction.Create(1, 4), Real.Pow((Real)(-0.5), Integer.FromInt32(2)), "-0.5 ^ 2 => 1/4");
        }
    }
}
