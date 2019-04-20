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
        public Form1()
        {
            InitializeComponent();
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
            if(!testOriginConnection()) { e.Cancel = true; }

        }

        private bool testOriginConnection()
        {
            return true;
        }

        private void wizardPage3_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            //Error Control before change to page3
            //reset ErrorProvider
            errorProvider1.Clear();
            //if Export to Database is selected, test upper groupbox
            if(optExportToDatabase.Checked)
            {
                //Test for empty (each control)
                if (txtDestinationServerName.Text.Trim() == "") { errorProvider1.SetError(txtDestinationServerName, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationDatabase.Text.Trim() == "") { errorProvider1.SetError(txtDestinationDatabase, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationUserName.Text.Trim() == "") { errorProvider1.SetError(txtDestinationUserName, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationPassword.Text.Trim() == "") { errorProvider1.SetError(txtDestinationPassword, "This field can't be empty!"); e.Cancel = true; }
                if (txtDestinationPort.Text.Trim() == "") { errorProvider1.SetError(txtDestinationPort, "This field can't be empty!"); e.Cancel = true; }

                //Test connection to origin database
                if (!testDestinationConnection()) { e.Cancel = true; }
            }
            else //then Export to File is selected, test lower groupbox
            {
                if (txtDestinationFolder.Text.Trim() == "") { errorProvider1.SetError(txtDestinationFolder, "Please select a folder"); e.Cancel = true; }
            }
        }

        private bool testDestinationConnection()
        {
            return true;
        }

        private void optExportToDatabase_Click(object sender, EventArgs e)
        {
            //if Export to Database option is selected, enable upper groupbox and disable lower groupbox
            if(optExportToDatabase.Checked)
            {
                grpExportDatbase.Enabled = true;
                grpExportFile.Enabled = false;
            }
        }

        private void optExportToFile_Click(object sender, EventArgs e)
        {
            //if Export to File option is selected, enable lower groupbox and disable upper groupbox
            if (optExportToFile.Checked)
            {
                grpExportDatbase.Enabled = false;
                grpExportFile.Enabled = true;
            }
        }
    }
}
