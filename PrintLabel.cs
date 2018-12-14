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

// FYI for certificates: M:\QA\Certificados\CERTIFICATES
// Labels C:\Labels
namespace ph_GUI_V1
{
    public partial class GenerateLabelForm : Form
    {

        //Connection to SQL database
        public static SqlConnection dbConn;

        // Used in SQL queries, just to make reading easier
        string delimiterchar = "','";

        string dummyDate = "06FEB2019";
        string dummyCalpH = "10.56";
        string dummySlope = "59.1";

        public bool SaveToMDrive = true;

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                ScanSNLbl.Text = MyGlobals.ScanSNStrEng;
                GenerateBtn.Text = MyGlobals.GenerateLabelStrEng;
                ReturnBtn.Text = MyGlobals.ReturnStrEng;
                ExitBtn.Text = MyGlobals.ExitStrEng;
            }
            else
            {
                ScanSNLbl.Text = MyGlobals.ScanSNStrSpn;
                GenerateBtn.Text = MyGlobals.GenerateLabelStrSpn;
                ReturnBtn.Text = MyGlobals.ReturnStrSpn;
                ExitBtn.Text = MyGlobals.ExitStrSpn;
            }
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
                MessageBox.Show("Error connecting to database");
            }
        }

        public static string read(string serialNumber)
        {
            string PN = "";
            string queryStr = "SELECT [Part_Number_To_Build] FROM Subassembly_Info WHERE Subassembly_SN = " + "'" + serialNumber + "'";
            SqlCommand readComm = new SqlCommand(queryStr, dbConn);
            SqlDataReader reader = readComm.ExecuteReader();
            while (reader.Read())
            {
                PN = reader["Part_Number_To_Build"].ToString();
            }
            reader.Close();
            return PN;

        }

        public GenerateLabelForm()
        {
            InitializeComponent();
            EngSpanLabels();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            connect();
            string ScannedSN = ScannedSNBox.Text;
            string partNumber = read(ScannedSN);
            Console.WriteLine("The queried part number is: " + partNumber);
            string TxtFileStr = "";
            string filepath = "";

            string TodaysDateStr = DateTime.Now.ToString("ddMMyyyy");
            Console.WriteLine("Today is: " + TodaysDateStr);

            // get today's date
            DateTime TodaysDate = DateTime.Now;

            // WIP exp. date is 5 days from build
            DateTime WIPExpDate = DateTime.Now.AddDays(5);
            string WIPStr = WIPExpDate.ToString("dd/MM/yyyy");
            Console.WriteLine("WIP expiration date is: " + WIPStr);

            // final assy exp. date is 2 years from build
            DateTime FinalExpDate = DateTime.Now.AddYears(2);
            string FinalStr = FinalExpDate.ToString("dd/MM/yy");
            Console.WriteLine("Final assy expiration date is: " + FinalStr);

            string MonthDateStr = "";
            // get month in string form
            if (partNumber.Contains("PH"))
            {
                MonthDateStr = FinalExpDate.ToString("MM");
            }
            else
            {
                MonthDateStr = WIPExpDate.ToString("MM");
            }


            string Month = "";
            switch (MonthDateStr)
            {
                case "01":
                    Month = "JAN";
                    break;
                case "02":
                    Month = "FEB";
                    break;
                case "03":
                    Month = "MAR";
                    break;
                case "04":
                    Month = "APR";
                    break;
                case "05":
                    Month = "MAY";
                    break;
                case "06":
                    Month = "JUN";
                    break;
                case "07":
                    Month = "JUL";
                    break;
                case "08":
                    Month = "AUG";
                    break;
                case "09":
                    Month = "SEP";
                    break;
                case "10":
                    Month = "OCT";
                    break;
                case "11":
                    Month = "NOV";
                    break;
                case "12":
                    Month = "DEC";
                    break;
                default:
                    Month = "JAN";
                    break;
            }



            if (partNumber.Contains("PH"))
            {
                string FinalExpDateStr = FinalExpDate.ToString("dd") + Month + FinalExpDate.ToString("yyyy");
                // format text string pH style
                TxtFileStr =
                    ScannedSN +
                    "," +
                    dummyCalpH +
                    "," +
                    dummySlope +
                    "," +
                    FinalExpDateStr +
                    "," +
                    "Serial Number: " +
                    ScannedSN +
                    " Expiration Date: " +
                    FinalExpDateStr +
                    " Cal pH: " +
                    dummyCalpH +
                    " Slope: " +
                    dummySlope +
                    " www.emerson.com";

                filepath = "C:\\Labels\\pH_LabelFile.txt";
                //filepath = "http://rmtintra.emersonprocess.com/interim/LCENG/Pipeline/SUB/Project%20Documentation/07_Manufacturing/Label%20Test/pH_LabelFile.txt";


            }
            else if (partNumber.Contains("DW"))
            {
                // format text string DW style
                TxtFileStr =
                    ScannedSN +
                    "," +
                    "Serial Number: " +
                    ScannedSN +
                    " www.emerson.com";

                filepath = "C:\\Labels\\DW_LabelFile.txt";
                //filepath = "http://rmtintra.emersonprocess.com/interim/LCENG/Pipeline/SUB/Project%20Documentation/07_Manufacturing/Label%20Test/DW_LabelFile.txt";
            }

            else
            {
                // for WIP, exp. date = today + 5 days
                string WIPExpDateStr = WIPExpDate.ToString("dd") + Month + WIPExpDate.ToString("yyyy");

                // format text string WIP style
                TxtFileStr =
                    partNumber +
                    "," +
                    ScannedSN +
                    "," +
                    WIPExpDateStr +
                    "," +
                    "Part No: " +
                    partNumber +
                    " Serial Number: " +
                    ScannedSN +
                    " Exp. Date: " +
                    WIPExpDateStr;

                filepath = "C:\\Labels\\WIP_LabelFile.txt";
                //filepath = "http://rmtintra.emersonprocess.com/interim/LCENG/Pipeline/SUB/Project%20Documentation/07_Manufacturing/Label%20Test/WIP_LabelFile.txt";
            }



            Console.WriteLine("Below is string to write to file:");
            Console.WriteLine(TxtFileStr);

            System.IO.File.WriteAllText(@filepath, TxtFileStr);

            string filestr;
            if (MyGlobals.InEnglish == true)
            {
                filestr = MyGlobals.LabelPathEng + filepath;
            }
            else
            { 
                filestr = MyGlobals.LabelPathSpn + filepath;
            }

                MessageBox.Show(filestr);



        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI f1 = new MainGUI();
            f1.ShowDialog();
            this.Close();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //ScanSNTimer.Stop();
        }

        private void ScannedSNBox_TextChanged(object sender, EventArgs e)
        {
            //ScanSNTimer.Start();
        }

        private void MDriveRadio_CheckedChanged(object sender, EventArgs e)
        {
            SaveToMDrive = true;
        }

        private void ATERadio_CheckedChanged(object sender, EventArgs e)
        {
            SaveToMDrive = false;
        }
    }
}
