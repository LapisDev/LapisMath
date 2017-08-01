using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lapis.Math.Algebra.Expressions;
using Lapis.Math.LinearAlgebra;

namespace Lapis.Math.LinearAlgebra.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        private Matrix matA = Matrix.FromArray(new Expression[,]
        {
            { Number.FromInt32(1), Number.FromInt32(1), Number.FromInt32(1) },
            { Number.FromInt32(2), Number.FromInt32(3), Number.FromInt32(0) },
            { Number.FromInt32(1), Number.FromInt32(0), Number.FromInt32(2) }
        });

        private Matrix matB = Matrix.FromArray(new Expression[,]
        {
            { Number.FromInt32(0), Number.FromInt32(0), Number.FromInt32(-1) },
            { Number.FromInt32(1), Number.FromInt32(4), Number.FromInt32(-1) },
            { Number.FromInt32(-1), Number.FromInt32(-4), Number.FromInt32(2) } 
        });

        private Matrix matC = Matrix.FromArray(new Expression[,]
        {
            { Symbol.FromString("a"), Symbol.FromString("b") },
            { Symbol.FromString("c"), Symbol.FromString("d") }
        });

        
        [TestMethod()]
        public void TransposeTest()
        {
            var r = Matrix.Transpose(matA);
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Number.FromInt32(1), Number.FromInt32(2), Number.FromInt32(1) },
                { Number.FromInt32(1), Number.FromInt32(3), Number.FromInt32(0) },
                { Number.FromInt32(1), Number.FromInt32(0), Number.FromInt32(2) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void ExchangeRowsTest()
        {
            var r = Matrix.ExchangeRows(matA, 1, 2);
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Number.FromInt32(2), Number.FromInt32(3), Number.FromInt32(0) },
                { Number.FromInt32(1), Number.FromInt32(1), Number.FromInt32(1) },
                { Number.FromInt32(1), Number.FromInt32(0), Number.FromInt32(2) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void RowMultiplyTest()
        {
            var r = Matrix.RowMultiply(matA, 1, Symbol.FromString("x"));
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Symbol.FromString("x"), Symbol.FromString("x"), Symbol.FromString("x") },
                { Number.FromInt32(2), Number.FromInt32(3), Number.FromInt32(0) },
                { Number.FromInt32(1), Number.FromInt32(0), Number.FromInt32(2) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void RowMultiplyAddTest()
        {
            var r = Matrix.RowMultiplyAdd(matA, 1, 2, Symbol.FromString("x"));
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Number.FromInt32(1), Number.FromInt32(1), Number.FromInt32(1) },
                { Number.FromInt32(2) + Symbol.FromString("x"), Number.FromInt32(3) + Symbol.FromString("x"), Symbol.FromString("x") },
                { Number.FromInt32(1), Number.FromInt32(0), Number.FromInt32(2) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void RowEchelonTest()
        {
            var r = Matrix.RowEchelon(matB);
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Number.FromInt32(1), Number.FromInt32(4), Number.FromInt32(-1) },
                { Number.FromInt32(0), Number.FromInt32(0), Number.FromInt32(-1) },
                { Number.FromInt32(0), Number.FromInt32(0), Number.FromInt32(0) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void RowSimplestTest()
        {
            var r = Matrix.RowSimplest(matB);
            Console.WriteLine(r.ToString());
            var excepted = Matrix.FromArray(new Expression[,]
            {
                { Number.FromInt32(1), Number.FromInt32(4), Number.FromInt32(0) },
                { Number.FromInt32(0), Number.FromInt32(0), Number.FromInt32(1) },
                { Number.FromInt32(0), Number.FromInt32(0), Number.FromInt32(0) }
            });
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void RankTest()
        {
            var r = Matrix.Rank(matB);
            Console.WriteLine(r.ToString());
            var excepted = 2;
            if (r != excepted)
                Assert.Fail();
        }

        [TestMethod()]
        public void InverseTest()
        {
            var r = Matrix.Inverse(matC);
            Console.WriteLine(r.ToString());
            Assert.Inconclusive();
        }

        [TestMethod()]
        public void DeterminantTest()
        {
            var r = Matrix.Determinant(matC);
            Console.WriteLine(r.ToString());
            Assert.Inconclusive();
        }
    }
}
