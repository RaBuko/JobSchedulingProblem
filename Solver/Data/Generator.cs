using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.Data
{
    public class Generator
    {
        public static List<Job> GenerateJobs(GeneratorOptions options)
        {
            var generatedList = new List<Job>();

            for (int i = 0; i < options.JobsCount; i++)
            {
                generatedList.Add(new Job()
                {
                    Index = i,
                    Time = ThreadSafeRandom.ThisThreadsRandom.Next(1, options.MaxTime == null || options.MaxTime < 1 ? options.JobsCount * 2 : options.MaxTime.Value),
                    Weight = ThreadSafeRandom.ThisThreadsRandom.Next(1, options.MaxWeight == null || options.MaxWeight < 1 ? options.JobsCount * 2 : options.MaxWeight.Value),
                    Term = ThreadSafeRandom.ThisThreadsRandom.Next(1, options.MaxTerm == null || options.MaxTerm < 1 ? options.JobsCount * 2 : options.MaxTerm.Value),
                });
            }

            return generatedList;
        }
    }
}
