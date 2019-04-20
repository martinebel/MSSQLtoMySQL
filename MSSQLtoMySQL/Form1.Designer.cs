namespace MSSQLtoMySQL
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.stepWizardControl1 = new AeroWizard.StepWizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.btnOriginTestConnection = new System.Windows.Forms.Button();
            this.optCustomAuth = new System.Windows.Forms.RadioButton();
            this.optWindowsAuth = new System.Windows.Forms.RadioButton();
            this.lblOriginExampleTwo = new System.Windows.Forms.Label();
            this.lblOriginExampleOne = new System.Windows.Forms.Label();
            this.txtOriginDatabase = new System.Windows.Forms.TextBox();
            this.lblOriginDatabase = new System.Windows.Forms.Label();
            this.txtOriginPassword = new System.Windows.Forms.TextBox();
            this.lblOriginPassword = new System.Windows.Forms.Label();
            this.txtOriginUserName = new System.Windows.Forms.TextBox();
            this.lblOriginUserName = new System.Windows.Forms.Label();
            this.txtOriginServer = new System.Windows.Forms.TextBox();
            this.lblOriginServerName = new System.Windows.Forms.Label();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.optExportToFile = new System.Windows.Forms.RadioButton();
            this.optExportToDatabase = new System.Windows.Forms.RadioButton();
            this.grpExportFile = new System.Windows.Forms.GroupBox();
            this.chkSaveEachTableAsFile = new System.Windows.Forms.CheckBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblDestinationFolder = new System.Windows.Forms.Label();
            this.txtDestinationFolder = new System.Windows.Forms.TextBox();
            this.grpExportDatbase = new System.Windows.Forms.GroupBox();
            this.lblDestinationPort = new System.Windows.Forms.Label();
            this.lblDestinationServerName = new System.Windows.Forms.Label();
            this.optOverwriteDatabase = new System.Windows.Forms.RadioButton();
            this.txtDestinationPort = new System.Windows.Forms.TextBox();
            this.txtDestinationServerName = new System.Windows.Forms.TextBox();
            this.optCreateDatabase = new System.Windows.Forms.RadioButton();
            this.lblDestinationUserName = new System.Windows.Forms.Label();
            this.txtDestinationUserName = new System.Windows.Forms.TextBox();
            this.btnDestinationTestConnection = new System.Windows.Forms.Button();
            this.lblDestinationPassword = new System.Windows.Forms.Label();
            this.lblDestinationExampleOne = new System.Windows.Forms.Label();
            this.txtDestinationPassword = new System.Windows.Forms.TextBox();
            this.txtDestinationDatabase = new System.Windows.Forms.TextBox();
            this.lblDestinationDatabase = new System.Windows.Forms.Label();
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage5 = new AeroWizard.WizardPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wizardPage6 = new AeroWizard.WizardPage();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.grpExportFile.SuspendLayout();
            this.grpExportDatbase.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // stepWizardControl1
            // 
            this.stepWizardControl1.BackColor = System.Drawing.Color.White;
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.Pages.Add(this.wizardPage1);
            this.stepWizardControl1.Pages.Add(this.wizardPage2);
            this.stepWizardControl1.Pages.Add(this.wizardPage3);
            this.stepWizardControl1.Pages.Add(this.wizardPage4);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Pages.Add(this.wizardPage6);
            this.stepWizardControl1.ShowProgressInTaskbarIcon = true;
            this.stepWizardControl1.Size = new System.Drawing.Size(705, 498);
            this.stepWizardControl1.StepListFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.stepWizardControl1.TabIndex = 0;
            this.stepWizardControl1.Text = "MSSQL to MySQL Converter";
            this.stepWizardControl1.Title = "MSSQL to MySQL Database Converter Wizard";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.linkLabel1);
            this.wizardPage1.Controls.Add(this.textBox1);
            this.wizardPage1.Controls.Add(this.label3);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage1, "Welcome");
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Welcome";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(264, 61);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(90, 15);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View on GitHub";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 80);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(471, 108);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "This program uses AeroWizard © David Hall";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Made with ♥ by Martin Ebel | ebel.martin@gmail.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "MSSQL to MySQL database converter";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.btnOriginTestConnection);
            this.wizardPage2.Controls.Add(this.optCustomAuth);
            this.wizardPage2.Controls.Add(this.optWindowsAuth);
            this.wizardPage2.Controls.Add(this.lblOriginExampleTwo);
            this.wizardPage2.Controls.Add(this.lblOriginExampleOne);
            this.wizardPage2.Controls.Add(this.txtOriginDatabase);
            this.wizardPage2.Controls.Add(this.lblOriginDatabase);
            this.wizardPage2.Controls.Add(this.txtOriginPassword);
            this.wizardPage2.Controls.Add(this.lblOriginPassword);
            this.wizardPage2.Controls.Add(this.txtOriginUserName);
            this.wizardPage2.Controls.Add(this.lblOriginUserName);
            this.wizardPage2.Controls.Add(this.txtOriginServer);
            this.wizardPage2.Controls.Add(this.lblOriginServerName);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.NextPage = this.wizardPage3;
            this.wizardPage2.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage2, "Select Origin");
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Select Origin";
            this.wizardPage2.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage2_Commit);
            // 
            // btnOriginTestConnection
            // 
            this.btnOriginTestConnection.Location = new System.Drawing.Point(20, 292);
            this.btnOriginTestConnection.Name = "btnOriginTestConnection";
            this.btnOriginTestConnection.Size = new System.Drawing.Size(115, 23);
            this.btnOriginTestConnection.TabIndex = 5;
            this.btnOriginTestConnection.Text = "Test Connection";
            this.btnOriginTestConnection.UseVisualStyleBackColor = true;
            // 
            // optCustomAuth
            // 
            this.optCustomAuth.AutoSize = true;
            this.optCustomAuth.Location = new System.Drawing.Point(17, 125);
            this.optCustomAuth.Name = "optCustomAuth";
            this.optCustomAuth.Size = new System.Drawing.Size(248, 19);
            this.optCustomAuth.TabIndex = 4;
            this.optCustomAuth.Text = "Use the following username and password";
            this.optCustomAuth.UseVisualStyleBackColor = true;
            this.optCustomAuth.CheckedChanged += new System.EventHandler(this.optCustomAuth_Click);
            // 
            // optWindowsAuth
            // 
            this.optWindowsAuth.AutoSize = true;
            this.optWindowsAuth.Checked = true;
            this.optWindowsAuth.Location = new System.Drawing.Point(17, 100);
            this.optWindowsAuth.Name = "optWindowsAuth";
            this.optWindowsAuth.Size = new System.Drawing.Size(178, 19);
            this.optWindowsAuth.TabIndex = 4;
            this.optWindowsAuth.TabStop = true;
            this.optWindowsAuth.Text = "Use Windows Authentication";
            this.optWindowsAuth.UseVisualStyleBackColor = true;
            this.optWindowsAuth.CheckedChanged += new System.EventHandler(this.optWindowsAuth_Click);
            // 
            // lblOriginExampleTwo
            // 
            this.lblOriginExampleTwo.AutoSize = true;
            this.lblOriginExampleTwo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblOriginExampleTwo.Location = new System.Drawing.Point(158, 59);
            this.lblOriginExampleTwo.Name = "lblOriginExampleTwo";
            this.lblOriginExampleTwo.Size = new System.Drawing.Size(160, 15);
            this.lblOriginExampleTwo.TabIndex = 3;
            this.lblOriginExampleTwo.Text = "tcp:DEV-PC\\SQLSERVER,5000";
            // 
            // lblOriginExampleOne
            // 
            this.lblOriginExampleOne.AutoSize = true;
            this.lblOriginExampleOne.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblOriginExampleOne.Location = new System.Drawing.Point(102, 44);
            this.lblOriginExampleOne.Name = "lblOriginExampleOne";
            this.lblOriginExampleOne.Size = new System.Drawing.Size(212, 15);
            this.lblOriginExampleOne.TabIndex = 3;
            this.lblOriginExampleOne.Text = "Examples: DEVELOPMENT\\SQLSERVER ";
            // 
            // txtOriginDatabase
            // 
            this.txtOriginDatabase.Location = new System.Drawing.Point(105, 245);
            this.txtOriginDatabase.Name = "txtOriginDatabase";
            this.txtOriginDatabase.Size = new System.Drawing.Size(229, 23);
            this.txtOriginDatabase.TabIndex = 2;
            // 
            // lblOriginDatabase
            // 
            this.lblOriginDatabase.AutoSize = true;
            this.lblOriginDatabase.Location = new System.Drawing.Point(17, 248);
            this.lblOriginDatabase.Name = "lblOriginDatabase";
            this.lblOriginDatabase.Size = new System.Drawing.Size(55, 15);
            this.lblOriginDatabase.TabIndex = 1;
            this.lblOriginDatabase.Text = "Database";
            // 
            // txtOriginPassword
            // 
            this.txtOriginPassword.Enabled = false;
            this.txtOriginPassword.Location = new System.Drawing.Point(105, 206);
            this.txtOriginPassword.Name = "txtOriginPassword";
            this.txtOriginPassword.Size = new System.Drawing.Size(229, 23);
            this.txtOriginPassword.TabIndex = 2;
            this.txtOriginPassword.UseSystemPasswordChar = true;
            // 
            // lblOriginPassword
            // 
            this.lblOriginPassword.AutoSize = true;
            this.lblOriginPassword.Enabled = false;
            this.lblOriginPassword.Location = new System.Drawing.Point(17, 209);
            this.lblOriginPassword.Name = "lblOriginPassword";
            this.lblOriginPassword.Size = new System.Drawing.Size(57, 15);
            this.lblOriginPassword.TabIndex = 1;
            this.lblOriginPassword.Text = "Password";
            // 
            // txtOriginUserName
            // 
            this.txtOriginUserName.Enabled = false;
            this.txtOriginUserName.Location = new System.Drawing.Point(105, 168);
            this.txtOriginUserName.Name = "txtOriginUserName";
            this.txtOriginUserName.Size = new System.Drawing.Size(229, 23);
            this.txtOriginUserName.TabIndex = 2;
            // 
            // lblOriginUserName
            // 
            this.lblOriginUserName.AutoSize = true;
            this.lblOriginUserName.Enabled = false;
            this.lblOriginUserName.Location = new System.Drawing.Point(17, 171);
            this.lblOriginUserName.Name = "lblOriginUserName";
            this.lblOriginUserName.Size = new System.Drawing.Size(62, 15);
            this.lblOriginUserName.TabIndex = 1;
            this.lblOriginUserName.Text = "UserName";
            // 
            // txtOriginServer
            // 
            this.txtOriginServer.Location = new System.Drawing.Point(102, 14);
            this.txtOriginServer.Name = "txtOriginServer";
            this.txtOriginServer.Size = new System.Drawing.Size(229, 23);
            this.txtOriginServer.TabIndex = 2;
            // 
            // lblOriginServerName
            // 
            this.lblOriginServerName.AutoSize = true;
            this.lblOriginServerName.Location = new System.Drawing.Point(14, 17);
            this.lblOriginServerName.Name = "lblOriginServerName";
            this.lblOriginServerName.Size = new System.Drawing.Size(74, 15);
            this.lblOriginServerName.TabIndex = 1;
            this.lblOriginServerName.Text = "Server Name";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.optExportToFile);
            this.wizardPage3.Controls.Add(this.optExportToDatabase);
            this.wizardPage3.Controls.Add(this.grpExportFile);
            this.wizardPage3.Controls.Add(this.grpExportDatbase);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.NextPage = this.wizardPage4;
            this.wizardPage3.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage3, "Select Destination");
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Select Destination";
            this.wizardPage3.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage3_Commit);
            // 
            // optExportToFile
            // 
            this.optExportToFile.AutoSize = true;
            this.optExportToFile.Location = new System.Drawing.Point(12, 253);
            this.optExportToFile.Name = "optExportToFile";
            this.optExportToFile.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.optExportToFile.Size = new System.Drawing.Size(98, 19);
            this.optExportToFile.TabIndex = 2;
            this.optExportToFile.Text = "Export to File";
            this.optExportToFile.UseVisualStyleBackColor = true;
            this.optExportToFile.Click += new System.EventHandler(this.optExportToFile_Click);
            // 
            // optExportToDatabase
            // 
            this.optExportToDatabase.AutoSize = true;
            this.optExportToDatabase.Checked = true;
            this.optExportToDatabase.Location = new System.Drawing.Point(12, 14);
            this.optExportToDatabase.Name = "optExportToDatabase";
            this.optExportToDatabase.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.optExportToDatabase.Size = new System.Drawing.Size(128, 19);
            this.optExportToDatabase.TabIndex = 2;
            this.optExportToDatabase.TabStop = true;
            this.optExportToDatabase.Text = "Export to Database";
            this.optExportToDatabase.UseVisualStyleBackColor = true;
            this.optExportToDatabase.Click += new System.EventHandler(this.optExportToDatabase_Click);
            // 
            // grpExportFile
            // 
            this.grpExportFile.Controls.Add(this.chkSaveEachTableAsFile);
            this.grpExportFile.Controls.Add(this.btnSelectFolder);
            this.grpExportFile.Controls.Add(this.lblDestinationFolder);
            this.grpExportFile.Controls.Add(this.txtDestinationFolder);
            this.grpExportFile.Enabled = false;
            this.grpExportFile.Location = new System.Drawing.Point(3, 255);
            this.grpExportFile.Name = "grpExportFile";
            this.grpExportFile.Size = new System.Drawing.Size(501, 85);
            this.grpExportFile.TabIndex = 1;
            this.grpExportFile.TabStop = false;
            // 
            // chkSaveEachTableAsFile
            // 
            this.chkSaveEachTableAsFile.AutoSize = true;
            this.chkSaveEachTableAsFile.Location = new System.Drawing.Point(97, 54);
            this.chkSaveEachTableAsFile.Name = "chkSaveEachTableAsFile";
            this.chkSaveEachTableAsFile.Size = new System.Drawing.Size(231, 19);
            this.chkSaveEachTableAsFile.TabIndex = 15;
            this.chkSaveEachTableAsFile.Text = "Save each table / view as a separate file";
            this.chkSaveEachTableAsFile.UseVisualStyleBackColor = true;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(353, 23);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(107, 24);
            this.btnSelectFolder.TabIndex = 14;
            this.btnSelectFolder.Text = "Select Folder...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.AutoSize = true;
            this.lblDestinationFolder.Location = new System.Drawing.Point(9, 27);
            this.lblDestinationFolder.Name = "lblDestinationFolder";
            this.lblDestinationFolder.Size = new System.Drawing.Size(67, 15);
            this.lblDestinationFolder.TabIndex = 9;
            this.lblDestinationFolder.Text = "Destination";
            // 
            // txtDestinationFolder
            // 
            this.txtDestinationFolder.Location = new System.Drawing.Point(97, 24);
            this.txtDestinationFolder.Name = "txtDestinationFolder";
            this.txtDestinationFolder.Size = new System.Drawing.Size(229, 23);
            this.txtDestinationFolder.TabIndex = 13;
            // 
            // grpExportDatbase
            // 
            this.grpExportDatbase.Controls.Add(this.lblDestinationPort);
            this.grpExportDatbase.Controls.Add(this.lblDestinationServerName);
            this.grpExportDatbase.Controls.Add(this.optOverwriteDatabase);
            this.grpExportDatbase.Controls.Add(this.txtDestinationPort);
            this.grpExportDatbase.Controls.Add(this.txtDestinationServerName);
            this.grpExportDatbase.Controls.Add(this.optCreateDatabase);
            this.grpExportDatbase.Controls.Add(this.lblDestinationUserName);
            this.grpExportDatbase.Controls.Add(this.txtDestinationUserName);
            this.grpExportDatbase.Controls.Add(this.btnDestinationTestConnection);
            this.grpExportDatbase.Controls.Add(this.lblDestinationPassword);
            this.grpExportDatbase.Controls.Add(this.lblDestinationExampleOne);
            this.grpExportDatbase.Controls.Add(this.txtDestinationPassword);
            this.grpExportDatbase.Controls.Add(this.txtDestinationDatabase);
            this.grpExportDatbase.Controls.Add(this.lblDestinationDatabase);
            this.grpExportDatbase.Location = new System.Drawing.Point(3, 16);
            this.grpExportDatbase.Name = "grpExportDatbase";
            this.grpExportDatbase.Size = new System.Drawing.Size(501, 224);
            this.grpExportDatbase.TabIndex = 1;
            this.grpExportDatbase.TabStop = false;
            // 
            // lblDestinationPort
            // 
            this.lblDestinationPort.AutoSize = true;
            this.lblDestinationPort.Location = new System.Drawing.Point(351, 29);
            this.lblDestinationPort.Name = "lblDestinationPort";
            this.lblDestinationPort.Size = new System.Drawing.Size(29, 15);
            this.lblDestinationPort.TabIndex = 9;
            this.lblDestinationPort.Text = "Port";
            // 
            // lblDestinationServerName
            // 
            this.lblDestinationServerName.AutoSize = true;
            this.lblDestinationServerName.Location = new System.Drawing.Point(6, 29);
            this.lblDestinationServerName.Name = "lblDestinationServerName";
            this.lblDestinationServerName.Size = new System.Drawing.Size(52, 15);
            this.lblDestinationServerName.TabIndex = 9;
            this.lblDestinationServerName.Text = "Server IP";
            // 
            // optOverwriteDatabase
            // 
            this.optOverwriteDatabase.AutoSize = true;
            this.optOverwriteDatabase.Location = new System.Drawing.Point(210, 167);
            this.optOverwriteDatabase.Name = "optOverwriteDatabase";
            this.optOverwriteDatabase.Size = new System.Drawing.Size(170, 19);
            this.optOverwriteDatabase.TabIndex = 19;
            this.optOverwriteDatabase.Text = "Overwrite Existing Database";
            this.optOverwriteDatabase.UseVisualStyleBackColor = true;
            // 
            // txtDestinationPort
            // 
            this.txtDestinationPort.Location = new System.Drawing.Point(389, 26);
            this.txtDestinationPort.Name = "txtDestinationPort";
            this.txtDestinationPort.Size = new System.Drawing.Size(68, 23);
            this.txtDestinationPort.TabIndex = 13;
            this.txtDestinationPort.Text = "3306";
            // 
            // txtDestinationServerName
            // 
            this.txtDestinationServerName.Location = new System.Drawing.Point(94, 26);
            this.txtDestinationServerName.Name = "txtDestinationServerName";
            this.txtDestinationServerName.Size = new System.Drawing.Size(229, 23);
            this.txtDestinationServerName.TabIndex = 13;
            // 
            // optCreateDatabase
            // 
            this.optCreateDatabase.AutoSize = true;
            this.optCreateDatabase.Checked = true;
            this.optCreateDatabase.Location = new System.Drawing.Point(94, 167);
            this.optCreateDatabase.Name = "optCreateDatabase";
            this.optCreateDatabase.Size = new System.Drawing.Size(110, 19);
            this.optCreateDatabase.TabIndex = 19;
            this.optCreateDatabase.TabStop = true;
            this.optCreateDatabase.Text = "Create Database";
            this.optCreateDatabase.UseVisualStyleBackColor = true;
            // 
            // lblDestinationUserName
            // 
            this.lblDestinationUserName.AutoSize = true;
            this.lblDestinationUserName.Location = new System.Drawing.Point(6, 83);
            this.lblDestinationUserName.Name = "lblDestinationUserName";
            this.lblDestinationUserName.Size = new System.Drawing.Size(62, 15);
            this.lblDestinationUserName.TabIndex = 8;
            this.lblDestinationUserName.Text = "UserName";
            // 
            // txtDestinationUserName
            // 
            this.txtDestinationUserName.Location = new System.Drawing.Point(94, 80);
            this.txtDestinationUserName.Name = "txtDestinationUserName";
            this.txtDestinationUserName.Size = new System.Drawing.Size(229, 23);
            this.txtDestinationUserName.TabIndex = 12;
            // 
            // btnDestinationTestConnection
            // 
            this.btnDestinationTestConnection.Location = new System.Drawing.Point(9, 192);
            this.btnDestinationTestConnection.Name = "btnDestinationTestConnection";
            this.btnDestinationTestConnection.Size = new System.Drawing.Size(115, 23);
            this.btnDestinationTestConnection.TabIndex = 18;
            this.btnDestinationTestConnection.Text = "Test Connection";
            this.btnDestinationTestConnection.UseVisualStyleBackColor = true;
            // 
            // lblDestinationPassword
            // 
            this.lblDestinationPassword.AutoSize = true;
            this.lblDestinationPassword.Location = new System.Drawing.Point(6, 112);
            this.lblDestinationPassword.Name = "lblDestinationPassword";
            this.lblDestinationPassword.Size = new System.Drawing.Size(57, 15);
            this.lblDestinationPassword.TabIndex = 7;
            this.lblDestinationPassword.Text = "Password";
            // 
            // lblDestinationExampleOne
            // 
            this.lblDestinationExampleOne.AutoSize = true;
            this.lblDestinationExampleOne.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDestinationExampleOne.Location = new System.Drawing.Point(94, 56);
            this.lblDestinationExampleOne.Name = "lblDestinationExampleOne";
            this.lblDestinationExampleOne.Size = new System.Drawing.Size(126, 15);
            this.lblDestinationExampleOne.TabIndex = 15;
            this.lblDestinationExampleOne.Text = "Example: 192.168.1.100";
            // 
            // txtDestinationPassword
            // 
            this.txtDestinationPassword.Location = new System.Drawing.Point(94, 109);
            this.txtDestinationPassword.Name = "txtDestinationPassword";
            this.txtDestinationPassword.Size = new System.Drawing.Size(229, 23);
            this.txtDestinationPassword.TabIndex = 11;
            // 
            // txtDestinationDatabase
            // 
            this.txtDestinationDatabase.Location = new System.Drawing.Point(94, 138);
            this.txtDestinationDatabase.Name = "txtDestinationDatabase";
            this.txtDestinationDatabase.Size = new System.Drawing.Size(229, 23);
            this.txtDestinationDatabase.TabIndex = 10;
            // 
            // lblDestinationDatabase
            // 
            this.lblDestinationDatabase.AutoSize = true;
            this.lblDestinationDatabase.Location = new System.Drawing.Point(6, 141);
            this.lblDestinationDatabase.Name = "lblDestinationDatabase";
            this.lblDestinationDatabase.Size = new System.Drawing.Size(55, 15);
            this.lblDestinationDatabase.TabIndex = 6;
            this.lblDestinationDatabase.Text = "Database";
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.dataGridView1);
            this.wizardPage4.Controls.Add(this.label4);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.NextPage = this.wizardPage5;
            this.wizardPage4.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage4, "Object Selection");
            this.wizardPage4.TabIndex = 5;
            this.wizardPage4.Text = "Object Selection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Select the objects you want to export:";
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.listView1);
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.NextPage = this.wizardPage6;
            this.wizardPage5.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage5, "Progress");
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Progress";
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(4, 4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(500, 337);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Object";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 240;
            // 
            // wizardPage6
            // 
            this.wizardPage6.AllowBack = false;
            this.wizardPage6.AllowCancel = false;
            this.wizardPage6.AllowNext = false;
            this.wizardPage6.IsFinishPage = true;
            this.wizardPage6.Name = "wizardPage6";
            this.wizardPage6.ShowCancel = false;
            this.wizardPage6.ShowNext = false;
            this.wizardPage6.Size = new System.Drawing.Size(507, 344);
            this.stepWizardControl1.SetStepText(this.wizardPage6, "Finish");
            this.wizardPage6.TabIndex = 7;
            this.wizardPage6.Text = "Finish";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(19, 23);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(474, 311);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Object";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 350;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Schema";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Data";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.Width = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 498);
            this.Controls.Add(this.stepWizardControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MSSQL to MySQL Converter";
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.grpExportFile.ResumeLayout(false);
            this.grpExportFile.PerformLayout();
            this.grpExportDatbase.ResumeLayout(false);
            this.grpExportDatbase.PerformLayout();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            this.wizardPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.StepWizardControl stepWizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
        private AeroWizard.WizardPage wizardPage3;
        private AeroWizard.WizardPage wizardPage4;
        private AeroWizard.WizardPage wizardPage5;
        private AeroWizard.WizardPage wizardPage6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOriginTestConnection;
        private System.Windows.Forms.RadioButton optCustomAuth;
        private System.Windows.Forms.RadioButton optWindowsAuth;
        private System.Windows.Forms.Label lblOriginExampleTwo;
        private System.Windows.Forms.Label lblOriginExampleOne;
        private System.Windows.Forms.TextBox txtOriginDatabase;
        private System.Windows.Forms.Label lblOriginDatabase;
        private System.Windows.Forms.TextBox txtOriginPassword;
        private System.Windows.Forms.Label lblOriginPassword;
        private System.Windows.Forms.TextBox txtOriginUserName;
        private System.Windows.Forms.Label lblOriginUserName;
        private System.Windows.Forms.TextBox txtOriginServer;
        private System.Windows.Forms.Label lblOriginServerName;
        private System.Windows.Forms.GroupBox grpExportFile;
        private System.Windows.Forms.GroupBox grpExportDatbase;
        private System.Windows.Forms.Label lblDestinationServerName;
        private System.Windows.Forms.RadioButton optOverwriteDatabase;
        private System.Windows.Forms.TextBox txtDestinationServerName;
        private System.Windows.Forms.RadioButton optCreateDatabase;
        private System.Windows.Forms.Label lblDestinationUserName;
        private System.Windows.Forms.TextBox txtDestinationUserName;
        private System.Windows.Forms.Button btnDestinationTestConnection;
        private System.Windows.Forms.Label lblDestinationPassword;
        private System.Windows.Forms.Label lblDestinationExampleOne;
        private System.Windows.Forms.TextBox txtDestinationPassword;
        private System.Windows.Forms.TextBox txtDestinationDatabase;
        private System.Windows.Forms.Label lblDestinationDatabase;
        private System.Windows.Forms.RadioButton optExportToFile;
        private System.Windows.Forms.RadioButton optExportToDatabase;
        private System.Windows.Forms.CheckBox chkSaveEachTableAsFile;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblDestinationFolder;
        private System.Windows.Forms.TextBox txtDestinationFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblDestinationPort;
        private System.Windows.Forms.TextBox txtDestinationPort;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
    }
}

