using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Solver.Methods
{
    class GeneticAlgorithmOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }
        public GuiConnection GuiConnection { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public int NumberOfIterations { get; set; }
        public SelectionMechanism SelectionMechanism { get; set; }
        public decimal MutationRate { get; set; }
        public decimal CrossoverRate { get; set; }
        public int ChromosomeCount { get; set; }
        public List<List<int>> StartingPopulation { get; set; }
    }

    enum SelectionMechanism
    {
        RouletteWheel = 0,
        BestSelection = 1,
        RandomSelection = 2,
    }
}
