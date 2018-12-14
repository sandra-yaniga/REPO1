using System;
using System.Data.SqlClient;

namespace ph_GUI_V1 { 
    public class Database_Interact
    {
        public Database_Interact()
        {

            SqlConnection ConnectToDB ()
            {
                // String used to connect to database
                string sConnectionString = "Server = USMTNPMDEAIDB05; Database = SUB_DB_PRACTICE; User Id = sub_db_writer; Password = s&b_wr*t3";
               
                // Object used to connect to database
                SqlConnection objConn = new SqlConnection(sConnectionString);

                // Open connection to database
                objConn.Open();

                // Pass back the connection to the database
                return objConn;
            }

            void DisconnectFromDB ()
            {
                objConn.Close();
            }


            double ReadVal (string strTableName, string columnName)
            {
                string SELECTString = "SELECT columnName FROM strTableName";

            }
        }
    }
}
