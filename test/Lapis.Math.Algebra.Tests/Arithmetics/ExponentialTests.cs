using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lapis.Math.Algebra.Arithmetics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Arithmetics.Tests
{
    [TestClass()]
    public class ExponentialTests
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
        public void ExpandTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Exponential.Expand(input);
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

            test(Expression.Exp(2 * x + y), Expression.Pow(Expression.Exp(x), 2) * Expression.Exp(y));
            test(Expression.Exp(2 * a * x + 3 * y * z), Expression.Pow(Expression.Exp(a * x), 2) * Expression.Pow(Expression.Exp(y * z), 3));
            test(Expression.Exp(2 * (x + y)), Expression.Pow(Expression.Exp(x), 2) * Expression.Pow(Expression.Exp(y), 2));
            test(1 / Expression.Exp(2 * x) - Expression.Pow(Expression.Exp(x), 2), -Expression.Pow(Expression.Exp(x), 2) + Expression.Pow(Expression.Exp(x), -2));
            test(Expression.Exp((x + y) * (x - y)), Expression.Exp(Expression.Pow(x, 2)) / Expression.Exp(Expression.Pow(y, 2)));

            test(Expression.Ln(Expression.Pow(c * x, a)) + Expression.Ln(Expression.Pow(y, b) * z), a * Expression.Ln(c) + a * Expression.Ln(x) + b * Expression.Ln(y) + Expression.Ln(z));

            Assert.IsFalse(failed, sb.ToString());
        }

        [TestMethod()]
        public void ContractTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Exponential.Contract(input);
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

            test(Expression.Exp(x) * Expression.Exp(y), Expression.Exp(x + y));
            test(Expression.Pow( Expression.Exp(x),a), Expression.Exp(a*x));
            test(Expression.Exp(x) *(Expression .Exp (x)+ Expression.Exp(y)),Expression .Exp (2*x)+ Expression.Exp(x + y));
            test(Expression.Pow(Expression.Exp(Expression.Exp(x)), Expression.Exp(y)), Expression.Exp(Expression.Exp(x + y)));

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
                var result = Exponential.Simplify(input);
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

            test(1 / (Expression.Exp(x) * (Expression.Exp(y) + Expression.Exp(-x))) - (Expression.Exp(x + y) - 1) / (Expression.Pow(Expression.Exp(x + y), 2) - 1),
                0);

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
