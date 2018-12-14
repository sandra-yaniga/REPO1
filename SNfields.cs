///////////////////////////////////////////////////////////////////////////////
//                   
// Title:            ph GUI V1
// Files:            Program.cs, MainGUIPage.cs, FPYfields.cs, FTPfields.cs, 
//                   SNfields.cs, reports.cs, reports1.cs, reports2.cs,  
//                   testForm.cs
//
// This File:        SNfields.cs
//
// Author:           Sam Radovich and Sandy Yaniga
// Email:            samuel.radovich@emerson.com
// Company:          Emerson Electric, Rosemount
// 
////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ph_GUI_V1
{
    public partial class SNfields : Form
    {
        public SNfields()
        {
            InitializeComponent();
        }

        //intermediate step to pass SN to report. report will generate idea about specific report
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            reports2 rep = new reports2();
            rep.Closed += (s, args) => this.Close();
            rep.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI mainGUI = new MainGUI();
            mainGUI.Closed += (s, args) => this.Close();
            mainGUI.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
