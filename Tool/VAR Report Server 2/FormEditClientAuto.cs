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
    public partial class FormEditClientAuto : Form
    {
        public FormEditClientAuto(ClientAuto currentObj)
        {
            InitializeComponent();
            txtUsername.Text = currentObj.Username;
            txtTimeInfo.Text = currentObj.TimeInfo;
            txtInput.Text = currentObj.Input;
            _client = currentObj;
        }

        private ClientAuto _client = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_client.Input != txtInput.Text)
            {
                _client.Input = txtInput.Text;
                _client.Command.Enqueue(ClientCommand.UpdateInput);
            }

            if (_client.TimeInfo != txtTimeInfo.Text)
            {
                _client.TimeInfo = txtTimeInfo.Text;
                _client.Command.Enqueue(ClientCommand.UpdateTime);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void FormEditClientAuto_Load(object sender, EventArgs e)
        {
            txtTimeInfo.Focus();
        }
    }
}
