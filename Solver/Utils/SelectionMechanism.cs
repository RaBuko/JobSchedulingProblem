using System.Collections.Generic;
using System.Linq;

namespace Solver.Utils
{
    public enum SelectionMechanismEnum
    {
        RouletteWheel = 0,
        BestSelection = 1,
        RandomSelection = 2,
    }

    public static class SelectionMechanism
    {
        public static int RouletteWheelSelection(List<(List<int> jobsOrder, double fitness)> chromosomes)
        {
            var p = ThreadSafeRandom.GetRandomDouble(0, chromosomes.Sum(x => x.fitness));
            int iter = 0;
            for (iter = 0; iter < chromosomes.Count; iter++)
            {
                if (p <= 0) break;
                p -= chromosomes[iter].fitness;
            }
            if (iter >= chromosomes.Count) iter = chromosomes.Count - 1; //safeguard, if precision exceeded chromosome count in result
            return iter;
        }
    }
}
