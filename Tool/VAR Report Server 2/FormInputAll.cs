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
    public partial class FormInputAll : Form
    {
        public FormInputAll(List<User> lstUser)
        {
            InitializeComponent();
            this._listUser = lstUser;
        }

        private List<User> _listUser = new List<User>();
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string text = txtInput.Text.Trim();
                UserBusiness.UpdateInputForList(_listUser, text);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
