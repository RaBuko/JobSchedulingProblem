using Solver.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace FormsApp.Helpers
{
    class Loader
    {
        /// <summary>
        /// Loads jobs from specified file. Each file contains the data listed 
        /// one after the other. The n processing times are listed first (Time), 
        /// followed by the n weights (Weight), and finally n due dates (Term).
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Set of parsed jobs</returns>
        public static List<Job> LoadJobsFromFile(string path)
        {
            
            var list = new List<Job>();
            var file = File.ReadAllText(path, Encoding.ASCII).Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (file.Count() % 3 != 0)
            {
                throw new InvalidDataException($"File {path} can't be parsed to job data. Make sure given file is follows the pattern: The n processing times are listed first (Time), followed by the n weights (Weight), and finally n due dates (Term)");
            }

            int numberOfJobs = file.Count() / 3;

            for (int i = 0; i < numberOfJobs; i++)
            {
                list.Add(new Job()
                {
                    Index = i,
                    Time = int.Parse(file.ElementAt(i)),
                    Weight = int.Parse(file.ElementAt(i + numberOfJobs)),
                    Term = int.Parse(file.ElementAt(i + numberOfJobs * 2))
                });
            }

            return list;
        }

        public static void WriteJobsToFile(string path, List<Job> jobs)
        {
            var sb = new StringBuilder();

            foreach (var item in jobs)
            {
                sb.Append(item.Time + " ");
            }

            foreach (var item in jobs)
            {
                sb.Append(item.Weight + " ");
            }

            foreach (var item in jobs)
            {
                sb.Append(item.Term + " ");
            }

            File.WriteAllText(path, sb.ToString().Trim());
        }

        public static AppSettings LoadAppSettings()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            if (!File.Exists(path)) throw new Exception($"{path} does not exists");
            var data = File.ReadAllText(path);
            var obj = JsonConvert.DeserializeObject<AppSettings>(data);
            return obj;
        }

        public static string GetExampleDataPath(string relativePath, string fileName = null) => Path.Combine(Directory.GetCurrentDirectory(), relativePath, fileName ?? "");

        public static string[] FindExampleData(string relativePath) => Directory.GetFiles(GetExampleDataPath(relativePath));
    }
}
