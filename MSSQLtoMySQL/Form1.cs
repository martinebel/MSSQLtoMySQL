using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

       
        public void UpdateStatus(string message, int status, int row)
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
                    this.lvTasks.Items[row].Selected = true;
                    this.lvTasks.Items[row].EnsureVisible();
                    break;
                case 1: //error, show final page
                    this.lvTasks.Items[row].ImageIndex = 1;
                    
                    //stepWizardControl1.NextPage();
                    pictureBox1.Image = imageList1.Images[1];
                    lblEndStatus.Text = "Tasks completed with errors";
                    txtEndInfo.Text += message + Environment.NewLine+"LAST MSSQL COMMAND:" +Environment.NewLine+ SQLConn.getLastSQLCommand() + Environment.NewLine+"LAST MySQL COMMAND:"+ Environment.NewLine + MySQLConn.getLastSQLCommand()+Environment.NewLine+"--------------------------------------"+Environment.NewLine;
                    this.lvTasks.Items[row].Selected = true;
                    this.lvTasks.Items[row].EnsureVisible();
                    break;
                case 2: //progress
                    this.lvTasks.Items[row].ImageIndex = 4;
                    this.lvTasks.Items[row].Selected = true;
                    this.lvTasks.Items[row].EnsureVisible();
                    break;
                case 99: //successful end
                    stepWizardControl1.NextPage();
                    if (txtEndInfo.Text.Trim() == "")
                    {
                        pictureBox1.Image = imageList1.Images[0];
                        lblEndStatus.Text = "Tasks completed successfuly!";
                        lblEndDeclaration.Visible = false;
                        txtEndInfo.Visible = false;
                        button1.Visible = false;
                    }
                    
                    
                    break;

            }
            try
            {
                this.lvTasks.Items[row].SubItems[1].Text = message;
            }
            catch { }
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
                else
                {
                    testMySQLCase();
                }
                lblDestinationStatus.Visible = false;
            }
            else //then Export to File is selected, test lower groupbox
            {
                if (txtDestinationFolder.Text.Trim() == "") { errorProvider1.SetError(txtDestinationFolder, "Please select a folder"); e.Cancel = true; }
            }

           

        }

        private void testMySQLCase()
        {
            if(MySQLConn.testCase())
            {
                MessageBox.Show("Your MySQL Engine has lower_case_table_names set to True. This can result in errors during database conversion due to table/view name casing. Please set this variable to zero before continuing.\n\nOn *nix Systems: /etc/mysql/my.cnf\nOn Windows Systems: /my.cnf", "MySQL lower_case_table_names ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            //load origin database objects
            
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
            else
            {
                //save current connection data to file, to use it the next time
                StreamWriter fileWriter = new StreamWriter(Application.StartupPath + "\\data.ini",false);
                fileWriter.WriteLine(txtOriginServer.Text);
                fileWriter.WriteLine(txtOriginUserName.Text);
                fileWriter.WriteLine(txtOriginPassword.Text);
                fileWriter.WriteLine(txtOriginDatabase.Text);

                fileWriter.WriteLine(txtDestinationServerName.Text);
                fileWriter.WriteLine(txtDestinationPort.Text);
                fileWriter.WriteLine(txtDestinationUserName.Text);
                fileWriter.WriteLine(txtDestinationPassword.Text);
                fileWriter.WriteLine(txtDestinationDatabase.Text);
                fileWriter.Close();
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
                item = lvTasks.Items.Add("Creating MySQL Database: " + txtDestinationDatabase.Text);
                item.SubItems.Add("Waiting...");
            }
            if (optCreateDatabase.Checked)
            {
                item = lvTasks.Items.Add("Creating MySQL Database: " + txtDestinationDatabase.Text);
                item.SubItems.Add("Waiting...");
            } 

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
            if (optExportToDatabase.Checked)
            {
                System.Threading.Thread procThread = new System.Threading.Thread(this.toDatabase);
                procThread.Start();
            }
            else
            {
                System.Threading.Thread procThread = new System.Threading.Thread(this.toFile);
                procThread.Start();
            }
        }

        private void toDatabase() // This is the actual method of the thread
        {

            int startRow = 0;
            string tempQuery = "";
            int counter = 0;
            string _separator = " ";
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
            if (optCreateDatabase.Checked)
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
            if (optAppendDatabase.Checked)
            {
                startRow = 0;
                //use mysql database
                if (MySQLConn.ExecuteNonQuery("use `" + txtDestinationDatabase.Text + "`;"))
                {
                    UpdateStatus("Success", 0, 0);
                }
                else
                {
                    UpdateStatus(MySQLConn.getLastError(), 1, 0);
                    return;
                }
            }

            conversionHelper conversion;

            if (optWindowsAuth.Checked)
            {
                conversion = new conversionHelper(txtOriginServer.Text, txtOriginDatabase.Text);
            }
            else
            {
                conversion = new conversionHelper(txtOriginServer.Text, txtOriginDatabase.Text, txtOriginUserName.Text, txtOriginPassword.Text);
            }

            //create list of table names
            List<string> tableNamesList = new List<string>();
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                {
                    tableNamesList.Add(dgvObjects.Rows[i].Cells[1].Value.ToString());
                }
            }


            //migrate objects

            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                
                //create schema
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    tempQuery = conversion.convertSchema(dgvObjects.Rows[i].Cells[0].Value.ToString(), dgvObjects.Rows[i].Cells[1].Value.ToString(), tableNamesList, _separator);
                    //ToDo: support more objects?
                    //create schema
                    if (!MySQLConn.ExecuteNonQuery(tempQuery))
                    {
                        UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                        //return;
                    }
                    else
                    {
                        UpdateStatus("Success", 0, startRow);
                    }
                    
                    startRow++;
                }

                //copy data
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[3].Value))
                {

                    //ToDo: support more objects?
                    List<string> listaQuery = new List<string>();
                    listaQuery = conversion.copyData(dgvObjects.Rows[i].Cells[0].Value.ToString(), dgvObjects.Rows[i].Cells[1].Value.ToString(), startRow, this, _separator);
                    counter = 1;
                    //insert data
                    foreach (string query in listaQuery)
                    {
                        UpdateStatus("Writing data " + counter + " of " + listaQuery.Count, 3, startRow);
                        if (!MySQLConn.ExecuteNonQuery(tempQuery))
                        {
                            UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                            //return;
                        }
                        else
                        {
                            UpdateStatus("Success", 0, startRow);
                        }
                        counter++;
                    }

                    //UpdateStatus("Success", 0, startRow);
                    startRow++;
                }

            }



            //create table primary key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    UpdateStatus("Creating index " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery = conversion.makeIndex(dgvObjects.Rows[i].Cells[1].Value.ToString(), _separator);
                    //insert data
                    if (tempQuery.Trim() != "")
                    {
                        if (!MySQLConn.ExecuteNonQuery(tempQuery))
                        {
                            UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                            //return;
                        }
                    }

                }
            }


            //create identity columns
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    UpdateStatus("Creating identity columns " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery = conversion.makeIdentity(dgvObjects.Rows[i].Cells[1].Value.ToString(), _separator);
                    //execute command
                    if (tempQuery.Trim() != "")
                    {
                        if (!MySQLConn.ExecuteNonQuery(tempQuery))
                        {
                            UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                            //return;
                        }
                    }

                }
            }

            //create foreign key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    UpdateStatus("Creating foreign key " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery = conversion.makeForeignKey(dgvObjects.Rows[i].Cells[1].Value.ToString(), _separator);
                    if (tempQuery.Trim() != "")
                    {
                        if (!MySQLConn.ExecuteNonQuery(tempQuery))
                        {
                            UpdateStatus(MySQLConn.getLastError(), 1, startRow);
                            //return;
                        }
                    }


                }
            }
            //report successful end of tasks (code 99)
            UpdateStatus("success", 99, 99);
        }


        private void toFile() // This is the actual method of the thread
        {

            int startRow = 0;
            string tempQuery = "";
            int counter = 0;
            string fileName = txtOriginDatabase.Text+".sql";
            string _separator = " ";
            if (!chkMinifySQL.Checked) { _separator = Environment.NewLine; }
            //ToDo: convert SQL Server Database Collation to MySql Database Collation

            //obtaining sql server collation for database:
            //select collation_name from sys.databases where name='yourdbname' 
            File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, "/* Please check lower_case_table_names value is set to zero in 'my.cnf' file. to prevent errors due to table/view name casing */" + Environment.NewLine);
            if (optOverwriteDatabase.Checked)
            {
                startRow = 2;
                //delete mysql database
                tempQuery = "drop database `" + txtOriginDatabase.Text + "`;"+_separator;
                File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName,tempQuery);
                UpdateStatus("Success", 0, 0);


                //create mysql database
                tempQuery = "create database `" + txtOriginDatabase.Text + "`;" + _separator+"use `" + txtOriginDatabase.Text + "`;" + _separator;
                File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery);
                UpdateStatus("Success", 0, 1);
                
            }
            if (optCreateDatabase.Checked)
            {
                startRow = 1;
                //create mysql database
                tempQuery = "create database `" + txtOriginDatabase.Text + "`;" + _separator+"use `" + txtOriginDatabase.Text + "`;" + _separator;
                File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery);
                UpdateStatus("Success", 0, 0);
                
            }
            if (optAppendDatabase.Checked)
            {
                startRow = 0;
                //use mysql database
                tempQuery = "use `" + txtOriginDatabase.Text + "`;" + _separator;
                File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery);
                UpdateStatus("Success", 0, 0);
            }
            
            conversionHelper conversion;

            if (optWindowsAuth.Checked)
            {
                conversion = new conversionHelper(txtOriginServer.Text, txtOriginDatabase.Text);
            }
            else
            {
                conversion = new conversionHelper(txtOriginServer.Text, txtOriginDatabase.Text, txtOriginUserName.Text, txtOriginPassword.Text);
            }

            //create list of table names
            List<string> tableNamesList = new List<string>();
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (dgvObjects.Rows[i].Cells[0].Value.ToString() == "TABLE")
                {
                    tableNamesList.Add(dgvObjects.Rows[i].Cells[1].Value.ToString());
                }
            }

                //migrate objects

            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (chkSaveEachTableAsFile.Checked) { fileName = dgvObjects.Rows[i].Cells[1].Value.ToString()+".sql"; File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, "/* Please check lower_case_table_names value is set to zero in 'my.cnf' file. to prevent errors due to table/view name casing */" + Environment.NewLine); }
                //create schema
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    tempQuery = conversion.convertSchema(dgvObjects.Rows[i].Cells[0].Value.ToString(), dgvObjects.Rows[i].Cells[1].Value.ToString(), tableNamesList, _separator);
                    //ToDo: support more objects?
                         //create schema
                            File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery + _separator);
                            UpdateStatus("Success", 0, startRow);
                            startRow++;
                }

                //copy data
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[3].Value))
                {

                    //ToDo: support more objects?
                    List<string> listaQuery = new List<string>();
                    listaQuery = conversion.copyData(dgvObjects.Rows[i].Cells[0].Value.ToString(), dgvObjects.Rows[i].Cells[1].Value.ToString(),startRow,this, _separator);
                    counter = 1;
                            //insert data
                    foreach(string query in listaQuery)
                    {
                        UpdateStatus("Writing data " + counter + " of " + listaQuery.Count, 3, startRow);
                        File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, query + _separator);
                        counter++;
                    }
                            
                            UpdateStatus("Success", 0, startRow);
                            startRow++;
                }

            }

           
            //create table primary key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    if (chkSaveEachTableAsFile.Checked) { fileName = dgvObjects.Rows[i].Cells[1].Value.ToString() + ".sql"; }
                    UpdateStatus("Creating index " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery=conversion.makeIndex(dgvObjects.Rows[i].Cells[1].Value.ToString(), _separator);
                        //insert data
                        File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery );

                    }
                }
            

            //create identity columns
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    if (chkSaveEachTableAsFile.Checked) { fileName = dgvObjects.Rows[i].Cells[1].Value.ToString() + ".sql"; }
                    UpdateStatus("Creating identity columns " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery = conversion.makeIdentity(dgvObjects.Rows[i].Cells[1].Value.ToString(), _separator);
                    //execute command
                    File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery );
                    
                }
            }

            //create foreign key
            for (int i = 0; i < dgvObjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvObjects.Rows[i].Cells[2].Value))
                {
                    if (chkSaveEachTableAsFile.Checked) { fileName = dgvObjects.Rows[i].Cells[1].Value.ToString() + ".sql"; }
                    UpdateStatus("Creating foreign key " + (i + 1) + " of " + dgvObjects.Rows.Count, 3, startRow);
                    tempQuery = conversion.makeForeignKey(dgvObjects.Rows[i].Cells[1].Value.ToString(),_separator);
                    File.AppendAllText(txtDestinationFolder.Text + "\\" + fileName, tempQuery );

                    
                }
            }


            //report successful end of tasks (code 99)
            UpdateStatus("success", 99, 99);
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
            //if last data file exists, then load it
            if (File.Exists(Application.StartupPath + "\\data.ini"))
            {
                StreamReader fileReader = new StreamReader(Application.StartupPath + "\\data.ini");
                txtOriginServer.Text = fileReader.ReadLine();
                txtOriginUserName.Text = fileReader.ReadLine();
                txtOriginPassword.Text = fileReader.ReadLine();
                txtOriginDatabase.Text = fileReader.ReadLine();

                txtDestinationServerName.Text = fileReader.ReadLine();
                txtDestinationPort.Text = fileReader.ReadLine();
                txtDestinationUserName.Text = fileReader.ReadLine();
                txtDestinationPassword.Text = fileReader.ReadLine();
                txtDestinationDatabase.Text = fileReader.ReadLine();
                fileReader.Close();
            }
        }

        private void wizardPage6_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File (*.txt)|*.txt";
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName,false);
                sw.WriteLine(txtEndInfo.Text);
                sw.Close();
            }
        }
    }
}
