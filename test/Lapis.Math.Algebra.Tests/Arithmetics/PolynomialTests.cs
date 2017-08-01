using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lapis.Math.Algebra.Arithmetics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Algebra.Arithmetics.Tests
{
    [TestClass()]
    public class PolynomialTests
    {
        private readonly Symbol x = Symbol.FromString("x");
        private readonly Symbol y = Symbol.FromString("y");
        private readonly Symbol z = Symbol.FromString("z");
        private readonly Symbol a = Symbol.FromString("a");
        private readonly Symbol b = Symbol.FromString("b");
        private readonly Symbol c = Symbol.FromString("c");
        private readonly Symbol d = Symbol.FromString("d");

        [TestMethod()]
        public void GcdTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, Expression> test = (Expression input1, Expression input2, Expression expected) =>
            {
                var result = Polynomial.Gcd(input1, input2, x);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: Gcd(\t{1} , \t {2}\t) \t => \t {3}.", counter, input1, input2, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: Gcd(\t{1} , \t {2}\t) \t => \t {3} \t , \t {4} expected.\n", counter, input1, input2, result, expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };

            test(Expression.Pow(x, 2) + 2 * x + 1, Expression.Pow(x, 2) - 1, x + 1);
            test(Expression.Pow(x, 4) - 2 * Expression.Pow(x, 2) + 1, Expression.Pow(x, 2) - 1, Expression.Pow(x, 2) - 1);
            test(Expression.Pow(x, 7) - 4 * Expression.Pow(x, 5) - Expression.Pow(x, 2) + 4, Expression.Pow(x, 5) - 4 * Expression.Pow(x, 3) - Expression.Pow(x, 2) + 4,
                Expression.Pow(x, 3) - Expression.Pow(x, 2) - 4 * x + 4);
            test(Expression.Pow(x, 2) + 2 * x * y + Expression.Pow(y, 2), Expression.Pow(x, 2) - Expression.Pow(y, 2), x + y);
            test(Expression.Pow(x, 2) * z + 2 * x * y * z + Expression.Pow(y, 2) * z, Expression.Pow(x, 2) - Expression.Pow(y, 2), x + y);
            test(Expression.Pow(x, 2) + Expression.Pow(y, 2) + Expression.Pow(z, 2) + 2 * x * y + 2 * x * z + 2 * y * z, x + y + z, x + y + z);

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod()]
        public void DivideTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, Expression, Expression> test = (Expression input1, Expression input2, Expression expected1, Expression expected2) =>
            {
                Expression quo, rem;
                Polynomial.Divide(input1, input2, x, out quo, out rem);
                if (expected1 == quo && expected2 == rem)
                    Console.WriteLine(string.Format("{0}\t Passed: Divide(\t{1} , \t {2} \t) \t => \t {3} , \t {4}.", counter, input1, input2, quo, rem));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: Divide(\t{1} , \t {2} \t) \t => \t {3} , \t {4}\t ; \t {5} , {6} expected.\n", counter, input1, input2, quo, rem, expected1, expected2);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };

            test(5 * Expression.Pow(x, 2) + 4 * x + 1, 2 * x + 3,
                2.5 * x - 1.75, 6.25);
            test(Expression.Pow(x, 3) - 2 * Expression.Pow(x, 2) - 4, x - 3,
                Expression.Pow(x, 2) + x + 3, 5);
            test(3 * Expression.Pow(x, 3) + Expression.Pow(x, 2) + x + 5, 5 * Expression.Pow(x, 2) - 3 * x + 1,
                0.6 * x + 14.0 / 25, 52.0 / 25 * x + 111.0 / 25);
            test(Expression.Pow(x, 3) - 12 * Expression.Pow(x, 2) - a, Expression.Pow(x, 2) - 2 * x + 1,
                x - 10, -21 * x - a + 10);

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
