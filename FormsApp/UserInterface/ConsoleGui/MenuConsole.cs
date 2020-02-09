using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FormsApp.UserInterface.ConsoleGui
{
    class MenuConsole : IMenu
    {
        readonly Type[] Methods = { typeof(BruteForceMethod), typeof(DynamicProgrammingMethod) };

        public MainMenuChoiceEnum MainMenuChoice(bool loaded)
        {
            string mainMenu = $@"
Problem szeregowania zadań

Dane {(loaded ? "ZAŁADOWANE" : "NIEZAŁADOWANE")}

Rozwiąż problem:
1. Wczytaj dane z pliku
2. Wygeneruj nowe dane
3. Wyświetl załadowane dane
4. Rozwiąż dla załadowanych danych
0. Zakończ program
";
            Console.WriteLine(mainMenu);
            return (MainMenuChoiceEnum)ConsoleHelper.GetInputInt();
        }


        public string ChooseFile(string pathToFiles)
        {
            Console.WriteLine("\n\nWCZYTYWANIE DANYCH Z PLIKU");
            var files = FormsApp.Helpers.Loader.FindExampleData(pathToFiles);
            Console.WriteLine($"Znalezione pliki: {files.Length}");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i}: {Path.GetFileName(files[i])}");
            }
            Console.WriteLine($"Wybór (0 - {files.Length - 1})");
            int index = ConsoleHelper.GetInputInt(0, files.Length - 1);
            return files[index];
        }

        public void ShowData(List<Job> data)
        {
            if (data == null || data.Count == 0) { Console.WriteLine("Nie załadowano żadnych danych"); return; }

            string format = "{0,3}: {1,4}|{2,4}|{3,4}\n";
            var sb = new StringBuilder();
            sb.AppendLine("\n");
            sb.AppendFormat(format, "ID", "Time", "Wght", "Term");

            foreach (var item in data)
            {
                sb.AppendFormat(format, item.Index, item.Time, item.Weight, item.Term);
            }

            sb.AppendLine("Nacisnij klawisz by wrocic do menu");
            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }

        GeneratorOptions IMenu.ChooseGenerateDataOptions()
        {
            Console.WriteLine("\n\nGENEROWANIE DANYCH Z PLIKU");
            Console.WriteLine("\nIle zadań chcesz wygenerować? : ");
            var options = new GeneratorOptions() { JobsCount = ConsoleHelper.GetInputInt(1, 1000) };
            Console.WriteLine("\nMaksymalny czas trwania zadania (0 - domyślne 2x ilość zadań)? : ");
            options.MaxTime = ConsoleHelper.GetInputInt(0, 2000, true);
            Console.WriteLine("\nMaksymalna waga zadania (0 - domyślne 2x ilość zadań)? : ");
            options.MaxWeight = ConsoleHelper.GetInputInt(0, 2000, true);
            Console.WriteLine("\nMaksymalny termin zadania (0 - domyślne 2x ilość zadań)? : ");
            options.MaxTerm = ConsoleHelper.GetInputInt(0, 2000, true);
            return options;
        }



        public void SaveGeneratedData(string examplesPath, List<Job> data)
        {
            Console.Write("Podaj nazwe pliku ktory ma zostac wygenerowany, aby pominąć nie podawaj nic : ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path)) return;
            else if (string.IsNullOrWhiteSpace(Path.GetExtension(path))) path += ".txt";
            FormsApp.Helpers.Loader.WriteJobsToFile(FormsApp.Helpers.Loader.GetExampleDataPath(examplesPath + path), data);
        }

        public void MethodsMenu(List<Job> dataJob)
        {
            if (dataJob == null || dataJob.Count < 1)
            {
                Console.WriteLine("Nie załadowano danych, powrót");
                return;
            }
            Console.WriteLine("Wybierz metode rozwiązania:");
            for (int i = 0; i < Methods.Length; i++)
            {
                Console.WriteLine(@$"{i}: {Methods[i].Name}");
            }
            var chosen = Activator.CreateInstance(Methods[ConsoleHelper.GetInputInt(0, Methods.Length - 1)]) as IMethod;
                        
            Console.WriteLine("Przygotowywanie danych ...");
            var methodOptions = new BruteForceOptions();// chosen.Prepare();

            Console.WriteLine("Rozpoczęcie działania");
            Stopwatch stopwatch = new Stopwatch();
            var solution = chosen.Solve(methodOptions, stopwatch, Console.Write);
            var elapsed = stopwatch.Elapsed;
            Console.WriteLine(string.Format("Czas trwania: {0:00}:{1:00}:{2:00}.{3:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds / 10));
            Console.WriteLine($"Opóźnienie (term): {solution.Item2}");
            Console.WriteLine("Znalezione ułożenie zadań: ");
            for (int i = 0; i < solution.Item1.Count; i++)
            {
                Console.Write(solution.Item1[i]);
                if (i != solution.Item1.Count - 1) { Console.Write(","); }
            }
        }
    }
}
