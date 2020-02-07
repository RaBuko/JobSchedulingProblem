using System;

namespace Solver.Data
{
    public class Job
    {
        public int Index { get; set; }
        public int Time { get; set; }
        public int Weight { get; set; }
        public int Term { get; set; }

        public override string ToString()
        {
            return $"({Index}:{Time}|{Weight}|{Term})";
        }

        public int CountPenalty(int actualTime = 0)
        {
            // Ti = max(0, Ci - di)
            return Math.Max(0, (actualTime - Term) * Weight);
        }
    }
}
