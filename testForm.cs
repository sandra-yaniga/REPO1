///////////////////////////////////////////////////////////////////////////////
//                   
// Title:            ph GUI V1
// Files:            Program.cs, MainGUIPage.cs, FPYfields.cs, FTPfields.cs, 
//                   SNfields.cs, reports.cs, reports1.cs, reports2.cs,  
//                   testForm.cs
//
// This File:        testForm.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;
using System.Data.SqlClient;

namespace ph_GUI_V1
{

    public partial class testForm : Form
    {



        //int clickCount = 0;

        List<int> qtyList = new List<int>();
        List<string> partnumList = new List<string>();
        IDictionary<string, int> lookupTable = new Dictionary<string, int>();
        List<int> qtyScannedList = new List<int>();


        public static string SerialNumber = "";

        // This needs to be changed in testForm.cs and assemFields.cs
        public static string SqlOrAccess = "SQL";

        bool OKToWrite = false;

        //bool to display testForm or not
        public static bool display = true;

        //error if we read in an assembly that has too many parts
        public static bool sizeError = false;

        public bool SNObtained = false;
        public bool FirstTimeThrough = true;

        int totalQtyToScan;

        //var to store passed input from main. Will be WO or SN to pick an assembly
        String input = assemFields.pn.Trim(' ');

        String step = assemFields.stepNum.ToString().Trim(' ');
        String ofSteps = assemFields.q.Trim(' ');

        //if the assembly has a test, will have to display a test button
        bool hasTest = false;
        //public strings to store serial number, work order and badge number, simplifies from tb(N)Text
        public static String sn = assemFields.snums;
        public static String wo = assemFields.wo;
        public static String bn = assemFields.bn;

        //array to store the assembly data we are using. shouldn't have more than 100 entries, but can expand
        String[] workSet = new String[100];
        string[] qtyArray = new string[100];

        string[] PNArray = new string[100];
        string[] LotArray = new string[100];
        string[] SNArray = new string[100];
        int CountTracker = 0;

        //To count if you are done entering info
        int numberOfLabels = 0;

        // Used in SQL queries, just to make reading easier
        string delimiterchar = "','";

        //-------------------------------
        // Order of columns in data tables
        //-------------------------------

        // Subassembly_Info table
        public static int Component_SN_Col_SubassyInfo = 1;
        public static int Component_Lot_Col_SubassyInfo = 2;
        public static int Component_Part_Number_SubassyInfo = 3;
        public static int Sensor_ID_SubassyInfo = 4;
        public static int Subassembly_SN_SubassyInfo = 5;

        // Sensor_Info table
        public static int Sensor_SN_SensorInfo = 1;
        public static int Work_Order_SensorInfo = 2;
        public static int Part_Number_SensorInfo = 3;

        //stores labels so that we can check if they're all green and admit to test program
        List<Label> labels = new List<Label>();

        //counts number of non-null slots in array so we know an accurate measurement of length
        //int count = 0;

        //Connection to SQL database
        public static SqlConnection dbConn;
        public static SqlDataReader SQLtestRead;
        public static SqlDataReader sreader;

        // Connection to Access database
        public static OleDbConnection objConn;
        public static OleDbDataReader ACCESStestRead;
        public static OleDbDataReader oreader;

        //stores the date so we can record it int the data base
        static DateTime d = DateTime.Now;
        static String ds = d.ToShortDateString();
        static DateTime date = DateTime.Parse(ds);

        //strings that store the text from respective text boxes. (SN, BN, WO)
        public static String tb1Text;
        public static String tb2Text;
        public static String tb3Text;

        private void EngSpanLabels()
        {
            if (MyGlobals.InEnglish == true)
            {
                label2.Text = MyGlobals.ScanSNfromWOStrEng;

                label1.Text = MyGlobals.ScanBarcodeStrEng;

                BuildPartNumberLbl.Text = MyGlobals.BuildingPNStrEng;

                BuildWorkOrderLbl.Text = MyGlobals.BuildingWOStrEng;

                //lblt.Text = MyGlobals.AssemblyStrEng; // This has to be done where the label is created

                button3.Text = MyGlobals.SubmitStrEng;

                Exit.Text = MyGlobals.ExitStrEng;


            }
            else
            {
                label2.Text = MyGlobals.ScanSNfromWOStrSpn;

                label1.Text = MyGlobals.ScanBarcodeStrSpn;

                BuildPartNumberLbl.Text = MyGlobals.BuildingPNStrSpn;

                BuildWorkOrderLbl.Text = MyGlobals.BuildingWOStrSpn;

                //lblt.Text = MyGlobals.AssemblyStrSpn; // This has to be done where the label is created

                button3.Text = MyGlobals.SubmitStrSpn;

                Exit.Text = MyGlobals.ExitStrSpn;
            }

            label2.Refresh();

            label1.Refresh();

            BuildPartNumberLbl.Refresh();

            BuildWorkOrderLbl.Refresh();

            //lblt.Refresh();; // This has to be done where the label is created

            button3.Refresh(); 

        }


                /*
                 * initialization of testForm(). dictate if we should display form or return to MainGUI and show
                 * error messages
                 */
                public testForm()
        {

            //findout if form is shown so that it can be hidden (hide must be called after form is shown)
            Shown += testForm_Shown;
            //connect and scan in part data file and see if theres a match. Display if valid
            connect();
            read();

            if (display)
            {
                InitializeComponent();
                populate();
                EngSpanLabels();
            }

        }

        // ----------------------------- DATABASE CONNECT FUNCTIONS-------------------------------------
        /*
         * Method connects to data base and catches any exceptions that may occur. Also opens connection. 
         * Connection closed later in button clicks and in error handling section of MainGUI()
         */

        public static void connect()
        {
            //Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\Samurad\\Desktop\\C# Programs\\ph GUI V1\\demopHDatabase.mdb;Persist Security Info=False
            string connectionString = "Server = USMTNPMDEAIDB05; Database = SUB_DB_PRACTICE; User Id = sub_db_writer; Password = s&b_wr*t3";
            try
            {
                //if (SqlOrAccess == "SQL")
                //{
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
                //}
                //else
                //{
                //	string sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\sandyan\\Documents\\SUB_DB_LOCAL.accdb;Persist Security Info=False";
                //	                    //OleDbConnection 
                //                objConn = new OleDbConnection(sConnectionString);
                //                // Open connection to database
                //                objConn.Open();
                //}
            }

            catch (Exception)
            {
                MessageBox.Show("Cannot connect to database x");
            }
        }
        /*
        * Method reads from Assembly_Info table to populate forms.
        */

        private void read()
        {
            //check if we have the assembly requested on record
            try
            {

                //read all part asssemblies we have on record
                String testString = "SELECT [Part_Number] FROM Assembly_Info";
                var checkComm = new SqlCommand(testString, dbConn);
                SqlDataReader testRead = checkComm.ExecuteReader();

                //read line by line
                while (testRead.Read())
                {
                    //break and continue if we have a match                 
                    if(testRead["Part_Number"].ToString().Equals(input))
                    {
                        testRead.Close();
                        display = true;
                        break;
                    }
                    //if we reach the end and theres still no match, return,
                    //close connections and display error
                    else if (testRead.Read() == false)
                    {
                        if (MyGlobals.InEnglish == true) { 
                            MessageBox.Show(MyGlobals.PNNotFoundInDBEng);
                            MessageBox.Show(MyGlobals.ReturnToMainEng);
                        }
                        else
                        {
                            MessageBox.Show(MyGlobals.PNNotFoundInDBSpn);
                            MessageBox.Show(MyGlobals.ReturnToMainSpn);
                        }
                        testRead.Close();
                        if (SqlOrAccess == "SQL")
                        {
                            dbConn.Close();
                        }
                        else
                        {
                            objConn.Close();
                        }
                        display = false;                        
                        Application.Exit();
                        return;
                    }

                }
            }
            catch (Exception)
            {
                if (SqlOrAccess == "SQL")
                {
                    dbConn.Close();
                }
                else
                {
                    objConn.Close();
                }
                MessageBox.Show("Error reading from database.");
            }
            //we have a match so we can safely attempt to
            //populate workSet from data base Assembly_Info table to display
            try
            {
                //get data assosciated with assembly we are looking for
                String cString = "SELECT[Part_Number], [Assembly_Title], [Has_Test], [Component_Part_Number], [Quantity] FROM Assembly_Info" +
                                      " WHERE Part_Number = " + "'" + input + "'";
                
                SqlCommand readComm = new SqlCommand(cString, dbConn);
                readComm.CommandTimeout = 3600;
                SqlDataReader reader = readComm.ExecuteReader();


                //idex to insert components into workSet
                int i = 3;
                //fill workSet
                while (reader.Read())
                {
                    workSet[0] = reader["Part_Number"].ToString();
                    workSet[1] = reader["Assembly_Title"].ToString();
                    workSet[2] = reader["Has_Test"].ToString();

                    workSet[i] = reader["Component_Part_Number"].ToString();

                    Console.WriteLine(workSet[i]);

                    partnumList.Insert(i - 3, workSet[i]);
                    qtyList.Insert(i - 3, Int32.Parse(reader["Quantity"].ToString()));
                    lookupTable.Add(workSet[i], Int32.Parse(reader["Quantity"].ToString()));
                    qtyScannedList.Insert(i - 3, 0);
                    i++;
                    Console.WriteLine("Reader i = " + i);

                }
                reader.Close();
                //count = qtyList.Sum() + 3;
                totalQtyToScan = qtyList.Sum();
            }
            catch (Exception)
            {
                if (SqlOrAccess == "SQL")
                {
                    dbConn.Close();
                }
                else
                {
                    objConn.Close();
                }
                MessageBox.Show("Error reading from database");
            }

        }


        // ----------------------------- END DATABASE READ FUNCTIONS-------------------------------------


        /*
         *Determines if form is shown. Returns to main if we have an error that is handled in that way.
         * Must call hide AFTER form is shown or else unwanted windows appear.
         */
        private void testForm_Shown(object sender, EventArgs e)
        {
            //if display is false, hide this form and return to main
            if (!display)
            {

                this.Hide();

            }
        }

        /*
         *method populates the form with user specified assembly data 
         */
        private void populate()
        {
            //obtain amount of data in workSet that is not null or whitespace
            for (int i = 0; i < workSet.Length; i++)
            {
                if (String.IsNullOrWhiteSpace(workSet[i]))
                {
                    break;
                }
                else
                {
                    //count++;
                    //count = totalQtyToScan + 1;
                    //count = qtyList.Sum() + 3;
                }
                //before anything, see if we can handle the number of parts asked in assembly (64 is max right now)
                //if (count - 3 > 64)
                if (qtyList.Count > 64)
                {

                    sizeError = true;
                    display = false;
                    return;
                }
            }
            //count = totalQtyToScan;
            //set hasTest to true if workSet line is supposed to have a test
            if (workSet[2] == "1") { hasTest = true; }
            //Creation of the Title label. Not added to labels, only check if parts are scanned
            Label lblt = new Label();
            lblt.Name = "titleLabel";
            lblt.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblt.ForeColor = System.Drawing.Color.Black;
            lblt.Location = new System.Drawing.Point(25, 25);
            lblt.Size = new System.Drawing.Size(160, 35);
            lblt.TabIndex = 8;
            lblt.TextAlign = ContentAlignment.MiddleCenter;
            lblt.BorderStyle = BorderStyle.FixedSingle;
            lblt.BackColor = Color.White;
            lblt.TabStop = false;
            string txtstr = "";
            if (MyGlobals.InEnglish == true)
            {
                txtstr = MyGlobals.AssemblyStrEng + step + "/" + ofSteps;
            } else
            {
                txtstr = MyGlobals.AssemblyStrSpn + step + "/" + ofSteps;
            }
            lblt.Text = (txtstr);
            this.Controls.Add(lblt);

            this.WOBuildTextBox.Text = wo;
            this.BuildPNTextBox.Text = input;

            //begin at 3 because that's the first index of workSet with part data
            //loop so that the form can expand dynamically
            //count - 3 = number of parts. used often.
            //for (int i = 3; i < count; i++)
            //for (int i = 3; i<qtyList.Sum() +3; i++)
            for (int i = 3; i < qtyList.Count + 3;  i++)
            {

                //otherwise, handle one column case. (i compared to 19 because count is scaled up by 3. 16 is max num of 
                // parts in each column)
                if (i <= 16)
                {
                    //create label for each part in workSet. Vertically scaled by i.
                    Label lbl = new Label();
                    lbl.Name = "label" + i.ToString();
                    lbl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.ForeColor = System.Drawing.Color.White;
                    lbl.BackColor = System.Drawing.Color.Red;
                    lbl.Location = new System.Drawing.Point(50, 50 + ((i - 2) * 30));
                    lbl.Size = new System.Drawing.Size(200, 21);
                    lbl.TabIndex = 8;
                    lbl.TabStop = false;
                    lbl.Text = workSet[i];

                    //partnumList.Insert(i - 3, workSet[i]);

                    

                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    //create event handler for when a label is click
                    lbl.Click += new EventHandler(this.lbl_Click);
                    //add to list of labels, purpose explained at labels declaration
                    labels.Add(lbl);
                    this.Controls.Add(lbl);
                    //scale the size of the form by number of parts we have                    
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    ////this.ClientSize = new System.Drawing.Size(415, 200 + ((i - 2) * 30));
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);

                    Label qtylabel = new Label();
                    qtylabel.Name = "qtylabel" + i.ToString();
                    qtylabel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    qtylabel.ForeColor = System.Drawing.Color.Black;
                    qtylabel.BackColor = System.Drawing.Color.White;
                    qtylabel.Location = new System.Drawing.Point(10, 50 + ((i - 2) * 30));
                    qtylabel.Size = new System.Drawing.Size(40, 21);
                    qtylabel.Text = "0/" + qtyList[i-3].ToString();
                    qtylabel.BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(qtylabel);
                    qtylabel.Top = (50 + ((i - 2) * 30));
                    qtylabel.Refresh();

                    this.Name = "testForm";
                    this.Text = "Test Form";
                    this.ResumeLayout(false);
                    //draw return button at the bottom of the form


                    //if the assembly has a test, draw a test button
                    if (hasTest)
                    {

                        this.button2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button2.Location = new System.Drawing.Point(300, 145 + ((i - 2) * 38));
                        //this.button2.Location = new System.Drawing.Point(300, 25);
                        this.button2.Name = "button2";
                        this.button2.Size = new System.Drawing.Size(99, 35);
                        this.button2.TabIndex = 0;
                        this.button2.TabStop = false;
                        this.button2.Text = "Test";
                        this.button2.UseVisualStyleBackColor = true;
                        //this.button2.Hide();

                    }
                    else
                    {
                        this.button3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button3.Location = new System.Drawing.Point(500, 145 + ((i - 2) * 30));
                        this.button3.Name = "button3";
                        this.button3.Size = new System.Drawing.Size(99, 35);
                        this.button3.TabIndex = 0;
                        this.button3.TabStop = false;
                        this.button3.Text = "Submit";
                        this.button3.UseVisualStyleBackColor = true;
                        //this.button3.Hide();
                    }

                }
                //handles needing two columns
                else if (i > 16 && i < 35)
                {
                    //draws form, size doesnt need to be dynamic
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    //this.ClientSize = new System.Drawing.Size(620, 680);

                    this.Name = "testForm";
                    this.Text = "Test Form";
                    this.ResumeLayout(false);
                    //return button location doesnt need to have dynamic location

                    //draws label for each part in assembly
                    Label lbl = new Label();
                    lbl.Name = "label" + i.ToString();
                    lbl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.ForeColor = System.Drawing.Color.White;
                    lbl.BackColor = System.Drawing.Color.Red;
                    lbl.Location = new System.Drawing.Point(340, 50 + ((i - 2) * 30) - 420);
                    lbl.Size = new System.Drawing.Size(200, 21);
                    lbl.TabIndex = 8;
                    lbl.TabStop = false;
                    lbl.Text = workSet[i];
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    //setuo event handler for if label is clicked
                    lbl.Click += new EventHandler(this.lbl_Click);
                    //add to labels
                    labels.Add(lbl);
                    this.Controls.Add(lbl);

                    Label qtylabel = new Label();
                    qtylabel.Name = "qtylabel" + i.ToString();
                    qtylabel.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    qtylabel.ForeColor = System.Drawing.Color.Black;
                    qtylabel.BackColor = System.Drawing.Color.White;
                    qtylabel.Location = new System.Drawing.Point(300, 50 + ((i - 2) * 30));
                    qtylabel.Size = new System.Drawing.Size(40, 21);
                    qtylabel.Text = "0/" + qtyList[i - 3].ToString();
                    qtylabel.BorderStyle = BorderStyle.FixedSingle;
                    this.Controls.Add(qtylabel);
                    qtylabel.Top = (50 + ((i - 2) * 30) - 420);
                    qtylabel.Refresh();


                    //draw test button if the assembly has a step
                    if (hasTest)
                    {
                        this.button2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button2.Location = new System.Drawing.Point(510, 635);
                        this.button2.Name = "button2";
                        this.button2.Size = new System.Drawing.Size(99, 35);
                        this.button2.TabIndex = 0;
                        this.button2.TabStop = false;
                        this.button2.Text = "Test";
                        this.button2.UseVisualStyleBackColor = true;
                        //this.button2.Hide();
                    }
                    else
                    {
                        this.button3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button3.Location = new System.Drawing.Point(510, 635);
                        this.button3.Name = "button3";
                        this.button3.Size = new System.Drawing.Size(99, 35);
                        this.button3.TabIndex = 0;
                        this.button3.TabStop = false;
                        this.button3.Text = "Submit";
                        this.button3.UseVisualStyleBackColor = true;
                        //this.button3.Hide();
                    }

                }
                //handles three columns
                else if (i > 34 && i < 51)
                {
                    //draw the form. dynamic size not neccesary
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    //this.ClientSize = new System.Drawing.Size(850, 680);

                    this.Name = "testForm";
                    this.Text = "Test Form";
                    this.ResumeLayout(false);
                    //draw the return button

                    //draw the labels
                    Label lbl = new Label();
                    lbl.Name = "label" + i.ToString();
                    lbl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.ForeColor = System.Drawing.Color.White;
                    lbl.BackColor = System.Drawing.Color.Red;
                    lbl.Location = new System.Drawing.Point(475, 50 + ((i - 2) * 30) - 960);
                    lbl.Size = new System.Drawing.Size(200, 21);
                    lbl.TabIndex = 8;
                    lbl.TabStop = false;
                    lbl.Text = workSet[i];
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    //setup event handler for label click event 
                    lbl.Click += new EventHandler(this.lbl_Click);
                    // add to labels
                    labels.Add(lbl);
                    this.Controls.Add(lbl);
                    //draw test button if needed
                    if (hasTest)
                    {
                        this.button2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button2.Location = new System.Drawing.Point(735, 635);
                        this.button2.Name = "button2";
                        this.button2.Size = new System.Drawing.Size(99, 25);
                        this.button2.TabIndex = 0;
                        this.button2.TabStop = false;
                        this.button2.Text = "Test";
                        this.button2.UseVisualStyleBackColor = true;
                        //this.button2.Hide();
                    }
                    else
                    {
                        this.button3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button3.Location = new System.Drawing.Point(735, 635);
                        this.button3.Name = "button3";
                        this.button3.Size = new System.Drawing.Size(99, 25);
                        this.button3.TabIndex = 0;
                        this.button3.TabStop = false;
                        this.button3.Text = "Submit";
                        this.button3.UseVisualStyleBackColor = true;
                        //this.button3.Hide();
                    }
                }
                //handles needing four columns
                else if (i > 50 && i < 67)
                {
                    // draw form
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    //this.ClientSize = new System.Drawing.Size(1060, 680);

                    this.Name = "testForm";
                    this.Text = "Test Form";
                    this.ResumeLayout(false);
                    //draw return button

                    //draw labels for each part
                    Label lbl = new Label();
                    lbl.Name = "label" + i.ToString();
                    lbl.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.ForeColor = System.Drawing.Color.White;
                    lbl.BackColor = System.Drawing.Color.Red;
                    lbl.Location = new System.Drawing.Point(700, 50 + ((i - 2) * 30) - 1440);
                    lbl.Size = new System.Drawing.Size(200, 21);
                    lbl.TabIndex = 8;
                    lbl.TabStop = false;
                    lbl.Text = workSet[i];
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    //setup event handler for label click
                    lbl.Click += new EventHandler(this.lbl_Click);
                    //add to labels
                    labels.Add(lbl);
                    this.Controls.Add(lbl);
                    //draw test button if needed for assembly
                    if (hasTest)
                    {

                        this.button2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button2.Location = new System.Drawing.Point(950, 635);
                        this.button2.Name = "button2";
                        this.button2.Size = new System.Drawing.Size(99, 25);
                        this.button2.TabIndex = 0;
                        this.button2.TabStop = false;
                        this.button2.Text = "Test";
                        this.button2.UseVisualStyleBackColor = true;
                        //this.button2.Hide();
                    }
                    else
                    {
                        this.button3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.button3.Location = new System.Drawing.Point(950, 635);
                        this.button3.Name = "button3";
                        this.button3.Size = new System.Drawing.Size(99, 25);
                        this.button3.TabIndex = 0;
                        this.button3.TabStop = false;
                        this.button3.Text = "Submit";
                        this.button3.UseVisualStyleBackColor = true;
                        //this.button3.Hide();
                    }
                }
            }
            //after everything is drawn, create the event handler for a test button click             
            this.button2.Click += new System.EventHandler(this.button2_Click);
            //after everything is drawn, create the event handler for a submit button click             
            this.button3.Click += new System.EventHandler(this.button3_Click);

            BarcodeEntryTextBox.Focus();
        }





        /*
        *handles logic for a test button click. only launches test when all parts assembled (green)
        *and a valid string exists in the SN, BN, WO text box. for now these are stored in string 
        *arrays, may need to parse from other file (maybe database) so it's easier to update.
        */
        private void button2_Click(object sender, EventArgs e)
        {
            ////counts number of labels that are green
            //int colorCount = 0;

            ////iterate through labels and update colorCount for each one that is green 
            //foreach (Label lbl in labels)
            //{
            //    if (lbl.BackColor == System.Drawing.Color.Green)
            //    {
            //        colorCount++;
            //    }
            //}
            ////if all labels are green and valid fields, run test
            ////if ((colorCount == (count - 3)))
            //if (OKToWrite == true) 
            //{
            //    try
            //    {
            //            // insert();
            //            // insertTest();

            //            //if (hasTest)
            //            //{ 
            //            //    button2.Show();
            //            //}
            //            //else
            //            //{
            //            //    button3.Show();
            //            //}

            //            PassDataToDB();

            //        //dbConn.Close();
            //        MessageBox.Show("TEST LAUNCHED.");
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Error adding to database.");
            //    }

            //    //have to clear these variables so that when we go to start another assembly, 
            //    //false positives dont exist
            //    tb1Text = null;
            //    tb2Text = null;
            //    tb3Text = null;
            //    this.Hide();


            //}
            //else
            //{

            //    Label lbl = new Label();
            //    lbl.Name = "errorLabel";
            //    lbl.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    lbl.ForeColor = System.Drawing.Color.Red;
            //    lbl.Location = new System.Drawing.Point((this.Width / 2) - 190, this.Height - 65);
            //    lbl.Size = new System.Drawing.Size(500, 21);
            //    lbl.TabIndex = 8;
            //    lbl.TabStop = false;
            //    lbl.Text = "All parts and fields need to be assembled/valid to launch a test.";
            //    lbl.BorderStyle = BorderStyle.None;

            //    this.Controls.Add(lbl);
            //}

        }

        /*
         * handles logic for a sumbit button click. only submits data to data base when all parts assembled (green)
         *and a valid string exists in the SN, BN, WO text box. for now these are stored in string 
         *arrays, may need to parse from other file (maybe database) so it's easier to update.
         * Identical to button2_Click, but doesnt launch a test
         */
        private void button3_Click(object sender, EventArgs e)
        {
            //counts number of labels that are green
            int colorCount = 0;

            //iterate through labels and update colorCount for each one that is green 
            foreach (Label lbl in labels)
            {
                if (lbl.BackColor == Color.Green)
                {
                    colorCount++;
                }
            }
            //if all labels are green and valid fields, run test
            //if ((colorCount == (count - 3)))
            //if (colorCount == totalQtyToScan)
            if (OKToWrite == true)
            {

                //try
                //{
                //    // insert();
                //    dbConn.Close();
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("Error adding to database.");
                //}

                PassDataToDB();

                //have to clear these variables so that when we go to start another assembly, 
                //falsely valid data doesnt exist
                tb1Text = null;
                tb2Text = null;
                tb3Text = null;
                this.Hide();

            }
            else
            {

                Label lbl = new Label();
                lbl.Name = "errorLabel";
                lbl.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.ForeColor = System.Drawing.Color.Red;
                lbl.Location = new System.Drawing.Point((this.Width / 2) - 190, this.Height - 65);
                lbl.Size = new System.Drawing.Size(500, 21);
                lbl.TabIndex = 8;
                lbl.TabStop = false;
                lbl.Text = "All parts and fields need to be assembled/valid to launch a test.";
                lbl.BorderStyle = BorderStyle.None;

                this.Controls.Add(lbl);
            }

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

        private void SNEntryTextBox_TextChanged(object sender, EventArgs e)
        {
            //The timer is added to the form
            InputTxtBoxTimer.Start();
        }




        private void InputTxtBoxTimer_Tick_1(object sender, EventArgs e)
        {

            InputTxtBoxTimer.Stop();
            //int numberOfLabelsNeeded = count - 3;

            // if this sn has not been found in db

            int numberOfLabelsNeeded = qtyList.Sum();
            

            //MessageBox.Show("numberOfLabelsNeeded = " + numberOfLabelsNeeded);
            //MessageBox.Show("numberOfLabels = " + numberOfLabels);

            string ScannedInput = BarcodeEntryTextBox.Text;

            string[] returnStrArray;

            returnStrArray = ParseBarcode(ScannedInput);

            string PNStr = returnStrArray[0];
            string SNStr = returnStrArray[1];
            string LOTStr = returnStrArray[2];



            Console.WriteLine("PNStr = " + PNStr);
            if (PNStr != "NULL")
            {


                for (int workSetIndex = 0; workSetIndex < workSet.Length; workSetIndex++)
                {
                    //MessageBox.Show("workSetIndex = " + workSetIndex);
                    
                    //if (workSet[workSetIndex] == PNStr)
                    if (Equals(workSet[workSetIndex],PNStr))
                    {
                        //Console.WriteLine("the text matches");
                        //MessageBox.Show("The text matches");
                    }

                }

                BarcodeEntryTextBox.Enabled = false;

                List<Label> lbls = Controls.OfType<Label>().ToList();
                bool FoundAMatch = false;
                foreach (var lbl in lbls)
                {
                    //Console.WriteLine("Iterating in foreach lbl loop");
                    //Used to upper to error check label string
                    if (string.Compare(lbl.Text.ToUpper().ToString(), PNStr) == 0)
                    {
                        //qtyScannedList[indexofPNStr]++
                        if (partnumList.Contains(PNStr))
                        {                  
                            int PNindex1 = partnumList.IndexOf(PNStr);
                            int QTYscanned = qtyScannedList[PNindex1];
                            qtyScannedList[PNindex1]++;

                            //modify count
                            int totalQty;
                            int indxToMod = PNindex1 + 3;
                            string labelToMod = "qtylabel" + indxToMod.ToString();
                            //MessageBox.Show("labelToMod = " + labelToMod);
                            lookupTable.TryGetValue(PNStr, out totalQty);
                            var control = this.Controls.OfType<Label>().FirstOrDefault(c => c.Name == labelToMod);
                            if (qtyScannedList[PNindex1] <= totalQty)
                            {
                                control.Text = qtyScannedList[PNindex1] + "/" + totalQty;
                            }
                            
                            
                        } 
                        else
                        {
                            MessageBox.Show("PNStr " + " not found in list");
                        }

                        int qtyNeeded;
                        int PNindex = partnumList.IndexOf(PNStr);
                        lookupTable.TryGetValue(PNStr, out qtyNeeded);
                        if (qtyScannedList[PNindex] <= qtyNeeded)
                        {
                            //MessageBox.Show("Incremeting label count");
                            numberOfLabels++;
                            PNArray[CountTracker] = PNStr;
                            LotArray[CountTracker] = LOTStr;
                            SNArray[CountTracker] = SNStr;
                            CountTracker++;
                        }
                        if (qtyScannedList[PNindex] == qtyNeeded)
                        {
                            lbl.BackColor = Color.Green;
                        }
                        if (qtyScannedList[PNindex] > qtyNeeded)
                        {
                            MessageBox.Show("Done scanning this part number");
                        }


                            Console.WriteLine("FoundAMatch = true");
                        FoundAMatch = true;
                        break;

                    }
                    //if (lbl.BackColor == Color.Green)
                    //{
                    //    numberOfLabels++;
                    //}
                }

                if (FoundAMatch == false)
                {
                    Console.WriteLine("Wrong Part Number Scanned!");
                    if (MyGlobals.InEnglish == true)
                    {
                        MessageBox.Show("Wrong Part Number Scanned!");
                    } else
                    {
                        MessageBox.Show("Número de pieza incorrecta escaneado!");
                    }
                }

                BarcodeEntryTextBox.Clear();
                BarcodeEntryTextBox.Focus();

                if (numberOfLabels == numberOfLabelsNeeded)
                {
                    //Code neeeded here to write to database and close window
                    Console.WriteLine("All PNs scanned!");
                    InputTxtBoxTimer.Stop();
                    BarcodeEntryTextBox.Enabled = false;
                    OKToWrite = true;
                    button3.Focus(); // Put focus to SUBMIT button when all PNs are scanned
                    //PassDataToDB();
                    //ReadCertData();

                }

                if (numberOfLabels != numberOfLabelsNeeded)
                {
                    BarcodeEntryTextBox.Enabled = true;
                    BarcodeEntryTextBox.Focus();
                }



            }

            
        }




        private void SNScanBox_TextChanged(object sender, EventArgs e)
        {
            SNScanBoxTimer.Start();
            


            ////------------------------------------------------------------
            //if (SNObtained == false)
            //{
            //    SerialNumber = SNScanBox.Text;
            //    SNScanBoxTimer.Stop();


            //    foreach (string str in MyGlobals.CountingSerialNumbers)
            //    {
            //        if (str == SerialNumber)
            //        {
            //            MyGlobals.IsDuplicateSN = true;
            //            MessageBox.Show("This serial number has already been scanned!");
            //            SNScanBox.Text = "";
            //            SNScanBox.Focus();
            //            break;

            //        }

            //    }

            //    if (MyGlobals.IsDuplicateSN == false)
            //    {
            //        MyGlobals.CountingSerialNumbers[MyGlobals.SNCounter] = SerialNumber;
            //        SNObtained = true;
            //        SNScanBox.Enabled = false;
            //        SNEntryTextBox.Enabled = true;
            //        SNEntryTextBox.Focus();
            //    }


            //    //    if (MyGlobals.IsUniqueSN == true)
            //    //    {
            //    //        MyGlobals.CountingSerialNumbers[MyGlobals.SNCounter] = SerialNumber;
            //    //        SNObtained = true;
            //    //        SNScanBox.Enabled = false;
            //    //        SNEntryTextBox.Enabled = true;
            //    //        SNEntryTextBox.Focus();
            //    //    }
            //    //    else
            //    //    {
            //    //        MessageBox.Show("This serial number has already been scanned!");
            //    //        MyGlobals.IsUniqueSN = false;

            //    //    }
            //    //}



            //   // SNScanBoxTimer.Start();

            //}
            ////-----------------------------------------------------------------------


        }

        private void SNScanBoxTimer_Tick(object sender, EventArgs e)
        {
            bool DoAddStr = false;
            SNScanBoxTimer.Stop();
            Console.WriteLine("SNObtained variable = " + SNObtained.ToString());

            if (FirstTimeThrough == true)
            {
                SerialNumber = SNScanBox.Text;
                SNScanBoxTimer.Stop();

                // Check database to see if this serial number has already been built
                //string testString = "SELECT [Subassembly_SN] FROM Subassembly_Info WHERE Subassembly_SN = '" + SerialNumber + "'";
                string testString = "SELECT [Subassembly_SN] FROM Subassembly_Info";
                SqlCommand checkComm = new SqlCommand(testString, dbConn);
                SqlDataReader reader1 = checkComm.ExecuteReader();
                while(reader1.Read())
                {
                    if (reader1["Subassembly_SN"].ToString().Equals(SerialNumber))
                    {
                        reader1.Close();
                        string showthis;
                        if (MyGlobals.InEnglish == true)
                        {
                            showthis = MyGlobals.SNAlreadyScannedEng;
                        }
                        else
                        {
                            showthis = MyGlobals.SNAlreadyScannedSpn;
                        }
                        MessageBox.Show(showthis);
                        SNScanBox.Clear();
                        SNScanBoxTimer.Stop();
                        SNScanBox.Focus();
                        SNObtained = false;
                        DoAddStr = false;
                        break;
                    }
                    else
                    {
                        DoAddStr = true;
                    }
                }
                reader1.Close();
                //here

                if (DoAddStr == true)
                {
                    SNObtained = true;
                    BarcodeEntryTextBox.Enabled = true;
                    BarcodeEntryTextBox.Focus();
                }

            }


            
        }

        public void PassDataToDB()
        {
            //MessageBox.Show("We are now passing data to DB");

            // From a parsed barcode:
            // The returned string array is always in the order {PN, SN, LOT}

            // Component_PN  = "PN"
            // Component_SN  = "SN"
            // Component_LOT = "LOT"

            // [Subassembly_SN] is the Serial Number scanned from testForm, 
            // which is the serial number of whatever you are building from the work order

            //if (input != "03770-1101-0001") {

            SerialNumber = SNScanBox.Text;

            if ((input.ToUpper().Contains("PH") == false) && (input.ToUpper().Contains("DW") == false))
            {
                //MessageBox.Show("NOT pH FINAL ASSY");
                //if (true) { 
                // For a sub-assembly built from raw parts,
                // populate table with Component_Part_Number/Component_Lot and list with Subassembly_SN
                // The lots are stored with the part numbers
                //      The part numbers are stored in the array PNArray 
                //      The lot numbers are stored in the array LotArray
                // [Subassembly_SN] is the serial number from testForm,
                // which is the serial number you are building for the work order
                // [Component_SN] is a serial number that comes along for the ride from building
                // your current part using another subassembly
                // [Component_Part_Number] is used whether the current part is a raw part or subassembly
                // [Component_Lot] is only used with raw parts
                // Sensor_ID is set to 0 for now, still figuring out how to link completed sensor to subassemblies

                for (int i = 0; i < CountTracker; i++)
                {
                    Console.WriteLine("Serial Number from Work Order = " + SerialNumber);
                    Console.WriteLine("PN = " + PNArray[i]);
                    Console.WriteLine("Lot = " + LotArray[i]);
                    Console.WriteLine("Component SN =" + SNArray[i]);

                    string InsertString = "INSERT INTO Subassembly_Info " +
                        "([Subassembly_SN],[Component_SN],[Component_Lot],[Component_Part_Number],[Work_Order],[Part_Number_To_Build])" +
                        "Values (" + "'" + SerialNumber + delimiterchar + SNArray[i] + delimiterchar + LotArray[i] + delimiterchar + PNArray[i] + delimiterchar + wo + delimiterchar + input + "'" + ")";

                    Console.WriteLine(InsertString);

                    SqlCommand InsertDataCmd = new SqlCommand(InsertString, dbConn);
                    InsertDataCmd.ExecuteNonQuery();

                }
            }
            else
            {
                // For final sensor (if input == "03770-1101-0001")
                for (int i = 0; i < CountTracker; i++)
                {
                    Console.WriteLine("Final Sensor SN = " + SerialNumber);
                    Console.WriteLine("Subassembly SN =" + SNArray[i]);
                    Console.WriteLine("Work Order = " + wo);

                    string InsertString2 = "INSERT INTO Final_Sensor_Info" +
                       "([Final_Sensor_SN],[Subassembly_SN],[Work_Order])" +
                       "Values(" + "'" + SerialNumber + delimiterchar + SNArray[i] + delimiterchar + wo + "'" + ")";
                    Console.WriteLine(InsertString2);

                    SqlCommand InsertDataCmd = new SqlCommand(InsertString2, dbConn);
                    InsertDataCmd.ExecuteNonQuery();

                    string InsertString = "INSERT INTO Subassembly_Info " +
                    "([Subassembly_SN],[Component_SN],[Component_Lot],[Component_Part_Number],[Work_Order],[Part_Number_To_Build])" +
                    "Values (" + "'" + SerialNumber + delimiterchar + SNArray[i] + delimiterchar + LotArray[i] + delimiterchar + PNArray[i] + delimiterchar + wo + delimiterchar + input + "'" + ")";

                    Console.WriteLine(InsertString);

                    SqlCommand InsertDataCmd1 = new SqlCommand(InsertString, dbConn);
                    InsertDataCmd1.ExecuteNonQuery();

                }
            }
        }

        public static string[] ReadCertData()
        {
            int i = 0;
            string[] Sensor_SN_array = new string[100];
            string[] Work_Order_array = new string[100];
            string[] Part_Number_array = new string[100];
            string[] Sensor_Subassy_SN_array = new string[100];


            string SelectString1 = "SELECT * " +
                "FROM [Sensor_Info]" +
                "WHERE Sensor_SN = '12345'"
                ;

            SqlCommand ReadCmd = new SqlCommand(SelectString1, dbConn);

            SqlDataReader reader = ReadCmd.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine("Reader has rows!");
            }
            else
            {
                Console.WriteLine("Reader has nothing :(");
            }

            while (reader.Read())
            {
                Console.WriteLine("Starting i = " + i);
                Sensor_SN_array[i] = reader.GetString(Sensor_SN_SensorInfo);
                Work_Order_array[i] = reader.GetString(Work_Order_SensorInfo);
                Part_Number_array[i] = reader.GetString(Part_Number_SensorInfo);
                i++;
                Console.WriteLine("After ++, i=" + i);
            }

            Console.WriteLine("Sensor_SN" + "\t" + "Work_Order" + "\t" + "Part_Number");
            for (int j = 0; j < i; j++)
            {
                Console.WriteLine(Sensor_SN_array[j] + "\t" + Work_Order_array[j] + "\t" + Part_Number_array[j]);
            }



            string[] returnArray = { "NULL" };
            return returnArray;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("somebody hit exit button");

            DialogResult dr = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.OKCancel);

            switch (dr)
            {
                case DialogResult.OK:
                    Application.Exit();
                    break;

                case DialogResult.Cancel:
                    break;
            }
        }
           // Application.Exit();
        

        //-----------------------------------------------------------------------------------------------
        //                                           Junk functions
        //-----------------------------------------------------------------------------------------------


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InputTxtBoxTimer_Tick(object sender, EventArgs e)
        {

            //InputTxtBoxTimer.Stop();
            //// SRY need to add in functionality to recognize PN alone from scanned barcode

            //string ScannedInput = SNEntryTextBox.Text;

            //string[] returnStrArray;

            //returnStrArray = ParseBarcode(ScannedInput);

            //string PNStr = returnStrArray[0];

            //for (int i = 0; i < returnStrArray.Length; i++)
            //{
            //    Console.WriteLine("writing ReturnStrArray...");
            //    Console.WriteLine(returnStrArray[i]);
            //}

            //Console.WriteLine("PNStr = " + PNStr);

            //for (int workSetIndex = 0; workSetIndex < workSet.Length; workSetIndex++)
            //{
            //    if (workSet[workSetIndex] == PNStr)
            //    {
            //        Console.WriteLine("the text matches");
            //    }

            //}

            ////To count if you are done entering info
            //int numberOfLabels = 0;


            //SNEntryTextBox.Enabled = false;  //not sure if this is needed


            //List<Label> lbls = Controls.OfType<Label>().ToList();
            //foreach (var lbl in lbls)
            //{
            //    //Add function to parse label string here

            //    //Used to upper to error check label string
            //    if (string.Compare(lbl.Text.ToUpper().ToString(), PNStr) == 0)
            //    {
            //        //Add code as needed


            //        //Change the back color here
            //        lbl.BackColor = Color.Green;
            //    }
            //    if (lbl.BackColor == Color.Green)
            //    {
            //        numberOfLabels++;
            //    }

            //}


            //SNEntryTextBox.Clear();

            ////Pseudo-code for if done
            ////Check to see if all labels have been scanned in
            ////if (numberOfLabels == numberOfLabelsNeeded)
            ////{
            ////Code neeeded here to write to database and close window
            ////}


            //SNEntryTextBox.Enabled = true;   //not sure if this is needed
        }

        private void SNEntryTextBox_Click(object sender, System.EventArgs e)
        {

        }

        private void testForm_Load(object sender, EventArgs e)
        {

        }

        private void InputTxtBox_TextChanged(object sender, EventArgs e)
        {
            //The timer is added to the form
            InputTxtBoxTimer.Start();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        /*
        *change label text to green to indicate it's been assembled. Upon click now,
        *will be triggerred by a scan in the future
        */
        private void lbl_Click(object sender, System.EventArgs e)
        {
            //Label lbl = (Label)sender;
            //lbl.ForeColor = Color.White;
            //lbl.BackColor = Color.Green;
            //clickCount++;

            //if (clickCount == (count - 3))
            //{
            //    button2.BackColor = Color.Pink;
            //}

        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI f1 = new MainGUI();
            f1.ShowDialog();
            this.Close();
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{



        //}


    }
    //public static class MyGlobals
    //{
    //    public static string[] CountingSerialNumbers = new string[100];
    //    public static int SNCounter = 0;
    //    public static bool IsDuplicateSN = false;

    //}

    //// Check to see if the current SN is already in the list
    //foreach (string str in MyGlobals.CountingSerialNumbers)
    //{
    //    // If already in the list, display message to user and boot out of this input session to try again
    //    if (string.Equals(str, SerialNumber))
    //    {
    //        //MyGlobals.IsDuplicateSN = true;
    //        IsDuplicateSN = true;
    //        string showthis;
    //        if (MyGlobals.InEnglish == true)
    //        {
    //            showthis = MyGlobals.SNAlreadyScannedEng;
    //        }
    //        else
    //        {
    //            showthis = MyGlobals.SNAlreadyScannedSpn;
    //        }
    //        MessageBox.Show(showthis);
    //        SNScanBox.Clear();
    //        SNScanBoxTimer.Stop();
    //        SNScanBox.Focus();
    //        SNObtained = false;
    //        DoAddStr = false;
    //        Console.WriteLine("MyGlobals.IsDuplicateSN is " + IsDuplicateSN);
    //        break;

    //    }

    //    else

    //    {
    //        Console.WriteLine("Strings were not equal!!");
    //        Console.WriteLine("SerialNumber: " + SerialNumber);
    //        Console.WriteLine("str: " + str);
    //        Console.WriteLine("Before adding string, MyGlobals.SNCounter = " + MyGlobals.SNCounter);
    //        //MyGlobals.CountingSerialNumbers[MyGlobals.SNCounter] = SerialNumber;
    //        Console.WriteLine("After adding string, MyGlobals.SNCounter = " + MyGlobals.SNCounter);
    //        DoAddStr = true;
    //        //SNObtained = true;
    //        //SNScanBox.Enabled = false;
    //        //SNEntryTextBox.Enabled = true;
    //        //SNEntryTextBox.Focus();
    //        //MyGlobals.SNCounter++;
    //    }



    //}

}
