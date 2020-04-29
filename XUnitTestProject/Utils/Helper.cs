using CsvHelper;
using FormsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace XUnitTestProject
{
    public static class Helper
    {
        public static string TestDirPath = $"{Directory.GetCurrentDirectory()}//TESTS";
        public static AppSettings AppSettings = Loader.LoadAppSettings();

        public static List<DataSample> LoadExampleFiles(Action<string> output = null)
        {
            var json = Loader.LoadJson<Dictionary<string, string>>(Loader.LoadFileFromAppDirectory(AppSettings.SolvedDictFileName));
            return Loader.SearchDirectoryForJobsFiles(AppSettings.ExamplesPath, output).Select(x =>
            {
                var dataSample = new DataSample() { FileName = Path.GetFileNameWithoutExtension(x), Data = Loader.LoadJobsFromFile(x) };
                try
                {
                    dataSample.Best = int.Parse(json[dataSample.FileName] ?? null);
                }
                catch
                {
                    dataSample.Best = null;
                }

                return dataSample;
            }).ToList();
        }

        public static void WriteResultsToCsv(string methodName, int size, int bestFoundScore, List<Result> results, string expandFileName = "")
        {
            Directory.CreateDirectory(TestDirPath);
            using var writer = new StreamWriter($"{TestDirPath}//{methodName}_{size}{expandFileName}.csv");

            results = results.Select(x =>
            {
                x.BestFoundScore = bestFoundScore;
                x.Method = methodName;
                x.Size = size;
                x.PercentScoreToBestScore = (x.Score / (float)bestFoundScore).ToString("0.00");
                return x;
            }).ToList();


            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(results);
        }

        public static void LogStatus(string text)
        {
            string logPath = $"{DateTime.Today:yyyy-MM-dd}-testlog.txt";
            File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss.ffffff} : {text}{Environment.NewLine}", Encoding.UTF8);
        }
    }
}
