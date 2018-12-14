///////////////////////////////////////////////////////////////////////////////
//                   
// Title:            ph GUI V1
// Files:            Program.cs, MainGUIPage.cs, FPYfields.cs, FTPfields.cs, 
//                   SNfields.cs, reports.cs, reports1.cs, reports2.cs,  
//                   testForm.cs
//
// This File:        reports.cs
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
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ph_GUI_V1
{
    public partial class reports : Form
    {
 
        public reports()
        {
            InitializeComponent();
          
        }
        
        //return button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI mainGUI = new MainGUI();
            mainGUI.Closed += (s, args) => this.Close();
            mainGUI.Show();
        }
        //Converts chart to JPEG and saves to specified location
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.Save(@"C:\Users\Samurad\Pictures\report1.jpg", ImageFormat.Jpeg);
                   
                textBox1.Text = "Success converting to JPEG";
            }
            catch (Exception)
            {
                textBox1.Text = "Error converting to JPEG";
            }
        }
    }
}
