using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

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
            List<int> best = options.Data.Select(x => x.Index).ToList();
            List<(List<int> indiv, double fitness)> pop;

            options.OldPopCount = (int)(options.OldPopPart * options.PopulationSize);
            options.GuiConnection?.LogText?.Invoke($"Wielkość starej populacji: {options.OldPopCount}");

            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();
            try
            {
                pop = GenerateStartingPopulation(options.Data, options.PopulationSize);
                pop.Sort((x, y) => y.fitness.CompareTo(x.fitness));
                best = pop[0].indiv;
                int iter = 0;
                while (iter < options.IterationCount)
                {
                    pop = GenerateNewPopulation(pop, options);

                    for (int i = 0; i < pop.Count; i++)
                    {
                        if (ThreadSafeRandom.GetRandomDouble() < options.MutationChance)
                        {
                            options.GuiConnection?.LogText?.Invoke("");
                            Mutate(pop[i].indiv);
                            pop[i] = (pop[i].indiv, CalculateFitness(pop[i].indiv, options.Data));
                        }
                    }

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
            minTardiness = options.Data.JobsFromIndexList(best).CountPenalty();
            return (null, minTardiness);
        }

        private void Mutate(List<int> inChromosome)
        {
            int i = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            int j = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            CollectionExtension.Swap(ref i, ref j);
        }

        List<(List<int> indiv, double fitness)> GenerateNewPopulation(List<(List<int> indiv, double fitness)> prevPop, AlphaDominantGeneticOptions options)
        {
            var newPop = new List<(List<int> indiv, double fitness)>(prevPop.GetRange(0, options.OldPopCount));
            var (indiv, _) = newPop[0];

            while (newPop.Count != options.PopulationSize)
            {
                for (int i = 1; i < options.OldPopCount; i++)
                {
                    List<int> child = Crossover(indiv, newPop[i].indiv);
                    newPop.Add((child, CalculateFitness(child, options.Data)));
                }
            }

            return newPop;
        }

        List<int> Crossover(List<int> parent1, List<int> parent2)
        {
            int len = parent1.Count;
            int cutIndex = ThreadSafeRandom.ThisThreadsRandom.Next(len);
            int testIndex;

            var child = new List<int>(parent1);

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
