using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Solver.Utils;
using System.Reflection;

namespace FormsApp
{
    public partial class MainForm : Form
    {
        private List<Job> data;
        private readonly Action<string> LogTextAction;
        private List<string> foundFiles = new List<string>();
        private string lastSearchedDirectory;
        private readonly List<(Type methodType, Type methodOptionType)> algorithms;
        private IMethodOptions methodOptions;

        public MainForm()
        {
            InitializeComponent();
            LogTextAction = LogText;
            LoadFoundDataFiles(Program.AppSettings.ExamplesPath);
            algorithms = Solver.Utils.Helper.GetMethodAndOptionsTypes();
            AlgorithmChangeComboBox.Items.AddRange(algorithms
                .Select(x => x.methodType.Name.Replace("Method", string.Empty)).ToArray());
            AlgorithmChangeComboBox.SelectedIndex = 0;
            MaxLineLength = CalculateMaxLineLengthRichTextBox();
        }


        private void InstructionsButton_Click(object sender, EventArgs e)
        {
            var readme = Loader.LoadFileFromAppDirectory(Program.AppSettings.ReadmeFileName);
            LogText(readme);
        }

        delegate void LogGraphicsCallback(List<Job> jobs);

        private void LogGraphics(List<Job> jobs)
        {
            if (LogRichTextBox.InvokeRequired)
            {
                Invoke(new LogGraphicsCallback(LogGraphics), new object[] { jobs });
            }
            else
            {
                LogRichTextBox.AppendText(GetFormattedJobs(jobs) + Environment.NewLine);
            }
        }

        delegate void LogTextCallback(string text);

        private void LogText(string text)
        {
            if (LogRichTextBox.InvokeRequired)
            {
                Invoke(new LogTextCallback(LogText), new object[] { text });
            }
            else
            {
                LogRichTextBox.AppendText($"{DateTime.UtcNow:HH:mm:ss.fff} : {text}" + Environment.NewLine);
            }
        }

        private void ClearLogButton_Click(object sender, EventArgs e) => LogRichTextBox.Clear();

        private readonly int MaxLineLength;

        private int CalculateMaxLineLengthRichTextBox()
        {
            Graphics g = LogRichTextBox.CreateGraphics();
            float twoCharW = g.MeasureString("aa", LogRichTextBox.Font).Width;
            float oneCharW = g.MeasureString("a", LogRichTextBox.Font).Width;
            return (int)((float)LogRichTextBox.Width / (twoCharW - oneCharW));
        }

        private string GetFormattedJobs(List<Job> jobs)
        {
            StringBuilder sb = new StringBuilder();
            var listOfDividedIndexes = DivideJobsForView(jobs);
            foreach (var indexes in listOfDividedIndexes)
            {
                var jobInThisRange = jobs.GetRange(indexes.Item1, indexes.Item2 - indexes.Item1 + 1);
                foreach (var job in jobInThisRange)
                {
                    sb.AppendFormat("\u250C{0}\u2510", new string('\u2500', job.Time));
                }
                sb.AppendLine();
                foreach (var job in jobInThisRange)
                {
                    string infoToFit = $"{job.Index}\u2500{job.Weight}\u2500{job.Term}\u2500{job.Time}";
                    while (infoToFit.Length > job.Time && infoToFit.Contains('\u2500'))
                    {
                        infoToFit = infoToFit.Remove(infoToFit.LastIndexOf('\u2500'));
                    }
                    sb.AppendFormat("\u2502{0}\u2502", infoToFit.Center(job.Time, '\u2500'));
                }
                sb.AppendLine();
                foreach (var job in jobInThisRange)
                {
                    sb.AppendFormat("\u2514{0}\u2518", new string('\u2500', job.Time));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private List<(int, int)> DivideJobsForView(List<Job> jobs)
        {
            var list = new List<(int, int)>();
            int countedLength = 0;
            int startIndex = 0;
            for (int i = 0; i < jobs.Count; i++)
            {
                countedLength += jobs[i].Time + 2;
                if (countedLength >= MaxLineLength)
                {
                    list.Add((startIndex, i - 1));
                    countedLength = jobs[i].Time;
                    startIndex = i;
                }
            }
            list.Add((startIndex, jobs.Count - 1));
            return list;
        }

        private void AlgorithmChangeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var methodOptionType = algorithms.Find(x => x.methodType.Name.Contains(AlgorithmChangeComboBox.SelectedItem.ToString())).methodOptionType;
            var userDefinedOptions = methodOptionType.GetProperties().Where(x => x.GetCustomAttribute(typeof(UserDefined)) != null).ToList();

            if (!ParametersDataGridView.Columns.Contains("ParameterName"))
            {
                ParametersDataGridView.Columns[ParametersDataGridView.Columns.Add("ParameterName", "Parametr")].ReadOnly = true;
            }

            if (!ParametersDataGridView.Columns.Contains("Value"))
            {
                ParametersDataGridView.Columns.Add("Value", "Wartość");
            }

            ParametersDataGridView.Rows.Clear();
            List<(string name, string value)> parameters = new List<(string name, string value)>();
            for (int i = 0; i < userDefinedOptions.Count(); i++)
            {
                var param = userDefinedOptions[i].GetCustomAttribute(typeof(UserDefined)) as UserDefined;
                var index = ParametersDataGridView.Rows.Add(param.ParameterFormalName, null);
                if (param.Type == typeof(bool))
                {
                    var checkBoxCell = new DataGridViewCheckBoxCell();
                    checkBoxCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ParametersDataGridView[1, index] = checkBoxCell;
                    ParametersDataGridView[1, index].Value = param.DefaultValue;
                }
                else if (param.Type.IsEnum)
                {
                    var comboBox = new DataGridViewComboBoxCell();
                    var names = Enum.GetNames(param.Type);
                    comboBox.Items.AddRange(names);
                    comboBox.Value = names[0];
                    ParametersDataGridView[1, index] = comboBox;
                }
                else
                {
                    ParametersDataGridView[1, index].Value = param.DefaultValue;
                }
            }
        }
    }
}
