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
    public partial class FormAddUser : Form
    {
        public FormAddUser()
        {
            InitializeComponent();
        }

        public User ResultUser = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    Username = txtUsername.Text.Trim(),
                    Input = txtInput.Text.Trim()
                };
                UserBusiness.Insert(user);
                ResultUser = UserBusiness.GetUser(user.Username);
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormAddUser_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
