using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Utils
{
    public static class IntManip
    {
        public static void SwapInts(ref int a, ref int b)
        {
            if (a == b) return;
            (a, b) = (b, a);
        }

        public static string IntToBin(this int number, int length)
        {
            return Convert.ToString(number, 2).PadLeft(length, '0');
        }

        public static int BinToInt(this string binary)
        {
            return Convert.ToInt32(binary, 2);
        }

        public static List<Job> JobsFromIndexList(List<int> indexes, List<Job> data)
        {
            var resultData = new List<Job>();
            indexes.ForEach(i => resultData.Add(data.Find(x => x.Index == i)));
            return resultData;
        }
    }
}
