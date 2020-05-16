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

            var minTardiness = int.MaxValue;
            List<int> indexes;
            (List<int> jobOrder, double fitness) bestGlobalOrder = (options.Data.Select(x => x.Index).ToList(), 0);

            if (options.GuiConnection != null)
            {
                options.GuiConnection?.LogText?.Invoke($"PARAMETRY:");
                foreach ((PropertyInfo prop, string value) in MethodHelper.GetParametersValues(options))
                {
                    options.GuiConnection?.LogText?.Invoke($"{prop.Name} : {value}");
                }
            }

            int oldPopCount = (int)(options.PopulationSize * (1 - options.CrossoverRate));
            options.GuiConnection?.LogText?.Invoke($"W nowej populacji pozostanie {oldPopCount} osobnikow z poprzedniej generacji (CrossoverRate)");

            int mutatePopCount = (int)(options.PopulationSize * options.MutationRate);
            options.GuiConnection?.LogText?.Invoke($"W każdej iteracji {oldPopCount} osobnikow przejdzie mutacje (MutationRate)");

            int iter = 0;
            options.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();
            try
            {
                options.GuiConnection?.LogText?.Invoke("Generowanie populacji startowej");
                var population = GenerateStartingPopulation(options.Data, options.PopulationSize);

                options.GuiConnection?.LogText?.Invoke("Obliczenie funkcji celu dla populacji startowej");
                bestGlobalOrder = ComputeFitness(population, options.Data);

                while (iter < options.IterationCount)
                {
                    options.GuiConnection?.LogText?.Invoke($"Iteracja: {iter}, generowanie nowej populacji");
                    population = population.OrderByDescending(x => x.fitness).ToList();

                    population = GenerateNewPopulation(population, options, oldPopCount);

                    indexes = ThreadSafeRandom.GenerateUniqueRandom(mutatePopCount, 0, population.Count);
                    options.GuiConnection?.LogText?.Invoke($"Mutują: {string.Join(',', indexes)}");
                    foreach (var i in indexes)
                    {
                        Mutate(population[i].jobsOrder);
                    }


                    bestGlobalOrder = ComputeFitness(population, options.Data);
                    options.GuiConnection?.LogText?.Invoke($"Najlepszy wynik funkcji celu = {bestGlobalOrder.fitness}");

                    iter += 1;
                    options.CancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                imethodoptions.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }

            stopwatch.Stop();
            minTardiness = options.Data.JobsFromIndexList(bestGlobalOrder.jobOrder).CountPenalty();
            return (bestGlobalOrder.jobOrder, minTardiness);
        }


        List<(List<int> jobsOrder, double fitness)> GenerateStartingPopulation(List<Job> data, int chromosomeCount)
        {
            var startingPopulation = new List<(List<int> jobsOrder, double fitness)>(chromosomeCount);
            var indexes = data.Select(x => x.Index).ToList();
            for (int i = 0; i < chromosomeCount; i++)
            {
                var clonedList = new List<int>(indexes);
                clonedList.Shuffle();
                startingPopulation.Add((clonedList, 0));
            }
            return startingPopulation;
        }

        (List<int> jobsOrder, double fitness) ComputeFitness(List<(List<int> jobsOrder, double fitness)> population, List<Job> jobs)
        {
            (List<int> jobsOrder, double fitness) bestFitness = population[0];
            for (int i = 0; i < population.Count; i++)
            {
                population[i] = (population[i].jobsOrder, CalculateFitness(population[i].jobsOrder, jobs));
                if (bestFitness.fitness < population[i].fitness)
                {
                    bestFitness = population[i];
                }
            }
            return bestFitness;
        }

        private List<(List<int> jobsOrder, double fitness)> GenerateNewPopulation(List<(List<int> jobsOrder, double fitness)> population, GeneticAlgorithmOptions options, int oldPopCount)
        {
            int parent1Index;
            int parent2Index;
            List<int> parent1;
            List<int> parent2;
            List<int> parentStays;

            var newPopulation = new List<(List<int> jobsOrder, double fitness)>();
            var indexes = ThreadSafeRandom.GenerateUniqueRandom(oldPopCount, 0, population.Count);
            options.GuiConnection?.LogText?.Invoke($"Zostaja w nastepnej generacji: {string.Join(',', indexes)}");

            foreach (var i in indexes) { newPopulation.Add(population[i]); }

            while (newPopulation.Count < population.Count)
            {
                parent1Index = options.SelectionMechanismAction.Invoke(population);
                parent2Index = options.SelectionMechanismAction.Invoke(population);
                options.GuiConnection?.LogText?.Invoke($"P1: {parent1Index} | P2: {parent2Index}");
                parent1 = population[parent1Index].jobsOrder;
                parent2 = population[parent2Index].jobsOrder;
                newPopulation.Add((CreateOffspring(parent1, parent2), 0));
                parentStays = ThreadSafeRandom.ThisThreadsRandom.NextDouble() < 0.5 ? parent1 : parent2;
                options.GuiConnection?.LogText?.Invoke($"Nowy osobnik, zostaje rodzic: {string.Join(',', parentStays)}");
                newPopulation.Add((parentStays, 0));
            }

            while (newPopulation.Count > population.Count) newPopulation.RemoveAt(newPopulation.Count - 1);

            return newPopulation;
        }

        private void Mutate(List<int> inChromosome)
        {
            int i = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            int j = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            CollectionExtension.Swap(ref i, ref j);
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
