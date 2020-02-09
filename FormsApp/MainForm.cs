using FormsApp.Dialogs;
using FormsApp.Helpers;
using Solver.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class MainForm : Form
    {
        public List<Job> data;

        public MainForm()
        {
            InitializeComponent();
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
            var module = new FileSavingDialog();
            module.SaveData(data);
        }

        private void GenerateDataButton_Click(object sender, EventArgs e)
        {
            GenerateDataForm generateDataForm = new GenerateDataForm(Log);
            generateDataForm.Show();
            generateDataForm.VisibleChanged += GenerateDataFormVisibleChanged;
        }

        private void GenerateDataFormVisibleChanged(object sender, EventArgs e)
        {
            GenerateDataForm frm = (GenerateDataForm)sender;
            if (!frm.Visible)
            {
                data = Generator.GenerateJobs(frm.GeneratorOptions);
                frm.Dispose();
            }
        }

        private void SearchFolderButton_Click(object sender, EventArgs e)
        {

        }

        private void FoundDataFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
