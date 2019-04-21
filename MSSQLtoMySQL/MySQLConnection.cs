using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MSSQLtoMySQL
{
    class MySQLConnection
    {
        private MySqlConnection myConnection;
        MySqlDataReader readermsql;
        private string lastError;
        private string lastSQLCommand;
        private string ConnectionPath;

        public MySQLConnection() { }

        public bool Connect(string ServerName, string Port, string DatabaseName, string UserName, string Password)
        {
            try
            {
                ConnectionPath = "server="+ ServerName+"; port="+Port+";uid="+UserName+";pwd="+Password+";";
                myConnection = new MySql.Data.MySqlClient.MySqlConnection();
                myConnection.ConnectionString = ConnectionPath;
                myConnection.Open();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException SQLEx)
            {
                lastError = SQLEx.Message;
                return false;
            }
        }

        public bool ExecuteNonQuery(string SQLCommand)
        {
            try
            {
                MySqlCommand cmd = myConnection.CreateCommand();
                cmd.CommandText = SQLCommand;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException SQLEx)
            {
                lastError = SQLEx.Message;
                lastSQLCommand = SQLCommand;
                return false;
            }

        }

        public string getLastError()
        {
            return lastError;
        }

        public string getLastSQLCommand()
        {
            return lastSQLCommand;
        }

        public void Close()
        {
            myConnection.Close();
        }
    }
}
