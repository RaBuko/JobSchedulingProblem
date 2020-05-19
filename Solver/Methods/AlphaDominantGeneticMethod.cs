using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Solver.Methods
{
    public class AlphaDominantGeneticMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions options)
        {
            var methodOptions = options as AlphaDominantGeneticOptions;
            return methodOptions;
        }

        public (List<int> jobOrder, int minTardiness) Solve(IMethodOptions imethodoptions, Stopwatch stopwatch)
        {
            var options = imethodoptions as AlphaDominantGeneticOptions;
            var minTardiness = options.Data.CountPenalty();
            int dataCount = options.Data.Count;
            List<int> best = options.Data.Select(x => x.Index).ToList();
            List<(List<int> indiv, double fitness)> pop;  
            string prev = "";

            options.OldPopCount = (int)(options.PopulationSize * (1 - options.CrossoverRate));
            options.GuiConnection?.LogText?.Invoke($"Wielkość starej populacji: {options.OldPopCount}");

            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();
            try
            {
                options.GuiConnection?.LogText?.Invoke("Generowanie populacji startowej");
                pop = GenerateStartingPopulation(options.Data, options.PopulationSize);
                
                options.GuiConnection?.LogText?.Invoke("Sortowanie");
                pop.Sort((x, y) => y.fitness.CompareTo(x.fitness));
                best = pop[0].indiv;
                int iter = 0;
                while (iter < options.IterationCount)
                {
                    options.GuiConnection?.LogText?.Invoke($"Osobnik alpha o F = {pop[0].fitness}");
                    options.GuiConnection?.LogText?.Invoke($"Iteracja: {iter}, generowanie nowej populacji");

                    pop = GenerateNewPopulation(pop, options);

                    for (int i = 0; i < pop.Count; i++)
                    {
                        if (ThreadSafeRandom.GetRandomDouble() < options.MutationChance)
                        {
                            if (options.GuiConnection != null) prev = $"{string.Join(',', pop[i].indiv)}, F = {pop[i].fitness}";
                            int index1 = ThreadSafeRandom.ThisThreadsRandom.Next(dataCount);
                            int index2 = ThreadSafeRandom.ThisThreadsRandom.Next(dataCount);
                            pop[i].indiv.Swap(index1, index2);
                            pop[i] = (pop[i].indiv, CalculateFitness(pop[i].indiv, options.Data));
                            options.GuiConnection?.LogText?.Invoke($"   Mutacja: i = {i} | {prev} -> {string.Join(',', pop[i].indiv)}, F = {pop[i].fitness}");
                        }
                    }

                    options.GuiConnection?.LogText?.Invoke("    Sortowanie");
                    pop.Sort((x, y) => y.fitness.CompareTo(x.fitness));
                    best = pop[0].indiv;

                    iter += 1;
                    options.CancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                imethodoptions.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }
            stopwatch.Stop();
            options.GuiConnection?.LogText?.Invoke($"Koniec : {DateTime.Now:HH:mm:ss.fff}");
            minTardiness = options.Data.JobsFromIndexList(best).CountPenalty();
            return (best, minTardiness);
        }

        List<(List<int> indiv, double fitness)> GenerateNewPopulation(List<(List<int> indiv, double fitness)> prevPop, AlphaDominantGeneticOptions options)
        {
            var newPop = new List<(List<int> indiv, double fitness)>(prevPop.GetRange(0, options.OldPopCount));
            var alpha = newPop[0].indiv;

            while (true)
            {
                for (int i = 1; i < options.OldPopCount; i++)
                {
                    List<int> child = Crossover(alpha, newPop[i].indiv);
                    newPop.Add((child, CalculateFitness(child, options.Data)));
                    if (newPop.Count >= options.PopulationSize)
                    {
                        if (newPop.Count > options.PopulationSize) { newPop = newPop.GetRange(0, options.PopulationSize); }
                        return newPop;
                    }
                }
            }
        }

        List<int> Crossover(List<int> parent1, List<int> parent2)
        {
            int len = parent1.Count;
            int cutIndex = ThreadSafeRandom.ThisThreadsRandom.Next(len);
            int testIndex;

            var child = new List<int>(parent1.GetRange(0, cutIndex));

            for (int i = cutIndex; i < len; i++)
            {
                testIndex = parent2[i];
                if (child.Contains(testIndex))
                {
                    child.Add(parent2.First(x => !child.Contains(x)));
                }
                else
                {
                    child.Add(parent2[i]);
                }
            }
            return child;
        }

        List<(List<int> order, double fitness)> GenerateStartingPopulation(List<Job> data, int popCount)
        {
            var dataOrder = data.Select(x => x.Index).ToList();
            var startPop = new List<(List<int> order, double fitness)>(popCount);

            for (int i = 0; i < popCount; i++)
            {
                var clonedList = new List<int>(dataOrder);
                clonedList.Shuffle();
                startPop.Add((clonedList, CalculateFitness(clonedList, data)));
            }
            return startPop;
        }

        private double CalculateFitness(List<int> indexes, List<Job> data)
        {
            return 1 / (double)data.JobsFromIndexList(indexes).CountPenalty(); // fi = 1 / sum(WiTi) - fitness func
        }
    }
}
