using FormsApp.Helpers;
using System;
using System.Windows.Forms;

namespace FormsApp
{
    static class Program
    {
        public static AppSettings AppSettings;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppSettings = Loader.LoadAppSettings();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new MainForm();
            Application.Run(form);
        }
    }
}
