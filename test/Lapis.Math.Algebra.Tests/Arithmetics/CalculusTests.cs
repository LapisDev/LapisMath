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
    public class CalculusTests
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
        public void DerivativeTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Calculus.Derivative(input, x);
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

            test(a * x, a);
            test(Expression.Sin(x), Expression.Cos(x));
            test(x * Expression.Sin(x), Expression.Sin(x) + x * Expression.Cos(x));
            test(a * Expression.Pow(x, 2), 2 * a * x);
            test(a * Expression.Pow(x, b), a * b * Expression.Pow(x, b - 1));
            test(a * Expression.Pow(x, 2) + b * x + c, 2 * a * x + b);

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod()]
        public void TaylorTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression, int, Expression> test = (Expression input1, Expression input2, int input3, Expression expected) =>
            {
                var result = Calculus.Taylor(input1, x, input2, input3);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: Taylor(\t{1} , x = {2}, degree {3} )\t => \t {4}.", counter, input1, input2, input3, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: Taylor(\t{1} , x = {2}, degree {3} )\t => \t {4} , \t {5} expected.\n", counter, input1, input2, input3, result, expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };          

            test(1 / (1 - x), 0, 3, 1 + x + Expression.Pow(x, 2));
            test(1 / x, 1, 3, 3 - 3 * x + Expression.Pow(x, 2));
            test(Expression.Ln(x), 1, 3, -1.5 + 2 * x - 0.5 * Expression.Pow(x, 2));
            test(Expression.Ln(x), 1, 4, -Number.FromInt32(11) / 6 + 3 * x - 1.5 * Expression.Pow(x, 2) + Expression.Pow(x, 3) / 3);
            test(Expression.Sin(x) + Expression.Cos(x), 0, 3, 1 + x - 0.5 * Expression.Pow(x, 2));
            test(Expression.Sin(x) + Expression.Cos(x), 0, 4, 1 + x - 0.5 * Expression.Pow(x, 2) - Expression.Pow(x, 3) / 6);

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
