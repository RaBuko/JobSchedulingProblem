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
            if (methodOptions == null) methodOptions = new DynamicProgrammingOptions();
            var subsets = new Dictionary<string, int>
            {
                [0.IntToBin(methodOptions.Data.Count)] = 0
            };

            for (int i = 1; i < Math.Pow(2, methodOptions.Data.Count); i++)
            {
                subsets[i.IntToBin(methodOptions.Data.Count)] = int.MaxValue;
            }

            var dynamicProgrammingOptions = methodOptions as DynamicProgrammingOptions;
            dynamicProgrammingOptions.Subsets = subsets;
            return dynamicProgrammingOptions;
        }

        public (List<int>, int) Solve(IMethodOptions options, Stopwatch stopwatch, GuiActions guiActions = null)
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


            guiActions?.Log?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}\n", null, true);
            if (!stopwatch.IsRunning) stopwatch.Start();
            for (int i = 1; i < dpaOptions.Subsets.Count; i++)
            {
                tmp = i.IntToBin(jobs.Count);
                rozw = int.MaxValue;
                cmax = 0;
                guiActions?.Log?.Invoke($"OPT({tmp}):\n", null, true);

                for (int j = 0; j < tmp.Length; j++)
                {
                    if (tmp[j].Equals('1'))
                    {
                        cmax += jobs[jobs.Count - j - 1].Time; // wyliczenie cmax calkowitego dla danego i
                    }
                }

                guiActions?.Log?.Invoke($"\tCMAX : {cmax}\n", null, true);

                for (int j = 0; j < tmp.Length; j++)
                {
                    if (tmp[j].Equals('1'))
                    {
                        index = jobs.Count - j - 1;
                        tmp = tmp.ReplaceAt(j, '0');
                        opt = jobs[index].CountPenalty(cmax);
                        result = subsets[tmp] + opt; // OPT(xn,..,xi+1,xi-1,..,x0) + wx*Tx 
                        guiActions?.Log?.Invoke($"\tOPT({tmp}) + w{index}T{index} = {subsets[tmp]} + {opt} = {result}\n", null, true);
                        rozw = Math.Min(rozw, result); // wybranie najmniejszego kosztu dla danego i
                        tmp = i.IntToBin(jobs.Count);
                    }
                }
                guiActions?.Log?.Invoke("", GetAllIndexes(tmp, jobs), false);
                subsets[tmp] = rozw;
            }

            if (stopwatch.IsRunning) stopwatch.Stop();
            guiActions?.Log?.Invoke($"Koniec : {DateTime.Now:HH:mm:ss.fff}\n", null, true);

            return (new List<int>(), rozw);
        }

        private List<Job> GetAllIndexes(string s, List<Job> allJobs)
        {
            var chosenJobs = new List<Job>();
            for (int i = s.IndexOf('1'); i > -1; i = s.IndexOf('1', i + 1))
            {
                chosenJobs.Add(allJobs[i]);
            }
            return chosenJobs;
        }
    }
}
