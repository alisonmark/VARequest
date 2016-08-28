using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VAR_Report_Server
{
    public partial class FormAddClientAuto : Form
    {
        public FormAddClientAuto()
        {
            InitializeComponent();
        }

        public static ClientAuto Client = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (FormClientManagement.ClientExists(txtUsername.Text))
            {
                MessageBox.Show("Username đã tồn tại, hãy chọn username khác");
                return;
            }

            Client = new ClientAuto();
            Client.Username = txtUsername.Text;
            Client.Input = txtInput.Text;
            Client.TimeInfo = txtTimeInfo.Text;
            Client.Started = false;
            Client.Connected = false;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
