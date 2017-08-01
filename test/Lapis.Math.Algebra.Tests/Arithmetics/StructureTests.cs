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
    public class StructureTests
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
        public void SymbolsTest()
        {
            int counter = 1;
            Action<Expression, string> test = (Expression expression, string expected) =>
            {
                var result = Structure.Symbols(expression);
                Console.Write("{0} : SymbolsOf {1} \t  => \t", counter, expression);
                
                var sb = new StringBuilder();
                foreach (var t in result)
                {
                    sb.Append(t.Identifier).Append(" , ");
                }
                sb.Length -= 3;
                Console.WriteLine(sb.ToString());

                if (sb.ToString() != expected)
                {
                    Assert.Fail();
                }
                counter++;
            };

            test(x + y, "x , y");
            test(x + y / x, "x , y");
            test(2 * x + z, "x , z");
            test(x * y + z, "x , y , z");          
            test(Expression.Sin(x) + y * 2, "y , x");
        }

        [TestMethod()]
        public void ContainsTest()
        {
            int counter = 1;
            Action<Expression, Symbol, bool> test = (Expression expression, Symbol symbol, bool expected) =>
            {
                var result = Structure.Contains(expression, symbol);
                Console.WriteLine(
                    "{0} : {1} contains {2} \t  => \t {3}", 
                    counter, expression, symbol, result
                );
                if (result != expected)
                {
                    Assert.Fail();
                }
                counter++;
            };

            test(x + y, x, true);
            test(x + y / x, z, false);
            test(2 * x + z, z, true);
            test(x * y + z, y, true);
            test(Expression.Sin(x) + y * 2, x, true);
        }

        [TestMethod()]
        public void ContainsAnyTest()
        {
            int counter = 1;
            Action<Expression, ISet<Symbol>, bool> test = (Expression expression, ISet<Symbol> symbols, bool expected) =>
            {
                var result = Structure.ContainsAny(expression, symbols);
                var sb = new StringBuilder();
                foreach (var t in symbols)
                {
                    sb.Append(t).Append(" , ");
                }
                sb.Length -= 3;
                Console.WriteLine(
                    "{0} : {1} contains any of {2} \t  => \t {3}", 
                    counter, expression, sb, result
                );
                if (result != expected)
                {
                    Assert.Fail();
                }
                counter++;
            };

            var set = new HashSet<Symbol>() { x, y, z };
            test(x + y, set, true);
            test(x + y / x, set, true);
            test(2 * a, set, false);
            test(x * y + z, set, true);
            test(Expression.Sin(x) + y * 2, set, true);
        }

        [TestMethod()]
        public void ContainsAllTest()
        {
            int counter = 1;
            Action<Expression, ISet<Symbol>, bool> test = (Expression expression, ISet<Symbol> symbols, bool expected) =>
            {
                var result = Structure.ContainsAll(expression, symbols);
                var sb = new StringBuilder();
                foreach (var t in symbols)
                {
                    sb.Append(t).Append(" , ");
                }
                sb.Length -= 3;
                Console.WriteLine(
                    "{0} : {1} contains all of {2} \t  => \t {3}", 
                    counter, expression, sb, result
                );
                if (result != expected)
                {
                    Assert.Fail();
                }
                counter++;
            };

            var set = new HashSet<Symbol>() { x, y, z };
            test(x + y, set, false);
            test(x + y / x, set, false);
            test(2 * a, set, false);
            test(x * y + z, set, true);
            test(Expression.Sin(x) + y * 2, set, false);            
        }

        [TestMethod()]
        public void ReplaceTest()
        {
            int counter = 1;
            Action<Expression, Expression, Expression, string> test = (Expression expression, Expression old, Expression n, string expected) =>
            {
                var result = Structure.Replace(expression, old,n);
                Console.WriteLine(
                    "{0} : Replace {1} in {2} with {3} \t  => \t {4}", 
                    counter, old, expression, n, result
                );
                if (result.ToString() != expected)
                {
                    Assert.Fail();
                }
                counter++;
            };
                     
            test(x + y, x, 1, "1 + y");
            test(x + y / x, x, z, "y / z + z");
            test(2 * a, 2, 5, "5 * a");
            test(x * y + z, x * y, z, "z + z");
            test(Expression.Sin(x) + y * 2, Expression.Sin(x), Expression.Cos(x), "2 * y + cos(x)");
        }
    }
}
