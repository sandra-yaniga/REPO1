

namespace ph_GUI_V1
{
    public partial class assemFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(assemFields));
            this.label1 = new System.Windows.Forms.Label();
            this.WorkOrderEntryTextBox = new System.Windows.Forms.TextBox();
            this.PNEntryTextBox = new System.Windows.Forms.TextBox();
            this.BadgeNumberEntryTextBox = new System.Windows.Forms.TextBox();
            this.QuantityEntryTextBox = new System.Windows.Forms.TextBox();
            this.LaunchCompEntryPageBtn = new System.Windows.Forms.Button();
            this.WO_Label = new System.Windows.Forms.Label();
            this.PN_Label = new System.Windows.Forms.Label();
            this.Badge_Label = new System.Windows.Forms.Label();
            this.Qty_Label = new System.Windows.Forms.Label();
            this.WOFocusTimer = new System.Windows.Forms.Timer(this.components);
            this.PNFocusTimer = new System.Windows.Forms.Timer(this.components);
            this.BadgeFocusTimer = new System.Windows.Forms.Timer(this.components);
            this.AssyInfoExitBtn = new System.Windows.Forms.Button();
            this.ReturnBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASSEMBLY INFORMATION";
            // 
            // WorkOrderEntryTextBox
            // 
            this.WorkOrderEntryTextBox.AcceptsTab = true;
            this.WorkOrderEntryTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderEntryTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.WorkOrderEntryTextBox.Location = new System.Drawing.Point(47, 86);
            this.WorkOrderEntryTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WorkOrderEntryTextBox.Name = "WorkOrderEntryTextBox";
            this.WorkOrderEntryTextBox.Size = new System.Drawing.Size(231, 23);
            this.WorkOrderEntryTextBox.TabIndex = 1;
            this.WorkOrderEntryTextBox.TabStop = false;
            this.WorkOrderEntryTextBox.Click += new System.EventHandler(this.textBox1_Click);
            this.WorkOrderEntryTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PNEntryTextBox
            // 
            this.PNEntryTextBox.AcceptsTab = true;
            this.PNEntryTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PNEntryTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.PNEntryTextBox.Location = new System.Drawing.Point(47, 135);
            this.PNEntryTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PNEntryTextBox.Name = "PNEntryTextBox";
            this.PNEntryTextBox.Size = new System.Drawing.Size(231, 23);
            this.PNEntryTextBox.TabIndex = 2;
            this.PNEntryTextBox.TabStop = false;
            this.PNEntryTextBox.Click += new System.EventHandler(this.textBox2_Click);
            this.PNEntryTextBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // BadgeNumberEntryTextBox
            // 
            this.BadgeNumberEntryTextBox.AcceptsTab = true;
            this.BadgeNumberEntryTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BadgeNumberEntryTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.BadgeNumberEntryTextBox.Location = new System.Drawing.Point(47, 183);
            this.BadgeNumberEntryTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BadgeNumberEntryTextBox.Name = "BadgeNumberEntryTextBox";
            this.BadgeNumberEntryTextBox.Size = new System.Drawing.Size(231, 23);
            this.BadgeNumberEntryTextBox.TabIndex = 3;
            this.BadgeNumberEntryTextBox.TabStop = false;
            this.BadgeNumberEntryTextBox.Click += new System.EventHandler(this.textBox3_Click);
            this.BadgeNumberEntryTextBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // QuantityEntryTextBox
            // 
            this.QuantityEntryTextBox.AcceptsTab = true;
            this.QuantityEntryTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityEntryTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.QuantityEntryTextBox.Location = new System.Drawing.Point(47, 231);
            this.QuantityEntryTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.QuantityEntryTextBox.Name = "QuantityEntryTextBox";
            this.QuantityEntryTextBox.Size = new System.Drawing.Size(231, 23);
            this.QuantityEntryTextBox.TabIndex = 4;
            this.QuantityEntryTextBox.TabStop = false;
            this.QuantityEntryTextBox.Click += new System.EventHandler(this.textBox4_Click);
            this.QuantityEntryTextBox.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // LaunchCompEntryPageBtn
            // 
            this.LaunchCompEntryPageBtn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchCompEntryPageBtn.Location = new System.Drawing.Point(312, 231);
            this.LaunchCompEntryPageBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LaunchCompEntryPageBtn.Name = "LaunchCompEntryPageBtn";
            this.LaunchCompEntryPageBtn.Size = new System.Drawing.Size(100, 28);
            this.LaunchCompEntryPageBtn.TabIndex = 5;
            this.LaunchCompEntryPageBtn.TabStop = false;
            this.LaunchCompEntryPageBtn.Text = "NEXT";
            this.LaunchCompEntryPageBtn.UseVisualStyleBackColor = true;
            this.LaunchCompEntryPageBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // WO_Label
            // 
            this.WO_Label.AutoSize = true;
            this.WO_Label.Location = new System.Drawing.Point(45, 68);
            this.WO_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WO_Label.Name = "WO_Label";
            this.WO_Label.Size = new System.Drawing.Size(139, 13);
            this.WO_Label.TabIndex = 6;
            this.WO_Label.Text = "Scan a Work Order Number";
            // 
            // PN_Label
            // 
            this.PN_Label.AutoSize = true;
            this.PN_Label.Location = new System.Drawing.Point(45, 117);
            this.PN_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PN_Label.Name = "PN_Label";
            this.PN_Label.Size = new System.Drawing.Size(112, 13);
            this.PN_Label.TabIndex = 7;
            this.PN_Label.Text = "Scan the Part Number";
            // 
            // Badge_Label
            // 
            this.Badge_Label.AutoSize = true;
            this.Badge_Label.Location = new System.Drawing.Point(45, 165);
            this.Badge_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Badge_Label.Name = "Badge_Label";
            this.Badge_Label.Size = new System.Drawing.Size(131, 13);
            this.Badge_Label.TabIndex = 8;
            this.Badge_Label.Text = "Scan Your Badge Number";
            // 
            // Qty_Label
            // 
            this.Qty_Label.AutoSize = true;
            this.Qty_Label.Location = new System.Drawing.Point(45, 213);
            this.Qty_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Qty_Label.Name = "Qty_Label";
            this.Qty_Label.Size = new System.Drawing.Size(92, 13);
            this.Qty_Label.TabIndex = 9;
            this.Qty_Label.Text = "Scan the Quantity";
            // 
            // WOFocusTimer
            // 
            this.WOFocusTimer.Interval = 250;
            this.WOFocusTimer.Tick += new System.EventHandler(this.WOFocusTimer_Tick);
            // 
            // PNFocusTimer
            // 
            this.PNFocusTimer.Interval = 250;
            this.PNFocusTimer.Tick += new System.EventHandler(this.PNFocusTimer_Tick);
            // 
            // BadgeFocusTimer
            // 
            this.BadgeFocusTimer.Tick += new System.EventHandler(this.BadgeFocusTimer_Tick);
            // 
            // AssyInfoExitBtn
            // 
            this.AssyInfoExitBtn.Location = new System.Drawing.Point(341, 55);
            this.AssyInfoExitBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AssyInfoExitBtn.Name = "AssyInfoExitBtn";
            this.AssyInfoExitBtn.Size = new System.Drawing.Size(100, 26);
            this.AssyInfoExitBtn.TabIndex = 10;
            this.AssyInfoExitBtn.Text = "Exit";
            this.AssyInfoExitBtn.UseVisualStyleBackColor = true;
            this.AssyInfoExitBtn.Click += new System.EventHandler(this.AssyInfoExitBtn_Click);
            // 
            // ReturnBtn
            // 
            this.ReturnBtn.Location = new System.Drawing.Point(341, 13);
            this.ReturnBtn.Name = "ReturnBtn";
            this.ReturnBtn.Size = new System.Drawing.Size(100, 23);
            this.ReturnBtn.TabIndex = 11;
            this.ReturnBtn.Text = "Return";
            this.ReturnBtn.UseVisualStyleBackColor = true;
            this.ReturnBtn.Click += new System.EventHandler(this.ReturnBtn_Click);
            // 
            // assemFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 305);
            this.Controls.Add(this.ReturnBtn);
            this.Controls.Add(this.AssyInfoExitBtn);
            this.Controls.Add(this.Qty_Label);
            this.Controls.Add(this.Badge_Label);
            this.Controls.Add(this.PN_Label);
            this.Controls.Add(this.WO_Label);
            this.Controls.Add(this.LaunchCompEntryPageBtn);
            this.Controls.Add(this.QuantityEntryTextBox);
            this.Controls.Add(this.BadgeNumberEntryTextBox);
            this.Controls.Add(this.PNEntryTextBox);
            this.Controls.Add(this.WorkOrderEntryTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "assemFields";
            this.Text = "Assembly Fields";
            this.Load += new System.EventHandler(this.assemFields_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WorkOrderEntryTextBox;
        private System.Windows.Forms.TextBox PNEntryTextBox;
        private System.Windows.Forms.TextBox BadgeNumberEntryTextBox;
        private System.Windows.Forms.TextBox QuantityEntryTextBox;
        private System.Windows.Forms.Button LaunchCompEntryPageBtn;
        private System.Windows.Forms.Label WO_Label;
        private System.Windows.Forms.Label PN_Label;
        private System.Windows.Forms.Label Badge_Label;
        private System.Windows.Forms.Label Qty_Label;
        private System.Windows.Forms.Timer WOFocusTimer;
        private System.Windows.Forms.Timer PNFocusTimer;
        private System.Windows.Forms.Timer BadgeFocusTimer;
        private System.Windows.Forms.Button AssyInfoExitBtn;
        private System.Windows.Forms.Button ReturnBtn;
    }
}