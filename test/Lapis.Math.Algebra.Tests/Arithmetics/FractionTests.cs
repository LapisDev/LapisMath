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
    public class FractionTests
    {
        private readonly Symbol x = Symbol.FromString("x");
        private readonly Symbol y = Symbol.FromString("y");
        private readonly Symbol z = Symbol.FromString("z");
        private readonly Symbol a = Symbol.FromString("a");
        private readonly Symbol b = Symbol.FromString("b");
        private readonly Symbol c = Symbol.FromString("c");
        private readonly Symbol d = Symbol.FromString("d");
        private readonly Symbol e = Symbol.FromString("e");
        private readonly Symbol f = Symbol.FromString("f");

        [TestMethod()]
        public void RationalizeTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Fraction.Rationalize(input);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: {1} \t => \t {2}.", counter, input, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: {1} \t => \t {2} \t , \t {3} expected.\n", counter, input, result, expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };

            test(a + 1, a + 1);
            test(a / b + c / d, (a * d + b * c) / (b * d));
            test(1 + 1 / (1 + 1 / x), (1 + 2 * x) / (1 + x));
            test(1 / Expression.Pow(1 + 1 / x, 0.5) + Expression.Pow(1 + 1 / x, 1.5), 
                (Expression.Pow(x, 2) + Expression.Pow(1 + x, 2)) / (Expression.Pow(x, 2) * Expression.Pow((1 + x) / x, 0.5)));
            test(Expression.Pow(1 + 1 / x, 2), Expression.Pow(1 + x, 2) / Expression.Pow(x, 2));

            test(x / z + y / Expression.Pow(z, 2), (y * z + x * Expression.Pow(z, 2)) / Expression.Pow(z, 3));

            // test((Expression.Pow(1 / (Expression.Pow(x + y, 2) + 1), 0.5) + 1) * (Expression.Pow(1 / (Expression.Pow(x + y, 2) + 1), 0.5) - 1) / (x + 1),
            //     ((-1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 0.5)) * (1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 0.5))) / (1 + x));

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod()]
        public void ExpandTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Fraction.Expand(input);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: {1} \t => \t {2}.", counter, input, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: {1} \t => \t {2} \t , \t {3} expected.\n", counter, input, result, expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };

            test(a / b + c / d + e / f, (a * d * f + b * c * f + b * d * e) / (b * d * f));

            test((Expression.Pow(1 / (Expression.Pow(x + y, 2) + 1), 0.5) + 1) * (Expression.Pow(1 / (Expression.Pow(x + y, 2) + 1), 0.5) - 1) / (x + 1),
                 (-Expression.Pow(x, 2) - 2 * x * y - Expression.Pow(y, 2)) / (1 + x + Expression.Pow(x, 2) + Expression.Pow(x, 3) + 2 * x * y + 2 * Expression.Pow(x, 2) * y + Expression.Pow(y, 2) + x * Expression.Pow(y, 2)));

            test((1 / (1 / a + c / (a * b)) + (a * b * c + a * Expression.Pow(c, 2)) / Expression.Pow(b + c, 2) - a), 0);

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod()]
        public void SimplifyTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Fraction.Simplify(input, x);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: {1} \t => \t {2}.", counter, input, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: {1} \t => \t {2} \t , \t {3} expected.\n", counter, input, result, expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };

            test((Expression.Pow(x, 2) - 1) / (x + 1), x - 1);
            
            //test((x + 1) / (Expression.Pow(x, 2) - 1 - (x + 1) * (x - 1)), ComplexInfinity.Instance);
            
            test(1 / (1 + 1 / (x + 1)) + 2 / (x + 2), (3 + x) / (2 + x));

            test(y / x + z / Expression.Pow(x, 2), (z + x * y) / Expression.Pow(x, 2));

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
