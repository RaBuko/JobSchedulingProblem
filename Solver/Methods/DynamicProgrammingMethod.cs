using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Solver.Methods
{
    public class DynamicProgrammingMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions methodOptions)
        {
            var subsets = new Dictionary<string, int>
            {
                [0.IntToBin(methodOptions.Data.Count)] = 0
            };

            for (int i = 1; i < Math.Pow(2, methodOptions.Data.Count); i++)
            {
                subsets[i.IntToBin(methodOptions.Data.Count)] = int.MaxValue;
            }
            var dynamicMethodOptions = methodOptions as DynamicProgrammingOptions;
            dynamicMethodOptions.Subsets = subsets;
            return dynamicMethodOptions;
        }

        public (List<int> jobOrder, int minTardiness) Solve(IMethodOptions options, Stopwatch stopwatch)
        {
            DynamicProgrammingOptions dpaOptions = options as DynamicProgrammingOptions;
            var jobs = dpaOptions.Data;
            var subsets = dpaOptions.Subsets;

            int rozw = int.MaxValue;
            int result = 0;
            int index = 0;
            int cmax = 0;
            int opt = 0;
            string tmp = string.Empty;

            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");

            if (!stopwatch.IsRunning) stopwatch.Start();
            try
            {
                for (int i = 1; i < dpaOptions.Subsets.Count; i++)
                {
                    tmp = i.IntToBin(jobs.Count);
                    rozw = int.MaxValue;
                    cmax = 0;
                    options.GuiConnection?.LogText?.Invoke($"OPT({tmp}):");

                    for (int j = 0; j < tmp.Length; j++)
                    {
                        if (tmp[j].Equals('1'))
                        {
                            cmax += jobs[jobs.Count - j - 1].Time; // wyliczenie cmax calkowitego dla danego i
                        }
                    }

                    options.GuiConnection?.LogText?.Invoke($"\tCMAX : {cmax}");

                    for (int j = 0; j < tmp.Length; j++)
                    {
                        if (tmp[j].Equals('1'))
                        {
                            index = jobs.Count - j - 1;
                            tmp = tmp.ReplaceAt(j, '0');
                            opt = jobs[index].CountPenalty(cmax);
                            result = subsets[tmp] + opt; // OPT(xn,..,xi+1,xi-1,..,x0) + wx*Tx 
                            options.GuiConnection?.LogText?.Invoke($"\tOPT({tmp}) + w{index}T{index} = {subsets[tmp]} + {opt} = {result}");
                            rozw = Math.Min(rozw, result); // wybranie najmniejszego kosztu dla danego i
                            tmp = i.IntToBin(jobs.Count);
                        }
                    }
                    options.GuiConnection?.LogGraphics?.Invoke(jobs.JobsFromBitString(tmp));
                    subsets[tmp] = rozw;
                    options.CancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                options.GuiConnection?.LogText?.Invoke("Przerwano zadanie");
            }
            stopwatch.Stop();
            dpaOptions.Subsets = null;
            return (new List<int>(), rozw);
        }
    }
}
