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
    public class ElementaryTests
    {
        private readonly Symbol x = Symbol.FromString("x");
        private readonly Symbol y = Symbol.FromString("y");
        private readonly Symbol z = Symbol.FromString("z");
        private readonly Symbol a = Symbol.FromString("a");
        private readonly Symbol b = Symbol.FromString("b");
        private readonly Symbol c = Symbol.FromString("c");
        private readonly Symbol d = Symbol.FromString("d");

        


        [TestMethod()]
        public void ExpandTest()
        {
            int counter = 1;
            bool failed = false;
            var sb = new StringBuilder();
            Action<Expression, Expression> test = (Expression input, Expression expected) =>
            {
                var result = Elementary.Expand(input);
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

            test(2 * (a + b) - (a + b), a + b);
            test((a + b) - 2 * (a + b), -a - b);

            test((x + 1) * (x - 1), Expression.Pow(x, 2) - 1);
            test((x + 1) * (x + 1), Expression.Pow(x, 2) + 2 * x + 1);

            test((x * Expression.Pow(y + 1, 1.5) + 1) * (x * Expression.Pow(y + 1, 1.5) - 1),
                -1 + Expression.Pow(1 + y, 3) * Expression.Pow(x, 2));
            test(Elementary.Expand((x * Expression.Pow(y + 1, 1.5) + 1) * (x * Expression.Pow(y + 1, 1.5) - 1)),
                -1 + Expression.Pow(x, 2) + 3 * Expression.Pow(x, 2) * y + 3 * Expression.Pow(x, 2) * Expression.Pow(y, 2) + Expression.Pow(x, 2) * Expression.Pow(y, 3));

            test((x + 1) * (x + 3), Expression.Pow(x, 2) + 4 * x + 3);
            test(Expression.Pow(a + b, 2), Expression.Pow(a, 2) + Expression.Pow(b, 2) + 2 * a * b);

            test(Expression.Pow(a + b, 3), Expression.Pow(a, 3) + Expression.Pow(b, 3) + 3 * Expression.Pow(a, 2) * b + 3 * a * Expression.Pow(b, 2));
            test(Expression.Pow(a + b, 4), Expression.Pow(a, 4) + Expression.Pow(b, 4) + 4 * Expression.Pow(a, 3) * b + 4 * a * Expression.Pow(b, 3) + 6 * Expression.Pow(a, 2) * Expression.Pow(b, 2));
            test(Expression.Pow(a + b + c, 2), Expression.Pow(a, 2) + Expression.Pow(b, 2) + Expression.Pow(c, 2) + 2 * a * b + 2 * a * c + 2 * b * c);

            Assert.IsFalse(failed,sb.ToString());
           
        }
    }
}
