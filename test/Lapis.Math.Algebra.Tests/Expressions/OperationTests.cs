using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lapis.Math.Algebra.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Lapis.Math.Algebra.Expressions.Tests
{
    [TestClass()]
    public partial class ExpressionTests
    {
        private readonly Symbol x = Symbol.FromString("x");
        private readonly Symbol y = Symbol.FromString("y");
        private readonly Symbol z = Symbol.FromString("z");
        private readonly Symbol a = Symbol.FromString("a");
        private readonly Symbol b = Symbol.FromString("b");
        private readonly Symbol c = Symbol.FromString("c");
        private readonly Symbol d = Symbol.FromString("d");


        [TestMethod]
        public void AddTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, string> test = (Expression result, Expression expected, string input) =>
            {              
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

            test(x + 0, x, "x + 0");
            test(x + 1, 1 + x, "x + 1");
            test(1 + x, 1 + x, "1 + x");
            test(x + y, x+y, "x + y");
            test(y + x, x+y, "y + x");

            test(x + x, 2*x, "x + x");
            test(x +x+ x, 3 * x, "x + x + x");
            test(2 * x + 3 * x, 5 * x, "2 * x + 3 * x");
            test(-2 * x + 3 * x, x, "-2 * x + 3 * x");
            test(2 * x + -3 * x, -x, "2 * x + -3 * x");

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod]
        public void MultiplyTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, string> test = (Expression result, Expression expected, string input) =>
            {
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

            test(x * 0, 0, "x * 0");
            test(x * 1, x, "x * 1");
            test(1*x , x, "1 * x");
            test(x * y, x*y, "x * y");
            test(y * x, x*y, "y * x");

            test(x * x, Expression .Pow (x,2), "x * x");
            test(x * x * x, Expression.Pow(x, 3), "x * x * x");
            test(Expression.Pow(x, 2) * Expression.Pow(x, 3), Expression.Pow(x, 5), "x ^ 2 * x ^ 3");
            test(Expression.Pow(x, -2) * Expression.Pow(x, 3), x, "x ^ -2 * x ^ 3");
            test(Expression.Pow(x, 2) * Expression.Pow(x, -3), 1 / x, "x ^ 2 * x ^ -3");      

            Assert.IsFalse(failed, sb.ToString());
        }


        [TestMethod()]
        public void CompositeTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, string> test = (Expression result, Expression expected, string input) =>
            {
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

            test(a + b + c + a * b + a * c + b * c, a + a * b + a * c + b + b * c + c, "a + b + c + a * b + a * c + b * c");
            test(c * b + c * a + b * a + c + b + a, a + a * b + a * c + b + b * c + c, "c * b + c * a + b * a + c + b + a");

            test(a * a + b * b, Expression.Pow(a, 2) + Expression.Pow(b, 2), "a * a + b * b");
            test(a * a * a + a * a, Expression.Pow(a, 2) + Expression.Pow(a, 3), "a * a * a + a * a");
            test(a * a * b * b, Expression.Pow(a, 2) * Expression.Pow(b, 2), "a * a * b * b");
            test((a + c) * (a + c) + (a + b) * (a + b), Expression.Pow(a + b, 2) + Expression.Pow(a + c, 2), "(a + c) * (a + c) + (a + b) * (a + b)");

            test((a + b) * y * x, (a + b) * x * y, "(a + b) * y * x");
            test((a + b) * (x * y), (a + b) * x * y, "(a + b) * (x * y)");

            test(x * Expression.Pow(x, 2) * Expression.Pow(x, 3), Expression.Pow(x, 6), "x * x ^ 2 * x ^ 3");
            test(Expression.Pow(Expression.Pow(x, 2), 3), Expression.Pow(x, 6), "(x ^ 2) ^ 3");

            test(2 * (x * y) * Expression.Pow(z, 2), 2 * x * y * Expression.Pow(z, 2), "2 * (x * y) * z ^ 2");
            test(1 * x * y * Expression.Pow(z, Number.FromInt32(2)), x * y * Expression.Pow(z, 2), "1 * x * y * z ^ 2");
            test(2 * x * y * z * Expression.Pow(z, 2), 2 * x * y * Expression.Pow(z, 3), "2 * x * y * z * z ^ 2");

            test((x + y) - (x + y), 0, "(x + y) - (x + y)");

            test(-1 * x, -x, "-1 * x");
            test(-(-x), x, "-(-x)");

            test(1 / x, Expression.Pow(x, -1), "1 / x");
            test(-1 / x, -Expression.Pow(x, -1), "-1 / x");
            test(1 / (1 / x), x, "1 / (1 / x)");

            test(2 + 1 / x - 1, 1 + 1 / x, "2 + 1 / x - 1");

            test(-x * y / 3, -1.0 / 3 * x * y, "-x * y / 3");

            test((a / b / (c * a)) * (c * d / a) / d, 1 / a * 1 / b, "(a / b / (c * a)) * (c * d / a) / d");
            test(Expression.Pow((Expression.Pow(x * y, 0.5) * Expression.Pow(z, 2)), 2), x * y * Expression.Pow(z, 4), "((x * y) ^ 0.5 * z ^ 2) ^ 2");
            test(Expression.Pow(a, 1.5) * Expression.Pow(a, 0.5), Expression.Pow(a, 2), "a ^ 1.5 * a ^ 0.5");

            test((a * b) / (b * a), 1, "(a * b) / (b * a)");
            test(Expression.Pow(a * b, 2) / (a * b), a * b, "(a * b) ^ 2 / (a * b)");
            test((a * b) / Expression.Pow(a * b, 2), 1 / a * 1 / b, "(a * b) / (a * b) ^ 2");

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
