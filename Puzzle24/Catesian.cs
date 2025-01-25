using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Puzzle24;

public class Catesian(int[] input, int itemNum) : IEnumerable<List<int>>
{
    /// <summary>
    /// 笛卡尔积
    /// </summary>
    /// <returns></returns>
    public IEnumerator<List<int>> GetEnumerator()
    {       
        int totalItemCount = Pow(input.Length, itemNum);
        List<List<int>> list = new List<List<int>>(totalItemCount);
        for (int i = 0; i < totalItemCount; i++)
        {
            var item = new List<int>(itemNum);
            list.Add(item);
        }

        for (var idx = totalItemCount / input.Length; idx >= 1; idx /= input.Length)
        {
            // Console.WriteLine($"idx = {idx} ");
            try
            {
                for (var i = 0; i < totalItemCount; i++)
                {
                    var item = list[i];
                    var j = (i / idx) % input.Length;
                    item.Add(input[j]);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"error: {e.Message}\n idx = {idx}");
                throw;
            }
            
        }

        // StringBuilder ss = new StringBuilder();
        // ss.AppendLine($"l count : {totalItemCount} \n ");
        // foreach (var l in list)
        // {
        //     ss.AppendLine($"l = {l.Count}, {(PuzzleSolver.Op)l[0]}, {(PuzzleSolver.Op)l[1]}, {(PuzzleSolver.Op)l[2]}");
        // }
        // Console.WriteLine(ss.ToString());
        return list.GetEnumerator();
    }
    /// <summary>
    /// 计算笛卡尔积的总数，如4个元素，3次，则结果为4的3次方。
    /// 不使用Math.Pow是因为它是浮点数，我担心结果不正确。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static int Pow(int x, int y)
    {
        int result = 1;
        for (int i = 0; i < y; i++)
        {
            result *= x;
        }
        return result;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}