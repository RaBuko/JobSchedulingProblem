using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace XUnitTestProject
{
    public class Test10
    {
        private readonly ITestOutputHelper output;
        private readonly int jobsCount = 10;
        private readonly int numOfTests = 10;
        private readonly List<Job> data;
        private int bestTardiness = int.MaxValue;

        public Test10(ITestOutputHelper outputHelper)
        {
            output = outputHelper;
            var file = $"{Helper.AppSettings.ExamplesPath}\\{jobsCount}.txt";
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

        [Fact]
        public void BruteForceTest()
        {
            var results = new List<Result>();
            var options = new BruteForceOptions() { Data = data, };
            var method = new BruteForceMethod();

            output.WriteLine("START Prepare");
            var stopwatch = new Stopwatch();
            options = method.Prepare(options) as BruteForceOptions;
            stopwatch.Stop();
            string elapsedPrepare = stopwatch.Elapsed.ToString("G");
            output.WriteLine("END Prepare");

            for (int i = 0; i < numOfTests + 1; i++)
            {
                stopwatch = new Stopwatch();
                var (jobOrder, minTardiness) = method.Solve(options, stopwatch);
                stopwatch.Stop();
                var result = new Result()
                {
                    Score = minTardiness,
                    ExecElapsed = stopwatch.Elapsed.ToString("G"),
                    PrepareElapsed = elapsedPrepare,
                };
                if (i != 0) results.Add(result);
                Helper.LogStatus($"{method.GetType().Name} {i}/{numOfTests} | ExecElapsed = {result.ExecElapsed}");
            }

            var min = results.Min(x => x.Score);
            if (bestTardiness > min) { bestTardiness = min; }
            Helper.WriteResultsToCsv(method.GetType().Name, jobsCount, bestTardiness, results);
        }

        [Fact]
        public void DynamicProgrammingTest()
        {
            var results = new List<Result>();
            var options = new DynamicProgrammingOptions() { Data = data, };
            var method = new DynamicProgrammingMethod();

            output.WriteLine("START Prepare");
            var stopwatch = new Stopwatch();
            options = method.Prepare(options) as DynamicProgrammingOptions;
            stopwatch.Stop();
            string elapsedPrepare = stopwatch.Elapsed.ToString("G");
            output.WriteLine("END Prepare");

            for (int i = 0; i < numOfTests + 1; i++)
            {
                stopwatch = new Stopwatch();
                var (jobOrder, minTardiness) = method.Solve(options, stopwatch);
                stopwatch.Stop();
                var result = new Result()
                {
                    Score = minTardiness,
                    ExecElapsed = stopwatch.Elapsed.ToString("G"),
                    PrepareElapsed = elapsedPrepare,
                };
                if (i != 0) results.Add(result);
                Helper.LogStatus($"{method.GetType().Name} {i}/{numOfTests} | ExecElapsed = {result.ExecElapsed}");
            }

            var min = results.Min(x => x.Score);
            if (bestTardiness > min) { bestTardiness = min; }
            Helper.WriteResultsToCsv(method.GetType().Name, jobsCount, bestTardiness, results);
        }

        [Fact]
        public void GeneticAlgorithmTest()
        {
            var results = new List<Result>();
            var options = new GeneticAlgorithmOptions()
            {
                Data = data,
                ChromosomeCount = 100,
                CrossoverRate = 0.2,
                MutationRate = 0.8,
                NumberOfIterations = 10000,
                SelectionMechanism = Solver.Utils.SelectionMechanismEnum.RouletteWheel,
            };
            var method = new GeneticAlgorithmMethod();

            output.WriteLine("START Prepare");
            var stopwatch = new Stopwatch();
            options = method.Prepare(options) as GeneticAlgorithmOptions;
            stopwatch.Stop();
            string elapsedPrepare = stopwatch.Elapsed.ToString("G");
            output.WriteLine("END Prepare");

            for (int i = 0; i < numOfTests + 1; i++)
            {
                stopwatch = new Stopwatch();
                var (jobOrder, minTardiness) = method.Solve(options, stopwatch);
                stopwatch.Stop();
                var result = new Result()
                {
                    Score = minTardiness,
                    ExecElapsed = stopwatch.Elapsed.ToString("G"),
                    PrepareElapsed = elapsedPrepare,
                };

                if (i != 0) results.Add(result);
                Helper.LogStatus($"{method.GetType().Name} {i}/{numOfTests} | ExecElapsed = {result.ExecElapsed}");
            }

            var min = results.Min(x => x.Score);
            if (bestTardiness > min) { bestTardiness = min; }
            string expandedName = $"_CC{options.ChromosomeCount}_CR{options.CrossoverRate:0%}_MR{options.MutationRate:0%}_I{options.NumberOfIterations.ToString()}_{string.Join("", options.SelectionMechanism.ToString().Where(x => char.IsUpper(x)))}";
            Helper.WriteResultsToCsv(method.GetType().Name, jobsCount, bestTardiness, results, expandedName);
        }
    }
}
