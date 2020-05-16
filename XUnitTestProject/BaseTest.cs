using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace XUnitTestProject
{
    public abstract class BaseTest
    {
        private readonly ITestOutputHelper output;
        protected readonly List<Job> data;
        private int bestTardiness = int.MaxValue;
        private readonly string file;
        private readonly int jobsCount;
        private readonly int testsToMake;

        protected BaseTest(ITestOutputHelper outputHelper, int inJobsCount, int inTestsToMake)
        {
            testsToMake = inTestsToMake;
            jobsCount = inJobsCount;
            output = outputHelper;
            file = $"{Helper.AppSettings.ExamplesPath}\\{jobsCount}.txt";
            if (!File.Exists(file))
            {
                data = Generator.GenerateJobs(new GeneratorOptions()
                {
                    JobsCount = jobsCount,
                    MaxTerm = jobsCount * 2,
                    MaxTime = jobsCount * 2,
                    MaxWeight = jobsCount * 2,
                });
                Loader.WriteJobsToFile(file, data);
            }

            var found = Loader.FindBest(Path.GetFileNameWithoutExtension(file), Helper.AppSettings.SolvedDictFileName);
            if (found.Contains("-")) bestTardiness = int.MaxValue;
            else bestTardiness = int.Parse(found);
            data = Loader.LoadJobsFromFile(file);
        }

        protected void TestMethod(IMethod method, IMethodOptions options)
        {
            var results = new List<Result>();
            output.WriteLine("START Prepare");
            options = method.Prepare(options);
            output.WriteLine("END Prepare");

            for (int i = 0; i < testsToMake + 1; i++)
            {
                var stopwatch = new Stopwatch();
                var (jobOrder, minTardiness) = method.Solve(options, stopwatch);
                stopwatch.Stop();
                var result = new Result()
                {
                    Score = minTardiness,
                    ExecElapsed = stopwatch.Elapsed.ToString("G"),
                    Parameters = GetParameters(options)
                };
                if (i != 0) results.Add(result);
                Helper.LogStatus($"{method.GetType().Name} {i}/{testsToMake} | ExecElapsed = {result.ExecElapsed}");
            }

            var min = results.Min(x => x.Score);
            if (bestTardiness > min) { bestTardiness = min; Loader.TrySaveNewBest(file, bestTardiness, Helper.AppSettings.SolvedDictFileName); }                      

            Helper.WriteResultsToCsv(method.GetType().Name, jobsCount, bestTardiness, results);
        }

        private string GetParameters(IMethodOptions options)
        {
            string expandedName = string.Empty;
            if (options.GetType() == typeof(GeneticAlgorithmOptions))
            {
                var genOptions = options as GeneticAlgorithmOptions;
                expandedName = $"I{genOptions.IterationCount}_PS{genOptions.PopulationSize}_CR{genOptions.CrossoverRate:0}_MR{genOptions.MutationRate:0}";
            }
            else if (options.GetType() == typeof(SimulatedAnnealingOptions))
            {
                var simAnnOptions = options as SimulatedAnnealingOptions;
                expandedName = $"I{simAnnOptions.IterationCount}";
            }
            else if (options.GetType() == typeof(AlphaDominantGeneticOptions))
            {
                var alphaDomGenOptions = options as AlphaDominantGeneticOptions;
                expandedName = $"I{alphaDomGenOptions.IterationCount}_PS{alphaDomGenOptions.PopulationSize}_OP{alphaDomGenOptions.OldPopPart:0}_MC{alphaDomGenOptions.MutationChance:0}";
            }
            return expandedName;
        }
    }
}
