using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MSSQLtoMySQL
{
    class conversionHelper
    {
        MSSQLConnection SQLConn = new MSSQLConnection();
        DataSet reader;
        DataTable dt;
        public conversionHelper(string server, string database)
        {
            SQLConn.Connect(server, database);
        }
        public conversionHelper(string server, string database, string user, string pass )
        {
            SQLConn.Connect(server, database, user, pass);
        }

        public string convertSchema(string objectType, string objectName,List<string>tableNames,string separator)
        {
            switch(objectType)
            {
                case "TABLE": return processTableSchema(objectName,separator); break;
                case "VIEW": return processViewSchema(objectName,tableNames,separator); break;
                default:return "";
            }
        }

        public string makeIndex(string objectName,string _separator)
        {
            string tempQuery = "";
            DataSet readerTemp;
            DataTable dtTemp;

            reader = SQLConn.ExecuteQuery("SELECT TableName = t.name, IndexName =ind.name, ind.is_unique, ind.is_primary_key FROM sys.indexes ind INNER JOIN sys.index_columns ic ON ind.object_id = ic.object_id and ind.index_id = ic.index_id INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id INNER JOIN sys.tables t ON ind.object_id = t.object_id where t.name='" + objectName + "' group by t.name,ind.name,ind.is_unique,ind.is_primary_key ORDER BY t.name, ind.name; ");
            dt = reader.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                if ((Convert.ToBoolean(row["is_unique"].ToString())) && (Convert.ToBoolean(row["is_primary_key"].ToString())))
                { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD CONSTRAINT " + row["IndexName"].ToString() + " PRIMARY KEY ("; }

                if ((Convert.ToBoolean(row["is_unique"].ToString())) && (!Convert.ToBoolean(row["is_primary_key"].ToString())))
                { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD CONSTRAINT " + row["IndexName"].ToString() + " UNIQUE ("; }

                if ((!Convert.ToBoolean(row["is_unique"].ToString())) && (Convert.ToBoolean(row["is_primary_key"].ToString())))
                { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD CONSTRAINT " + row["IndexName"].ToString() + " PRIMARY KEY("; }

                if ((!Convert.ToBoolean(row["is_unique"].ToString())) && (!Convert.ToBoolean(row["is_primary_key"].ToString())))
                { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD INDEX ("; ; }

                //get the columns for this key
                readerTemp = SQLConn.ExecuteQuery("SELECT TableName = t.name, IndexName = ind.name, IndexId = ind.index_id, ColumnId = ic.index_column_id, ColumnName = col.name, ind.*, ic.*, col.* FROM sys.indexes ind INNER JOIN sys.index_columns ic ON ind.object_id = ic.object_id and ind.index_id = ic.index_id INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id INNER JOIN sys.tables t ON ind.object_id = t.object_id where t.name='" + objectName + "' ORDER BY t.name, ind.name, ind.index_id, ic.index_column_id;");
                dtTemp = readerTemp.Tables[0];
                foreach (DataRow rowTemp in dtTemp.Rows)
                {
                    tempQuery += "`" + rowTemp["ColumnName"].ToString() + "`,";
                }
                //remove trailing comma
                tempQuery = tempQuery.Trim().TrimEnd(',') + ");" + _separator;
                
            }
            return tempQuery;
        }

        public string makeIdentity(string objectName, string _separator)
        {
            string tempQuery = "";
            reader = SQLConn.ExecuteQuery("select COLUMN_NAME, TABLE_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + objectName + "' and COLUMNPROPERTY(object_id(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 order by TABLE_NAME ");
            dt = reader.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                tempQuery = "ALTER TABLE `" + row["TABLE_NAME"].ToString() + "` ADD INDEX (`" + row["COLUMN_NAME"].ToString() + "`); ALTER TABLE `" + row["TABLE_NAME"].ToString() + "` MODIFY COLUMN `" + row["COLUMN_NAME"].ToString() + "` " + convertDataType(row["DATA_TYPE"].ToString(), row["CHARACTER_MAXIMUM_LENGTH"].ToString(), row["IS_NULLABLE"].ToString()) + " auto_increment;" + _separator;
                
            }
            return tempQuery;
        }

        public string makeForeignKey(string objectName, string _separator)
        {
            Random random = new Random();
            string tempQuery = "";
            reader = SQLConn.ExecuteQuery("SELECT f.name AS foreign_key_name ,OBJECT_NAME(f.parent_object_id) AS table_name ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name ,OBJECT_NAME(f.referenced_object_id) AS referenced_object ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name ,is_disabled ,delete_referential_action_desc ,update_referential_action_desc FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id WHERE f.parent_object_id = OBJECT_ID('" + objectName + "'); ");
            dt = reader.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                tempQuery = "ALTER TABLE `" + row["referenced_object"].ToString() + "` ADD INDEX (`" + row["referenced_column_name"].ToString() + "`); ALTER TABLE `" + row["table_name"].ToString() + "` ADD CONSTRAINT " + row["foreign_key_name"].ToString() + " FOREIGN KEY(`" + row["constraint_column_name"].ToString() + "`) REFERENCES `" + row["referenced_object"].ToString() + "` (`" + row["referenced_column_name"].ToString() + "`);" + _separator;
            }
            return tempQuery;
        }

        public List<string> copyData(string objectType, string objectName,int startRow,Form1 formulario,string _separator)
        {
            string tempQuery;
            List<string> arrayQuery=new List<string>();
            tempQuery = "INSERT INTO `" + objectName + "` VALUES ("+ _separator;
            //obtain table data
            reader = SQLConn.ExecuteQuery("SELECT * FROM [" + objectName + "]");
            dt = reader.Tables[0];
            int counter = 1;
            foreach (DataRow row in dt.Rows)
            {
                tempQuery = "INSERT INTO `" + objectName + "` VALUES (" + _separator;
                formulario.UpdateStatus("Getting data " + counter + " of " + dt.Rows.Count, 3, startRow);

                for (int q = 0; q < row.Table.Columns.Count; q++)
                {
                    if (isNumber(row[q].ToString().Replace('\'', ' ').Replace('\"', ' ')))
                    {
                        tempQuery += "'" + row[q].ToString().Replace('\'', ' ').Replace('\"', ' ').Replace(',', '.') + "'," + _separator;
                    }
                    else if (isDate(row[q].ToString().Replace('\'', ' ').Replace('\"', ' ')))
                    {
                        tempQuery += "'" + convertDate(row[q].ToString().Replace('\'', ' ').Replace('\"', ' ')) + "'," + _separator;
                    }

                    else
                    {
                        tempQuery += "'" + row[q].ToString().Replace('\'', ' ').Replace('\"', ' ') + "'," + _separator;
                    }
                }

                //remove trailing comma
                tempQuery = tempQuery.Trim().TrimEnd(',') + ");";
                arrayQuery.Add(tempQuery);
                counter++;
            }
            return arrayQuery;
        }

        private string processTableSchema(string tableName,string _separator)
        {
            string tempQuery = "";
            tempQuery = "CREATE TABLE `" + tableName + "` ("+_separator;
            //obtain table schema
            reader = SQLConn.ExecuteQuery("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' ORDER BY ORDINAL_POSITION ASC");
            dt = reader.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                tempQuery += "`" + row["COLUMN_NAME"].ToString() + "` " + convertDataType(row["DATA_TYPE"].ToString(), row["CHARACTER_MAXIMUM_LENGTH"].ToString(), row["IS_NULLABLE"].ToString()) + ","+_separator;
            }
            //remove trailing comma
            tempQuery = tempQuery.Trim().TrimEnd(',') + ");";
            tempQuery = System.Text.RegularExpressions.Regex.Replace(tempQuery, @"/\*([^*]|[\r\n]|(\*([^/]|[\r\n])))*\*/", "", System.Text.RegularExpressions.RegexOptions.Singleline);
            return tempQuery;
        }

        private string processViewSchema(string viewName,List<string>tableNames,string _separator)
        {
            string tempQuery;
            reader = SQLConn.ExecuteQuery("SELECT object_definition (OBJECT_ID(N'" + viewName + "'))  as 'Text'");
            dt = reader.Tables[0];

            tempQuery = "";
            foreach (DataRow row in dt.Rows)
            {
                tempQuery += row["Text"].ToString().Trim();
            }
            tempQuery = tempQuery.Replace("dbo.", "").Replace("[", "`").Replace("]", "`").Replace("\n", " ").Replace("\r", " ");
            if (tempQuery.IndexOf(" TOP ") != -1)
            {
                string topCantidad = "";
                string restultStr = string.Empty;
                string[] strWords = tempQuery.Split();

                //as we start this with 1 instead of 0, it will ignore first word
                for (int k = 0; k < strWords.Length; k++)
                {
                    if (strWords[k] == "TOP")
                    {

                        k++;
                        topCantidad = strWords[k];
                    }
                    else
                    {
                        restultStr += strWords[k] + " ";
                    }
                }
                restultStr += " LIMIT " + topCantidad.TrimStart('(').TrimEnd(')');
                tempQuery = restultStr;
            }
            tempQuery = tempQuery.Replace("WITH TIES", "");
            tempQuery += ";";
            tempQuery = System.Text.RegularExpressions.Regex.Replace(tempQuery, @"/\*([^*]|[\r\n]|(\*([^/]|[\r\n])))*\*/", "", System.Text.RegularExpressions.RegexOptions.Singleline);

           

            System.Text.RegularExpressions.Regex singleSpacify = new System.Text.RegularExpressions.Regex(" {2,}", System.Text.RegularExpressions.RegexOptions.Compiled);
            tempQuery = singleSpacify.Replace(tempQuery, _separator);
            tempQuery = tempQuery.ToLower();
            foreach (string table in tableNames)
            {
                tempQuery = tempQuery.Replace(" " + table.ToLower(), " " + table).Replace(table.ToLower() + " ", table + " ").Replace(" " + table.ToLower() + ".", " " + table + ".").Replace("\n"+table.ToLower(), "\n" + table);
                tempQuery = tempQuery.Replace("`" + table.ToLower() + "`", "`" + table + "`");
            }


            return tempQuery;
        }

        private bool isNumber(string v)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(v, @"^-?\d*[0-9]?(|.\d*[0-9]|,\d*[0-9])?$");
        }



        private string convertDate(string v)
        {
            DateTime dt;
            DateTime.TryParse(v, out dt);
            return String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        }

        private bool isDate(string v)
        {
            string strDate = v.ToString();
            try
            {
                DateTime dt;
                DateTime.TryParse(strDate, out dt);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }


        private string convertDataType(string type, string max_length, string isnullable)
        {
            //ToDo: support more datatypes, check data types length
            string isnullableReturn = "";
            if (isnullable == "NO") { isnullableReturn = "NOT NULL"; }
            switch (type)
            {
                case "bit":
                    return "TINYINT(1) " + isnullableReturn;
                    break;
                case "tinyint":
                    return "TINYINT " + isnullableReturn;
                    break;
                case "smallint":
                    return "SMALLINT " + isnullableReturn;
                case "int":
                    return "INT " + isnullableReturn;
                case "bigint":
                    return "BIGINT " + isnullableReturn;
                case "float":
                    if (max_length.Trim() == "")
                    { return "FLOAT " + isnullableReturn; }
                    if (int.Parse(max_length) < 25)
                    { return "FLOAT(" + max_length + ") " + isnullableReturn; }
                    else { return "DOUBLE(" + max_length + ") " + isnullableReturn; }
                    break;
                case "smallmoney":
                case "money":
                    if (max_length.Trim() == "")
                    { return "DOUBLE " + isnullableReturn; }
                    else
                    { return "DOUBLE(" + max_length + ") " + isnullableReturn; }

                    break;
                case "decimal":
                    if (max_length.Trim() == "")
                    { return "DECIMAL " + isnullableReturn; }
                    else
                    { return "DECIMAL(" + max_length + ") " + isnullableReturn; }
                    break;
                case "datetime":
                case "datetime2":
                    return "DATETIME " + isnullableReturn;
                    break;
                case "date":
                    return "DATE " + isnullableReturn;
                    break;
                case "time":
                case "time2":
                    return "TIME " + isnullableReturn;
                    break;
                case "smalldatetime":
                    return "TIMESTAMP " + isnullableReturn;
                    break;
                case "nchar":
                    return "CHAR " + isnullableReturn;
                    break;
                case "nvarchar":
                    if (int.Parse(max_length) < 0) { return "VARCHAR(255) " + isnullableReturn; }
                    else { return "VARCHAR(" + max_length + ") " + isnullableReturn; }
                    break;
                default:
                    return "VARCHAR(255)";

            }
        }
    }
}
