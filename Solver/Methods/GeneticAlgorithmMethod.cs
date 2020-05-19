using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Solver.Methods
{
    public class GeneticAlgorithmMethod : IMethod
    {
        public IMethodOptions Prepare(IMethodOptions options)
        {
            var dynamicMethodOptions = options as GeneticAlgorithmOptions;
            dynamicMethodOptions.SelectionMechanismAction = dynamicMethodOptions.SelectionMechanism switch
            {
                SelectionMechanismEnum.RouletteWheel => SelectionMechanism.RouletteWheelSelection,
                _ => throw new NotImplementedException(),
            };

            return dynamicMethodOptions;
        }

        public class PopulationComparer : IComparer<(List<int> jobOrder, double fitness)>
        {
            public int Compare((List<int> jobOrder, double fitness) x, (List<int> jobOrder, double fitness) y)
            {
                if (x.fitness == y.fitness) return 0;
                else if (x.fitness > y.fitness) return 1;
                else return -1;
            }
        }

        public (List<int> jobOrder, int minTardiness) Solve(IMethodOptions imethodoptions, Stopwatch stopwatch)
        {
            var options = imethodoptions as GeneticAlgorithmOptions;
            var dataCount = options.Data.Count;
            var minTardiness = int.MaxValue;
            List<int> indexes;
            List<(List<int> jobsOrder, double fitness)> population = new List<(List<int> jobsOrder, double fitness)>();

            if (options.GuiConnection != null)
            {
                options.GuiConnection?.LogText?.Invoke($"PARAMETRY:");
                foreach ((PropertyInfo prop, string value) in MethodHelper.GetParametersValues(options))
                {
                    options.GuiConnection?.LogText?.Invoke($"{prop.Name} : {value}");
                }
            }

            options.OldPopCount = (int)(options.PopulationSize * (1 - options.CrossoverRate));
            options.GuiConnection?.LogText?.Invoke($"W nowej populacji pozostanie {options.OldPopCount} osobnikow z poprzedniej generacji (CrossoverRate)");

            int mutatePopCount = (int)(options.PopulationSize * options.MutationRate);
            options.GuiConnection?.LogText?.Invoke($"W każdej iteracji {options.OldPopCount} osobnikow przejdzie mutacje (MutationRate)");

            int iter = 0;
            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();
            try
            {
                options.GuiConnection?.LogText?.Invoke("Generowanie populacji startowej");
                population = GenerateStartingPopulation(options.Data, options.PopulationSize);

                while (iter < options.IterationCount)
                {
                    options.GuiConnection?.LogText?.Invoke($"Iteracja: {iter}, generowanie nowej populacji");
                    population = GenerateNewPopulation(population, options);

                    indexes = ThreadSafeRandom.GenerateUniqueRandom(mutatePopCount, 0, population.Count);
                    options.GuiConnection?.LogText?.Invoke($"Mutują: {string.Join(',', indexes)}");
                    foreach (var i in indexes)
                    {
                        int index1 = ThreadSafeRandom.ThisThreadsRandom.Next(dataCount);
                        int index2 = ThreadSafeRandom.ThisThreadsRandom.Next(dataCount);
                        population[i].jobsOrder.Swap(index1, index2);
                        population[i] = (population[i].jobsOrder, CalculateFitness(population[i].jobsOrder, options.Data));
                    }

                    iter += 1;
                    options.CancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                imethodoptions.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }
            stopwatch.Stop();
            var bestFitness = population.Max(x => x.fitness);
            var (bestJobsOrder, fitness) = population.Find(x => x.fitness == bestFitness);
            minTardiness = options.Data.JobsFromIndexList(bestJobsOrder).CountPenalty();
            return (bestJobsOrder, minTardiness);
        }


        List<(List<int> jobsOrder, double fitness)> GenerateStartingPopulation(List<Job> data, int chromosomeCount)
        {
            var startingPopulation = new List<(List<int> jobsOrder, double fitness)>(chromosomeCount);
            var indexes = data.Select(x => x.Index).ToList();
            for (int i = 0; i < chromosomeCount; i++)
            {
                var clonedList = new List<int>(indexes);
                clonedList.Shuffle();
                startingPopulation.Add((clonedList, CalculateFitness(clonedList, data)));
            }
            return startingPopulation;
        }

        private List<(List<int> jobsOrder, double fitness)> GenerateNewPopulation(List<(List<int> jobsOrder, double fitness)> population, GeneticAlgorithmOptions options)
        {
            var newPopulation = new List<(List<int> jobsOrder, double fitness)>();
            var indexes = ThreadSafeRandom.GenerateUniqueRandom(options.OldPopCount, 0, population.Count);
            options.GuiConnection?.LogText?.Invoke($"Zostaja w nastepnej generacji: {string.Join(',', indexes)}");

            foreach (var i in indexes) { newPopulation.Add(population[i]); }

            while (newPopulation.Count < population.Count)
            {
                var parent1Index = options.SelectionMechanismAction.Invoke(population);
                var parent2Index = options.SelectionMechanismAction.Invoke(population);
                options.GuiConnection?.LogText?.Invoke($"P1: {parent1Index} | P2: {parent2Index}");
                var parent1 = population[parent1Index];
                var parent2 = population[parent2Index];
                var offspring = CreateOffspring(parent1.jobsOrder, parent2.jobsOrder);
                newPopulation.Add((offspring, CalculateFitness(offspring, options.Data)));
                var parentStays = ThreadSafeRandom.ThisThreadsRandom.NextDouble() < 0.5 ? parent1 : parent2;
                options.GuiConnection?.LogText?.Invoke($"Nowy osobnik, zostaje rodzic: {string.Join(',', parentStays)}");
                newPopulation.Add(parentStays);
            }

            while (newPopulation.Count > population.Count) newPopulation.RemoveAt(newPopulation.Count - 1);

            return newPopulation;
        }

        private List<int> CreateOffspring(List<int> parent1, List<int> parent2) // based on Hamidreza Haddad and Mohammadreza Nematollahi solution
        {
            var offspring = new HashSet<int>();

            while (offspring.Count < parent1.Count)
            {
                for (int i = 0; i < parent1.Count; i++)
                {
                    offspring.Add(ThreadSafeRandom.ThisThreadsRandom.NextDouble() > 0.5 ? parent1[i] : parent2[i]);
                }
            }
            return offspring.ToList();
        }


        private double CalculateFitness(List<int> indexes, List<Job> data)
        {
            return 1 / (double)data.JobsFromIndexList(indexes).CountPenalty(); // fi = 1 / sum(WiTi) - fitness func
        }
    }
}
