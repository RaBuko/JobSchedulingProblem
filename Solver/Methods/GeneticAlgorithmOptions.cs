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

        [UserDefined("Pokaż szczegóły", typeof(bool), true)]
        public bool ShouldLogText { get; set; }

        [UserDefined("Pokaż zadania graficznie", typeof(bool), true)]
        public bool ShouldLogGraphics { get; set; }

        [UserDefined("Liczba iteracji", typeof(int), 1000)]
        public int NumberOfIterations { get; set; }

        [UserDefined("Ilość chromosomów", typeof(int), 60)]
        public int ChromosomeCount { get; set; }

        [UserDefined("Mechanizm selekcji", typeof(SelectionMechanismEnum), SelectionMechanismEnum.RouletteWheel)]
        public SelectionMechanismEnum SelectionMechanism { get; set; }

        [UserDefined("Częstotliwość mutacji", typeof(float), 0.8)]
        public double MutationRate { get; set; }

        [UserDefined("Częstotliwość krzyżowania", typeof(float), 0.2)]
        public double CrossoverRate { get; set; }

        public List<(List<int> jobsOrder, double fitness)> StartingPopulation { get; set; }
        public Func<List<(List<int> jobsOrder, double fitness)>, int> SelectionMechanismAction { get; set; }
    }
}
