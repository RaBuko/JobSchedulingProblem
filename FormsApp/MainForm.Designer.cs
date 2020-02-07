using System.Windows.Forms;

namespace FormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.ParametersButton = new System.Windows.Forms.Button();
            this.LoggingLevelButton = new System.Windows.Forms.Button();
            this.InstructionsButton = new System.Windows.Forms.Button();
            this.SolveButton = new System.Windows.Forms.Button();
            this.SavaData = new System.Windows.Forms.Button();
            this.GenerateDataButton = new System.Windows.Forms.Button();
            this.LoadDataGroupBox = new System.Windows.Forms.GroupBox();
            this.FoundDataFilesListBox = new System.Windows.Forms.ListBox();
            this.SearchFolderButton = new System.Windows.Forms.Button();
            this.AlgorithmLabel = new System.Windows.Forms.Label();
            this.AlgorithmChangeComboBox = new System.Windows.Forms.ComboBox();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.MainGroupBox.SuspendLayout();
            this.LoadDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.ParametersButton);
            this.MainGroupBox.Controls.Add(this.LoggingLevelButton);
            this.MainGroupBox.Controls.Add(this.InstructionsButton);
            this.MainGroupBox.Controls.Add(this.SolveButton);
            this.MainGroupBox.Controls.Add(this.SavaData);
            this.MainGroupBox.Controls.Add(this.GenerateDataButton);
            this.MainGroupBox.Controls.Add(this.LoadDataGroupBox);
            this.MainGroupBox.Controls.Add(this.AlgorithmLabel);
            this.MainGroupBox.Controls.Add(this.AlgorithmChangeComboBox);
            this.MainGroupBox.Location = new System.Drawing.Point(553, 12);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(235, 426);
            this.MainGroupBox.TabIndex = 2;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Menu";
            // 
            // ParametersButton
            // 
            this.ParametersButton.Location = new System.Drawing.Point(6, 95);
            this.ParametersButton.Name = "ParametersButton";
            this.ParametersButton.Size = new System.Drawing.Size(223, 38);
            this.ParametersButton.TabIndex = 6;
            this.ParametersButton.Text = "Dostosuj parametry";
            this.ParametersButton.UseVisualStyleBackColor = true;
            // 
            // LoggingLevelButton
            // 
            this.LoggingLevelButton.Location = new System.Drawing.Point(119, 156);
            this.LoggingLevelButton.Name = "LoggingLevelButton";
            this.LoggingLevelButton.Size = new System.Drawing.Size(107, 38);
            this.LoggingLevelButton.TabIndex = 6;
            this.LoggingLevelButton.Text = "Poziom logowania";
            this.LoggingLevelButton.UseVisualStyleBackColor = true;
            // 
            // InstructionsButton
            // 
            this.InstructionsButton.Location = new System.Drawing.Point(6, 156);
            this.InstructionsButton.Name = "InstructionsButton";
            this.InstructionsButton.Size = new System.Drawing.Size(107, 38);
            this.InstructionsButton.TabIndex = 6;
            this.InstructionsButton.Text = "Instrukcja";
            this.InstructionsButton.UseVisualStyleBackColor = true;
            this.InstructionsButton.Click += new System.EventHandler(this.InstructionsButton_Click);
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(6, 51);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(223, 38);
            this.SolveButton.TabIndex = 6;
            this.SolveButton.Text = "Rozwiąż";
            this.SolveButton.UseVisualStyleBackColor = true;
            // 
            // SavaData
            // 
            this.SavaData.Location = new System.Drawing.Point(119, 200);
            this.SavaData.Name = "SavaData";
            this.SavaData.Size = new System.Drawing.Size(110, 38);
            this.SavaData.TabIndex = 6;
            this.SavaData.Text = "Zapisz dane";
            this.SavaData.UseVisualStyleBackColor = true;
            // 
            // GenerateDataButton
            // 
            this.GenerateDataButton.Location = new System.Drawing.Point(6, 200);
            this.GenerateDataButton.Name = "GenerateDataButton";
            this.GenerateDataButton.Size = new System.Drawing.Size(107, 38);
            this.GenerateDataButton.TabIndex = 6;
            this.GenerateDataButton.Text = "Wygeneruj dane";
            this.GenerateDataButton.UseVisualStyleBackColor = true;
            // 
            // LoadDataGroupBox
            // 
            this.LoadDataGroupBox.Controls.Add(this.FoundDataFilesListBox);
            this.LoadDataGroupBox.Controls.Add(this.SearchFolderButton);
            this.LoadDataGroupBox.Location = new System.Drawing.Point(6, 244);
            this.LoadDataGroupBox.Name = "LoadDataGroupBox";
            this.LoadDataGroupBox.Size = new System.Drawing.Size(223, 176);
            this.LoadDataGroupBox.TabIndex = 2;
            this.LoadDataGroupBox.TabStop = false;
            this.LoadDataGroupBox.Text = "Wczytywanie danych";
            // 
            // FoundDataFilesListBox
            // 
            this.FoundDataFilesListBox.FormattingEnabled = true;
            this.FoundDataFilesListBox.ItemHeight = 15;
            this.FoundDataFilesListBox.Location = new System.Drawing.Point(6, 55);
            this.FoundDataFilesListBox.Name = "FoundDataFilesListBox";
            this.FoundDataFilesListBox.Size = new System.Drawing.Size(209, 109);
            this.FoundDataFilesListBox.TabIndex = 1;
            // 
            // SearchFolderButton
            // 
            this.SearchFolderButton.Location = new System.Drawing.Point(6, 22);
            this.SearchFolderButton.Name = "SearchFolderButton";
            this.SearchFolderButton.Size = new System.Drawing.Size(209, 27);
            this.SearchFolderButton.TabIndex = 0;
            this.SearchFolderButton.Text = "Przeszukaj folder";
            this.SearchFolderButton.UseVisualStyleBackColor = true;
            // 
            // AlgorithmLabel
            // 
            this.AlgorithmLabel.AutoSize = true;
            this.AlgorithmLabel.Location = new System.Drawing.Point(6, 30);
            this.AlgorithmLabel.Name = "AlgorithmLabel";
            this.AlgorithmLabel.Size = new System.Drawing.Size(60, 15);
            this.AlgorithmLabel.TabIndex = 5;
            this.AlgorithmLabel.Text = "Algorytm:";
            // 
            // AlgorithmChangeComboBox
            // 
            this.AlgorithmChangeComboBox.FormattingEnabled = true;
            this.AlgorithmChangeComboBox.Location = new System.Drawing.Point(72, 22);
            this.AlgorithmChangeComboBox.Name = "AlgorithmChangeComboBox";
            this.AlgorithmChangeComboBox.Size = new System.Drawing.Size(157, 23);
            this.AlgorithmChangeComboBox.TabIndex = 4;
            this.AlgorithmChangeComboBox.SelectedIndexChanged += new System.EventHandler(this.AlgorithmChangeComboBox_SelectedIndexChanged);
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.Size = new System.Drawing.Size(535, 426);
            this.LogRichTextBox.TabIndex = 3;
            this.LogRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LogRichTextBox);
            this.Controls.Add(this.MainGroupBox);
            this.Name = "MainForm";
            this.Text = "Problem szeregowania zadań";
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.LoadDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox MainGroupBox;
        private RichTextBox LogRichTextBox;
        private GroupBox LoadDataGroupBox;
        private ListBox FoundDataFilesListBox;
        private Button SearchFolderButton;
        private Label AlgorithmLabel;
        private ComboBox AlgorithmChangeComboBox;
        private Button ParametersButton;
        private Button LoggingLevelButton;
        private Button InstructionsButton;
        private Button SolveButton;
        private Button SavaData;
        private Button GenerateDataButton;
    }
}

