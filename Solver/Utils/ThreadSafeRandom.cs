using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Solver.Utils
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ??= new Random(unchecked((Environment.TickCount * 31) + Thread.CurrentThread.ManagedThreadId)); }
        }

        public static double GetRandomDouble(double min = 0.0, double max = 1.0)
        {
            return ThisThreadsRandom.NextDouble() * (max - min) + min;
        }

        public static List<int> GenerateUniqueRandom(int count, int min, int max)
        { 

            if (max <= min || count < 0 || (count > max - min && max - min > 0))
            {
                throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                        " (" + ((long)max - (long)min) + " values), or count " + count + " is illegal");
            }

            HashSet<int> candidates = new HashSet<int>();
            for (int top = max - count; top < max; top++)
            {
                if (!candidates.Add(ThisThreadsRandom.Next(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }

            List<int> result = candidates.ToList();

            for (int i = result.Count - 1; i > 0; i--)
            {
                int k = ThisThreadsRandom.Next(i + 1);
                int tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }
            return result;
        }

    }
}
