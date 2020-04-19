using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Solver.Methods
{
    class GeneticAlgorithmMethod : IMethod
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

        public (List<int>, int) Solve(IMethodOptions options, Stopwatch stopwatch)
        {
            var methodsOptions = options as GeneticAlgorithmOptions;

            var minTardiness = int.MaxValue;
            var population = methodsOptions.StartingPopulation;
            (List<int> jobOrder, double fitness) bestGlobalOrder = (population[0]);
            List<int> indexes;

            methodsOptions.GuiConnection?.LogText?.Invoke($"PARAMETRY:");
            foreach ((PropertyInfo prop, string value) in Helper.GetParametersValues(methodsOptions))
            {
                methodsOptions.GuiConnection?.LogText?.Invoke($"{prop.Name} : {value}");
            }

            int oldPopCount = (int)(population.Count * (1 - methodsOptions.CrossoverRate));
            methodsOptions.GuiConnection?.LogText?.Invoke($"W nowej populacji pozostanie {oldPopCount} osobnikow z poprzedniej generacji (CrossoverRate)");

            int mutatePopCount = (int)(population.Count * methodsOptions.MutationRate);
            methodsOptions.GuiConnection?.LogText?.Invoke($"W każdej iteracji {oldPopCount} osobnikow przejdzie mutacje (MutationRate)");

            methodsOptions.GuiConnection?.LogText?.Invoke($"Start : {DateTime.Now:HH:mm:ss.fff}");
            stopwatch.Start();

            try
            {
                methodsOptions.GuiConnection?.LogText?.Invoke("Generowanie populacji startowej");
                population = GenerateStartingPopulation(methodsOptions.Data, methodsOptions.ChromosomeCount);

                methodsOptions.GuiConnection?.LogText?.Invoke("Obliczenie funkcji celu dla populacji startowej");
                ComputeFitness(population, methodsOptions.Data);

                while (methodsOptions.NumberOfIterations > 0)
                {
                    methodsOptions.GuiConnection?.LogText?.Invoke($"Iteracja: {methodsOptions.NumberOfIterations}, generowanie nowej populacji");
                    population = GenerateNewPopulation(population, methodsOptions, oldPopCount);

                    indexes = ThreadSafeRandom.GenerateUniqueRandom(mutatePopCount, 0, population.Count);
                    methodsOptions.GuiConnection?.LogText?.Invoke($"Mutują: {string.Join(',', indexes)}");
                    foreach (var i in indexes)
                    {
                        Mutate(population[i].jobsOrder);
                    }

                    if (methodsOptions.GuiConnection?.LogText != null)
                    {
                        bestGlobalOrder = population.Find(y => y.fitness == population.Max(x => x.fitness));
                        methodsOptions.GuiConnection?.LogText?.Invoke($"Najlepszy wynik funkcji celu = {bestGlobalOrder.fitness}");
                    }

                    methodsOptions.NumberOfIterations -= 1;
                }
            }
            catch (OperationCanceledException)
            {
                options.GuiConnection?.LogText("Przerwano wykonwanie zadania");
            }

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

        void ComputeFitness(List<(List<int> jobsOrder, double fitness)> population, List<Job> jobs)
        {
            for (int i = 0; i < population.Count; i++)
            {
                population[i] = (population[i].jobsOrder, CalculateFitness(population[i].jobsOrder, jobs));
            }
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
                options.GuiConnection?.LogText?.Invoke($"Nowy osobnik, zostaje rodzic: {parentStays}");
                newPopulation.Add((parentStays, 0));
            }

            while (newPopulation.Count > population.Count) newPopulation.RemoveAt(newPopulation.Count - 1);

            return newPopulation;
        }

        private void Mutate(List<int> inChromosome)
        {
            int i = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            int j = inChromosome[ThreadSafeRandom.ThisThreadsRandom.Next(inChromosome.Count)];
            ArrayExtension.Swap(ref i, ref j);
        }

        private List<int> CreateOffspring(List<int> parent1, List<int> parent2) // based on Hamidreza Haddad and Mohammadreza Nematollahi solution
        {
            var offspring = new List<int>();
            for (int i = 0; i < parent1.Count; i++)
            {
                offspring.Add(ThreadSafeRandom.ThisThreadsRandom.NextDouble() < 0.5 ? parent1[i] : parent2[i]);
            }
            return offspring;
        }


        private double CalculateFitness(List<int> indexes, List<Job> data)
        {
            return 1 / data.JobsFromIndexList(indexes).CountPenalty(); // fi = 1 / sum(WiTi) - fitness func
        }
    }
}
