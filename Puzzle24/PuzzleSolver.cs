using System;
using System.Collections.Generic;
using System.Text;

namespace Puzzle24;

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
        var opElements = new List<int>
        {
            (int)Op.Plus,
            (int)Op.Minus,
            (int)Op.Multiply,
            (int)Op.Divide,
        };
        var o = new Catesian(opElements.ToArray(), 3);
        foreach (var item in p)
        {
            foreach (var op in o)
            {
                if (GetResult1(item, op) == 24)
                {
                    items1 = item;
                    ops = [(Op)op[0], (Op)op[1], (Op)op[2]];
                    return true;
                }

                if (GetResult2(item, op) == 24)
                {
                    items1 = item;
                    ops = [(Op)op[0], (Op)op[1], (Op)op[2]];
                    return true;
                }
            }
        }

        ops = null;
        items1 = null;

        return false;
    }

    /// <summary>
    /// result = (i0 op0 i1) op1 (i2 op2 i3)
    /// 2叉树的第5种可能
    /// </summary>
    /// <param name="item"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    private int GetResult2(IList<int> item, List<int> op)
    {
        int left = GetResult(item[0], (Op)op[0], item[1]);
        if (left < 0)
        {
            return -1;
        }
        int right = GetResult(item[2], (Op)op[2], item[3]);
        if (right < 0)
        {
            return -1;
        }
        int acc = GetResult(left, (Op)op[1], right);
        return acc;
    }

    /// <summary>
    /// result = ((i0 op0 i1) op1 i2) op2 i3
    /// 3节点2叉树有5种可能，
    /// 但是因为我们的op,item都是有重复元素的，
    /// 所以这一种运算符顺序就可以代表4种2叉树了
    /// </summary>
    /// <param name="item"></param>
    /// <param name="op"></param>
    /// <returns></returns>
    private static int GetResult1(IList<int> item, List<int> op)
    {
        int acc = GetResult(item[0], (Op)op[0], item[1]);
        if (acc < 0)
        {
            return -1;
        }
        acc = GetResult(acc, (Op)op[1], item[2]);
        if (acc < 0)
        {
            return -1;
        }
        acc = GetResult(acc, (Op)op[2], item[3]);
        return acc;
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