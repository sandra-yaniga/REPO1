///////////////////////////////////////////////////////////////////////////////
//                   
// Title:            ph GUI V1
// Files:            Program.cs, MainGUIPage.cs, FPYfields.cs, FTPfields.cs, 
//                   SNfields.cs, reports.cs, reports1.cs, reports2.cs,  
//                   testForm.cs
//
// This File:        FPYfields.cs
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
    public partial class FPYfields : Form
    {
        public FPYfields()
        {
            InitializeComponent();
        }


        //button clicks launch First Pass Yield reports. Will pass unique data 
        //to graph 
    
        private void button7_Click(object sender, EventArgs e)
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.textBox3.ForeColor = Color.Black;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.ForeColor = Color.Black;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.textBox4.ForeColor = Color.Black;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            this.textBox5.ForeColor = Color.Black;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            this.textBox6.ForeColor = Color.Black;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            reports rep = new reports();
            rep.Closed += (s, args) => this.Close();
            rep.Show();
        }
    }
}
