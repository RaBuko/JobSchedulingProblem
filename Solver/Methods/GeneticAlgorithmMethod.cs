using Solver.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Solver.Utils;

namespace Solver.Methods
{
    class GeneticAlgorithmMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions options)
        {
            var dynamicMethodOptions = options as GeneticAlgorithmOptions;
            var indexes = options.Data.Select(x => x.Index).ToList();
            List<List<int>> startingPopulation = new List<List<int>>(dynamicMethodOptions.ChromosomeCount);
            for (int i = 0; i < dynamicMethodOptions.ChromosomeCount; i++)
            {
                var clonedList = new List<int>(indexes);
                clonedList.Shuffle();
                startingPopulation.Add(clonedList);
            }
            dynamicMethodOptions.StartingPopulation = startingPopulation;
            return dynamicMethodOptions;
        }

        public (List<int>, int) Solve(IMethodOptions options, Stopwatch stopwatch)
        {
            var methodsOptions = options as GeneticAlgorithmOptions;

            var minTardiness = int.MaxValue;
            List<List<int>> population = methodsOptions.StartingPopulation;
            List<int> bestGlobalOrder = new List<int>();

            methodsOptions.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();       

            try
            {
                while (methodsOptions.NumberOfIterations > 0)
                {
                    methodsOptions.GuiConnection?.LogText?.Invoke("Iteracja: " + methodsOptions.NumberOfIterations);



                    methodsOptions.NumberOfIterations -= 1;
                }
            }
            catch (OperationCanceledException)
            {
                options.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }

            return (bestGlobalOrder, minTardiness);
        }
    }
}
