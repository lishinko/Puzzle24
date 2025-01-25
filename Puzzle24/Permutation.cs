using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle24
{
    internal class Permutation(int[] nums) : IEnumerable<IList<int>>
    {
        public IEnumerator<IList<int>> GetEnumerator() {
            var perms = Permute(nums);
            foreach (var perm in perms) {
                yield return perm;
            }
        }

        private static IList<IList<int>> Permute(int[] nums)
        {
            var list = new List<IList<int>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }

        private static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }

        private static void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }
        static void PrintResult(IList<IList<int>> lists)
        {
            Console.WriteLine("[");
            foreach (var list in lists)
            {
                Console.WriteLine($"    [{string.Join(',', list)}]");
            }
            Console.WriteLine("]");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
