namespace ph_GUI_V1
{
    partial class GenerateLabelForm
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
            this.components = new System.ComponentModel.Container();
            this.ScanSNLbl = new System.Windows.Forms.Label();
            this.ScannedSNBox = new System.Windows.Forms.TextBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.ReturnBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.ScanSNTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ScanSNLbl
            // 
            this.ScanSNLbl.AutoSize = true;
            this.ScanSNLbl.Location = new System.Drawing.Point(35, 27);
            this.ScanSNLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ScanSNLbl.Name = "ScanSNLbl";
            this.ScanSNLbl.Size = new System.Drawing.Size(101, 13);
            this.ScanSNLbl.TabIndex = 0;
            this.ScanSNLbl.Text = "Scan Serial Number";
            // 
            // ScannedSNBox
            // 
            this.ScannedSNBox.Location = new System.Drawing.Point(38, 42);
            this.ScannedSNBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ScannedSNBox.Name = "ScannedSNBox";
            this.ScannedSNBox.Size = new System.Drawing.Size(182, 20);
            this.ScannedSNBox.TabIndex = 1;
            this.ScannedSNBox.TextChanged += new System.EventHandler(this.ScannedSNBox_TextChanged);
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(38, 80);
            this.GenerateBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(119, 49);
            this.GenerateBtn.TabIndex = 2;
            this.GenerateBtn.Text = "Generate Label";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // ReturnBtn
            // 
            this.ReturnBtn.Location = new System.Drawing.Point(38, 197);
            this.ReturnBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ReturnBtn.Name = "ReturnBtn";
            this.ReturnBtn.Size = new System.Drawing.Size(62, 30);
            this.ReturnBtn.TabIndex = 3;
            this.ReturnBtn.Text = "Return";
            this.ReturnBtn.UseVisualStyleBackColor = true;
            this.ReturnBtn.Click += new System.EventHandler(this.ReturnBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(128, 197);
            this.ExitBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(69, 30);
            this.ExitBtn.TabIndex = 4;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = true;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // ScanSNTimer
            // 
            this.ScanSNTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GenerateLabelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 246);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ReturnBtn);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.ScannedSNBox);
            this.Controls.Add(this.ScanSNLbl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GenerateLabelForm";
            this.Text = "Generate Label";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ScanSNLbl;
        private System.Windows.Forms.TextBox ScannedSNBox;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Button ReturnBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Timer ScanSNTimer;
    }
}