using System;
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
            this.ParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.ParametersFlowLayountPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DetailsTextLogCheckBox = new System.Windows.Forms.CheckBox();
            this.GraphicLogCheckBox = new System.Windows.Forms.CheckBox();
            this.LoadDataGroupBox = new System.Windows.Forms.GroupBox();
            this.FoundDataFilesListBox = new System.Windows.Forms.ListBox();
            this.SearchFolderButton = new System.Windows.Forms.Button();
            this.SaveDataButton = new System.Windows.Forms.Button();
            this.ShowDataButton = new System.Windows.Forms.Button();
            this.GenerateDataButton = new System.Windows.Forms.Button();
            this.InfoGroupBox = new System.Windows.Forms.GroupBox();
            this.BestScoreLabel = new System.Windows.Forms.Label();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.DataLoadedLabel = new System.Windows.Forms.Label();
            this.CountJobsLabel = new System.Windows.Forms.Label();
            this.BestScoreInfoLabel = new System.Windows.Forms.Label();
            this.FileNameInfoLabel = new System.Windows.Forms.Label();
            this.CountJobsInfoLabel = new System.Windows.Forms.Label();
            this.DataLoadedInfoLabel = new System.Windows.Forms.Label();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.InstructionsButton = new System.Windows.Forms.Button();
            this.SolveButton = new System.Windows.Forms.Button();
            this.AlgorithmLabel = new System.Windows.Forms.Label();
            this.AlgorithmChangeComboBox = new System.Windows.Forms.ComboBox();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.MainGroupBox.SuspendLayout();
            this.ParametersGroupBox.SuspendLayout();
            this.ParametersFlowLayountPanel.SuspendLayout();
            this.LoadDataGroupBox.SuspendLayout();
            this.InfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.ParametersGroupBox);
            this.MainGroupBox.Controls.Add(this.LoadDataGroupBox);
            this.MainGroupBox.Controls.Add(this.InfoGroupBox);
            this.MainGroupBox.Controls.Add(this.ClearLogButton);
            this.MainGroupBox.Controls.Add(this.InstructionsButton);
            this.MainGroupBox.Controls.Add(this.SolveButton);
            this.MainGroupBox.Controls.Add(this.AlgorithmLabel);
            this.MainGroupBox.Controls.Add(this.AlgorithmChangeComboBox);
            this.MainGroupBox.Location = new System.Drawing.Point(12, 12);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(235, 607);
            this.MainGroupBox.TabIndex = 2;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Menu";
            // 
            // ParametersGroupBox
            // 
            this.ParametersGroupBox.Controls.Add(this.ParametersFlowLayountPanel);
            this.ParametersGroupBox.Location = new System.Drawing.Point(6, 140);
            this.ParametersGroupBox.Name = "ParametersGroupBox";
            this.ParametersGroupBox.Size = new System.Drawing.Size(223, 153);
            this.ParametersGroupBox.TabIndex = 15;
            this.ParametersGroupBox.TabStop = false;
            this.ParametersGroupBox.Text = "Parametry";
            // 
            // ParametersFlowLayountPanel
            // 
            this.ParametersFlowLayountPanel.Controls.Add(this.DetailsTextLogCheckBox);
            this.ParametersFlowLayountPanel.Controls.Add(this.GraphicLogCheckBox);
            this.ParametersFlowLayountPanel.Location = new System.Drawing.Point(10, 22);
            this.ParametersFlowLayountPanel.Name = "ParametersFlowLayountPanel";
            this.ParametersFlowLayountPanel.Size = new System.Drawing.Size(207, 125);
            this.ParametersFlowLayountPanel.TabIndex = 0;
            // 
            // DetailsTextLogCheckBox
            // 
            this.DetailsTextLogCheckBox.AutoSize = true;
            this.DetailsTextLogCheckBox.Location = new System.Drawing.Point(3, 3);
            this.DetailsTextLogCheckBox.Name = "DetailsTextLogCheckBox";
            this.DetailsTextLogCheckBox.Size = new System.Drawing.Size(109, 19);
            this.DetailsTextLogCheckBox.TabIndex = 0;
            this.DetailsTextLogCheckBox.Text = "Loguj szczegóły";
            this.DetailsTextLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // GraphicLogCheckBox
            // 
            this.GraphicLogCheckBox.AutoSize = true;
            this.GraphicLogCheckBox.Location = new System.Drawing.Point(3, 28);
            this.GraphicLogCheckBox.Name = "GraphicLogCheckBox";
            this.GraphicLogCheckBox.Size = new System.Drawing.Size(154, 19);
            this.GraphicLogCheckBox.TabIndex = 1;
            this.GraphicLogCheckBox.Text = "Pokaż zadania graficznie";
            this.GraphicLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoadDataGroupBox
            // 
            this.LoadDataGroupBox.Controls.Add(this.FoundDataFilesListBox);
            this.LoadDataGroupBox.Controls.Add(this.SearchFolderButton);
            this.LoadDataGroupBox.Controls.Add(this.SaveDataButton);
            this.LoadDataGroupBox.Controls.Add(this.ShowDataButton);
            this.LoadDataGroupBox.Controls.Add(this.GenerateDataButton);
            this.LoadDataGroupBox.Location = new System.Drawing.Point(6, 299);
            this.LoadDataGroupBox.Name = "LoadDataGroupBox";
            this.LoadDataGroupBox.Size = new System.Drawing.Size(223, 206);
            this.LoadDataGroupBox.TabIndex = 2;
            this.LoadDataGroupBox.TabStop = false;
            this.LoadDataGroupBox.Text = "Dane";
            // 
            // FoundDataFilesListBox
            // 
            this.FoundDataFilesListBox.FormattingEnabled = true;
            this.FoundDataFilesListBox.ItemHeight = 15;
            this.FoundDataFilesListBox.Location = new System.Drawing.Point(10, 87);
            this.FoundDataFilesListBox.Name = "FoundDataFilesListBox";
            this.FoundDataFilesListBox.Size = new System.Drawing.Size(207, 109);
            this.FoundDataFilesListBox.TabIndex = 1;
            this.FoundDataFilesListBox.SelectedIndexChanged += new System.EventHandler(this.FoundDataFilesListBox_SelectedIndexChanged);
            // 
            // SearchFolderButton
            // 
            this.SearchFolderButton.Location = new System.Drawing.Point(10, 55);
            this.SearchFolderButton.Name = "SearchFolderButton";
            this.SearchFolderButton.Size = new System.Drawing.Size(99, 27);
            this.SearchFolderButton.TabIndex = 0;
            this.SearchFolderButton.Text = "Znajdź";
            this.SearchFolderButton.UseVisualStyleBackColor = true;
            this.SearchFolderButton.Click += new System.EventHandler(this.SearchFolderButton_Click);
            // 
            // SaveDataButton
            // 
            this.SaveDataButton.Location = new System.Drawing.Point(115, 55);
            this.SaveDataButton.Name = "SaveData";
            this.SaveDataButton.Size = new System.Drawing.Size(102, 27);
            this.SaveDataButton.TabIndex = 6;
            this.SaveDataButton.Text = "Zapisz";
            this.SaveDataButton.UseVisualStyleBackColor = true;
            this.SaveDataButton.Click += new System.EventHandler(this.SaveData_Click);
            // 
            // ShowDataButton
            // 
            this.ShowDataButton.Location = new System.Drawing.Point(10, 22);
            this.ShowDataButton.Name = "ShowDataButton";
            this.ShowDataButton.Size = new System.Drawing.Size(99, 27);
            this.ShowDataButton.TabIndex = 6;
            this.ShowDataButton.Text = "Wyświetl dane";
            this.ShowDataButton.UseVisualStyleBackColor = true;
            this.ShowDataButton.Click += new System.EventHandler(this.ShowDataButton_Click);
            // 
            // GenerateDataButton
            // 
            this.GenerateDataButton.Location = new System.Drawing.Point(115, 22);
            this.GenerateDataButton.Name = "GenerateDataButton";
            this.GenerateDataButton.Size = new System.Drawing.Size(102, 27);
            this.GenerateDataButton.TabIndex = 6;
            this.GenerateDataButton.Text = "Wygeneruj";
            this.GenerateDataButton.UseVisualStyleBackColor = true;
            this.GenerateDataButton.Click += new System.EventHandler(this.GenerateDataButton_Click);
            // 
            // InfoGroupBox
            // 
            this.InfoGroupBox.Controls.Add(this.BestScoreLabel);
            this.InfoGroupBox.Controls.Add(this.FileNameLabel);
            this.InfoGroupBox.Controls.Add(this.DataLoadedLabel);
            this.InfoGroupBox.Controls.Add(this.CountJobsLabel);
            this.InfoGroupBox.Controls.Add(this.BestScoreInfoLabel);
            this.InfoGroupBox.Controls.Add(this.FileNameInfoLabel);
            this.InfoGroupBox.Controls.Add(this.CountJobsInfoLabel);
            this.InfoGroupBox.Controls.Add(this.DataLoadedInfoLabel);
            this.InfoGroupBox.Location = new System.Drawing.Point(6, 511);
            this.InfoGroupBox.Name = "InfoGroupBox";
            this.InfoGroupBox.Size = new System.Drawing.Size(223, 90);
            this.InfoGroupBox.TabIndex = 14;
            this.InfoGroupBox.TabStop = false;
            this.InfoGroupBox.Text = "Info";
            // 
            // BestScoreLabel
            // 
            this.BestScoreLabel.AutoSize = true;
            this.BestScoreLabel.Location = new System.Drawing.Point(76, 64);
            this.BestScoreLabel.Name = "BestScoreLabel";
            this.BestScoreLabel.Size = new System.Drawing.Size(12, 15);
            this.BestScoreLabel.TabIndex = 12;
            this.BestScoreLabel.Text = "-";
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(76, 49);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(12, 15);
            this.FileNameLabel.TabIndex = 13;
            this.FileNameLabel.Text = "-";
            // 
            // DataLoadedLabel
            // 
            this.DataLoadedLabel.AutoSize = true;
            this.DataLoadedLabel.ForeColor = System.Drawing.Color.Red;
            this.DataLoadedLabel.Location = new System.Drawing.Point(75, 19);
            this.DataLoadedLabel.Name = "DataLoadedLabel";
            this.DataLoadedLabel.Size = new System.Drawing.Size(87, 15);
            this.DataLoadedLabel.TabIndex = 8;
            this.DataLoadedLabel.Text = "Niezaładowane";
            // 
            // CountJobsLabel
            // 
            this.CountJobsLabel.AutoSize = true;
            this.CountJobsLabel.Location = new System.Drawing.Point(75, 34);
            this.CountJobsLabel.Name = "CountJobsLabel";
            this.CountJobsLabel.Size = new System.Drawing.Size(13, 15);
            this.CountJobsLabel.TabIndex = 10;
            this.CountJobsLabel.Text = "0";
            // 
            // BestScoreInfoLabel
            // 
            this.BestScoreInfoLabel.AutoSize = true;
            this.BestScoreInfoLabel.Location = new System.Drawing.Point(6, 64);
            this.BestScoreInfoLabel.Name = "BestScoreInfoLabel";
            this.BestScoreInfoLabel.Size = new System.Drawing.Size(60, 15);
            this.BestScoreInfoLabel.TabIndex = 11;
            this.BestScoreInfoLabel.Text = "Najlepszy:";
            // 
            // FileNameInfoLabel
            // 
            this.FileNameInfoLabel.AutoSize = true;
            this.FileNameInfoLabel.Location = new System.Drawing.Point(6, 49);
            this.FileNameInfoLabel.Name = "FileNameInfoLabel";
            this.FileNameInfoLabel.Size = new System.Drawing.Size(29, 15);
            this.FileNameInfoLabel.TabIndex = 13;
            this.FileNameInfoLabel.Text = "Plik:";
            // 
            // CountJobsInfoLabel
            // 
            this.CountJobsInfoLabel.AutoSize = true;
            this.CountJobsInfoLabel.Location = new System.Drawing.Point(6, 34);
            this.CountJobsInfoLabel.Name = "CountJobsInfoLabel";
            this.CountJobsInfoLabel.Size = new System.Drawing.Size(68, 15);
            this.CountJobsInfoLabel.TabIndex = 9;
            this.CountJobsInfoLabel.Text = "Ilość zadań:";
            // 
            // DataLoadedInfoLabel
            // 
            this.DataLoadedInfoLabel.AutoSize = true;
            this.DataLoadedInfoLabel.Location = new System.Drawing.Point(6, 19);
            this.DataLoadedInfoLabel.Name = "DataLoadedInfoLabel";
            this.DataLoadedInfoLabel.Size = new System.Drawing.Size(37, 15);
            this.DataLoadedInfoLabel.TabIndex = 7;
            this.DataLoadedInfoLabel.Text = "Dane:";
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(119, 95);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(110, 38);
            this.ClearLogButton.TabIndex = 6;
            this.ClearLogButton.Text = "Wyczyść log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // InstructionsButton
            // 
            this.InstructionsButton.Location = new System.Drawing.Point(6, 95);
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
            this.SolveButton.Text = "START";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
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
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.HideSelection = false;
            this.LogRichTextBox.Location = new System.Drawing.Point(253, 12);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.Size = new System.Drawing.Size(775, 607);
            this.LogRichTextBox.TabIndex = 3;
            this.LogRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 631);
            this.Controls.Add(this.LogRichTextBox);
            this.Controls.Add(this.MainGroupBox);
            this.Name = "MainForm";
            this.Text = "Problem szeregowania zadań";
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ParametersGroupBox.ResumeLayout(false);
            this.ParametersFlowLayountPanel.ResumeLayout(false);
            this.ParametersFlowLayountPanel.PerformLayout();
            this.LoadDataGroupBox.ResumeLayout(false);
            this.InfoGroupBox.ResumeLayout(false);
            this.InfoGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private void LogRichTextBox_TextChanged(object sender, EventArgs e)
        {
            LogRichTextBox.SelectionStart = LogRichTextBox.Text.Length;
            LogRichTextBox.ScrollToCaret();
        }

        #endregion


        private GroupBox MainGroupBox;
        private RichTextBox LogRichTextBox;
        private GroupBox LoadDataGroupBox;
        private ListBox FoundDataFilesListBox;
        private Button SearchFolderButton;
        private Label AlgorithmLabel;
        private ComboBox AlgorithmChangeComboBox;
        private Button ClearLogButton;
        private Button InstructionsButton;
        private Button SolveButton;
        private Button SaveDataButton;
        private Button GenerateDataButton;
        private Button ShowDataButton;
        private Label BestScoreLabel;
        private Label BestScoreInfoLabel;
        private Label CountJobsLabel;
        private Label CountJobsInfoLabel;
        private Label DataLoadedLabel;
        private Label DataLoadedInfoLabel;
        private Label FileNameLabel;
        private Label FileNameInfoLabel;
        private GroupBox InfoGroupBox;
        private GroupBox ParametersGroupBox;
        private FlowLayoutPanel ParametersFlowLayountPanel;
        private CheckBox DetailsTextLogCheckBox;
        private CheckBox GraphicLogCheckBox;
    }
}

