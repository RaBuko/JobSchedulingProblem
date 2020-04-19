using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Solver.Utils
{
    public static class ArrayExtension
    {
        public static int[] ReverseSubarray(this int[] array, int startPos, int endPos)
        {
            if (startPos > endPos) throw new ArgumentException();
            while (startPos < endPos)
            {
                (array[startPos], array[endPos]) = (array[endPos], array[startPos]);
                startPos += 1;
                endPos -= 1;
            }

            return array;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }

}
