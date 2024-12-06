using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle24
{
    internal class Puzzle24
    {
        public enum Op
        {
            Plus,
            Minus,
            Multiply,
            Divide,
        }
        public Puzzle24(params int[] items)
        {
            _items = items;
        }
        public bool Solve24(out IList<int> items, out Op[] ops)
        {
            var p = new Permutation(_items);
            ops = new Op[3];
            foreach (var item in p)
            {
                var arr = item.ToArray();
                var s = item[0];
                var span = arr.AsSpan().Slice(1);
                var r = GetResult(span, s, ops.AsSpan());
                if (r.value == 24)
                {
                    items = item;
                    return true;
                }
            }
            items = _items;
            return false;
        }
        private enum FoldResult
        {
            Success,
            Fail,
            Proceed,
        }
        private struct ResultInfo
        {
            public ResultInfo(FoldResult r, int v)
            {
                result = r;
                value = v;
            }
            public FoldResult result;
            public int value;
        }

        private static ResultInfo GetResult(Span<int> items, int acc, Span<Op> ops)
        {
            if (items.Length <= 0)
            {
                if (acc == 24)
                {
                    return new ResultInfo(FoldResult.Success, acc);
                }
                return new ResultInfo(FoldResult.Proceed, acc);
            }
            for (Op op = Op.Plus; op <= Op.Divide; op++)
            {
                var newAcc = GetResult(acc, op, items[0]);
                if (newAcc < 0)
                {
                    continue;
                }
                ops[0] = op;
                var ret = GetResult(items.Slice(1), newAcc, ops.Slice(1));
                if (ret.result == FoldResult.Proceed)
                {
                    continue;
                }
                return ret;
            }
            return new ResultInfo(FoldResult.Fail, -1);
        }
        private static int GetResult(int acc, Op op, int value)
        {
            switch (op)
            {
                case Op.Plus:
                    return acc + value;
                case Op.Minus:
                    return acc - value;
                case Op.Multiply:
                    return acc * value;
                case Op.Divide:
                    if (acc % value == 0)
                    {
                        return acc / value;
                    }
                    return -1;
            }
            return -1;
        }

        public string GetResult()
        {
            return string.Empty;
        }

        private int[] _items;
    }
}
