namespace FormsApp.Dialogs
{
    partial class GenerateDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GenerateOptionsInfoLabel = new System.Windows.Forms.Label();
            this.JobsCountLabel = new System.Windows.Forms.Label();
            this.MaxTermLabel = new System.Windows.Forms.Label();
            this.JobsCountTextBox = new System.Windows.Forms.TextBox();
            this.MaxTermTextBox = new System.Windows.Forms.TextBox();
            this.MaxWeightTextBox = new System.Windows.Forms.TextBox();
            this.MaxWeightLabel = new System.Windows.Forms.Label();
            this.MaxTimeTextBox = new System.Windows.Forms.TextBox();
            this.MaxTimeLabel = new System.Windows.Forms.Label();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GenerateOptionsInfoLabel
            // 
            this.GenerateOptionsInfoLabel.AutoSize = true;
            this.GenerateOptionsInfoLabel.Location = new System.Drawing.Point(12, 9);
            this.GenerateOptionsInfoLabel.Name = "GenerateOptionsInfoLabel";
            this.GenerateOptionsInfoLabel.Size = new System.Drawing.Size(154, 15);
            this.GenerateOptionsInfoLabel.TabIndex = 0;
            this.GenerateOptionsInfoLabel.Text = "Opcje generowania danych:";
            // 
            // JobsCountLabel
            // 
            this.JobsCountLabel.AutoSize = true;
            this.JobsCountLabel.Location = new System.Drawing.Point(12, 50);
            this.JobsCountLabel.Name = "JobsCountLabel";
            this.JobsCountLabel.Size = new System.Drawing.Size(68, 15);
            this.JobsCountLabel.TabIndex = 0;
            this.JobsCountLabel.Text = "Ilość zadań:";
            // 
            // MaxTermLabel
            // 
            this.MaxTermLabel.AutoSize = true;
            this.MaxTermLabel.Location = new System.Drawing.Point(12, 79);
            this.MaxTermLabel.Name = "MaxTermLabel";
            this.MaxTermLabel.Size = new System.Drawing.Size(79, 15);
            this.MaxTermLabel.TabIndex = 0;
            this.MaxTermLabel.Text = "Maks. termin:";
            // 
            // JobsCountTextBox
            // 
            this.JobsCountTextBox.Location = new System.Drawing.Point(127, 47);
            this.JobsCountTextBox.Name = "JobsCountTextBox";
            this.JobsCountTextBox.Size = new System.Drawing.Size(100, 23);
            this.JobsCountTextBox.TabIndex = 1;
            this.JobsCountTextBox.Text = "10";
            // 
            // MaxTermTextBox
            // 
            this.MaxTermTextBox.Location = new System.Drawing.Point(127, 76);
            this.MaxTermTextBox.Name = "MaxTermTextBox";
            this.MaxTermTextBox.Size = new System.Drawing.Size(100, 23);
            this.MaxTermTextBox.TabIndex = 1;
            this.MaxTermTextBox.Text = "10";
            // 
            // MaxWeightTextBox
            // 
            this.MaxWeightTextBox.Location = new System.Drawing.Point(127, 105);
            this.MaxWeightTextBox.Name = "MaxWeightTextBox";
            this.MaxWeightTextBox.Size = new System.Drawing.Size(100, 23);
            this.MaxWeightTextBox.TabIndex = 1;
            this.MaxWeightTextBox.Text = "10";
            // 
            // MaxWeightLabel
            // 
            this.MaxWeightLabel.AutoSize = true;
            this.MaxWeightLabel.Location = new System.Drawing.Point(13, 108);
            this.MaxWeightLabel.Name = "MaxWeightLabel";
            this.MaxWeightLabel.Size = new System.Drawing.Size(72, 15);
            this.MaxWeightLabel.TabIndex = 0;
            this.MaxWeightLabel.Text = "Maks. waga:";
            // 
            // MaxTimeTextBox
            // 
            this.MaxTimeTextBox.Location = new System.Drawing.Point(127, 134);
            this.MaxTimeTextBox.Name = "MaxTimeTextBox";
            this.MaxTimeTextBox.Size = new System.Drawing.Size(100, 23);
            this.MaxTimeTextBox.TabIndex = 1;
            this.MaxTimeTextBox.Text = "10";
            // 
            // MaxTimeLabel
            // 
            this.MaxTimeLabel.AutoSize = true;
            this.MaxTimeLabel.Location = new System.Drawing.Point(13, 137);
            this.MaxTimeLabel.Name = "MaxTimeLabel";
            this.MaxTimeLabel.Size = new System.Drawing.Size(108, 15);
            this.MaxTimeLabel.TabIndex = 0;
            this.MaxTimeLabel.Text = "Maks. czas trwania:";
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(25, 179);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 2;
            this.GenerateButton.Text = "Generuj";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // GoBackButton
            // 
            this.GoBackButton.Location = new System.Drawing.Point(139, 179);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(75, 23);
            this.GoBackButton.TabIndex = 2;
            this.GoBackButton.Text = "Anuluj";
            this.GoBackButton.UseVisualStyleBackColor = true;
            // 
            // GenerateDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 224);
            this.Controls.Add(this.GoBackButton);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.MaxWeightTextBox);
            this.Controls.Add(this.JobsCountTextBox);
            this.Controls.Add(this.MaxTermTextBox);
            this.Controls.Add(this.JobsCountLabel);
            this.Controls.Add(this.MaxTimeTextBox);
            this.Controls.Add(this.MaxTimeLabel);
            this.Controls.Add(this.GenerateOptionsInfoLabel);
            this.Controls.Add(this.MaxWeightLabel);
            this.Controls.Add(this.MaxTermLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GenerateDataForm";
            this.Text = "Wygeneruj dane";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GenerateOptionsInfoLabel;
        private System.Windows.Forms.Label JobsCountLabel;
        private System.Windows.Forms.Label MaxTermLabel;
        private System.Windows.Forms.TextBox JobsCountTextBox;
        private System.Windows.Forms.TextBox MaxTermTextBox;
        private System.Windows.Forms.TextBox MaxWeightTextBox;
        private System.Windows.Forms.Label MaxWeightLabel;
        private System.Windows.Forms.TextBox MaxTimeTextBox;
        private System.Windows.Forms.Label MaxTimeLabel;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Button GoBackButton;
    }
}