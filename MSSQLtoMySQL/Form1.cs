using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSQLtoMySQL
{
    public partial class Form1 : Form
    {
        MSSQLConnection SQLConn = new MSSQLConnection();
        MySQLConnection MySQLConn = new MySQLConnection();
        DataSet reader;
        DataTable dt;
        private delegate void UpdateStatusDelegate(string message,int status,int row);

        public Form1()
        {
            InitializeComponent();
        }

       
        private void UpdateStatus(string message, int status, int row)
        {
            if (this.lvTasks.InvokeRequired)
            {
                this.Invoke(new UpdateStatusDelegate(this.UpdateStatus), new object[] { message, status,row });
                return;
            }

            switch(status)
            {
                case 0: //success
                this.lvTasks.Items[row].ImageIndex = 0;
                    break;
                case 1: //error, show final page
                    this.lvTasks.Items[row].ImageIndex = 1;
                    stepWizardControl1.NextPage();
                    pictureBox1.Image = imageList1.Images[1];
                    lblEndStatus.Text = "Tasks completed with errors";
                    txtEndInfo.Text = message;
                    break;
                case 2: //progress
                    this.lvTasks.Items[row].ImageIndex = 4;
                    break;
                case 99: //successful end
                    stepWizardControl1.NextPage();
                    pictureBox1.Image = imageList1.Images[0];
                    lblEndStatus.Text = "Tasks completed successfuly!";
                    lblEndDeclaration.Visible = false;
                    txtEndInfo.Visible = false;
                    break;

            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Show AeroWizard GitHub WebPage
            System.Diagnostics.Process.Start("https://github.com/dahall/AeroWizard");
        }

        private void optWindowsAuth_Click(object sender, EventArgs e)
        {
            //If Windows Auth is selected, disable UserName and Password fields
            if(optWindowsAuth.Checked)
            {
                lblOriginUserName.Enabled = false;
                lblOriginPassword.Enabled = false;
                txtOriginUserName.Enabled = false;
                txtOriginPassword.Enabled = false;
            }
        }

        private void optCustomAuth_Click(object sender, EventArgs e)
        {
            //If Custom Auth is selected, enable UserName and Password fields
            //and put focus on UserName
            if (optCustomAuth.Checked)
            {
                lblOriginUserName.Enabled = true;
                lblOriginPassword.Enabled = true;
                txtOriginUserName.Enabled = true;
                txtOriginPassword.Enabled = true;
                txtOriginUserName.Focus();
                txtOriginUserName.SelectAll();
            }
        }

        private void wizardPage2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //Error Control before change to page3
            //reset ErrorProvider
            errorProvider1.Clear();
            //Test for empty (each control)
            if (txtOriginServer.Text.Trim() == "") { errorProvider1.SetError(txtOriginServer, "This field can't be empty!"); e.Cancel=true; }
            if (txtOriginDatabase.Text.Trim() == "") { errorProvider1.SetError(txtOriginDatabase, "This field can't be empty!"); e.Cancel = true; }
            if (optCustomAuth.Checked)
            {
                if (txtOriginUserName.Text.Trim() == "") { errorProvider1.SetError(txtOriginUserName, "This field can't be empty!"); e.Cancel = true; }
                if (txtOriginPassword.Text.Trim() == "") { errorProvider1.SetError(txtOriginPassword, "This field can't be empty!"); e.Cancel = true; }
            }

            //Test connection to origin database
            lblOriginStatus.Visible = true;
            if (!testOriginConnection())
            {
                e.Cancel = true;
                MessageBox.Show(SQLConn.getLastError(), "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblOriginStatus.Visible = false;
        }

        private bool testOriginConnection()
        {
            if (optWindowsAuth.Checked)
            {
                 return SQLConn.Connect(txtOriginServer.Text, txtOriginDatabase.Text);
            }
            else
            {
                return SQLConn.Connect(txtOriginServer.Text, txtOriginDatabase.Text, txtOriginUserName.Text, txtOriginPassword.Text);
            }
        }

        private void wizardPage3_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //Error Control before change to page4
            //reset ErrorProvider
            errorProvider1.Clear();
            //if Export to Database is selected, test upper groupbox
            if(optExportToDatabase.Checked)
            {
                //Test for empty (each control)
                if (txtDestinationServerName.Text.Trim() == "") { errorProvider1.SetError(txtDestinationServerName, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationDatabase.Text.Trim() == "") { errorProvider1.SetError(txtDestinationDatabase, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationUserName.Text.Trim() == "") { errorProvider1.SetError(txtDestinationUserName, "This field can't be empty!"); e.Cancel = true; }
                
                if (txtDestinationPort.Text.Trim() == "") { errorProvider1.SetError(txtDestinationPort, "This field can't be empty!"); e.Cancel = true; }

                //Test connection to destination server
                lblDestinationStatus.Visible = true;
                if (!testDestinationConnection())
                {
                    e.Cancel = true;
                    MessageBox.Show(MySQLConn.getLastError(), "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                lblDestinationStatus.Visible = false;
            }
            else //then Export to File is selected, test lower groupbox
            {
                if (txtDestinationFolder.Text.Trim() == "") { errorProvider1.SetError(txtDestinationFolder, "Please select a folder"); e.Cancel = true; }
            }

        }

        private bool testDestinationConnection()
        {
            return MySQLConn.Connect(txtDestinationServerName.Text,txtDestinationPort.Text, txtDestinationDatabase.Text, txtDestinationUserName.Text, txtDestinationPassword.Text);
        }

        private void optExportToDatabase_Click(object sender, EventArgs e)
        {
            //if Export to Database option is selected, enable upper groupbox and disable lower groupbox
            if(optExportToDatabase.Checked)
            {
                grpExportDatbase.Enabled = true;
                grpExportFile.Enabled = false;
                txtDestinationServerName.Focus();
                txtDestinationServerName.SelectAll();
            }
        }

        private void optExportToFile_Click(object sender, EventArgs e)
        {
            //if Export to File option is selected, enable lower groupbox and disable upper groupbox
            if (optExportToFile.Checked)
            {
                grpExportDatbase.Enabled = false;
                grpExportFile.Enabled = true;
                txtDestinationFolder.Focus();
                txtDestinationFolder.SelectAll();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Show CreativeCommons License
            System.Diagnostics.Process.Start("https://creativecommons.org/licenses/by/3.0/");
        }

        private void btnOriginTestConnection_Click(object sender, EventArgs e)
        {
            lblOriginStatus.Visible = true;
            if (testOriginConnection())
            {
                lblOriginStatus.Visible = false;
                MessageBox.Show("Test Connection was Successful!", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblOriginStatus.Visible = false;
                MessageBox.Show(SQLConn.getLastError(), "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wizardPage4_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            int rowNumber;
            //if dgvObjects is empty, load origin database objects
            if(dgvObjects.Rows.Count==0)
            {
                reader=SQLConn.ExecuteQuery("select * from "+txtOriginDatabase.Text+".INFORMATION_SCHEMA.TABLES order by TABLE_TYPE,TABLE_NAME");
                dt = reader.Tables[0];
                foreach(DataRow row in dt.Rows)
                {
                    //ToDo: support more objects?
                    //ToDo: column 0: show icon instead?
                    switch(row["TABLE_TYPE"].ToString())
                    {
                        case "BASE TABLE":
                            dgvObjects.Rows.Add("TABLE", row["TABLE_NAME"].ToString(), true, true);
                            break;
                        case "VIEW":
                            rowNumber=dgvObjects.Rows.Add("VIEW", row["TABLE_NAME"].ToString(), true, null);
                            dgvObjects.Rows[rowNumber].Cells[3].ReadOnly = true; //views has no data
                            break;
                    }
                }
            }
        }

        private void btnDestinationTestConnection_Click(object sender, EventArgs e)
        {
            lblDestinationStatus.Visible = true;
            if (testDestinationConnection())
            {
                lblDestinationStatus.Visible = false;
                MessageBox.Show("Test Connection was Successful!", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblDestinationStatus.Visible = false; 
                MessageBox.Show(MySQLConn.getLastError(), "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDestinationFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnQuickActionApply_Click(object sender, EventArgs e)
        {
            switch (cmbQuickAction.SelectedIndex)
            {
                case 0: //select all table schema
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                        { dgvObjects.Rows[i].Cells[2].Value = true; }
                    }
                    break;
                case 1: //select all table data
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                        { dgvObjects.Rows[i].Cells[3].Value = true; }
                    }
                    break;
                case 2: //unselect all table schema
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                        { dgvObjects.Rows[i].Cells[2].Value = false; }
                    }
                    break;
                case 3: //unselect all table data
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                        { dgvObjects.Rows[i].Cells[3].Value = false; }
                    }
                    break;
                case 4: //select all view schema
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "VIEW")
                        { dgvObjects.Rows[i].Cells[2].Value = true; }
                    }
                    break;
                case 5: //unselect all view schema
                    for (int i = 0; i < dgvObjects.Rows.Count; i++)
                    {
                        if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "VIEW")
                        { dgvObjects.Rows[i].Cells[2].Value = false; }
                    }
                    break;
            }
        }

        private void wizardPage4_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if(MessageBox.Show("Are you sure to continue with this process?","Continue",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void wizardPage5_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            ListViewItem item;

            wizardPage5.AllowBack = false;
            wizardPage5.AllowNext = false;

            //add tasks to listview
            if (optOverwriteDatabase.Checked)
            {
                item = lvTasks.Items.Add("Deleting MySQL Database: " + txtDestinationDatabase.Text);
                item.SubItems.Add("Waiting...");
            }
            
                item = lvTasks.Items.Add("Creating MySQL Database: " + txtDestinationDatabase.Text);
                item.SubItems.Add("Waiting...");
               

            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value)) //schema
                {
                    item = lvTasks.Items.Add("Creating " + dgvObjects.Rows[i].Cells[0].Value.ToString().ToLower() + " Schema: " + dgvObjects.Rows[i].Cells[1].Value.ToString());
                    item.SubItems.Add("Waiting...");
                }
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[3].Value)) //data
                {
                    item = lvTasks.Items.Add("Copying " + dgvObjects.Rows[i].Cells[0].Value.ToString().ToLower() + " Data: " + dgvObjects.Rows[i].Cells[1].Value.ToString());
                    item.SubItems.Add("Waiting...");
                }
            }

            item = lvTasks.Items.Add("Creating Table Indexes");
            item.SubItems.Add("Waiting...");
            //now we can call the procedures
            System.Threading.Thread procThread = new System.Threading.Thread(this.Process);
            procThread.Start();
        }

        private void Process() // This is the actual method of the thread
        {

            int startRow = 1;
            string tempQuery = "";
            int counter = 0;
            //ToDo: convert SQL Server Database Collation to MySql Database Collation

            //obtaining sql server collation for database:
            //select collation_name from sys.databases where name='yourdbname' 

            if (optOverwriteDatabase.Checked)
            {
                startRow = 2;
                //delete mysql database
                UpdateStatus("Deleting...", 3, 0);
                if (MySQLConn.ExecuteNonQuery("drop database `" + txtDestinationDatabase.Text+ "`;"))
                {
                    UpdateStatus("Success", 0, 0);
                }
                else
                {
                    UpdateStatus(MySQLConn.getLastError(), 1, 0);
                    return;
                }

                //create mysql database
                UpdateStatus("Creating...", 3, 1);
                if (MySQLConn.ExecuteNonQuery("create database `" + txtDestinationDatabase.Text + "`; use `" + txtDestinationDatabase.Text+ "`;"))
                {
                    UpdateStatus("Success", 0, 1);
                }
                else
                {
                    UpdateStatus(MySQLConn.getLastError(), 1, 1);
                    return;
                }
            }
            else
            {
                startRow = 1;
                //create mysql database
                UpdateStatus("Creating...", 3, 0);
                if (MySQLConn.ExecuteNonQuery("create database `" + txtDestinationDatabase.Text + "`; use `" + txtDestinationDatabase.Text+ "`;"))
                {
                    UpdateStatus("Success", 0, 0);
                }
                else
                {
                    UpdateStatus(MySQLConn.getLastError(), 1, 0);
                    return;
                }
            }

            //migrate objects
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                //create schema
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    //ToDo: support more objects?
                    switch (dgvObjects.Rows[i].Cells[0].Value.ToString())
                    {
                        case "TABLE":
                            tempQuery = "CREATE TABLE `" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "` (";
                            //obtain table schema
                            reader = SQLConn.ExecuteQuery("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "' ORDER BY ORDINAL_POSITION ASC");
                            dt = reader.Tables[0];
                            foreach (DataRow row in dt.Rows)
                            {
                                tempQuery += "`" + row["COLUMN_NAME"].ToString() + "` " + convertDataType(row["DATA_TYPE"].ToString(), row["CHARACTER_MAXIMUM_LENGTH"].ToString(), row["IS_NULLABLE"].ToString()) + ",";
                            }
                            //remove trailing comma
                            tempQuery = tempQuery.TrimEnd(',') + ");";
                            //create schema
                            if (MySQLConn.ExecuteNonQuery(tempQuery))
                            {
                                UpdateStatus("Success", 0, startRow);
                            }
                            else
                            {
                                UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                                return;
                            }

                            startRow++;
                            break;
                        case "VIEW":
                            reader = SQLConn.ExecuteQuery("EXEC sp_helptext '" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "';");
                            dt = reader.Tables[0];
                            if(dt.Rows.Count==0)
                            {
                                reader = SQLConn.ExecuteQuery("SELECT object_definition (OBJECT_ID(N'" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "'))  as 'Text'");
                                dt = reader.Tables[0];
                            }
                            tempQuery = "";
                            foreach(DataRow row in dt.Rows)
                            {
                                tempQuery += row["Text"].ToString().Trim() + " ";
                            }
                            tempQuery = tempQuery.Replace("dbo.", "").Replace("[", "`").Replace("]", "`").Replace("\n","").Replace("\r","");
                            //create schema
                            if (MySQLConn.ExecuteNonQuery(tempQuery))
                            {
                                UpdateStatus("Success", 0, startRow);
                            }
                            else
                            {
                                UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                                return;
                            }

                            startRow++;
                            break;
                    }
                }

                //copy data
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    //ToDo: support more objects?
                    switch (dgvObjects.Rows[i].Cells[0].Value.ToString())
                    {
                        case "TABLE":
                            tempQuery = "INSERT INTO `" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "` VALUES (";
                            //obtain table data
                            reader = SQLConn.ExecuteQuery("SELECT * FROM [" + dgvObjects.Rows[i].Cells[1].Value.ToString()+"]");
                            dt = reader.Tables[0];
                            counter = 1;
                            foreach (DataRow row in dt.Rows)
                            {
                                tempQuery = "INSERT INTO `" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "` VALUES (";
                                //lvTasks.Items[startRow].SubItems[1].Text = "Copying data " + counter + " of " + dt.Rows.Count;
                                UpdateStatus("Copying data " + counter + " of " + dt.Rows.Count, 3, startRow);

                                for (int q = 0; q < row.Table.Columns.Count; q++)
                                {
                                    tempQuery += "'" + row[q].ToString().Replace('\'', ' ').Replace('\"',' ') + "',";
                                }

                                //remove trailing comma
                                tempQuery = tempQuery.TrimEnd(',') + ");";
                                //insert data
                                if (!MySQLConn.ExecuteNonQuery(tempQuery))
                                {
                                    UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                                    return;
                                }
                                counter++;
                            }

                            UpdateStatus("Success", 0, startRow);
                            startRow++;
                            break;
                    }
                }

            }

           

            
            
            DataSet readerTemp;
            DataTable dtTemp;
            //create table primary key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                UpdateStatus("Creting index "+(i+1)+" of "+ dgvObjects.Rows.Count, 3, startRow);
                reader = SQLConn.ExecuteQuery("SELECT TableName = t.name, IndexName =ind.name, ind.is_unique, ind.is_primary_key FROM sys.indexes ind INNER JOIN sys.index_columns ic ON ind.object_id = ic.object_id and ind.index_id = ic.index_id INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id INNER JOIN sys.tables t ON ind.object_id = t.object_id where t.name='" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "' group by t.name,ind.name,ind.is_unique,ind.is_primary_key ORDER BY t.name, ind.name; ");
                dt = reader.Tables[0];

                foreach(DataRow row in dt.Rows)
                {
                    if((Convert.ToBoolean(row["is_unique"].ToString())) && (Convert.ToBoolean(row["is_primary_key"].ToString())))
                    { tempQuery = "ALTER TABLE `" + row["TableName"].ToString()+ "` ADD CONSTRAINT " + row["IndexName"].ToString() + "UNIQUE PRIMARY KEY ("; }

                    if ((Convert.ToBoolean(row["is_unique"].ToString())) && (!Convert.ToBoolean(row["is_primary_key"].ToString())))
                    { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD CONSTRAINT " + row["IndexName"].ToString() + " UNIQUE ("; }

                    if ((!Convert.ToBoolean(row["is_unique"].ToString())) && (Convert.ToBoolean(row["is_primary_key"].ToString())))
                    { tempQuery = "ALTER TABLE `" + row["TableName"].ToString()+ "` ADD CONSTRAINT " + row["IndexName"].ToString() + " PRIMARY KEY("; }

                    if ((!Convert.ToBoolean(row["is_unique"].ToString())) && (!Convert.ToBoolean(row["is_primary_key"].ToString())))
                    { tempQuery = "ALTER TABLE `" + row["TableName"].ToString() + "` ADD INDEX ("; ; }
                    
                    //get the columns for this key
                    readerTemp = SQLConn.ExecuteQuery("SELECT TableName = t.name, IndexName = ind.name, IndexId = ind.index_id, ColumnId = ic.index_column_id, ColumnName = col.name, ind.*, ic.*, col.* FROM sys.indexes ind INNER JOIN sys.index_columns ic ON ind.object_id = ic.object_id and ind.index_id = ic.index_id INNER JOIN sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id INNER JOIN sys.tables t ON ind.object_id = t.object_id where t.name='" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "' ORDER BY t.name, ind.name, ind.index_id, ic.index_column_id;");
                    dtTemp = readerTemp.Tables[0];
                    foreach (DataRow rowTemp in dtTemp.Rows)
                    {
                        tempQuery += "`"+rowTemp["ColumnName"].ToString() + "`,";
                    }
                    //remove trailing comma
                    tempQuery = tempQuery.TrimEnd(',') + ");";
                    //insert data
                    if (!MySQLConn.ExecuteNonQuery(tempQuery))
                    {
                        UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                        return;
                    }

                }
                
            }

            //create identity columns

            reader = SQLConn.ExecuteQuery("select COLUMN_NAME, TABLE_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,IS_NULLABLE from INFORMATION_SCHEMA.COLUMNS where COLUMNPROPERTY(object_id(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 order by TABLE_NAME ");
            dt = reader.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                tempQuery = "ALTER TABLE `" + row["TABLE_NAME"].ToString() + "` ADD INDEX (`" + row["COLUMN_NAME"].ToString() + "`); ALTER TABLE `" + row["TABLE_NAME"].ToString() + "` MODIFY COLUMN `" + row["COLUMN_NAME"].ToString() + "` " + convertDataType(row["DATA_TYPE"].ToString(), row["CHARACTER_MAXIMUM_LENGTH"].ToString(), row["IS_NULLABLE"].ToString()) + " auto_increment";
                //execute command
                if (!MySQLConn.ExecuteNonQuery(tempQuery))
                {
                    UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                    return;
                }
            }

            //create foreign key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                Random random = new Random();
                UpdateStatus("Creting foreign key " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                reader = SQLConn.ExecuteQuery("SELECT f.name AS foreign_key_name ,OBJECT_NAME(f.parent_object_id) AS table_name ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name ,OBJECT_NAME(f.referenced_object_id) AS referenced_object ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name ,is_disabled ,delete_referential_action_desc ,update_referential_action_desc FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id WHERE f.parent_object_id = OBJECT_ID('" + dgvObjects.Rows[i].Cells[1].Value.ToString() + "'); ");
                dt = reader.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    tempQuery = "ALTER TABLE `" + row["table_name"].ToString()+ "` ADD CONSTRAINT " + row["foreign_key_name"].ToString() + " FOREIGN KEY(`" + row["constraint_column_name"].ToString() + "`) REFERENCES `" + row["referenced_object"].ToString() + "` (`" + row["referenced_column_name"].ToString() + "`);";
                    //insert data
                    if (!MySQLConn.ExecuteNonQuery(tempQuery))
                    {
                        tempQuery = "ALTER TABLE `" + row["referenced_object"].ToString() + "` ADD INDEX (`" + row["referenced_column_name"].ToString() + "`); ALTER TABLE `" + row["table_name"].ToString() + "` ADD CONSTRAINT " + row["foreign_key_name"].ToString() + " FOREIGN KEY(`" + row["constraint_column_name"].ToString() + "`) REFERENCES `" + row["referenced_object"].ToString() + "` (`" + row["referenced_column_name"].ToString() + "`);";
                        if (!MySQLConn.ExecuteNonQuery(tempQuery))
                        {
                            UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                            return;
                        }
                    }
                }

            }

            
            //report successful end of tasks (code 99)
            UpdateStatus("success", 99, 99);
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
                    if (max_length.Trim()=="")
                    { return "FLOAT " + isnullableReturn; }
                    if (int.Parse(max_length) <25)
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

        private void wizardPage3_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            txtDestinationServerName.Focus();
            txtDestinationServerName.SelectAll();
        }

        private void wizardPage5_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            e.Cancel = true;
        }

        private void wizardPage6_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            e.Cancel = true;
        }

        private void wizardPage5_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //e.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void wizardPage6_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {

        }
    }
}
