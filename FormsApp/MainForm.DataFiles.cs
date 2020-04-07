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
            if (data == null || data.Count == 0) { logging?.Invoke("Dane niezaładowane, nie można zapisać", null, true); }
            else
            {
                logging?.Invoke("Zapis danych...", null, true);
                var module = new FileSavingDialog();
                LoadedFile = module.SaveData(data);
                if (lastSearchedDirectory != null) LoadFoundDataFiles(lastSearchedDirectory);
                logging?.Invoke("Koniec", null, true);
            }
        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { logging?.Invoke("Dane niezaładowane, nie można pokazać", null, true); }
            else
            {
                logging?.Invoke($"Ilość zadań: {data.Count}", null, true);
                logging?.Invoke(string.Join('\n', data.Select(x => x.ToString())), null, true);
            }
        }

        private void GenerateDataButton_Click(object sender, EventArgs e)
        {
            logging?.Invoke("Generowanie danych...", null, true);
            GenerateDataForm generateDataForm = new GenerateDataForm(Log);
            generateDataForm.Show();
            generateDataForm.VisibleChanged += GenerateDataFormVisibleChanged;
        }

        private void GenerateDataFormVisibleChanged(object sender, EventArgs e)
        {
            GenerateDataForm frm = (GenerateDataForm)sender;
            if (!frm.Visible)
            {
                if (frm.GeneratorOptions == null) logging?.Invoke("Przerwano generowanie danych", null, true);
                else
                {
                    data = Generator.GenerateJobs(frm.GeneratorOptions);
                    ChangeDataLabels();
                    logging?.Invoke("Wygenerowano dane", null, true);
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
            logging?.Invoke($"Wczytywanie pliku {LoadedFile}", null, true);
            data = Loader.LoadJobsFromFile(LoadedFile);
            ChangeDataLabels(Path.GetFileName(LoadedFile));
            logging?.Invoke("Plik wczytany", null, true);
        }
    }
}
