using FormsApp.Dialogs;
using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace FormsApp
{
    public partial class MainForm : Form
    {
        private List<Job> data;
        private readonly Action<string> logging;
        private List<string> foundFiles = new List<string>();
        private string lastSearchedDirectory;
        private readonly List<(Type, Type)> algorithms;
        private IMethodOptions methodOptions;

        public MainForm()
        {
            InitializeComponent();
            logging = Log;
            LoadFoundDataFiles(Program.AppSettings.ExamplesPath);
            algorithms = Solver.Utils.Helper.GetMethodAndOptionsTypes();
            AlgorithmChangeComboBox.Items.AddRange(algorithms
                .Select(x => x.Item1.Name.Replace("Method", string.Empty)).ToArray());
            AlgorithmChangeComboBox.SelectedIndex = 0;
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            if (methodOptions == null) logging?.Invoke("Nie podano parametrów do algorytmu");
            else
            {
                var algoritmOptionRelation = algorithms[AlgorithmChangeComboBox.SelectedIndex];
                var algorithmType = algoritmOptionRelation.Item1;
                var algorithm = Activator.CreateInstance(algorithmType) as IMethod;
                methodOptions.Data = data;

                Stopwatch stopwatch = new Stopwatch();
                methodOptions = algorithm.Prepare(methodOptions);
                var results = algorithm.Solve(methodOptions, stopwatch, methodOptions.LogEverything ? logging : null);
                logging.Invoke($"Rezultat: {string.Join(",", results.Item1)}");
                logging.Invoke($"Best: {results.Item2}");
            }
        }

        private void ParametersButton_Click(object sender, EventArgs e)
        {
            var methodOptionRelation = algorithms[AlgorithmChangeComboBox.SelectedIndex];
            ParametersForm parametersForm = new ParametersForm(methodOptionRelation.Item2, logging);
            parametersForm.Show();
            parametersForm.VisibleChanged += ParametersFormVisibleChanged;
        }

        private void ParametersFormVisibleChanged(object sender, EventArgs e)
        {
            ParametersForm frm = (ParametersForm)sender;
            if (!frm.Visible)
            {
                if (frm.MethodOptions == null) logging?.Invoke("Przerwano podawanie parametrów");
                else
                {
                    methodOptions = frm.MethodOptions;
                    logging?.Invoke("Podano parametry");
                }
                frm.Dispose();
            }
        }

        private void InstructionsButton_Click(object sender, EventArgs e)
        {
            var readme = Loader.LoadFileFromAppDirectory(Program.AppSettings.ReadmeFileName);
            Log(readme);
        }

        private void Log(string toLog)
        {
            LogRichTextBox.AppendText($"{DateTime.UtcNow:HH:mm:ss.fff} : {toLog}\n");
        }

        private void SaveData_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { logging?.Invoke("Dane niezaładowane, nie można zapisać"); }
            else
            {
                logging?.Invoke("Zapis danych...");
                var module = new FileSavingDialog();
                module.SaveData(data);
                if (lastSearchedDirectory != null) LoadFoundDataFiles(lastSearchedDirectory);
                logging?.Invoke("Koniec");
            }
        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { logging?.Invoke("Dane niezaładowane, nie można pokazać"); }
            else
            {
                logging?.Invoke($"Ilość zadań: {data.Count}");
                logging?.Invoke(string.Join('\n', data.Select(x => x.ToString())));
            }
        }

        private void GenerateDataButton_Click(object sender, EventArgs e)
        {
            logging?.Invoke("Generowanie danych...");
            GenerateDataForm generateDataForm = new GenerateDataForm(Log);
            generateDataForm.Show();
            generateDataForm.VisibleChanged += GenerateDataFormVisibleChanged;
        }

        private void GenerateDataFormVisibleChanged(object sender, EventArgs e)
        {
            GenerateDataForm frm = (GenerateDataForm)sender;
            if (!frm.Visible)
            {
                if (frm.GeneratorOptions == null) logging?.Invoke("Przerwano generowanie danych");
                else
                {
                    data = Generator.GenerateJobs(frm.GeneratorOptions);
                    ChangeDataLabels();
                    logging?.Invoke("Wygenerowano dane");
                }
                frm.Dispose();
            }
        }

        private void SearchFolderButton_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                LoadFoundDataFiles(fbd.SelectedPath);
            }
        }

        private void LoadFoundDataFiles(string directoryPath)
        {
            lastSearchedDirectory = directoryPath;
            FoundDataFilesListBox.Items.Clear();
            foundFiles = Loader.SearchDirectoryForJobsFiles(directoryPath, logging);
            FoundDataFilesListBox.Items.AddRange(foundFiles.Select(x => Path.GetFileName(x)).ToArray());
        }

        private void ChangeDataLabels(string fileName = null)
        {
            if (data == null || data.Count == 0)
            {
                DataLoadedLabel.Text = "Niezaładowane";
                DataLoadedLabel.ForeColor = Color.Red;
            }
            else
            {
                DataLoadedLabel.Text = "Załadowane";
                DataLoadedLabel.ForeColor = Color.Green;
                CountJobsLabel.Text = data.Count.ToString();
                if (fileName != null)
                {
                    BestLabel.Text = Loader.FindBest(fileName);
                    FileNameLabel.Text = fileName;
                }
                else
                {
                    BestLabel.Text = "-";
                    FileNameLabel.Text = "-";
                }
            }
        }

        private void FoundDataFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chosenFile = foundFiles[FoundDataFilesListBox.SelectedIndex];
            logging?.Invoke($"Wczytywanie pliku {chosenFile}");
            data = Loader.LoadJobsFromFile(chosenFile);
            ChangeDataLabels(Path.GetFileName(chosenFile));
            logging?.Invoke("Plik wczytany");
        }

        private void ClearLogButton_Click(object sender, EventArgs e) => LogRichTextBox.Clear();

    }
}
