using FormsApp.Helpers;
using Solver.Data;
using System;
using System.Windows.Forms;

namespace FormsApp.Dialogs
{
    public partial class GenerateDataForm : Form
    {
        public GeneratorOptions GeneratorOptions;

        private readonly Action<string> Logging;

        public GenerateDataForm(Action<string> loggingAction = null)
        {
            Logging = loggingAction;
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (ValidateTextBox(JobsCountTextBox, "Liczba zadań") &&
                ValidateTextBox(MaxTermTextBox, "Maks. termin") &&
                ValidateTextBox(MaxTimeTextBox, "Maks. czas") &&
                ValidateTextBox(MaxWeightTextBox, "Maks. waga")
                )
            {
                GeneratorOptions = new GeneratorOptions()
                {
                    JobsCount = int.Parse(JobsCountTextBox.Text),
                    MaxTerm = int.Parse(MaxTermTextBox.Text),
                    MaxTime = int.Parse(MaxTimeTextBox.Text),
                    MaxWeight = int.Parse(MaxWeightTextBox.Text),
                };
                Logging?.Invoke($"Parametry generowania: {Loader.ParseToJsonString(GeneratorOptions)}");
                Visible = false;
            }
        }

        private bool ValidateTextBox(TextBox textBox, string type)
        {
            if (!int.TryParse(textBox.Text, out int result))
            {
                Logging?.Invoke($"{type}: {textBox.Text} nie jest liczbą");
                return false;
            }
            return true;
        }
    }
}
