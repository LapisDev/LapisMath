using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lapis.Math.Numerical.Test
{
    [TestClass]
    public class IntegralTest
    {
        [TestMethod]
        public void RombergTest()
        {
            Assert.AreEqual(0.5, Integral.Romberg(x => x, 0.0, 1.0), 1e-8, "x => 1/2 * x^2");
            Assert.AreEqual(1.0 / 3, Integral.Romberg(x => x * x, 0.0, 1.0), 1e-8, "x^2 => 1/3 * x^3");
        }
    }
}
