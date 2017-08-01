using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lapis.Math.Measurement;
using Lapis.Math.Algebra.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Lapis.Math.Measurement.Tests
{
    [TestClass()]
    public class QuantityTests
    {
        private static readonly Symbol x = Symbol.FromString("x");
        private static readonly Symbol y = Symbol.FromString("y");
        private static readonly Symbol z = Symbol.FromString("z");
        private static readonly Symbol a = Symbol.FromString("a");
        private static readonly Symbol b = Symbol.FromString("b");
        private static readonly Symbol c = Symbol.FromString("c");

        [TestMethod()]
        public void Test()
        {
            var m = Unit.Base("m");
            var cm = Unit.Create("cm", 0.01 * m);
            var km = Unit.Create("km", 1000 * m);
            var s = Unit.Base("s");
            var kmps = km / s;
            // Console.WriteLine(1.Unit(s) + 2.Unit(cm));

            Assert.AreEqual("m", m.ToString());
            Assert.AreEqual("cm", cm.ToString());
            Assert.AreEqual("km", km.ToString());
            Assert.AreEqual("km*s^-1", kmps.ToString());

            var two = 2 * Unit.None;
            Assert.AreEqual("2", two.ToString());


            Assert.AreEqual("102 cm", (1.Unit(m) + 2.Unit(cm)).ToString());
            Assert.AreEqual("(100 * x + y) cm", (x.Unit(m) + y.Unit(cm)).ToString());

            Assert.AreEqual("4 m*cm*s^-1", (1.Unit(m) * 2.Unit(cm) / 0.5.Unit(s)).ToString());

            Assert.AreEqual("(9/10) J", (1.0.m() * 0.4.N() + 0.5.J()).ToString());

            Assert.AreEqual(
                double.Parse(PhysicalConstants.NA.Value.ToString()),
                double.Parse((PhysicalConstants.R.Value / PhysicalConstants.kB.Value).ToString()),
                delta: 1e16
            );
        }

        
    }
}
