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

namespace FormsApp
{
    public partial class MainForm : Form
    {
        private List<Job> data;
        private readonly Action<string> logging;
        private List<string> foundFiles = new List<string>();
        private string lastSearchedDirectory;
        private readonly Type[] algorithms = { 
            typeof(DynamicProgrammingMethod), 
            typeof(BruteForceMethod) 
        };

        public MainForm()
        {
            InitializeComponent();
            logging = Log;
            LoadFoundDataFiles(Program.AppSettings.ExamplesPath);
        }

        private void ParametersButton_Click(object sender, EventArgs e)
        {

        }

        private void AlgorithmChangeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Type algorithmType = algorithms[AlgorithmChangeComboBox.SelectedIndex];
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
