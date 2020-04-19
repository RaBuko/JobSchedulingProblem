using Solver.Data;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Solver.Methods
{
    class GeneticAlgorithmOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }
        public GuiConnection GuiConnection { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public int NumberOfIterations { get; set; }
        public SelectionMechanismEnum SelectionMechanism { get; set; }
        public double MutationRate { get; set; }
        public double CrossoverRate { get; set; }
        public int ChromosomeCount { get; set; }
        public List<(List<int> jobsOrder, double fitness)> StartingPopulation { get; set; }
        public Func<List<(List<int> jobsOrder, double fitness)>, int> SelectionMechanismAction { get; set; }
    }
}
