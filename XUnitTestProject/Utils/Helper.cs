using CsvHelper;
using CsvHelper.Configuration;
using FormsApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public static class Helper
    {
        public static string TestDirPath = $"{Directory.GetCurrentDirectory()}//TESTS";
        public static AppSettings AppSettings = Loader.LoadAppSettings();

        [Fact]
        public static void SummarizeCSV()
        {
            var path = $"{TestDirPath}//SUMMARIZED.csv";
            int testCount = 20;

            foreach (var file in Directory.GetFiles(TestDirPath, "*.csv")
                .Where(x => char.IsDigit(x.Split("\\", StringSplitOptions.RemoveEmptyEntries).Last().First())))
            {


                List<Result> records = new List<Result>();
                int jobsCount = int.Parse(file.Split("\\", StringSplitOptions.RemoveEmptyEntries).Last().Split("_").First());
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };
                using (var reader = new StreamReader(file))
                {
                    using var csv = new CsvReader(reader, config);
                    records = csv.GetRecords<Result>().ToList();
                }

                config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = !File.Exists(path) };
                using (var writer = new StreamWriter(path, true))
                {
                    using var csv = new CsvWriter(writer, config);
                    foreach (var group in records.GroupBy(x => x.Method).OrderBy(x => x.Key))
                    {
                        var meanTime = group.Sum(x => TimeSpan.Parse(x.ExecElapsed).Ticks) / testCount;
                        var scoreDeviation = group.Sum(x => decimal.Parse(x.PercentScoreToBestScore)) / testCount;
                        csv.WriteRecord(new { JobsCount = jobsCount, Method = group.Key, MeanTime = meanTime, ScoreDeviation = scoreDeviation });
                        csv.NextRecord();
                    }
                }  
            }
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
