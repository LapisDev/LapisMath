/********************************************************************************
 * Module      : Lapis.Math.Algebra
 * Class       : ExpressionComparer
 * Description : Implenments an IComparer<Expression>
 * Created     : 2015/4/5
 * Note        :
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lapis.Math.Numbers;

namespace Lapis.Math.Algebra.Expressions
{
    class ExpressionComparer : IComparer<Expression>
    {
        public static readonly ExpressionComparer Instance = new ExpressionComparer();

        public int Compare(Expression x, Expression y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();      
            if (x is Number && y is Number)
                return ((Real)(Number)x).CompareTo((Real)(Number)y);
            else if (x is Symbol && y is Symbol)
                return SymCompare((Symbol)x, (Symbol)y);
            else if (x is Sum && y is Sum)
                return SumCompare((Sum)x, (Sum)y);
            else if (x is Product && y is Product)
                return ProdCompare((Product)x, (Product)y);
            else if (x is Power && y is Power)
                return PowCompare((Power)x, (Power)y);
            else if (x is Function && y is Function)
                return FuncCompare((Function)x, (Function)y);
            else if (x is MultiArgumentFunction && y is MultiArgumentFunction)
                return MFuncCompare((MultiArgumentFunction)x, (MultiArgumentFunction)y);
            else if (x is Number)
                return -1;
            else if (y is Number)
                return 1;
            else if (x is Product)
            {
                var cmp = Compare(((Product)x)[0], y);
                if (cmp != 0)
                    return cmp;
                else
                    return 1;
            }
            else if (y is Product)
            {
                var cmp = Compare(x, ((Product)y)[0]);
                if (cmp != 0)
                    return cmp;
                else
                    return -1;
            }
            else if (x is Power)
            {
                var pow = (Power)x;
                int cmp = Compare(pow.Base, (y));
                if (cmp != 0)
                    return cmp;
                else
                    return Compare(pow.Exponent, Number.One);
            }          
            else if (y is Power)
            {
                var pow = (Power)y;
                int cmp = Compare(x, pow.Base);
                if (cmp != 0)
                    return cmp;
                else
                    return Compare(Number.One, pow.Exponent);
            }
            else if (x is Sum)
            {
                var cmp = Compare(((Sum)x)[0], y);
                if (cmp != 0)
                    return cmp;
                else
                    return 1;
            }
            else if (y is Sum)
            {
                var cmp = Compare(x, ((Sum)y)[0]);
                if (cmp != 0)
                    return cmp;
                else
                    return -1;
            }
            else if (x is Function && y is MultiArgumentFunction)
            {
                var func = (Function)x;
                var mfunc = (MultiArgumentFunction)y;
                int cmp = func.Identifier.CompareTo(mfunc.Identifier);
                if (cmp != 0)
                    return cmp;
                else
                {
                    cmp = Compare(func.Argument, mfunc[0]);
                    if (cmp != 0)
                        return cmp;
                    else
                        return -1;
                }
            }
            else if (x is MultiArgumentFunction && y is Function)
            {
                var mfunc = (MultiArgumentFunction)x;
                var func = (Function)y;
                int cmp = mfunc.Identifier.CompareTo(func.Identifier);
                if (cmp != 0)
                    return cmp;
                else
                {
                    cmp = Compare(mfunc[0],func.Argument);
                    if (cmp != 0)
                        return cmp;
                    else
                        return 1;
                }
            }
            else if (x is Function && y is Symbol ||
                x is MultiArgumentFunction && y is Symbol)
                return 1;
            else if (x is Symbol && y is Function ||
                x is Symbol && y is MultiArgumentFunction)
                return -1;
            else if (x is Undefined ||
                x is PositiveInfinity ||
                x is NegativeInfinity)
                return 1;
            else if (y is Undefined ||
                y is PositiveInfinity ||
                y is NegativeInfinity)
                return -1;
            else
                return -1;
        }


        #region Private

        private int SymCompare(Symbol x, Symbol y)
        {
            return x.Identifier.CompareTo(y.Identifier);
        }

        private int SumCompare(Sum x, Sum y)
        {
            return Util.ItemCompare(x, y, this);
        }

        private int ProdCompare(Product x, Product y)
        {
            return Util.ItemCompare(x, y, this);
        }

        private int PowCompare(Power x, Power y)
        {
            int cmp = Compare(x.Base, y.Base);
            if (cmp != 0)
                return cmp;
            else
                return Compare(x.Exponent, y.Exponent);
        }

        private int FuncCompare(Function x, Function y)
        {
            if (x.Identifier != y.Identifier)
                return x.Identifier.CompareTo(y.Identifier);
            else
                return Compare(x.Argument, y.Argument);
        }

        private int MFuncCompare(MultiArgumentFunction x, MultiArgumentFunction y)
        {
            if (x.Identifier != y.Identifier)
                return x.Identifier.CompareTo(y.Identifier);
            else
                return Util.ItemCompare(x, y, this);
        }

        private ExpressionComparer() { }

        #endregion
    }
}
