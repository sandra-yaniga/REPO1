using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Word = Microsoft.Office.Interop.Word;


namespace ph_GUI_V1
{
    public partial class certForm : Form
    {

        // Still using dummy values for:
        // span
        // devicetype
        // model no.
        // factory info
        // station name
        // operator id

        // Need to add support for:
        // sales order
        // line item

        public static string SerialNumber;
        public static string docTemplate;
        public static string Q4Q8;

        //Connection to SQL database
        public static SqlConnection dbConn;

        public static string pHSlope;
        public static string pHOffset;
        public static string SensorPF;
        public static string SensorSpan;

        public string CertFileMsg;
        public string CertSaveLoc;

        bool Q4 = true;

        public bool SaveToMDrive = true;
        public string filepathroot;

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                label1.Text = MyGlobals.GenerateACertEng;
                label2.Text = MyGlobals.ScanSNStrEng;
                button1.Text = MyGlobals.GenerateLabelStrEng;
                button3.Text = MyGlobals.ReturnStrEng;
                exitButton.Text = MyGlobals.ExitStrEng;
                CertFileMsg = MyGlobals.CertPathEng;
                label3.Text = MyGlobals.CertTypeEng;
                label4.Text = MyGlobals.CertSaveLocEng;
            }
            else
            {
                label1.Text = MyGlobals.GenerateACertSpn;
                label2.Text = MyGlobals.ScanSNStrSpn;
                button1.Text = MyGlobals.GenerateLabelStrSpn;
                button3.Text = MyGlobals.ReturnStrSpn;
                exitButton.Text = MyGlobals.ExitStrSpn;
                CertFileMsg = MyGlobals.CertPathSpn;
                label3.Text = MyGlobals.CertTypeSpn;
                label4.Text = MyGlobals.CertSaveLocSpn;
            }
        }
        public certForm()
        {
            InitializeComponent();
            EngSpanLabels();
        }


        private void button1_Click(object sender, EventArgs e) // generate button
        {
            // Template document
            // This changes between Q4 and Q8
            if (Q4 == true)
            {
                // SRY Need to update docTemplate locations for M: drive
                docTemplate = "\\\\intruder\\userdata\\AnalyticalTestEng\\SUB_Certs\\Templates\\Q4 Cert Single Use Liquid Template.docx";
            } 
            else
            {
                docTemplate = "\\\\intruder\\userdata\\AnalyticalTestEng\\SUB_Certs\\Templates\\Q8 Cert Single Use Liquid Template.docx";
            }

            connect();

            string certString = "SELECT [pH_Slope_at_25C_Measurement], [Offset_at_pH7_Measurement], [pH_Slope_at_25C_PassFail], [Span_Measurement] FROM Calibration_Results WHERE [Serial_Number] = " + "'" + SerialNumber + "'";
            var checkComm = new SqlCommand(certString, dbConn);
            SqlDataReader certRead = checkComm.ExecuteReader();

            while (certRead.Read())
            {
                pHSlope = certRead["pH_Slope_at_25C_Measurement"].ToString();
                pHOffset = certRead["Offset_at_pH7_Measurement"].ToString();
                SensorPF = certRead["pH_Slope_at_25C_PassFail"].ToString();
                SensorSpan = certRead["Span_Measurement"].ToString();

                if (SensorPF.Equals("True"))
                {
                    SensorPF = "PASS";
                }
                else
                {
                    SensorPF = "FAIL";
                }


                Console.WriteLine("pHSlope = " + pHSlope);
                Console.WriteLine("pHOffset = " + pHOffset);
                Console.WriteLine("SensorPF = " + SensorPF);
            }
            Console.ReadLine();

            certRead.Close();
            dbConn.Close();

            // New document with replaced text
            // New doc for each serial number
            if (Q4==true)
            {
                Q4Q8 = "Q4";
            }
            else
            {
                Q4Q8 = "Q8";
            }
            string saveStr = filepathroot + Q4Q8 +"_CERT_" + SerialNumber + ".docx";

            object saveAsFileName = @saveStr;

            Word.Application ap = new Word.Application();
            object missing = System.Type.Missing;
            Word.Document doc1 = ap.Documents.Open(@docTemplate);

            try
            {
                FindAndReplace(ap, "<serialNumber>", SerialNumber);
                FindAndReplace(ap, "<pHslope1>", pHSlope);
                FindAndReplace(ap, "<pHoffset1>", pHOffset);
                FindAndReplace(ap, "<PF1>", SensorPF);
                //<span1>
                FindAndReplace(ap, "<span1>", SensorSpan);
                //<deviceType>
                FindAndReplace(ap, "<deviceType>", "Single Use");
                //<modelNumber>
                FindAndReplace(ap, "<modelNumber>", "550PH-COMMON");
                //<factoryInfo>
                FindAndReplace(ap, "<factoryInfo>", "Mexicali, MX");
                //<stationNameInfo>
                FindAndReplace(ap, "<stationNameInfo>", "SUB Tester 1");
                //<operatorIDInfo>
                FindAndReplace(ap, "<operatorIDInfo>", "72739");
                //<calDateInfo>
                string DateStr = DateTime.Now.ToString("MM/dd/yyyy");
                FindAndReplace(ap, "<calDateInfo>", DateStr);

                doc1.SaveAs(ref saveAsFileName,
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing, ref missing);

                //MessageBox.Show("Certificate Word document has been generated in " + saveStr);
                MessageBox.Show(CertFileMsg + saveStr);

            }
            catch
            {
                MessageBox.Show("Error populating cert form");
            }

            object doNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            doc1.Close(ref doNotSaveChanges, ref missing, ref missing);
        }
        private void button3_Click(object sender, EventArgs e) // return button
        {
            this.Hide();
            MainGUI f1 = new MainGUI();
            f1.ShowDialog();
            this.Close();
        }

 

        private void certForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CertSNScanBox_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            SerialNumber = CertSNScanBox.Text;



        }

        public static void connect()
        {
            string connectionString = "Server = USMTNPMDEAIDB05; Database = SUB_DB_PRACTICE; User Id = sub_db_writer; Password = s&b_wr*t3";
            try
            {
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot connect to database");
            }
        }

        public static void FindAndReplace(Word.Application app, string OldStr, string NewStr)
        {
            Word.Find findObject = app.Selection.Find;
            findObject.ClearFormatting();
            findObject.Text = OldStr;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = NewStr;

            object missing = System.Type.Missing;
            object replaceAll = Word.WdReplace.wdReplaceAll;
            findObject.Execute(ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Q4 = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Q4 = false;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MDriveRadio_CheckedChanged(object sender, EventArgs e)
        {
            SaveToMDrive = true;
            filepathroot = "M:\\QA\\Certificados\\CERTIFICATES\\";
        }

        private void ATERadio_CheckedChanged(object sender, EventArgs e)
        {
            SaveToMDrive = false;
            filepathroot = "\\\\intruder\\userdata\\AnalyticalTestEng\\SUB_Certs\\Generated_Certs\\";
        }
    }
}


