using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace ph_GUI_V1
{
    public partial class assemFields : Form
    {
        // This needs to be changed in testForm.cs and assemFields.cs
        public static string SqlOrAccess = "ACCESS";


        //Connection to SQL database
        public static SqlConnection dbConn;

        //Connection to Access database
        public static OleDbConnection objConn;

        //int clickCount = 0;


        //bool to display testForm or not
        public static bool display = true;

        //error if we read in an assembly that has too many parts
        public static bool sizeError = false;

        //var to store passed input from main. Will be WO or SN to pick an assembly
        //String input = assemFields.pn.Trim(' ');

        //   String step = assemFields.stepNum.ToString().Trim(' ');
        // String ofSteps = assemFields.q.Trim(' ');

        //if the assembly has a test, will have to display a test button
        //bool hasTest = false;
        //public strings to store serial number, work order and badge number, simplifies from tb(N)Text
        //public static String sn = assemFields.snums;
        //public static String wo = assemFields.wo;
        //public static String bn = assemFields.bn;

        //array to store the assembly data we are using. shouldn't have more than 100 entries, but can expand
        String[] workSet = new String[100];

        //stores labels so that we can check if they're all green and admit to test program
        List<Label> labels = new List<Label>();

        //counts number of non-null slots in array so we know an accurate measurement of length
        //int count = 0;


        public assemFields()
        {

            InitializeComponent();
            this.WorkOrderEntryTextBox.Focus();
            EngSpanLabels();
            

        }

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                label1.Text = MyGlobals.AssemblyInfoStrEng;

                WO_Label.Text = MyGlobals.ScanWorkOrderNumStrEng;

                PN_Label.Text = MyGlobals.ScanPartNumStrEng;

                Badge_Label.Text = MyGlobals.ScanBadgeNumStrEng;

                Qty_Label.Text = MyGlobals.ScanQtyStrEng;

                LaunchCompEntryPageBtn.Text = MyGlobals.NextStrEng;

                AssyInfoExitBtn.Text = MyGlobals.ExitStrEng;

                ReturnBtn.Text = MyGlobals.ReturnStrEng;

            }
            else
            {
                label1.Text = MyGlobals.AssemblyInfoStrSpn;

                WO_Label.Text = MyGlobals.ScanWorkOrderNumStrSpn;

                PN_Label.Text = MyGlobals.ScanPartNumStrSpn;

                Badge_Label.Text = MyGlobals.ScanBadgeNumStrSpn;

                Qty_Label.Text = MyGlobals.ScanQtyStrSpn;

                LaunchCompEntryPageBtn.Text = MyGlobals.NextStrSpn;

                AssyInfoExitBtn.Text = MyGlobals.ExitStrSpn;

                ReturnBtn.Text = MyGlobals.ReturnStrSpn;
            }

            label1.Refresh();

            WO_Label.Refresh();

            PN_Label.Refresh();

            Badge_Label.Refresh();

            Qty_Label.Refresh();

            LaunchCompEntryPageBtn.Refresh();

            

        }
        /*
        //display and error message when we cant display testForm 
            if (!testForm.display)
            {
                textBox1.Text = "Cannot find assembly. Please try again.";
                //reset for next call to testForm
                testForm.display = true;
                //close db connection
                testForm.dbConn.Close();
            }
            //display error when cannot handle size of assembly 
            if (testForm.sizeError)
            {
                textBox1.Text = "This program cannot handle an assembly that large.";
                //reset if the user wants to run a different, valid assembly
                testForm.sizeError = false;
                //close db connection
                testForm.dbConn.Close();
            }
            */

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            WOFocusTimer.Start();
            //WorkOrderEntryTextBox.ForeColor = Color.Black;
            //wo = WorkOrderEntryTextBox.Text;
            //PNEntryTextBox.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            PNFocusTimer.Start();
            //PNEntryTextBox.ForeColor = Color.Black;
            //pn = PNEntryTextBox.Text;
            //BadgeNumberEntryTextBox.Focus();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            BadgeFocusTimer.Start();
            //BadgeNumberEntryTextBox.ForeColor = Color.Black;
            //bn = BadgeNumberEntryTextBox.Text;
            //QuantityEntryTextBox.Focus();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            QuantityEntryTextBox.ForeColor = Color.Black;
            q = QuantityEntryTextBox.Text;
            LaunchCompEntryPageBtn.Focus();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            // WorkOrderEntryTextBox.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            //PNEntryTextBox.Clear();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            //BadgeNumberEntryTextBox.Clear();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            //QuantityEntryTextBox.Clear();
        }

        private void textBoxSN_Click(object sender, System.EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //txt.Clear();

        }

        private void textBoxSN_TextChanged(object sender, System.EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.ForeColor = Color.Black;

        }







        public static int stepNum;
        public static String wo;
        public static String pn;
        public static String bn;
        public static String snums;
        public static String q;
        private static int quantity;
        public List<String> sns = new List<string>();
        List<TextBox> tbs = new List<TextBox>();



        private void button1_Click(object sender, EventArgs e)
        {
            // Continue button just brings up "TestForm"


            stepNum = 1;
            //foreach (TextBox t in tbs)
            //{
            //    sns.Add(t.Text);
            //}
            wo = WorkOrderEntryTextBox.Text;
            pn = PNEntryTextBox.Text;
            bn = BadgeNumberEntryTextBox.Text;


            this.Hide();
            testForm f1 = new testForm();
            f1.ShowDialog();
            this.Close();

            //for (int j = 0; j < Convert.ToInt32(QuantityEntryTextBox.Text); j++)
            //{
            //    if (!testForm.display)
            //    {
            //        wo = null;
            //        pn = null;
            //        bn = null;
            //        q = null;
            //        quantity = 0;
            //        //assemFields fields = new assemFields();
            //        //fields.Closed += (s, args) => this.Close();
            //        //fields.Show();
            //        //this.Hide();
            //        testForm.display = true;
            //        WorkOrderEntryTextBox.Dispose();
            //        PNEntryTextBox.Dispose();
            //        BadgeNumberEntryTextBox.Dispose();
            //        QuantityEntryTextBox.Dispose();


            //        InitializeComponent();

            //        foreach (Control c in Controls)
            //        {
            //            if (c is Label && ((Label)c).Name == "errorLabel")
            //            {
            //                c.Dispose();
            //            }
            //        }
            //        foreach (Control c in Controls)
            //        {
            //            for (int i = Controls.Count; i >= 0; i--)
            //            {
            //                if (c is TextBox && ((TextBox)c).Name.StartsWith("textBoxSN"))
            //                {
            //                    c.Dispose();
            //                }
            //            }
            //        }
            //        foreach (Control c in Controls)
            //        {
            //            if (c is Button && ((Button)c).Name == "continueButton")
            //            {
            //                c.Dispose();
            //            }
            //        }

            //        return;

            //    }
            //    else
            //    {
            //        //snums = str;
            //        testForm test = new testForm();
            //        test.Closed += (s, args) => this.Close();
            //        test.ShowDialog();
            //        stepNum++;
            //    }
            //}
            ////this.Hide();
            //Close();

            //return;

        }

        private void continueButton_Click(object sender, System.EventArgs e)
        {
  
        }

        private void assemFields_Load(object sender, EventArgs e)
        {

        }

        private void WOFocusTimer_Tick(object sender, EventArgs e)
        {
            WOFocusTimer.Stop();
            WorkOrderEntryTextBox.ForeColor = Color.Black;
            wo = WorkOrderEntryTextBox.Text;
            PNEntryTextBox.Focus();
        }

        private void PNFocusTimer_Tick(object sender, EventArgs e)
        {
            PNFocusTimer.Stop();
            PNEntryTextBox.ForeColor = Color.Black;
            pn = PNEntryTextBox.Text;
            BadgeNumberEntryTextBox.Focus();
        }

        private void BadgeFocusTimer_Tick(object sender, EventArgs e)
        {
            BadgeFocusTimer.Stop();
            BadgeNumberEntryTextBox.ForeColor = Color.Black;
            bn = BadgeNumberEntryTextBox.Text;
            QuantityEntryTextBox.Focus();
        }

        

        private void AssyInfoExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI f1 = new MainGUI();
            f1.ShowDialog();
            this.Close();
        }
    }
}
