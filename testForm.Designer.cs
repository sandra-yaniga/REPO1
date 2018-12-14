namespace ph_GUI_V1
{
    partial class testForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BarcodeEntryTextBox = new System.Windows.Forms.TextBox();
            this.InputTxtBoxTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SNScanBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SNScanBoxTimer = new System.Windows.Forms.Timer(this.components);
            this.BuildPartNumberLbl = new System.Windows.Forms.Label();
            this.BuildWorkOrderLbl = new System.Windows.Forms.Label();
            this.BuildPNTextBox = new System.Windows.Forms.TextBox();
            this.WOBuildTextBox = new System.Windows.Forms.TextBox();
            this.Exit = new System.Windows.Forms.Button();
            this.ReturnBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(32730, 30295);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(176, 38);
            this.button2.TabIndex = 1;
            this.button2.TabStop = false;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(240, 30300);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 34);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // BarcodeEntryTextBox
            // 
            this.BarcodeEntryTextBox.Enabled = false;
            this.BarcodeEntryTextBox.Location = new System.Drawing.Point(805, 120);
            this.BarcodeEntryTextBox.Name = "BarcodeEntryTextBox";
            this.BarcodeEntryTextBox.Size = new System.Drawing.Size(205, 22);
            this.BarcodeEntryTextBox.TabIndex = 3;
            this.BarcodeEntryTextBox.Click += new System.EventHandler(this.SNEntryTextBox_Click);
            this.BarcodeEntryTextBox.TextChanged += new System.EventHandler(this.SNEntryTextBox_TextChanged);
            // 
            // InputTxtBoxTimer
            // 
            this.InputTxtBoxTimer.Interval = 500;
            this.InputTxtBoxTimer.Tick += new System.EventHandler(this.InputTxtBoxTimer_Tick_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(802, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scan Barcode";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // SNScanBox
            // 
            this.SNScanBox.Location = new System.Drawing.Point(805, 52);
            this.SNScanBox.Name = "SNScanBox";
            this.SNScanBox.Size = new System.Drawing.Size(205, 22);
            this.SNScanBox.TabIndex = 5;
            this.SNScanBox.TextChanged += new System.EventHandler(this.SNScanBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(802, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scan Serial Number from Work Order";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SNScanBoxTimer
            // 
            this.SNScanBoxTimer.Tick += new System.EventHandler(this.SNScanBoxTimer_Tick);
            // 
            // BuildPartNumberLbl
            // 
            this.BuildPartNumberLbl.AutoSize = true;
            this.BuildPartNumberLbl.Location = new System.Drawing.Point(802, 166);
            this.BuildPartNumberLbl.Name = "BuildPartNumberLbl";
            this.BuildPartNumberLbl.Size = new System.Drawing.Size(146, 17);
            this.BuildPartNumberLbl.TabIndex = 7;
            this.BuildPartNumberLbl.Text = "Building Part Number:";
            this.BuildPartNumberLbl.Click += new System.EventHandler(this.label3_Click);
            // 
            // BuildWorkOrderLbl
            // 
            this.BuildWorkOrderLbl.AutoSize = true;
            this.BuildWorkOrderLbl.Location = new System.Drawing.Point(802, 238);
            this.BuildWorkOrderLbl.Name = "BuildWorkOrderLbl";
            this.BuildWorkOrderLbl.Size = new System.Drawing.Size(140, 17);
            this.BuildWorkOrderLbl.TabIndex = 8;
            this.BuildWorkOrderLbl.Text = "Building Work Order:";
            // 
            // BuildPNTextBox
            // 
            this.BuildPNTextBox.Enabled = false;
            this.BuildPNTextBox.Location = new System.Drawing.Point(805, 187);
            this.BuildPNTextBox.Name = "BuildPNTextBox";
            this.BuildPNTextBox.Size = new System.Drawing.Size(205, 22);
            this.BuildPNTextBox.TabIndex = 9;
            this.BuildPNTextBox.Text = "(Part Number Empty)";
            // 
            // WOBuildTextBox
            // 
            this.WOBuildTextBox.Enabled = false;
            this.WOBuildTextBox.Location = new System.Drawing.Point(805, 259);
            this.WOBuildTextBox.Name = "WOBuildTextBox";
            this.WOBuildTextBox.Size = new System.Drawing.Size(202, 22);
            this.WOBuildTextBox.TabIndex = 10;
            this.WOBuildTextBox.Text = "(Work Order Empty)";
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.SystemColors.Control;
            this.Exit.Location = new System.Drawing.Point(805, 318);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(107, 29);
            this.Exit.TabIndex = 11;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // ReturnBtn
            // 
            this.ReturnBtn.Location = new System.Drawing.Point(805, 362);
            this.ReturnBtn.Name = "ReturnBtn";
            this.ReturnBtn.Size = new System.Drawing.Size(107, 28);
            this.ReturnBtn.TabIndex = 12;
            this.ReturnBtn.Text = "Return";
            this.ReturnBtn.UseVisualStyleBackColor = true;
            this.ReturnBtn.Click += new System.EventHandler(this.ReturnBtn_Click);
            // 
            // testForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 755);
            this.ControlBox = false;
            this.Controls.Add(this.ReturnBtn);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.WOBuildTextBox);
            this.Controls.Add(this.BuildPNTextBox);
            this.Controls.Add(this.BuildWorkOrderLbl);
            this.Controls.Add(this.BuildPartNumberLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SNScanBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BarcodeEntryTextBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "testForm";
            this.Text = "Test Form";
            this.Load += new System.EventHandler(this.testForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox BarcodeEntryTextBox;
        private System.Windows.Forms.Timer InputTxtBoxTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SNScanBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer SNScanBoxTimer;
        private System.Windows.Forms.Label BuildPartNumberLbl;
        private System.Windows.Forms.Label BuildWorkOrderLbl;
        private System.Windows.Forms.TextBox BuildPNTextBox;
        private System.Windows.Forms.TextBox WOBuildTextBox;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button ReturnBtn;
    }
}