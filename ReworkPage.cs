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

namespace ph_GUI_V1
{
    public partial class ReworkPage : Form
    {

        //Connection to SQL database
        public static SqlConnection dbConn;

        //bools for do rework on selected PN
        bool RwkPN1  = false;
        bool RwkPN2  = false;
        bool RwkPN3  = false;
        bool RwkPN4  = false;
        bool RwkPN5  = false;
        bool RwkPN6  = false;
        bool RwkPN7  = false;
        bool RwkPN8  = false;
        bool RwkPN9  = false;
        bool RwkPN10 = false;
        bool RwkPN11 = false;
        bool RwkPN12 = false;
        bool RwkPN13 = false;
        bool RwkPN14 = false;
        bool RwkPN15 = false;
        bool RwkPN16 = false;
        bool RwkPN17 = false;
        bool RwkPN18 = false;
        bool RwkPN19 = false;
        bool RwkPN20 = false;
        bool RwkPN21 = false;
        bool RwkPN22 = false;
        bool RwkPN23 = false;
        bool RwkPN24 = false;
        bool RwkPN25 = false;
        bool RwkPN26 = false;
        bool RwkPN27 = false;
        bool RwkPN28 = false;
        bool RwkPN29 = false;
        bool RwkPN30 = false;

        // Used in SQL queries, just to make reading easier
        string delimiterchar = "','";

        public static int ControlCounter = 1;

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                ScanSNTitleLbl.Text = MyGlobals.ScanSNStrEng;
                SubassyPNTitleLbl.Text = MyGlobals.SubassyPNStrEng;
                ComponentPNTitleLbl.Text = MyGlobals.CompPNStrEng;
                RwkSelectLbl.Text = MyGlobals.ReworkStrQuesEng;
                ComponentPNTitleLbl2.Text = MyGlobals.CompPNStrEng;
                RwkSelectLbl2.Text = MyGlobals.ReworkStrQuesEng;
                SubmitRwkBtn.Text = MyGlobals.SubmitRwkStrEng;
                RwkPgReturnBtn.Text = MyGlobals.ReturnStrEng;
                RwkPgExitBtn.Text = MyGlobals.ExitStrEng;
            }
            else
            {
                ScanSNTitleLbl.Text = MyGlobals.ScanSNStrSpn;
                SubassyPNTitleLbl.Text = MyGlobals.SubassyPNStrSpn;
                ComponentPNTitleLbl.Text = MyGlobals.CompPNStrSpn;
                SubassyPNTitleLbl.Text = MyGlobals.SubassyPNStrSpn;
                RwkSelectLbl.Text = MyGlobals.ReworkStrQuesSpn;
                ComponentPNTitleLbl2.Text = MyGlobals.CompPNStrSpn;
                RwkSelectLbl2.Text = MyGlobals.ReworkStrQuesSpn;
                SubmitRwkBtn.Text = MyGlobals.SubmitRwkStrSpn;
                RwkPgReturnBtn.Text = MyGlobals.ReturnStrSpn;
                RwkPgExitBtn.Text = MyGlobals.ExitStrSpn;
            }
        }

        public ReworkPage()
        {
            InitializeComponent();
            EngSpanLabels();
        }


        public static string[] ParseBarcode(string scannedCode)
        {

            // This function takes in a scanned barcode and returns a string array
            // The returned string array is always in the order {PN, SN, LOT}
            // Not all barcodes always have PN, SN, and LOT
            // Barcodes are not guaranteed to have PN, SN, LOT in the same order

            string PN = "NULL";
            string SN = "NULL";
            string LOT = "NULL";

            int PNlength = 3; // number of characters in "PN="
            int SNlength = 3; // number of characters in "SN="
            int LOTlength = 4; // number of characters in "LOT="

            // Scanned in barcode is delimited by ;
            // Split the scanned in string into an array by delimiter
            string[] splitStrArray = scannedCode.Split(';');

            // Check each string in the new sring array
            foreach (string str in splitStrArray)
            {
                // Check for "PN="
                if (str.IndexOf("PN=") != -1)
                {
                    // If "PN=" is found, grab the remaining string in the string array
                    PN = str.Substring(PNlength, str.Length - PNlength);
                    Console.WriteLine("PN: " + PN);
                }

                // Check for "SN="
                if (str.IndexOf("SN=") != -1)
                {
                    // If "SN=" is found, grab the remaining string in the string array
                    SN = str.Substring(SNlength, str.Length - SNlength);
                    Console.WriteLine("SN: " + SN);
                }

                // Check for "LOT="
                if (str.IndexOf("LOT=") != -1)
                {
                    // If "SN=" is found, grab the remaining string in the string array
                    LOT = str.Substring(LOTlength, str.Length - LOTlength);
                    Console.WriteLine("LOT: " + LOT);
                }

            }

            string[] parsedStrArray = { PN, SN, LOT };

            //MessageBox.Show("Done parsing barcode");

            return parsedStrArray;

        }

        public static void connect()
        {
            //Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\Samurad\\Desktop\\C# Programs\\ph GUI V1\\demopHDatabase.mdb;Persist Security Info=False
            string connectionString = "Server = USMTNPMDEAIDB05; Database = SUB_DB_PRACTICE; User Id = sub_db_writer; Password = s&b_wr*t3";
            try
            {
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
            }

            catch (Exception)
            {
                MessageBox.Show("Cannot connect to database x");
            }
        }

        public static string read(string serialNumber)
        {
            string PN = "";
            string queryStr = "SELECT [Part_Number_To_Build] FROM Subassembly_Info WHERE Subassembly_SN = " + "'" +  serialNumber + "'";
            SqlCommand readComm = new SqlCommand(queryStr, dbConn);
            SqlDataReader reader = readComm.ExecuteReader();
            while(reader.Read())
            {
               PN = reader["Part_Number_To_Build"].ToString();
            }
            reader.Close();
            return PN;

        }

        public static string[] readCompPNs(string serialNumber)
        {
            string[] returnStrArr = new string[100];
            int i = 0;

            string queryStr = "SELECT [Component_Part_Number] FROM Subassembly_Info WHERE Subassembly_SN = " + "'" + serialNumber + "'" + "AND Rework = 'False' OR Subassembly_SN = " + "'" + serialNumber + "'" +"AND Rework IS NULL";
            SqlCommand readComm = new SqlCommand(queryStr, dbConn);
            SqlDataReader reader = readComm.ExecuteReader();
            while (reader.Read())
            {
                returnStrArr[i] = reader["Component_Part_Number"].ToString();
                i++;
                Console.WriteLine(returnStrArr[i]);
            }
            reader.Close();
            return returnStrArr;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void RwkPgReturnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI f1 = new MainGUI();
            f1.ShowDialog();
            this.Close();
        }

        private void RwkPgExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RwkSNScanBox_TextChanged(object sender, EventArgs e)
        {
            RwkSNScanBoxTimer.Start();
        }

        private void RwkSNScanBoxTimer_Tick(object sender, EventArgs e)
        {
            RwkSNScanBoxTimer.Stop();

            string ScannedSN = RwkSNScanBox.Text;

            connect();

            string partNumber = read(ScannedSN);
            
            Console.WriteLine("The queried part number is: " + partNumber);
            SubassyPNLbl.Text = partNumber;
            SubassyPNLbl.Visible = true;
            SubassyPNLbl.Refresh();


            // Load the component part numbers into the form
            string[] compPNs = readCompPNs(ScannedSN);
            //int i = 1;
            foreach (string PNstr in compPNs)
            {
                Console.WriteLine("PNstr = " + PNstr);
                if (PNstr != null)
                {
                    Console.WriteLine("Component part number: " + PNstr);

                    string labelToMod = "CompPNLbl" + ControlCounter;
                    var control = Controls.OfType<Label>().FirstOrDefault(c => c.Name == labelToMod);
                    control.Text = PNstr;
                    control.Visible = true;
                    control.Refresh();

                    string checkboxToMod = "CompPNChkBx" + ControlCounter;
                    var controlCB = Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == checkboxToMod);
                    controlCB.Visible = true;
                    controlCB.Refresh();

                    ControlCounter++;
                }

                Console.WriteLine("ControlCounter = " + ControlCounter);
            

            }



        }

        private void CompPNChkBx1_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN1 == true)
            {
                RwkPN1 = false;
                CompScanBox1.Visible = false;
            }
            else
            {
                RwkPN1 = true;
                CompScanBox1.Visible = true;
            }

           // Console.WriteLine("Rework = " + RwkPN1.ToString());
        }

        private void CompPNChkBx2_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN2 == true)
            {
                RwkPN2 = false;
                CompScanBox2.Visible = false;
            }
            else
            {
                RwkPN2 = true;
                CompScanBox2.Visible = true;
            }
        }

        private void CompPNChkBx3_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN3 == true)
            {
                RwkPN3 = false;
                CompScanBox3.Visible = false;
            }
            else
            {
                RwkPN3 = true;
                CompScanBox3.Visible = true;
            }
        }

        private void CompPNChkBx4_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN4 == true)
            {
                RwkPN4 = false;
                CompScanBox4.Visible = false;
            }
            else
            {
                RwkPN4 = true;
                CompScanBox4.Visible = true;
            }
        }

        private void CompPNChkBx5_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN5 == true)
            {
                RwkPN5 = false;
                CompScanBox5.Visible = false;
            }
            else
            {
                RwkPN5 = true;
                CompScanBox5.Visible = true;
            }
        }

        private void CompPNChkBx6_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN6 == true)
            {
                RwkPN6 = false;
                CompScanBox6.Visible = false;
            }
            else
            {
                RwkPN6 = true;
                CompScanBox6.Visible = true;
            }
        }

        private void CompPNChkBx7_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN7 == true)
            {
                RwkPN7 = false;
                CompScanBox7.Visible = false;
            }
            else
            {
                RwkPN7 = true;
                CompScanBox7.Visible = true;
            }
        }

        private void CompPNChkBx8_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN8 == true)
            {
                RwkPN8 = false;
                CompScanBox8.Visible = false;
            }
            else
            {
                RwkPN8 = true;
                CompScanBox8.Visible = true;
            }
        }

        private void CompPNChkBx9_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN9 == true)
            {
                RwkPN9 = false;
                CompScanBox9.Visible = false;
            }
            else
            {
                RwkPN9 = true;
                CompScanBox9.Visible = true;
            }
        }

        private void CompPNChkBx10_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN10 == true)
            {
                RwkPN10 = false;
                CompScanBox10.Visible = false;
            }
            else
            {
                RwkPN10 = true;
                CompScanBox10.Visible = true;
            }
        }

        private void CompPNChkBx11_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN11 == true)
            {
                RwkPN11 = false;
                CompScanBox11.Visible = false;
            }
            else
            {
                RwkPN11 = true;
                CompScanBox11.Visible = true;
            }
        }

        private void CompPNChkBx12_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN12 == true)
            {
                RwkPN12 = false;
                CompScanBox12.Visible = false;
            }
            else
            {
                RwkPN12 = true;
                CompScanBox12.Visible = true;
            }
        }

        private void CompPNChkBx13_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN13 == true)
            {
                RwkPN13 = false;
                CompScanBox13.Visible = false;
            }
            else
            {
                RwkPN13 = true;
                CompScanBox13.Visible = true;
            }
        }

        private void CompPNChkBx14_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN14 == true)
            {
                RwkPN14 = false;
                CompScanBox14.Visible = false;
            }
            else
            {
                RwkPN14 = true;
                CompScanBox14.Visible = true;
            }
        }

        private void CompPNChkBx15_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN15 == true)
            {
                RwkPN15 = false;
                CompScanBox15.Visible = false;
            }
            else
            {
                RwkPN15 = true;
                CompScanBox15.Visible = true;
            }
        }

        private void CompPNChkBx16_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN16 == true)
            {
                RwkPN16 = false;
                CompScanBox16.Visible = false;
            }
            else
            {
                RwkPN16 = true;
                CompScanBox16.Visible = true;
            }
        }
        private void CompPNChkBx17_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN17 == true)
            {
                RwkPN17 = false;
                CompScanBox17.Visible = false;
            }
            else
            {
                RwkPN17 = true;
                CompScanBox17.Visible = true;
            }
        }

        private void CompPNChkBx18_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN18 == true)
            {
                RwkPN18 = false;
                CompScanBox18.Visible = false;
            }
            else
            {
                RwkPN18 = true;
                CompScanBox18.Visible = true;
            }
        }

        private void CompPNChkBx19_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN19 == true)
            {
                RwkPN19 = false;
                CompScanBox19.Visible = false;
            }
            else
            {
                RwkPN19 = true;
                CompScanBox19.Visible = true;
            }
        }

        private void CompPNChkBx20_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN20 == true)
            {
                RwkPN20 = false;
                CompScanBox20.Visible = false;
            }
            else
            {
                RwkPN20 = true;
                CompScanBox20.Visible = true;
            }
        }

        private void CompPNChkBx21_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN21 == true)
            {
                RwkPN21 = false;
                CompScanBox21.Visible = false;
            }
            else
            {
                RwkPN21 = true;
                CompScanBox21.Visible = true;
            }
        }

        private void CompPNChkBx22_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN22 == true)
            {
                RwkPN22 = false;
                CompScanBox22.Visible = false;
            }
            else
            {
                RwkPN22 = true;
                CompScanBox22.Visible = true;
            }
        }

        private void CompPNChkBx23_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN23 == true)
            {
                RwkPN23 = false;
                CompScanBox16.Visible = false;
            }
            else
            {
                RwkPN23 = true;
                CompScanBox23.Visible = true;
            }
        }

        private void CompPNChkBx24_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN24 == true)
            {
                RwkPN24 = false;
                CompScanBox24.Visible = false;
            }
            else
            {
                RwkPN24 = true;
                CompScanBox24.Visible = true;
            }
        }

        private void CompPNChkBx25_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN25 == true)
            {
                RwkPN25 = false;
                CompScanBox25.Visible = false;
            }
            else
            {
                RwkPN25 = true;
                CompScanBox25.Visible = true;
            }
        }
        private void CompPNChkBx26_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN26 == true)
            {
                RwkPN26 = false;
                CompScanBox26.Visible = false;
            }
            else
            {
                RwkPN26 = true;
                CompScanBox26.Visible = true;
            }
        }

        private void CompPNChkBx27_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN27 == true)
            {
                RwkPN27 = false;
                CompScanBox27.Visible = false;
            }
            else
            {
                RwkPN28 = true;
                CompScanBox27.Visible = true;
            }
        }

        private void CompPNChkBx28_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN28 == true)
            {
                RwkPN28 = false;
                CompScanBox28.Visible = false;
            }
            else
            {
                RwkPN28 = true;
                CompScanBox28.Visible = true;
            }
        }

        private void CompPNChkBx29_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN29 == true)
            {
                RwkPN29 = false;
                CompScanBox29.Visible = false;
            }
            else
            {
                RwkPN29 = true;
                CompScanBox29.Visible = true;
            }
        }

        private void CompPNChkBx30_CheckedChanged(object sender, EventArgs e)
        {
            if (RwkPN30 == true)
            {
                RwkPN30 = false;
                CompScanBox30.Visible = false;
            }
            else
            {
                RwkPN30 = true;
                CompScanBox30.Visible = true;
            }
        }

        private void SubmitRwkBtn_Click(object sender, EventArgs e)
        {

            string SerialNumber = "";
            string CompSN = "";
            string Lot = "";
            string PN = "";
            string wo = "";
            string PNtoBuild = "";

            int i = 0;
            for (i = 1; i < ControlCounter; i++)
            {

                string LblToGet = "CompPNLbl" + i;
                var control = Controls.OfType<Label>().FirstOrDefault(c => c.Name == LblToGet);
                string PARTNUMBER = control.Text;

                string CheckboxToGet = "CompPNChkBx" + i;
                var controlCB = Controls.OfType<CheckBox>().FirstOrDefault(c => c.Name == CheckboxToGet);

                string ScanBoxToGet = "CompScanBox" + i;
                var controlSB = Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == ScanBoxToGet);

                string[] returnStrArray;
                string ScannedInput = controlSB.Text;
                returnStrArray = ParseBarcode(ScannedInput);

                string PNStr = returnStrArray[0];
                string SNStr = returnStrArray[1];
                string LOTStr = returnStrArray[2];

                string queryStr = "SELECT * FROM Subassembly_Info WHERE Subassembly_SN = " + "'" + RwkSNScanBox.Text + "'" + "AND Component_Part_Number =" + "'" + PARTNUMBER + "'";
                SqlCommand readComm = new SqlCommand(queryStr, dbConn);
                SqlDataReader reader = readComm.ExecuteReader();
                while (reader.Read())
                {
                    SerialNumber = reader["Subassembly_SN"].ToString();
                    //CompSN = reader["Component_SN"].ToString();
                    //Lot = reader["Component_Lot"].ToString();
                    PN = reader["Component_Part_Number"].ToString();
                    wo = reader["Work_Order"].ToString();
                    PNtoBuild = reader["Part_Number_To_Build"].ToString();
                }
                reader.Close();

                if (controlCB.Checked == true)
                {
                    bool DoRework = true;
                    // Write to the data table, this time with the rework flag set to TRUE

                    CompSN = SNStr;
                    Lot = LOTStr;

                    string InsertString = "INSERT INTO Subassembly_Info " +
                    "([Subassembly_SN],[Component_SN],[Component_Lot],[Component_Part_Number],[Work_Order],[Part_Number_To_Build],[Rework])" +
                    "Values (" + "'" + SerialNumber + delimiterchar + CompSN + delimiterchar + Lot + delimiterchar + PN + delimiterchar + wo + delimiterchar + PNtoBuild + delimiterchar + DoRework + "'" + ")";
                    SqlCommand InsertDataCmd = new SqlCommand(InsertString, dbConn);
                    InsertDataCmd.ExecuteNonQuery();
                }

            }


            // Clear all of the controls

            // Uncheck check boxes
            CompPNChkBx1.Checked = false;
            CompPNChkBx2.Checked = false;
            CompPNChkBx3.Checked = false;
            CompPNChkBx4.Checked = false;
            CompPNChkBx5.Checked = false;
            CompPNChkBx6.Checked = false;
            CompPNChkBx7.Checked = false;
            CompPNChkBx8.Checked = false;
            CompPNChkBx9.Checked = false;
            CompPNChkBx10.Checked = false;
            CompPNChkBx11.Checked = false;
            CompPNChkBx12.Checked = false;
            CompPNChkBx13.Checked = false;
            CompPNChkBx14.Checked = false;
            CompPNChkBx15.Checked = false;
            CompPNChkBx16.Checked = false;
            CompPNChkBx17.Checked = false;
            CompPNChkBx18.Checked = false;
            CompPNChkBx19.Checked = false;
            CompPNChkBx20.Checked = false;
            CompPNChkBx21.Checked = false;
            CompPNChkBx22.Checked = false;
            CompPNChkBx23.Checked = false;
            CompPNChkBx24.Checked = false;
            CompPNChkBx25.Checked = false;
            CompPNChkBx26.Checked = false;
            CompPNChkBx27.Checked = false;
            CompPNChkBx28.Checked = false;
            CompPNChkBx29.Checked = false;
            CompPNChkBx30.Checked = false;


            // Make check boxes invisible
            CompPNChkBx1.Visible = false;
            CompPNChkBx2.Visible = false;
            CompPNChkBx3.Visible = false;
            CompPNChkBx4.Visible = false;
            CompPNChkBx5.Visible = false;
            CompPNChkBx6.Visible = false;
            CompPNChkBx7.Visible = false;
            CompPNChkBx8.Visible = false;
            CompPNChkBx9.Visible = false;
            CompPNChkBx10.Visible = false;
            CompPNChkBx11.Visible = false;
            CompPNChkBx12.Visible = false;
            CompPNChkBx13.Visible = false;
            CompPNChkBx14.Visible = false;
            CompPNChkBx15.Visible = false;
            CompPNChkBx16.Visible = false;
            CompPNChkBx17.Visible = false;
            CompPNChkBx18.Visible = false;
            CompPNChkBx19.Visible = false;
            CompPNChkBx20.Visible = false;
            CompPNChkBx21.Visible = false;
            CompPNChkBx22.Visible = false;
            CompPNChkBx23.Visible = false;
            CompPNChkBx24.Visible = false;
            CompPNChkBx25.Visible = false;
            CompPNChkBx26.Visible = false;
            CompPNChkBx27.Visible = false;
            CompPNChkBx28.Visible = false;
            CompPNChkBx29.Visible = false;
            CompPNChkBx30.Visible = false;

            // Set label text to NULL
            SubassyPNLbl.Text = "";
            CompPNLbl1.Text = "";
            CompPNLbl2.Text = "";
            CompPNLbl3.Text = "";
            CompPNLbl4.Text = "";
            CompPNLbl5.Text = "";
            CompPNLbl6.Text = "";
            CompPNLbl7.Text = "";
            CompPNLbl8.Text = "";
            CompPNLbl9.Text = "";
            CompPNLbl10.Text = "";
            CompPNLbl11.Text = "";
            CompPNLbl12.Text = "";
            CompPNLbl13.Text = "";
            CompPNLbl14.Text = "";
            CompPNLbl15.Text = "";
            CompPNLbl16.Text = "";
            CompPNLbl17.Text = "";
            CompPNLbl18.Text = "";
            CompPNLbl19.Text = "";
            CompPNLbl20.Text = "";
            CompPNLbl21.Text = "";
            CompPNLbl22.Text = "";
            CompPNLbl23.Text = "";
            CompPNLbl24.Text = "";
            CompPNLbl25.Text = "";
            CompPNLbl26.Text = "";
            CompPNLbl27.Text = "";
            CompPNLbl28.Text = "";
            CompPNLbl29.Text = "";
            CompPNLbl30.Text = "";

            // Make labels invisible
            SubassyPNLbl.Visible = false;
            CompPNLbl1.Visible = false;
            CompPNLbl2.Visible = false;
            CompPNLbl3.Visible = false;
            CompPNLbl4.Visible = false;
            CompPNLbl5.Visible = false;
            CompPNLbl6.Visible = false;
            CompPNLbl7.Visible = false;
            CompPNLbl8.Visible = false;
            CompPNLbl9.Visible = false;
            CompPNLbl10.Visible = false;
            CompPNLbl11.Visible = false;
            CompPNLbl12.Visible = false;
            CompPNLbl13.Visible = false;
            CompPNLbl14.Visible = false;
            CompPNLbl15.Visible = false;
            CompPNLbl16.Visible = false;
            CompPNLbl17.Visible = false;
            CompPNLbl18.Visible = false;
            CompPNLbl19.Visible = false;
            CompPNLbl20.Visible = false;
            CompPNLbl21.Visible = false;
            CompPNLbl22.Visible = false;
            CompPNLbl23.Visible = false;
            CompPNLbl24.Visible = false;
            CompPNLbl25.Visible = false;
            CompPNLbl26.Visible = false;
            CompPNLbl27.Visible = false;
            CompPNLbl28.Visible = false;
            CompPNLbl29.Visible = false;
            CompPNLbl30.Visible = false;

            // Clear text boxes
            CompScanBox1.Text = "";
            CompScanBox2.Text = "";
            CompScanBox3.Text = "";
            CompScanBox4.Text = "";
            CompScanBox5.Text = "";
            CompScanBox6.Text = "";
            CompScanBox7.Text = "";
            CompScanBox8.Text = "";
            CompScanBox9.Text = "";
            CompScanBox10.Text = "";
            CompScanBox11.Text = "";
            CompScanBox12.Text = "";
            CompScanBox13.Text = "";
            CompScanBox14.Text = "";
            CompScanBox15.Text = "";
            CompScanBox16.Text = "";
            CompScanBox17.Text = "";
            CompScanBox18.Text = "";
            CompScanBox19.Text = "";
            CompScanBox20.Text = "";
            CompScanBox21.Text = "";
            CompScanBox22.Text = "";
            CompScanBox23.Text = "";
            CompScanBox24.Text = "";
            CompScanBox25.Text = "";
            CompScanBox26.Text = "";
            CompScanBox27.Text = "";
            CompScanBox28.Text = "";
            CompScanBox29.Text = "";
            CompScanBox30.Text = "";
            RwkSNScanBox.Text = "";

            // Make text boxes invisible
            CompScanBox1.Visible = false;
            CompScanBox2.Visible = false;
            CompScanBox3.Visible = false;
            CompScanBox4.Visible = false;
            CompScanBox5.Visible = false;
            CompScanBox6.Visible = false;
            CompScanBox7.Visible = false;
            CompScanBox8.Visible = false;
            CompScanBox9.Visible = false;
            CompScanBox10.Visible = false;
            CompScanBox11.Visible = false;
            CompScanBox12.Visible = false;
            CompScanBox13.Visible = false;
            CompScanBox14.Visible = false;
            CompScanBox15.Visible = false;
            CompScanBox16.Visible = false;
            CompScanBox17.Visible = false;
            CompScanBox18.Visible = false;
            CompScanBox19.Visible = false;
            CompScanBox20.Visible = false;
            CompScanBox21.Visible = false;
            CompScanBox22.Visible = false;
            CompScanBox23.Visible = false;
            CompScanBox24.Visible = false;
            CompScanBox25.Visible = false;
            CompScanBox26.Visible = false;
            CompScanBox27.Visible = false;
            CompScanBox28.Visible = false;
            CompScanBox29.Visible = false;
            CompScanBox30.Visible = false;

            // Reset the control counter
            ControlCounter = 1;

        }

        private void ReworkPage_Load(object sender, EventArgs e)
        {

        }

        private void SubassyPNTitleLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
