using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FormsApp.Dialogs
{
    public class FileSavingDialog
    {
        public FileSavingDialog()
        {

        }

        internal void SaveData(List<Solver.Data.Job> data)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    var sb = new StringBuilder();

                    foreach (var item in data)
                    {
                        sb.Append(item.Time + " ");
                    }

                    foreach (var item in data)
                    {
                        sb.Append(item.Weight + " ");
                    }

                    foreach (var item in data)
                    {
                        sb.Append(item.Term + " ");
                    }

                    var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                    myStream.Write(bytes, 0, bytes.Length);
                    myStream.Close();
                }
            }
        }
    }
}
