using Solver.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace FormsApp.Helpers
{
    internal class Loader
    {
        /// <summary>
        /// Loads jobs from specified file. Each file contains the data listed 
        /// one after the other. The n processing times are listed first (Time), 
        /// followed by the n weights (Weight), and finally n due dates (Term).
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Set of parsed jobs</returns>
        internal static List<Job> LoadJobsFromFile(string path)
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

        internal static void WriteJobsToFile(string path, List<Job> jobs)
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

        #region JSON
        internal static AppSettings LoadAppSettings() => LoadJson<AppSettings>(LoadFileFromAppDirectory("appsettings.json"));

        private static T LoadJson<T>(string filePath) where T : class => JsonConvert.DeserializeObject<T>(filePath);

        internal static string ParseToJsonString(object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);

        #endregion

        internal static string GetExampleDataPath(string relativePath, string fileName = null) => Path.Combine(Directory.GetCurrentDirectory(), relativePath, fileName ?? "");

        internal static string[] FindExampleData(string relativePath) => Directory.GetFiles(GetExampleDataPath(relativePath));

        internal static string LoadFileFromAppDirectory(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (!File.Exists(path)) throw new Exception($"{path} does not exists");
            return File.ReadAllText(path);
        }

        internal static string FindBest(string fileName)
        {
            var dict = LoadJson<Dictionary<string, string>>(LoadFileFromAppDirectory(Program.AppSettings.SolvedDictFileName));
            fileName = Path.GetFileNameWithoutExtension(fileName);
            try
            {
                return dict[fileName];
            }
            catch
            {
                return "-";
            }

        }

        internal static List<string> SearchDirectoryForJobsFiles(string directoryPath, Action<string> LogText)
        {
            List<string> foundFiles = new List<string>();
            LogText($"Przeszukiwany folder: {directoryPath}");
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                try
                {
                    Loader.LoadJobsFromFile(file);
                }
                catch (InvalidDataException)
                {
                    LogText($"{Path.GetFileName(file)} nie ma właściwego formatu zadań");
                    continue;
                }
                catch (Exception)
                {
                    LogText($"Plik {Path.GetFileName(file)} jest niepoprawny");
                    continue;
                }

                foundFiles.Add(file);
            }
            LogText($"Znaleziono plików: {foundFiles.Count()}");
            return foundFiles;
        }

        internal static bool TrySaveNewBest(string fileName, decimal newBest)
        {
            bool save;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var foundBest = FindBest(fileName);
                save = foundBest.Contains("-") || int.Parse(foundBest) < newBest;
            }
            else
            {
                save = true;
            }

            if (save)
            {
                var json = LoadJson<Dictionary<string, string>>(LoadFileFromAppDirectory(Program.AppSettings.SolvedDictFileName));
                json[Path.GetFileNameWithoutExtension(fileName)] = newBest.ToString();
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), Program.AppSettings.SolvedDictFileName), JsonConvert.SerializeObject(json));
            }
            return save;
        }
    }
}
