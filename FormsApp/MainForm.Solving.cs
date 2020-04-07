using FormsApp.Dialogs;
using FormsApp.Helpers;
using Solver.Data;
using Solver.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FormsApp
{
    public partial class MainForm
    {
        private async void SolveButton_Click(object sender, EventArgs e)
        {
            var algoritmOptionRelation = algorithms[AlgorithmChangeComboBox.SelectedIndex];
            var algorithmType = algoritmOptionRelation.Item1;
            var algorithm = Activator.CreateInstance(algorithmType, true) as IMethod;
            if (methodOptions == null)
            {
                logging?.Invoke("Nie podano parametrów do algorytmu, uzycie domyslnych parametrow", null, true);
                methodOptions = algorithm.Prepare(methodOptions);
               
            }
            methodOptions.Data = data;
            (List<int>, int) results = (new List<int>(), int.MaxValue);
            await Task.Run(() =>
            {
                Stopwatch stopwatch = new Stopwatch();
                methodOptions = algorithm.Prepare(methodOptions);

                var guiActions = new GuiActions()
                {
                    Log = methodOptions.LogEverything ? logging : null
                };

                results = algorithm.Solve(methodOptions, stopwatch, guiActions);                
            });
            logging.Invoke($"Najlepsze ułożenie:", null, true);
            var resultData = new List<Job>();
            results.Item1.ForEach(i => resultData.Add(data.Find(x => x.Index == i)));
            logging.Invoke(null, resultData, false);
            logging.Invoke($"Best: {results.Item2}", null, true);
            if (Loader.TrySaveNewBest(LoadedFile, results.Item2))
            {
                logging.Invoke($"NOWE NAJLEPSZE ROZWIAZANIE: {results.Item2}", null, true);
                BestScoreLabel.Text = results.Item2.ToString();
            }

        }

        private void ParametersButton_Click(object sender, EventArgs e)
        {
            var methodOptionRelation = algorithms[AlgorithmChangeComboBox.SelectedIndex];
            ParametersForm parametersForm = new ParametersForm(methodOptionRelation.Item2, logging);
            parametersForm.Show();
            parametersForm.VisibleChanged += ParametersFormVisibleChanged;
        }

        private void ParametersFormVisibleChanged(object sender, EventArgs e)
        {
            ParametersForm frm = (ParametersForm)sender;
            if (!frm.Visible)
            {
                if (frm.MethodOptions == null) logging?.Invoke("Przerwano podawanie parametrów", null, true);
                else
                {
                    methodOptions = frm.MethodOptions;
                    logging?.Invoke("Podano parametry", null, true);
                }
                frm.Dispose();
            }
        }
    }
}
