using FormsApp.Helpers;
using Solver.Methods;
using Solver.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FormsApp.Dialogs
{
    public partial class ParametersForm : Form
    {
        readonly TextBox[] textBoxes;
        readonly Label[] labels;

        private readonly Action<string> logging;
        public IMethodOptions MethodOptions;
        private readonly List<PropertyInfo> userDefinedOptions;

        public ParametersForm(Type optionsType, Action<string> inLogging)
        {
            logging = inLogging;
            userDefinedOptions = optionsType.GetProperties().Where(x => x.GetCustomAttribute(typeof(UserDefined)) != null).ToList();
            logging?.Invoke($"Ilość definiowanych przez użytkownika parametrów: {userDefinedOptions.Count()}");
            MethodOptions = Activator.CreateInstance(optionsType) as IMethodOptions;
            InitializeComponent();
            textBoxes = new TextBox[userDefinedOptions.Count()];
            labels = new Label[userDefinedOptions.Count()];
            int initialPositionY = ParametersInfoMainLabel.Location.Y;

            for (int i = 0; i < userDefinedOptions.Count(); i++)
            {
                string text = (userDefinedOptions[i].GetCustomAttribute(typeof(UserDefined)) as UserDefined).ParameterFormalName;
                labels[i] = new Label
                {
                    Name = userDefinedOptions[i].Name + "Label",
                };

                using (Graphics g = CreateGraphics())
                {
                    labels[i].Text = $"{text} ({userDefinedOptions[i].PropertyType}) : ";
                    SizeF size = g.MeasureString(labels[i].Text, labels[i].Font, 495);
                    labels[i].Height = (int)Math.Ceiling(size.Height);
                    labels[i].Width = (int)Math.Ceiling(size.Width);
                }

                textBoxes[i] = new TextBox
                {
                    Name = userDefinedOptions[i].Name + "TextBox",
                    Text = userDefinedOptions[i].GetValue(MethodOptions).ToString() ?? "",
                };
            }

            for (int i = 0; i < userDefinedOptions.Count(); i++)
            {
                labels[i].Location = new Point(10, initialPositionY + labels[i].Height + 5);
                textBoxes[i].Location = new Point(20 + labels[i].Width, labels[i].Location.Y);
                labels[i].Visible = true;
                textBoxes[i].Visible = true;
                Controls.Add(labels[i]);
                Controls.Add(textBoxes[i]);
            }

            GoBackButton.Location = new Point(10, labels[^1].Location.Y + 30);
            Size = new Size(
                textBoxes.Max(x => x.Left + x.Width) + 20,
                GoBackButton.Top + GoBackButton.Height + 50
                );
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            foreach (var optionProp in userDefinedOptions)
            {
                string valueAsText = textBoxes.ToList().Find(x => x.Name.Equals(optionProp.Name + "TextBox")).Text;
                object value = null;
                try
                {
                    value = Convert.ChangeType(valueAsText, optionProp.PropertyType);
                }
                catch (Exception)
                {
                    logging?.Invoke($"Nie udało się przetłumaczyć pola {(optionProp.GetCustomAttribute(typeof(UserDefined)) as UserDefined).ParameterFormalName} na typ {optionProp.PropertyType}, wartość: {valueAsText}");
                }

                optionProp.SetValue(MethodOptions, value);
            }
            Visible = false;
        }
    }
}
