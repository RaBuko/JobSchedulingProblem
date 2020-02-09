using FormsApp.Dialogs;
using FormsApp.Helpers;
using Solver.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class MainForm : Form
    {
        public List<Job> data;
        public Action<string> Logging;

        public MainForm()
        {
            InitializeComponent();
            Logging = Log;
        }

        private void AlgorithmChangeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void SavaData_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { Logging?.Invoke("Dane niezaładowane, nie można zapisać"); }
            else
            {
                Logging?.Invoke("Zapis danych...");
                var module = new FileSavingDialog();
                module.SaveData(data);
                Logging?.Invoke("Koniec");
            }
        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            if (data == null || data.Count == 0) { Logging?.Invoke("Dane niezaładowane, nie można pokazać"); }
            else
            {
                Logging?.Invoke($"Ilość zadań: {data.Count}");
                Logging?.Invoke(string.Join('\n', data.Select(x => x.ToString())));
            }
        }

        private void GenerateDataButton_Click(object sender, EventArgs e)
        {
            Logging?.Invoke("Generowanie danych...");
            GenerateDataForm generateDataForm = new GenerateDataForm(Log);
            generateDataForm.Show();
            generateDataForm.VisibleChanged += GenerateDataFormVisibleChanged;
        }

        private void GenerateDataFormVisibleChanged(object sender, EventArgs e)
        {
            GenerateDataForm frm = (GenerateDataForm)sender;
            if (!frm.Visible)
            {
                if (frm.GeneratorOptions == null) Logging?.Invoke("Przerwano generowanie danych");
                else
                {
                    data = Generator.GenerateJobs(frm.GeneratorOptions);
                    Logging?.Invoke("Wygenerowano dane");
                    // TODO uzupełnienie Labelek
                }
                frm.Dispose();
            }
        }

        private void SearchFolderButton_Click(object sender, EventArgs e)
        {

        }

        private void ChangeDataLabels()
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
                BestLabel 
            }
        }

        private void FoundDataFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ClearLogButton_Click(object sender, EventArgs e) => LogRichTextBox.Clear();
    }
}
