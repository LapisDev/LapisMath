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
    public class TrigonometricTests
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
                var result = Trigonometric.Expand(input);
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

            test(Expression.Sin(2 * x), 2 * Expression.Sin(x) * Expression.Cos(x));
            test(Expression.Sin(a + x), Expression.Sin(x) * Expression.Cos(a) + Expression.Sin(a) * Expression.Cos(x));
                      
            test(Expression.Sin(2 * x + 3 * y),
                (Expression.Pow(Expression.Cos(x), 2) - Expression.Pow(Expression.Sin(x), 2)) * (-Expression.Pow(Expression.Sin(y), 3) + 3 * Expression.Sin(y) * Expression.Pow(Expression.Cos(y), 2)) +
                2 * Expression.Sin(x) * Expression.Cos(x) * (Expression.Pow(Expression.Cos(y), 3) - 3 * Expression.Pow(Expression.Sin(y), 2) * Expression.Cos(y)));

            test(Expression.Sin(2 * (x + y)), 
                2 * Expression.Sin(y) * (Expression.Pow(Expression.Cos(x), 2) - Expression.Pow(Expression.Sin(x), 2)) * Expression.Cos(y) + 2 * Expression.Sin(x) * Expression.Cos(x) * ((Expression.Pow(Expression.Cos(y), 2) - Expression.Pow(Expression.Sin(y), 2))));
            test(Expression.Cos(5 * x),
                5 * Expression.Pow(Expression.Sin(x), 4) * Expression.Cos(x) - 10 * Expression.Pow(Expression.Sin(x), 2) * Expression.Pow(Expression.Cos(x), 3) + Expression.Pow(Expression.Cos(x), 5));
                        
            test(Expression.Sin(2 * x) - 2 * Expression.Sin(x) * Expression.Cos(x), 0);

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
                var result = Trigonometric.Contract(input);
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

            test(Expression.Sin(a) * Expression.Sin(b), 0.5 * Expression.Cos(a - b) - 0.5 * Expression.Cos(a + b));
            // test((Expression.Sin(x) + Expression.Cos(y)) * Expression.Cos(y),
            //     0.5 + 0.5 * Expression.Sin(x + y) + 0.5 * Expression.Sin(x - y) + 0.5 * Expression.Cos(2 * y));
            test(Expression.Pow(Expression.Sin(x), 2) * Expression.Pow(Expression.Cos(x), 2),
                0.125 - 0.125 * Expression.Cos(4 * x));
            test(Expression.Pow(Expression.Cos(x), 4), 0.375 + 0.5 * Expression.Cos(2 * x) + 0.125 * Expression.Cos(4 * x));

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
                var result = Trigonometric.Simplify(input);
                if (expected == result)
                    Console.WriteLine(string.Format("{0}\t Passed: {1} \t => \t {2}.", counter, input, result));
                else
                {
                    failed = true;
                    var str = string.Format("\n{0}\t FAILED: {1} \t => \t {2} \t , \t {3} expected.\n", counter, input, result.ToString(ExpressionFormat.Strict), expected);
                    Console.WriteLine(str);
                    sb.Append(str);
                }
                counter++;
            };      

            test(Expression.Pow(Expression.Cos(x) + Expression.Sin(x), 4) + Expression.Pow(Expression.Cos(x) - Expression.Sin(x), 4) + Expression.Cos(4 * x) - 3,
                0);
            // test(Expression.Sin(x) + Expression.Sin(y) - 2 * Expression.Sin(x / 2 + y / 2) * Expression.Cos(x / 2 - y / 2),
            //     Expression.Sin(y) - 0.5 * Expression.Sin(x - y) - 0.5 * Expression.Sin(0.5 * x - 0.5 * y - (0.5 * x - 0.5 * y)) - 0.5 * Expression.Sin(-0.5 * x + 0.5 * y - (0.5 * x - 0.5 * y)));
            test(Expression.Sin(x) + Expression.Sin(y) - 2 * Expression.Sin(x / 2 + y / 2) * Expression.Cos(x / 2 - y / 2),
                0);         

            Assert.IsFalse(failed, sb.ToString());
        }
    }
}
