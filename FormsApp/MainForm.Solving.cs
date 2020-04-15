using FormsApp.Dialogs;
using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace FormsApp
{
    public partial class MainForm
    {
        private Task SolveTask;

        private async void SolveButton_Click(object sender, EventArgs e)
        {
            var preparedData = PrepareToSolve();

            (List<int> bestOrder, int minimizedTardiness) results = (new List<int>(), int.MaxValue);

            Stopwatch stopwatch = new Stopwatch();
            SolveTask = Task.Run(() =>
            {
                results = preparedData.method.Solve(preparedData.methodOptions, stopwatch);
                if (stopwatch.IsRunning) stopwatch.Stop();
            });

            SwitchUIMode();
            await SolveTask;
            SwitchUIMode();

            LogText($"Koniec : {DateTime.Now:HH:mm:ss.fff}");
            LogText($"Upłynęło : {stopwatch.Elapsed:hh\\:mm\\:ss\\.fffffff}");

            LogTextAction.Invoke($"Najlepsze ułożenie:");
            LogGraphics(Solver.Utils.IntManip.JobsFromIndexList(results.bestOrder, data));
            LogText($"Najmniejsze znalezione opóźnienie: {results.minimizedTardiness}");

            if (Loader.TrySaveNewBest(LoadedFile, results.minimizedTardiness))
            {
                LogText($"NOWE NAJLEPSZE ROZWIAZANIE: {results.minimizedTardiness}");
                BestScoreLabel.Text = results.minimizedTardiness.ToString();
            }
        }

        private (IMethod method, IMethodOptions methodOptions) PrepareToSolve()
        {
            var (methodType, methodOptionType) = algorithms[AlgorithmChangeComboBox.SelectedIndex];
            var method = Activator.CreateInstance(methodType) as IMethod;

            methodOptions = Activator.CreateInstance(methodOptionType) as IMethodOptions;
            methodOptions.Data = data;
            var guiConnection = new GuiConnection();
            if (GraphicLogCheckBox.Checked)
            {
                guiConnection.LogGraphics = LogGraphics;
            }
            if (DetailsTextLogCheckBox.Checked)
            {
                guiConnection.LogText = LogText;
            }

            methodOptions.GuiConnection = guiConnection;

            methodOptions = method.Prepare(methodOptions);

            return (method, methodOptions);
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
