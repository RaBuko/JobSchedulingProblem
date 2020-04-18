using Solver.Data;
using System.Collections.Generic;

namespace Solver.Utils
{
    public static class JobExtension
    {
        public static List<Job> JobsFromIndexList(this List<Job> data, List<int> indexes)
        {
            var resultData = new List<Job>();
            indexes.ForEach(i => resultData.Add(data.Find(x => x.Index == i)));
            return resultData;
        }

        public static List<Job> JobsFromBitString(this List<Job> allJobs, string s)
        {
            var chosenJobs = new List<Job>();
            for (int i = s.IndexOf('1'); i > -1; i = s.IndexOf('1', i + 1))
            {
                chosenJobs.Add(allJobs[i]);
            }
            return chosenJobs;
        }

        public static int CountPenalty(this List<Job> allJobs)
        {
            int penalty = 0;
            int timeEnded = 0;
            for (int i = 0; i < allJobs.Count; i++)
            {
                timeEnded += allJobs[i].Time;
                penalty += allJobs[i].CountPenalty(timeEnded);
            }
            return penalty;
        }
    }
}
