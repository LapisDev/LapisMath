using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lapis.Math.Algebra.Expressions;

namespace Lapis.Math.Algebra.Expressions.Tests
{
 
    public partial class ExpressionTests
    {
     

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("1 / (a * b)",(1 / (a * b)).ToString(ExpressionFormat.Friendly));
            Assert.AreEqual("a ^ (-1) * b ^ (-1)", (1 / (a * b)).ToString(ExpressionFormat.Strict));
            Assert.AreEqual("a / b + c / d", (a / b + c / d).ToString(ExpressionFormat.Friendly));

            Assert.AreEqual("((-1) + (1 + (x + y) ^ 2) ^ (-2)) * (1 + (1 + (x + y) ^ 2) ^ (-1/2)) * (1 + x) ^ (-1)",
                (((-1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 2)) * (1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 0.5))) / (1 + x)).ToString(ExpressionFormat.Strict));
            Assert.AreEqual("((-1 + 1 / (1 + (x + y) ^ 2) ^ 2) * (1 + (1 + (x + y) ^ 2) ^ (-1/2))) / (1 + x)",
                (((-1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 2)) * (1 + Expression.Pow(1 / (1 + Expression.Pow(x + y, 2)), 0.5))) / (1 + x)).ToString(ExpressionFormat.Friendly));
        }
    }
}
