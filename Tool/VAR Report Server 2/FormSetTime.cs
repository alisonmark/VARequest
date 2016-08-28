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
    public partial class FormSetTime : Form
    {
        public FormSetTime(List<ClientAuto> list)
        {
            InitializeComponent();

            _currentList = list;

            List<string> nameClient = new List<string>();
            foreach (ClientAuto item in list) 
                nameClient.Add(item.Username);

            txtUsername.Text = string.Join(", ", nameClient);
            txtTimeInfo.Text = string.Empty;

            
        }

        private List<ClientAuto> _currentList = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (ClientAuto item in _currentList)
            {
                if (item.TimeInfo != txtTimeInfo.Text)
                {
                    item.TimeInfo = txtTimeInfo.Text;
                    item.Command.Enqueue(ClientCommand.UpdateTime);
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void FormSetTime_Load(object sender, EventArgs e)
        {
txtTimeInfo.Focus();
        }
    }
}
