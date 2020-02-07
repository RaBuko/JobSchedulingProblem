using Solver.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp.UserInterface
{
    interface IMenu
    {
        public MainMenuChoiceEnum MainMenuChoice(bool loaded);
        string ChooseFile(string pathToFiles);
        GeneratorOptions ChooseGenerateDataOptions();
        void ShowData(List<Job> data);
        void MethodsMenu(List<Job> data);
        void SaveGeneratedData(string examplesPath, List<Job> data);
    }
}
