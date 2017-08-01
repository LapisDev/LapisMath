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
    public class IntegerTests
    {

        [TestMethod()]
        public void CompareTest()
        {
            Assert.AreEqual(-1, Integer.Compare(0, 3), "0 < 3");
            Assert.AreEqual(-1, Integer.Compare(2, 3), "2 < 3");
            Assert.AreEqual(0, Integer.Compare(3, 3), "3 = 3");
            Assert.AreEqual(1, Integer.Compare(4, 3), "4 > 3");
            Assert.AreEqual(1, Integer.Compare(6, 3), "6 > 3");
            Assert.AreEqual(-1, Integer.Compare(-1, 3), "-1 < 3");
            Assert.AreEqual(-1, Integer.Compare(-2, 3), "-2 < 3");
            Assert.AreEqual(-1, Integer.Compare(-3, 3), "-3 < 3");
            Assert.AreEqual(-1, Integer.Compare(-4, 3), "-4 < 3");
        }

        [TestMethod()]
        public void EqualTest()
        {
            Assert.AreEqual(false, Integer.Equal(0, 3), "0 < 3");
            Assert.AreEqual(false, Integer.Equal(2, 3), "2 < 3");
            Assert.AreEqual(true,  Integer.Equal(3, 3), "3 = 3");
            Assert.AreEqual(false,  Integer.Equal(4, 3), "4 > 3");
            Assert.AreEqual(false,  Integer.Equal(6, 3), "6 > 3");
            Assert.AreEqual(false, Integer.Equal(-1, 3), "-1 < 3");
            Assert.AreEqual(false, Integer.Equal(-2, 3), "-2 < 3");
            Assert.AreEqual(false, Integer.Equal(-3, 3), "-3 < 3");
            Assert.AreEqual(false, Integer.Equal(-4, 3), "-4 < 3");
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(3, Integer.Add(0, 3), "0 + 3 => 3");
            Assert.AreEqual(5, Integer.Add(2, 3), "2 + 3 => 5");
            Assert.AreEqual(6, Integer.Add(3, 3), "3 + 3 => 6");
            Assert.AreEqual(7, Integer.Add(4, 3), "4 + 3  => 7");
            Assert.AreEqual(9, Integer.Add(6, 3), "6 + 3  => 9");
            Assert.AreEqual(2, Integer.Add(-1, 3), "-1 + 3 => 2");
            Assert.AreEqual(1, Integer.Add(-2, 3), "-2 + 3 => 1");
            Assert.AreEqual(0, Integer.Add(-3, 3), "-3 + 3 => 0");
            Assert.AreEqual(-1, Integer.Add(-4, 3), "-4 + 3 => -1");
        }

        [TestMethod()]
        public void SubtractTest()
        {
            Assert.AreEqual(-3, Integer.Subtract(0, 3), "0 - 3 => -3");
            Assert.AreEqual(-1, Integer.Subtract(2, 3), "2 - 3 => -1");
            Assert.AreEqual(0, Integer.Subtract(3, 3), "3 - 3 => 0");
            Assert.AreEqual(1, Integer.Subtract(4, 3), "4 - 3  => 1");
            Assert.AreEqual(3, Integer.Subtract(6, 3), "6 - 3  => 3");
            Assert.AreEqual(-4, Integer.Subtract(-1, 3), "-1 - 3 => -4");
            Assert.AreEqual(-5, Integer.Subtract(-2, 3), "-2 - 3 => -5");
            Assert.AreEqual(-6, Integer.Subtract(-3, 3), "-3 - 3 => -6");
            Assert.AreEqual(-7, Integer.Subtract(-4, 3), "-4 - 3 => -7");
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            Assert.AreEqual(0, Integer.Multiply(0, 3), "0 * 3 => 0");
            Assert.AreEqual(6, Integer.Multiply(2, 3), "2 * 3 => 6");
            Assert.AreEqual(9, Integer.Multiply(3, 3), "3 * 3 => 9");
            Assert.AreEqual(12, Integer.Multiply(4, 3), "4 * 3  => 12");
            Assert.AreEqual(18, Integer.Multiply(6, 3), "6 * 3  => 18");
            Assert.AreEqual(-3, Integer.Multiply(-1, 3), "-1 * 3 => -3");
            Assert.AreEqual(-6, Integer.Multiply(-2, 3), "-2 * 3 => -6");
            Assert.AreEqual(-9, Integer.Multiply(-3, 3), "-3 * 3 => -9");
            Assert.AreEqual(-12, Integer.Multiply(-4, 3), "-4 * 3 => -12");
        }

        [TestMethod()]
        public void DivideTest()
        {
            Assert.AreEqual(0, Integer.Divide(0, 3), "0 / 3 => 0");
            Assert.AreEqual(0, Integer.Divide(2, 3), "2 / 3 => 0");
            Assert.AreEqual(1, Integer.Divide(3, 3), "3 / 3 => 1");
            Assert.AreEqual(1, Integer.Divide(4, 3), "4 / 3  => 1");
            Assert.AreEqual(2, Integer.Divide(6, 3), "6 / 3  => 2");
            Assert.AreEqual(0, Integer.Divide(-1, 3), "-1 / 3 => 0");
            Assert.AreEqual(0, Integer.Divide(-2, 3), "-2 / 3 => 0");
            Assert.AreEqual(-1, Integer.Divide(-3, 3), "-3 / 3 => -1");
            Assert.AreEqual(-1, Integer.Divide(-4, 3), "-4 / 3 => -1");
        }

        [TestMethod()]
        public void RemainderTest()
        {
            Assert.AreEqual(0, Integer.Remainder(0, 3), "0 % 3 => 0");
            Assert.AreEqual(2, Integer.Remainder(2, 3), "2 % 3 => 2");
            Assert.AreEqual(0, Integer.Remainder(3, 3), "3 % 3 => 0");
            Assert.AreEqual(1, Integer.Remainder(4, 3), "4 % 3  => 1");
            Assert.AreEqual(0, Integer.Remainder(6, 3), "6 % 3  => 0");
            Assert.AreEqual(-1, Integer.Remainder(-1, 3), "-1 % 3 => -1");
            Assert.AreEqual(-2, Integer.Remainder(-2, 3), "-2 % 3 => -2");
            Assert.AreEqual(0, Integer.Remainder(-3, 3), "-3 % 3 => 0");
            Assert.AreEqual(-1, Integer.Remainder(-4, 3), "-4 % 3 => -1");
        }

        [TestMethod()]
        public void NegateTest()
        {
            Assert.AreEqual(0, Integer.Negate(0), "-0 => 0");
            Assert.AreEqual(-2, Integer.Negate(2), "-2 => -2");
            Assert.AreEqual(1, Integer.Negate(-1), "-(-1)  => 1");
        }

        [TestMethod()]
        public void AbsTest()
        {
            Assert.AreEqual(0, Integer.Abs(0), "Abs(0) => 0");
            Assert.AreEqual(2, Integer.Abs(2), "Abs(2) => 2");
            Assert.AreEqual(1, Integer.Abs(-1), "Abs(-1)  => 1");
        }

        [TestMethod()]
        public void EvenOddTest()
        {
            Assert.IsTrue(Integer.Zero.IsEven, "0 is even");
            Assert.IsFalse(Integer.Zero.IsOdd, "0 is not odd");

            Assert.IsFalse(Integer.One.IsEven, "1 is not even");
            Assert.IsTrue(Integer.One.IsOdd, "1 is odd");

            Assert.IsFalse(Integer.MinusOne.IsEven, "-1 is not even");
            Assert.IsTrue(Integer.MinusOne.IsOdd, "-1 is odd");

            Assert.IsFalse(((Integer)Int32.MaxValue).IsEven, "Int32.Max is not even");
            Assert.IsTrue(((Integer)Int32.MaxValue).IsOdd, "Int32.Max is odd");

            Assert.IsTrue(((Integer)Int32.MinValue).IsEven, "Int32.Min is even");
            Assert.IsFalse(((Integer)Int32.MinValue).IsOdd, "Int32.Min is not odd");
        }

        [TestMethod()]
        public void IsPowerOfTwoTest()
        {
            for (var i = 2; i < 31; i++)
            {
                var x = Integer.One * (int)System.Math.Pow(2, i);
                Assert.IsTrue((x as Integer).IsPowerOfTwo, x + " is power of 2");
                Assert.IsFalse(((x - 1) as Integer).IsPowerOfTwo, x - 1 + " is not power of 2");
                Assert.IsFalse(((x + 1) as Integer).IsPowerOfTwo, x + 1 + " is not power of 2");
                Assert.IsFalse(((-x) as Integer).IsPowerOfTwo, -x + " is not power of 2");
                Assert.IsFalse(((-x + 1) as Integer).IsPowerOfTwo, -x + 1 + " is not power of 2");
                Assert.IsFalse(((-x - 1) as Integer).IsPowerOfTwo, -x - 1 + " is not power of 2");
            }
        }

        [TestMethod()]
        public void IsPerfectSquareTest()
        {
            var lastRadix = (int)System.Math.Floor(System.Math.Sqrt(Int32.MaxValue));
            for (var i = 0; i <= lastRadix; i++)
            {
                Assert.IsTrue(((Integer)(i * i)).IsPerfectSquare, i * i + " is perfect square");
            }

            for (var i = 2; i <= lastRadix; i++)
            {
                Assert.IsFalse(Integer.FromInt32(i * i - 1).IsPerfectSquare, i * i - 1 + " is not perfect square");
                Assert.IsFalse(Integer.FromInt32(i * i + 1).IsPerfectSquare, i * i + 1 + " is not perfect square");
            }
        }

        [TestMethod()]
        public void GcdTest()
        {
            Assert.AreEqual(0, Integer.Gcd(0, 0), "Gcd(0,0) => 0");
            Assert.AreEqual(6, Integer.Gcd(0, 6), "Gcd(0,6) => 6");
            Assert.AreEqual(1, Integer.Gcd(7, 13), "Gcd(7,13) => 1");
            Assert.AreEqual(7, Integer.Gcd(7, 14), "Gcd(7,14) = 7");
            Assert.AreEqual(1, Integer.Gcd(7, 15), "Gcd(7,15) => 1");
            Assert.AreEqual(3, Integer.Gcd(6, 15), "Gcd(6,15) => 3");

            Assert.AreEqual(5, Integer.Gcd(-5, 0), "Gcd(-5,0) => 5");
            Assert.AreEqual(5, Integer.Gcd(0, -5), "Gcd(0, -5) => 5");
            Assert.AreEqual(1, Integer.Gcd(-7, 15), "Gcd(-7,15) => 1");
            Assert.AreEqual(1, Integer.Gcd(-7, -15), "Gcd(-7,-15) => 1");

            Assert.AreEqual(2, Integer.Gcd(-10, 6, -8), "Gcd(-10,6,-8) => 2");
            Assert.AreEqual(1, Integer.Gcd(-10, 6, -8, 5, 9, 13), "Gcd(-10,6,-8,5,9,13) => 1");
            Assert.AreEqual(5, Integer.Gcd(-10, 20, 120, 60, -15, 1000), "Gcd(-10,20,120,60,-15,1000) => 5");
            Assert.AreEqual(3, Integer.Gcd(Int32.MaxValue - 1, Int32.MaxValue - 4, Int32.MaxValue - 7), "Gcd(Int64Max-1,Int64Max-4,Int64Max-7) => 3");
            Assert.AreEqual(123, Integer.Gcd(492, -2 * 492, 492 / 4), "Gcd(492, -984, 123) => 123");
        }

        [TestMethod()]
        public void LcmTest()
        {
            Assert.AreEqual(10, Integer.Lcm(10, 10), "Lcm(10,10) => 10");
            Assert.AreEqual(0, Integer.Lcm(0, 10), "Lcm(0,10) => 0");
            Assert.AreEqual(0, Integer.Lcm(10, 0), "Lcm(10,0) => 0");
            Assert.AreEqual(77, Integer.Lcm(11, 7), "Lcm(11,7) => 77");
            Assert.AreEqual(33, Integer.Lcm(11, 33), "Lcm(11,33) => 33");
            Assert.AreEqual(374, Integer.Lcm(11, 34), "Lcm(11,34) => 374");

            Assert.AreEqual(352, Integer.Lcm(11, -32), "Lcm(11,-32) => 352");
            Assert.AreEqual(352, Integer.Lcm(-11, 32), "Lcm(-11,32) => 352");
            Assert.AreEqual(352, Integer.Lcm(-11, -32), "Lcm(-11,-32) => 352");

            Assert.AreEqual(120, Integer.Lcm(-10, 6, -8), "Lcm(-10,6,-8) => 120");
            Assert.AreEqual(4680, Integer.Lcm(-10, 6, -8, 5, 9, 13), "Lcm(-10,6,-8,5,9,13) => 4680");
            Assert.AreEqual(3000, Integer.Lcm(-10, 20, 120, 60, -15, 1000), "Lcm(-10,20,120,60,-15,1000) => 3000");
            Assert.AreEqual(984, Integer.Lcm(492, -2 * 492, 492 / 4), "Lcm(492, -984, 123) => 984");
            Assert.AreEqual(2016, Integer.Lcm(32, 42, 36, 18), "Lcm(32,42,36,18) => 2016");
        }
    }
}
