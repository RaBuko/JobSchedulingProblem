namespace FormsApp.Dialogs
{
    partial class ParametersForm
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
            this.ParametersInfoMainLabel = new System.Windows.Forms.Label();
            this.GoBackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ParametersInfoMainLabel
            // 
            this.ParametersInfoMainLabel.AutoSize = true;
            this.ParametersInfoMainLabel.Location = new System.Drawing.Point(10, 9);
            this.ParametersInfoMainLabel.Name = "ParametersInfoMainLabel";
            this.ParametersInfoMainLabel.Size = new System.Drawing.Size(193, 15);
            this.ParametersInfoMainLabel.TabIndex = 0;
            this.ParametersInfoMainLabel.Text = "Uzupełnij parametry dla algorytmu:";
            // 
            // GoBackButton
            // 
            this.GoBackButton.Location = new System.Drawing.Point(10, 98);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Size = new System.Drawing.Size(75, 23);
            this.GoBackButton.TabIndex = 2;
            this.GoBackButton.Text = "Powrót";
            this.GoBackButton.UseVisualStyleBackColor = true;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // ParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 133);
            this.Controls.Add(this.GoBackButton);
            this.Controls.Add(this.ParametersInfoMainLabel);
            this.Name = "ParametersForm";
            this.Text = "ParametersForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ParametersInfoMainLabel;
        private System.Windows.Forms.Button GoBackButton;
    }
}