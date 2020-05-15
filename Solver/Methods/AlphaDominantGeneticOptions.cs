using Solver.Data;
using Solver.Utils;
using System.Collections.Generic;
using System.Threading;

namespace Solver.Methods
{
    public class AlphaDominantGeneticOptions : IMethodOptions
    {
        public List<Job> Data { get; set; }

        public GuiConnection GuiConnection { get; set; }

        public CancellationToken CancellationToken { get; set; }

        [UserDefined("Pokaż szczegóły", typeof(bool), true)]
        public bool ShouldLogText { get; set; }

        [UserDefined("Pokaż zadania graficznie", typeof(bool), true)]
        public bool ShouldLogGraphics { get; set; }

        [UserDefined("Liczba iteracji", typeof(int), 1000)]
        public int IterationCount { get; set; }

        [UserDefined("Wielkość populacji", typeof(int), 50)]
        public int PopulationSize { get; set; }

        [UserDefined("Szansa mutacji" ,typeof(float), 0.2)]
        public float MutationChance { get; set; }

        [UserDefined("Część starej populacji w kolejnych generacjach", typeof(float), 0.5)]
        public float OldPopPart { get; set; }

        public int OldPopCount { get; set; }
    }
}
