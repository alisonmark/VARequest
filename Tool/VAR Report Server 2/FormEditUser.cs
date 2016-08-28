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
    public partial class FormEditUser : Form
    {
        public FormEditUser(User user)
        {
            InitializeComponent();
            txtUsername.Text = user.Username;
            txtInput.Text = user.Input;
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
                UserBusiness.Update(user);
                ResultUser = user;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormEditUser_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
