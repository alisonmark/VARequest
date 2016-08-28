using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Auto_VAR
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtServerAddr.Text = Setting.ReportServerAddr;
            txtServerPort.Text = Setting.ReportServerPort.ToString();
            txtUsername.Text = Setting.LastUsername;

            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Client.Username = txtUsername.Text.Trim();
                Setting.ReportServerAddr = txtServerAddr.Text;
                int port = -1;
                if (int.TryParse(txtServerPort.Text, out port))
                    Setting.ReportServerPort = port;
                else
                {
                    MessageBox.Show("Cổng kết nối không hợp lệ.");
                    return;
                }
                var res = Client.StartRemotingClient();
                if (string.IsNullOrEmpty(res))
                {
                    this.Visible = false;
                    new FrmMain().Show();
                }
                else
                    MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được đến Server", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
