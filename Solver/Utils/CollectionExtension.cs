using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.Utils
{
    static class CollectionExtension
    {
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerableToClone) where T : ICloneable
        {
            return enumerableToClone.Select(item => (T)item.Clone());
        }

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

        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
