using Solver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Solver.Utils;

namespace Solver.Methods
{
    public class BruteForceMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions options) => options as BruteForceOptions;

        public (List<int>, int) Solve(IMethodOptions options, System.Diagnostics.Stopwatch stopwatch)
        {
            var data = options.Data;
            int time = 0;
            int penalty = 0;
            int minPenalty = int.MaxValue;
            int jobCount = data.Count;
            int[] temp = data.Select(x => x.Index).ToArray();
            int[] bestOrder = temp;
            string tolog;

            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            if (!stopwatch.IsRunning) stopwatch.Start();

            while (true)
            {
                time = 0;
                penalty = 0;

                tolog = "";
                for (int it = 0; it < jobCount; it++)
                {
                    tolog += temp[it] + (it != jobCount - 1 ? "," : string.Empty);
                    time += data[temp[it]].Time;
                    penalty += data[temp[it]].CountPenalty(time);
                }

                tolog += $" | Wynik = {penalty}";

                if (minPenalty > penalty)
                {
                    tolog += " < BEST";
                    minPenalty = penalty;
                    bestOrder = (int[])temp.Clone();
                }

                options.GuiConnection?.LogText?.Invoke(tolog);

                int i = jobCount - 1;
                while (i > 0 && temp[i - 1] >= temp[i]) { i--; }


                if (i == 0)
                {
                    break;
                }

                int j = i;
                while (j < jobCount && temp[j] > temp[i - 1]) j++;
                j--;
                IntManip.SwapInts(ref temp[i - 1], ref temp[j]);
                temp.ReverseSubarray(i, jobCount - 1);

            }
            return (bestOrder.ToList(), minPenalty);
        }
    }
}
