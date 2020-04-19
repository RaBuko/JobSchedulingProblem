using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class MainForm
    {
        private Task SolveTask;
        private CancellationTokenSource CancellationTokenSource;

        private async void SolveButton_Click(object sender, EventArgs e)
        {
            if (data == null)
            {
                LogText("Nie załadowano lub nie wygenerowano żadnej instancji testowej");
            }

            if (SolveTask?.Status == TaskStatus.Running)
            {
                LogText("Rozpoczęto bezpieczne przerwanie rozwiązywania");
                CancellationTokenSource.Cancel();
                return;
            }

            CancellationTokenSource = new CancellationTokenSource();

            var preparedData = PrepareToSolve(CancellationTokenSource.Token);

            (List<int> bestOrder, int minimizedTardiness) results = (new List<int>(), int.MaxValue);

            Stopwatch stopwatch = new Stopwatch();
            SolveTask = Task.Run(() =>
            {
                results = preparedData.method.Solve(preparedData.methodOptions, stopwatch);
                if (stopwatch.IsRunning) stopwatch.Stop();
            }, CancellationTokenSource.Token);

            SwitchUIMode();
            await SolveTask;
            SwitchUIMode();

            LogText($"Koniec : {DateTime.Now:HH:mm:ss.fff}");
            LogText($"Upłynęło : {stopwatch.Elapsed:hh\\:mm\\:ss\\.fffffff}");

            if (results.bestOrder.Any())
            {
                LogTextAction.Invoke($"Najlepsze ułożenie:");
                LogGraphics(data.JobsFromIndexList(results.bestOrder));
            }

            LogText($"Najmniejsze znalezione opóźnienie: {results.minimizedTardiness}");

            if (Loader.TrySaveNewBest(LoadedFile, results.minimizedTardiness))
            {
                LogText($"NOWE NAJLEPSZE ROZWIAZANIE: {results.minimizedTardiness}");
                BestScoreLabel.Text = results.minimizedTardiness.ToString();
            }
        }

        private (IMethod method, IMethodOptions methodOptions) PrepareToSolve(CancellationToken token)
        {
            var (methodType, methodOptionType) = algorithms[AlgorithmChangeComboBox.SelectedIndex];
            var method = Activator.CreateInstance(methodType) as IMethod;

            methodOptions = Activator.CreateInstance(methodOptionType) as IMethodOptions;
            methodOptions.Data = data;

            var userDefinedOptions = methodOptionType.GetProperties().Where(x => x.GetCustomAttribute(typeof(UserDefined)) != null).ToList();

            var guiConnection = new GuiConnection();

            foreach (var option in userDefinedOptions)
            {
                var userDefined = option.GetCustomAttribute(typeof(UserDefined)) as UserDefined;
                var value = FindValueInDataGridView(userDefined.ParameterFormalName);
                if (option.Name.Equals("ShouldLogText") && (bool)value)
                {
                    guiConnection.LogText = LogText;
                }
                if (option.Name.Equals("ShouldLogGraphics") && (bool)value)
                {
                    guiConnection.LogGraphics = LogGraphics;
                }
                if (option.PropertyType.IsEnum)
                {
                    value = Enum.Parse(userDefined.Type, value.ToString());
                }
                else
                {
                    if (value.GetType() != option.PropertyType)
                    {
                        value = Convert.ChangeType(value, option.PropertyType);
                    }
                    option.SetValue(methodOptions, value);
                }
            }

            methodOptions.GuiConnection = guiConnection;
            methodOptions.CancellationToken = token;

            methodOptions = method.Prepare(methodOptions);

            return (method, methodOptions);
        }

        private object FindValueInDataGridView(string searchParameterName)
        {
            object value = null;
            foreach (DataGridViewRow row in ParametersDataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchParameterName))
                {
                    value = row.Cells[1].Value;
                }
            }
            return value;
        }

        private void SwitchUIMode()
        {
            if (InstructionsButton.Enabled)
            {
                InstructionsButton.Enabled = false;
                GenerateDataButton.Enabled = false;
                SearchFolderButton.Enabled = false;
                ShowDataButton.Enabled = false;
                SaveDataButton.Enabled = false;
                SolveButton.Text = "STOP";
            }
            else
            {
                InstructionsButton.Enabled = true;
                GenerateDataButton.Enabled = true;
                SearchFolderButton.Enabled = true;
                ShowDataButton.Enabled = true;
                SaveDataButton.Enabled = true;
                SolveButton.Text = "START";
            }
        }
    }
}
