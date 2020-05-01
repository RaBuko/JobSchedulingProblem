using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Solver.Methods
{
    public class SimulatedAnnealingMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions options)
        {
            var methodOptions = options as SimulatedAnnealingOptions;
            return methodOptions;
        }

        public (List<int> jobOrder, int minTardiness) Solve(IMethodOptions imethodoptions, Stopwatch stopwatch)
        {
            var options = imethodoptions as SimulatedAnnealingOptions;
            List<int> jobOrder = options.Data.Select(x => x.Index).ToList();
            var minTardiness = int.MaxValue;

            var count = 0;
            int step = 2;
            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();

            try
            {
                while (count < options.IterationCount)
                {
                    options.GuiConnection?.LogText?.Invoke($"Iteracja: {count}");

                    List<int> candidateOrder = new List<int>(jobOrder);
                    candidateOrder.Swap(ThreadSafeRandom.ThisThreadsRandom.Next(0, jobOrder.Count), ThreadSafeRandom.ThisThreadsRandom.Next(0, jobOrder.Count));

                    options.GuiConnection?.LogText?.Invoke($"Kandydat: {string.Join(",", candidateOrder)}");
                    var penaltyChange = options.Data.JobsFromIndexList(candidateOrder).CountPenalty() - options.Data.JobsFromIndexList(jobOrder).CountPenalty();
                    options.GuiConnection?.LogText?.Invoke($"Zmiana sumy opóźnień: {penaltyChange}");

                    var alpha = 10 + count * step * 2; // control parameter
                    var acceptanceProbability = Math.Exp(-alpha * penaltyChange);
                    options.GuiConnection?.LogText?.Invoke($"Szansa akceptacji = { acceptanceProbability}");

                    if (acceptanceProbability > ThreadSafeRandom.GetRandomDouble(0, 1))
                    {
                        jobOrder = candidateOrder;
                        options.GuiConnection?.LogText?.Invoke($"Zaakceptowano{Environment.NewLine}");
                    }

                    count += 1;
                    options.CancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                imethodoptions.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }

            stopwatch.Stop();
            minTardiness = options.Data.JobsFromIndexList(jobOrder).CountPenalty();
            return (jobOrder, minTardiness);
        }
    }
}
