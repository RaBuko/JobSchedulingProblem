using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Solver.Methods
{
    public class DynamicProgrammingMethod : IMethod
    {
        public IMethodOptions Prepare(List<Job> jobs)
        {
            var subsets = new Dictionary<string, int>();
            subsets[0.IntToBin(jobs.Count)] = 0;
            for (int i = 1; i < Math.Pow(2, jobs.Count); i++)
            {
                subsets[i.IntToBin(jobs.Count)] = int.MaxValue;
            }

            return new DynamicProgrammingOptions()
            {
                Data = jobs,
                Subsets = subsets,
            };
        }

        public (List<int>, int) Solve(IMethodOptions options, Stopwatch stopwatch, Action<string> logging = null)
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


            logging?.Invoke($"Start : {DateTime.Now.ToString("HH:mm:ss.fff")}\n");
            if (!stopwatch.IsRunning) stopwatch.Start();
            for (int i = 1; i < dpaOptions.Subsets.Count; i++)
            {
                tmp = i.IntToBin(jobs.Count);
                rozw = int.MaxValue;
                cmax = 0;
                logging?.Invoke($"OPT({tmp}):\n");
                
                for (int j = 0; j < tmp.Length; j++)
                {
                    if (tmp[j].Equals('1'))
                    {
                        cmax += jobs[jobs.Count - j - 1].Time; // wyliczenie cmax calkowitego dla danego i
                    }
                }

                logging?.Invoke($"\tCMAX : {cmax}\n");
                
                for (int j = 0; j < tmp.Length; j++)
                {
                    if (tmp[j].Equals('1'))
                    {
                        index = jobs.Count - j - 1;
                        tmp = tmp.ReplaceAt(j, '0');
                        opt = Math.Max(cmax - jobs[index].Term, 0) * jobs[index].Weight;
                        result = subsets[tmp] + opt; // OPT(xn,..,xi+1,xi-1,..,x0) + wx*Tx 
                        logging?.Invoke($"\tOPT({tmp}) + w{index}T{index} = {subsets[tmp]} + {opt} = {result}\n");
                        rozw = Math.Min(rozw, result); // wybranie najmniejszego kosztu dla danego i
                        tmp = i.IntToBin(jobs.Count);
                    }
                }
                subsets[tmp] = rozw;
            }

            if (stopwatch.IsRunning) stopwatch.Stop();
            logging?.Invoke($"Koniec : {DateTime.Now.ToString("HH:mm:ss.fff")}\n");

            return (new List<int>(), rozw);
        }
    }
}
