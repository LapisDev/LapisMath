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
    public class PatternTests
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
        public void MatchTest()
        {
            int counter = 1;
            Action<Expression, Expression, bool, string> test = (Expression expression, Expression pattern, bool isMatch, string expected) =>
            {
                IDictionary<Symbol, Expression> r;
                var result = Pattern.Match(expression, pattern, out r);
                
                Console.Write(
                    "{0} : Match {1} \t with \t {2} \t => {3}", 
                    counter, expression, pattern, result
                );
                
                if (result)
                {
                    var sb = new StringBuilder();
                    foreach (var t in r)
                    {
                        sb.Append(t.Key).Append(" : ").Append(t.Value).Append(" ; ");
                    }
                    sb.Length -= 3;
                    Console.Write("\t{0}", sb.ToString());
                    
                    if (result != isMatch || sb.ToString() != expected)
                    {
                        Assert.Fail();
                    }
                }
                
                if (result != isMatch)
                {
                    Assert.Fail();
                }
                counter += 1;
            };

            test(x + y, x, true, "x : x + y");
            test(x + y / x, x + y, true, "x : y / x ; y : x");
            test(2 * x + y, 2 * x + z, true, "x : x ; z : y");
            test(3 * x + y, x * y + z, true, "x : 3 ; y : x ; z : y");
            test(x + y * 2, x + z * y, false, null);
            test(Expression.Sin(x) + y * 2, Expression.Sin(x) + z * y,
                true, "y : 2 ; z : y ; x : x");
        }
    }
}
