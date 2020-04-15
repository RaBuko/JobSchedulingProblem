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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;

namespace FormsApp
{
    public partial class MainForm
    {
        private string LoadedFile;

        private void SaveData_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { LogText("Dane niezaładowane, nie można zapisać"); }
            else
            {
                LogText("Zapis danych...");
                var module = new FileSavingDialog();
                LoadedFile = module.SaveData(data);
                if (lastSearchedDirectory != null) LoadFoundDataFiles(lastSearchedDirectory);
                LogText("Koniec");
            }
        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { LogText("Dane niezaładowane, nie można pokazać"); }
            else
            {
                LogText($"Ilość zadań: {data.Count}");
                LogText(string.Join('\n', data.Select(x => x.ToString())));
            }
        }

        private void GenerateDataButton_Click(object sender, EventArgs e)
        {
            LogText("Generowanie danych...");
            GenerateDataForm generateDataForm = new GenerateDataForm(LogText);
            generateDataForm.Show();
            generateDataForm.VisibleChanged += GenerateDataFormVisibleChanged;
        }

        private void GenerateDataFormVisibleChanged(object sender, EventArgs e)
        {
            GenerateDataForm frm = (GenerateDataForm)sender;
            if (!frm.Visible)
            {
                if (frm.GeneratorOptions == null) LogText("Przerwano generowanie danych");
                else
                {
                    data = Generator.GenerateJobs(frm.GeneratorOptions);
                    ChangeDataLabels();
                    LogText("Wygenerowano dane");
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
            foundFiles = Loader.SearchDirectoryForJobsFiles(directoryPath, LogTextAction);
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
                    BestScoreLabel.Text = Loader.FindBest(fileName);
                    FileNameLabel.Text = fileName;
                }
                else
                {
                    BestScoreLabel.Text = "-";
                    FileNameLabel.Text = "-";
                }
            }
        }

        private void FoundDataFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadedFile = foundFiles[FoundDataFilesListBox.SelectedIndex];
            LogText($"Wczytywanie pliku {LoadedFile}");
            data = Loader.LoadJobsFromFile(LoadedFile);
            ChangeDataLabels(Path.GetFileName(LoadedFile));
            LogText("Plik wczytany");
        }
    }
}
