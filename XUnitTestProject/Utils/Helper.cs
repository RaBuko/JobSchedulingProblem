using CsvHelper;
using CsvHelper.Configuration;
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

        public static void WriteResultsToCsv(string methodName, int size, int bestFoundScore, List<Result> results)
        {
            Directory.CreateDirectory(TestDirPath);
            var path = $"{TestDirPath}//{size}_{bestFoundScore}.csv";
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = !File.Exists(path) };
            using var writer = new StreamWriter(path, append: true);

            results = results.Select(x =>
            {
                x.Method = methodName.Replace("Method", "");
                x.PercentScoreToBestScore = (x.Score / (float)bestFoundScore).ToString("0.00");
                return x;
            }).ToList();

            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(results);
        }

        public static void LogStatus(string text)
        {
            string logPath = $"{DateTime.Today:yyyy-MM-dd}-testlog.txt";
            File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss.ffffff} : {text}{Environment.NewLine}", Encoding.UTF8);
        }
    }
}
