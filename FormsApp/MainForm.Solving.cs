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

        private int lastHeightPanel = 0;
        private readonly int SpaceYBetweenJobs = 3;
        private readonly int SpaceXBetweenJobs = 3;
        private readonly int JobHeight = 10;
        readonly Stack<Rectangle> rectanglesBuffer = new Stack<Rectangle>();

        private void DrawingPanel_Paint(object sender, PaintEventArgs pe)
        {
            if (lastHeightPanel + JobHeight * 3 > DrawingPanel.Height)
            {
                rectanglesBuffer.Clear();
                lastHeightPanel = 0;
            }

            Rectangle rect = new Rectangle() { X = 0, Y = lastHeightPanel, Width = 0, Height = JobHeight };
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    Log(rect.ToString());
                    rect.Width = data[i].Time;
                    rectanglesBuffer.Push(rect);
                    if (rect.X + rect.Width > DrawingPanel.Width)
                    {
                        rect.X = 0;
                        rect.Y += rect.Height + SpaceYBetweenJobs;
                    }
                    else
                    {
                        rect.X += rect.Width + SpaceXBetweenJobs;
                    }
                }
            }
            lastHeightPanel = rect.Y + JobHeight + SpaceYBetweenJobs;

            using Graphics g = pe.Graphics;
            using var brush = new SolidBrush(Color.FromArgb(200, 200, 200, 255));
            foreach (var r in rectanglesBuffer)
            {
                g.FillRectangle(brush, r);
            }
            g.Save();
        }

        private async void SolveButton_Click(object sender, EventArgs e)
        {
            if (methodOptions == null) logging?.Invoke("Nie podano parametrów do algorytmu");
            else
            {
                var algoritmOptionRelation = algorithms[AlgorithmChangeComboBox.SelectedIndex];
                var algorithmType = algoritmOptionRelation.Item1;
                var algorithm = Activator.CreateInstance(algorithmType) as IMethod;
                methodOptions.Data = data;

                var guiActions = new GuiActions()
                {
                    Log = methodOptions.LogEverything ? logging : null,
                    ChangeData = null,
                    RefreshGraphicsAction = DrawingPanel.Invalidate,
                };

                await Task.Run(() =>
                {
                    Stopwatch stopwatch = new Stopwatch();
                    methodOptions = algorithm.Prepare(methodOptions);
                    var results = algorithm.Solve(methodOptions, stopwatch, guiActions);
                    logging.Invoke($"Rezultat: {string.Join(",", results.Item1)}");
                    logging.Invoke($"Best: {results.Item2}");
                });
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
                if (frm.MethodOptions == null) logging?.Invoke("Przerwano podawanie parametrów");
                else
                {
                    methodOptions = frm.MethodOptions;
                    logging?.Invoke("Podano parametry");
                }
                frm.Dispose();
            }
        }
    }
}
