﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle24
{
    public class PuzzleSolver(params int[] items)
    {
        public enum Op
        {
            Plus,
            Minus,
            Multiply,
            Divide,
        }

        public bool Solve24(out IList<int> items1, out Op[] ops)
        {
            var p = new Permutation(items);
            ops = new Op[3];
            foreach (var item in p)
            {
                // Console.WriteLine($"item = {item[0]}, {item[1]}, {item[2]}, {item[3]}");
                var arr = item.ToArray();
                var s = item[0];
                var span = arr.AsSpan().Slice(1);
                var r = GetResult(span, s, ops.AsSpan());
                if (r.value == 24)
                {
                    items1 = item;
                    return true;
                }
            }
            items1 = items;
            return false;
        }
        private enum FoldResult
        {
            Success,
            NotFound,
        }
        private struct ResultInfo(FoldResult r, int v)
        {
            public FoldResult result = r;
            public int value = v;
        }

        private static ResultInfo GetResult(Span<int> items, int acc, Span<Op> ops)
        {
            if (items.Length <= 0)
            {
                if (acc == 24)
                {
                    Console.WriteLine($"success");
                    return new ResultInfo(FoldResult.Success, acc);
                }
                // Console.WriteLine($"proceed, acc = {acc}");
                return new ResultInfo(FoldResult.NotFound, acc);
            }
            for (var op = Op.Plus; op <= Op.Divide; op++)
            {
                var newAcc = GetResult(acc, op, items[0]);
                if (newAcc < 0)
                {
                    // Console.WriteLine($"continue, {acc} {op} {items[0]} = {newAcc}");
                    continue;
                }
                ops[0] = op;
                var ret = GetResult(items.Slice(1), newAcc, ops.Slice(1));
                if (ret.result == FoldResult.NotFound)
                {
                    continue;
                }
                return ret;
            }
            return new ResultInfo(FoldResult.NotFound, -1);
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
            return -1;
        }
        public static string PrintResult(IList<int> item, Op[] ops)
        {
            StringBuilder ss = new StringBuilder();
            var i = 0;
            var result = item[i];
            var old = result;
            result = PuzzleSolver.GetResult(old, ops[i], item[i + 1]);
            ss.AppendLine($"{old} {Op2Symbol(ops[i])} {item[i + 1]} = {result}");

            i++;
            old = result;
            result = PuzzleSolver.GetResult(old, ops[i], item[i + 1]);
            ss.AppendLine($"{old} {Op2Symbol(ops[i])} {item[i + 1]} = {result}");

            i++;
            old = result;
            result = PuzzleSolver.GetResult(old, ops[i], item[i + 1]);
            ss.AppendLine($"{old} {Op2Symbol(ops[i])} {item[i + 1]} = {result}");

            return ss.ToString();
        }
        private static string Op2Symbol(Op op)
        {
            return op switch
            {
                Op.Minus => "-",
                Op.Plus => "+",
                Op.Multiply => "x",
                Op.Divide => "/",
                _ => "+"
            };
        }
    }
}
