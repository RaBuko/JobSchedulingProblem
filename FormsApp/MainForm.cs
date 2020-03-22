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


        private void InstructionsButton_Click(object sender, EventArgs e)
        {
            var readme = Loader.LoadFileFromAppDirectory(Program.AppSettings.ReadmeFileName);
            Log(readme);
            DrawingPanel.Invalidate();
        }

        delegate void LogCallback(string text);

        private void Log(string toLog)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (LogRichTextBox.InvokeRequired)
            {
                LogCallback d = new LogCallback(Log);
                Invoke(d, new object[] { toLog });
            }
            else
            {
                LogRichTextBox.AppendText($"{DateTime.UtcNow:HH:mm:ss.fff} : {toLog}\n");
            }
        }

        private void ClearLogButton_Click(object sender, EventArgs e) => LogRichTextBox.Clear();
    }
}
