////////////////////////////////////////////////////////////////////////////////
//                   
// Title:            ph GUI V1
// Files:            Program.cs, MainGUIPage.cs, AssemblyStepSelector.cs,
//                   step1-301.cs, step2-301.cs, step3-301.cs, step4-301.cs,
//                   step4-301.cs, step5-301.cs, step6-301.cs, step6-301.cs
//                   step7-301.cs, finA-301.cs, FPYfields.cs, FTPfields.cs, 
//                   SNfields.cs, reports.cs, reports1.cs, reports2.cs           
//
// This File:        MainGUIPage.cs
//
// Author:           Sam Radovich and Sandy Yaniga
// Email:            samuel.radovich@emerson.com
// Company:          Emerson Electric, Rosemount
// 
/////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ph_GUI_V1
{
    public partial class MainGUI : Form
    {

        public MainGUI()
        {
            InitializeComponent();
            
        }
      
        
        
        // private void partToAssem_Click(object sender, EventArgs e)
       // {
        //    partToAssem.Clear();
       // }

        //private void partToAssem_TextChanged(object sender, EventArgs e)
        //{
        //    this.partToAssem.ForeColor = Color.Black;

        //}

        //setup passer and call testForm
     

        private void reort1Bttn_Click(object sender, EventArgs e)
        {
            /*
            this.Hide();
            FPYfields fpy = new FPYfields();
            fpy.Closed += (s, args) => this.Close();
            fpy.Show();
            */

            this.Hide();
            FPYfields f1 = new FPYfields();
            f1.ShowDialog();
            this.Close();
        }

        private void report2Bttn_Click(object sender, EventArgs e)
        {
            /*
            this.Hide();
            FTPfields ftp = new FTPfields();
            ftp.Closed += (s, args) => this.Close();
            ftp.Show();
            */

            this.Hide();
            FTPfields f1 = new FTPfields();
            f1.ShowDialog();
            this.Close();
        }

        private void report3Bttn_Click(object sender, EventArgs e)
        {
            /*
            this.Hide();
            SNfields SN = new SNfields();
            SN.Closed += (s, args) => this.Close();
            SN.Show();
            */

            this.Hide();
            SNfields f1 = new SNfields();
            f1.ShowDialog();
            this.Close();


        }

        private void printCertButton_Click_1(object sender, EventArgs e)
        {
            /*
            this.Hide();
            certForm certs = new certForm();
            certs.Closed += (s, args) => this.Close();
            certs.Show();
            */

            this.Hide();
            certForm f1 = new certForm();
            f1.ShowDialog();
            this.Close();


            //was calling josh's program, working on own version
            /*
            try
            {
                
                string exeFile = @"C:\Users\Samurad\Desktop\Programs from josh\files\WpfApp1.exe";
                System.Diagnostics.Process.Start(exeFile);
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("ODE");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("IOE");
            }
            catch (Win32Exception)
            {
                MessageBox.Show("Win32");
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("FNFE");
            }
        }
        */
        }

        private void beginAssemButton_Click(object sender, EventArgs e)
        {

            /*

            // passer = partToAssem.Text;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // for design in 96 DPI
            this.Hide();
            assemFields field = new assemFields();
            field.Closed += (s, args) => this.Close();
            field.ShowDialog();

            */

            Hide();
            assemFields f1 = new assemFields();
            f1.ShowDialog();
            Show();


        }

        private void MainGUI_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                MainGUI_title.Text = MyGlobals.MainPageStrEng;
                
                beginAssemBox.Text = MyGlobals.BeginAssemblyStrEng;

                beginAssemButton.Text = MyGlobals.GoStrEng;

                printCertButton.Text = MyGlobals.GoStrEng;

                printCertTxtB.Text = MyGlobals.PrintCertificateStrEng;

                printReportTxtB.Text = MyGlobals.PrintReportStrEng;

                reort1Bttn.Text = MyGlobals.FirstPassYieldStrEng;

                report2Bttn.Text = MyGlobals.FinalTestParetosStrEng;

                report3Bttn.Text = MyGlobals.DataBySNStrEng;

                MainPgExitBtn.Text = MyGlobals.ExitStrEng;

                RwkBtn.Text = MyGlobals.GoStrEng;

                PrintLblBtn.Text = MyGlobals.GoStrEng;

                RwkLbl.Text = MyGlobals.ReworkStrEng;

                PrintLabelLbl.Text = MyGlobals.PrintLblStrEng;


            }
            else
            {
                MainGUI_title.Text = MyGlobals.MainPageStrSpn;

                beginAssemBox.Text = MyGlobals.BeginAssemblyStrSpn;

                beginAssemButton.Text = MyGlobals.GoStrSpn;

                printCertButton.Text = MyGlobals.GoStrSpn;

                printCertTxtB.Text = MyGlobals.PrintCertificateStrSpn;

                printReportTxtB.Text = MyGlobals.PrintReportStrSpn;

                reort1Bttn.Text = MyGlobals.FirstPassYieldStrSpn;

                report2Bttn.Text = MyGlobals.FinalTestParetosStrSpn;

                report3Bttn.Text = MyGlobals.DataBySNStrSpn;

                MainPgExitBtn.Text = MyGlobals.ExitStrSpn;

                RwkBtn.Text = MyGlobals.GoStrSpn;

                PrintLblBtn.Text = MyGlobals.GoStrSpn;

                RwkLbl.Text = MyGlobals.ReworkStrSpn;

                PrintLabelLbl.Text = MyGlobals.PrintLblStrSpn;
            }

            MainGUI_title.Refresh();
            beginAssemBox.Refresh();
            beginAssemButton.Refresh();
            printCertButton.Refresh();
            printCertTxtB.Refresh();
            printReportTxtB.Refresh();
            reort1Bttn.Refresh();
            report2Bttn.Refresh();
            report3Bttn.Refresh();
        }

        private void EnglishSelectorBtn_CheckedChanged(object sender, EventArgs e)
        {
            MyGlobals.InEnglish = true;
            EngSpanLabels();
        }

        private void SpanishSelectorBtn_CheckedChanged(object sender, EventArgs e)
        {
            MyGlobals.InEnglish = false;
            EngSpanLabels();
        }

        private void MainPgExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void beginAssemBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void RwkBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReworkPage f1 = new ReworkPage();
            f1.ShowDialog();
            this.Close();
        }

        private void printCertTxtB_TextChanged(object sender, EventArgs e)
        {

        }

        private void RwkLbl_Click(object sender, EventArgs e)
        {

        }

        private void MainGUI_title_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrintLblBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            GenerateLabelForm f1 = new GenerateLabelForm();
            f1.ShowDialog();
            this.Close();
        }
    }
    public static class MyGlobals
    {
        // Contains global variables used throughout the program
        public static string[] CountingSerialNumbers = new string[100];
        public static int SNCounter = 0;
        //public static bool IsDuplicateSN = false;
        public static int loopyCounter = 0;
        public static bool InEnglish = true;

        // List of strings displayed in labels, text boxes, and messages
        // Used to make the choice between English and Spanish neater

        // Main Page
        public static string MainPageStrEng = "Main Page";
        public static string MainPageStrSpn = "Pagina Principal";

        public static string BeginAssemblyStrEng = "Begin Assembly";
        public static string BeginAssemblyStrSpn = "Empezar Ensamble";

        public static string GoStrEng = "Go";
        public static string GoStrSpn = "Avance";

        public static string PrintCertificateStrEng = "Print Certificate";
        public static string PrintCertificateStrSpn = "Imprimir Certificado";

        public static string ReworkStrEng = "Rework";
        public static string ReworkStrSpn = "Reanudación";

        public static string PrintLblStrEng = "Print Label";
        public static string PrintLblStrSpn = "Etiqueta de impresión";

        public static string PrintReportStrEng = "Print Report";
        public static string PrintReportStrSpn = "Imprimir Reporte";

        public static string FirstPassYieldStrEng = "First Pass Yield";
        public static string FirstPassYieldStrSpn = "First Pass Yield";

        public static string FinalTestParetosStrEng = "Final Test Paretos";
        public static string FinalTestParetosStrSpn = "Paretos de Prueba Final";

        public static string DataBySNStrEng = "Data by Serial Number";
        public static string DataBySNStrSpn = "Informacion por Numero de Serie";

        // Assembly Page

        public static string AssemblyInfoStrEng = "Assembly Information";
        public static string AssemblyInfoStrSpn = "Informacion de Ensamble";

        public static string ScanWorkOrderNumStrEng = "Scan the Work Order Number";
        public static string ScanWorkOrderNumStrSpn = "Escanear el Numero de Orden de Trabajo";

        public static string ScanPartNumStrEng = "Scan the Part Number";
        public static string ScanPartNumStrSpn = "Escanear el Numero de Parte";

        public static string ScanBadgeNumStrEng = "Scan Your Badge Number";
        public static string ScanBadgeNumStrSpn = "Escanear Su Gafete de Empleado";

        public static string ScanQtyStrEng = "Scan the Quantity";
        public static string ScanQtyStrSpn = "Escanear la Cantidad";

        public static string NextStrEng = "Next";
        public static string NextStrSpn = "Siguiente";

        // Test Page

        public static string ScanSNfromWOStrEng = "Scan Serial Number from Work Order";
        public static string ScanSNfromWOStrSpn = "Escanear Numero de Serie de Orden de Trabajo";

        public static string ScanBarcodeStrEng = "Scan Barcode";
        public static string ScanBarcodeStrSpn = "Escanear Codigo de Barras";

        public static string BuildingPNStrEng = "Building Part Number";
        public static string BuildingPNStrSpn = "Creando Numero de Parte";

        public static string BuildingWOStrEng = "Building Work Order";
        public static string BuildingWOStrSpn = "Creando Orden de Trabajo";

        public static string AssemblyStrEng = "Assembly";
        public static string AssemblyStrSpn = "Ensamble";

        public static string SubmitStrEng = "Submit";
        public static string SubmitStrSpn = "Envie";

        // Rework page
        public static string ScanSNStrEng = "Scan serial number"; // Also used in Print label page and cert page
        public static string ScanSNStrSpn = "Escanear Numero de Serie"; // Also used in Print label page and cert page

        public static string SubassyPNStrEng = "Subassembly PN";
        public static string SubassyPNStrSpn = "Número de pieza del subconjunto";

        public static string CompPNStrEng = "Component PN";
        public static string CompPNStrSpn = "Número de pieza del componente";

        public static string ReworkStrQuesEng = "Rework?";
        public static string ReworkStrQuesSpn = "¿Reanudación?";

        public static string SubmitRwkStrEng = "Submit Rework";
        public static string SubmitRwkStrSpn = "Enviar retrabajo";

        // Cert generation page
        public static string GenerateACertEng = "Generate Certificate";
        public static string GenerateACertSpn = "Generar un certificado";

        // Print label page
        public static string GenerateLabelStrEng = "Generate Label";
        public static string GenerateLabelStrSpn = "Generar etiqueta";

        // Pop-up windows
        public static string SNAlreadyScannedEng = "This serial number has already been built";
        public static string SNAlreadyScannedSpn = "Este número de serie ya ha sido construido";

        public static string PNNotFoundInDBEng = "Part number not found in database!";
        public static string PNNotFoundInDBSpn = "Número de parte no encontrado en la base de datos!";

        public static string ReturnToMainEng = "Returning to Main Page - Please Retry";
        public static string ReturnToMainSpn = "Regresando a la página principal - Por favor, vuelva a intentarlo";

        // Label generation form
        public static string LabelPathEng = "Label contained in file: ";
        public static string LabelPathSpn = "Archivo de etiqueta contenido en: ";

        // Certificate generation form
        public static string CertPathEng = "Certificate file contained in: ";
        public static string CertPathSpn = "Archivo de certificado contenido en";

        public static string CertTypeEng = "Certificate Type";
        public static string CertTypeSpn = "Tipo de certificado";

        public static string CertSaveLocEng = "Save Location";
        public static string CertSaveLocSpn = "Ubicación para guardar";

        // Used multiple places
        public static string ReturnStrEng = "Return";
        public static string ReturnStrSpn = "Volver";

        public static string ExitStrEng = "Exit";
        public static string ExitStrSpn = "Salida";

    }
}

