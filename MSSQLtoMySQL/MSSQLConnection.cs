using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MSSQLtoMySQL
{
    class MSSQLConnection
    {
        private SqlConnection myConnection;
        private System.Data.DataSet myReader = null;
        private string lastError;
        private string ConnectionPath;

        public MSSQLConnection()
        {

        }

        public bool Connect(string ServerName,string DatabaseName)
        {
            try
            {
                ConnectionPath = "Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";Integrated Security=SSPI;";
                myConnection = new SqlConnection(ConnectionPath);
                myConnection.Open();
                return true;
            }
            catch (SqlException SQLEx)
            {
                lastError = SQLEx.Message;
                return false;
            }
        }

        public bool Connect(string ServerName, string DatabaseName, string UserName,string Password)
        {
            try
            {
                ConnectionPath = "Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password;
                myConnection = new SqlConnection(ConnectionPath);
                myConnection.Open();
                return true;
            }
            catch (SqlException SQLEx)
            {
                lastError = SQLEx.Message;
                return false;
            }
        }

        public string getLastError()
        {
            return lastError;
        }

        public System.Data.DataSet ExecuteQuery(string SQLCommand)
        {
            try
            {

                SqlDataAdapter da = new SqlDataAdapter(SQLCommand, ConnectionPath);
                myReader = new System.Data.DataSet();
                da.Fill(myReader);
                return myReader;
            }
            catch (Exception ex)
            {
                lastError = ex.Message;
                return null;
            }
        }

        public void Close()
        {
            myConnection.Close();
        }

    }
}
