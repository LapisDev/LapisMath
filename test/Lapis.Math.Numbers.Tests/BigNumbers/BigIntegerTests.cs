using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lapis.Math.Numbers.BigNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
namespace Lapis.Math.Numbers.BigNumbers.Tests
{
    [TestClass()]
    public class BigIntegerTests
    {
        [TestMethod()]
        public void FactorialTest()
        {
            var sw = new Stopwatch();
           
            var prod = BigInteger.FromInt32(1);
            sw.Start();           
            for (int i = 1; i <= 5000; i++)
                prod = BigInteger.Multiply(prod, BigInteger.FromInt32(i));
            sw.Stop();
            var t1 = sw.ElapsedMilliseconds;
            Console.WriteLine("Calulation: {0} ms", t1);
            
            sw.Start();
            var actual = prod.ToString();
            sw.Stop();   
            var t2 = sw.ElapsedMilliseconds;        
            Console.WriteLine("ToString: {0} ms", t2);

            var expected = System.IO.File.ReadAllText("test_data/1.expected.txt");

            if (expected != actual)
            {
                System.IO.File.WriteAllText("test_data/1.actual.txt", actual);
                Assert.Fail();
            }
            if (t1 > 500 || t2 > 500)
            {
                Assert.Fail("Timeout");
            }
        }
    }
}
