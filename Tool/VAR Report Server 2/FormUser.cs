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
    public partial class FormUser : Form
    {
        public FormUser()
        {
            InitializeComponent();
        }

        private List<User> _currentListUser = new List<User>();
        private void BindData()
        {
            dtgUser.Rows.Clear();

            List<User> lstUser = UserBusiness.GetAll();
            foreach (User user in lstUser)
            {
                int index = dtgUser.Rows.Add();
                DataGridViewRow row = dtgUser.Rows[index];
                row.Tag = user;
                row.Cells[0].Value = false;
                row.Cells[1].Value = user.Username;
                row.Cells[2].Value = user.Input;
            }
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            //BindData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAddUser f = new FormAddUser();
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (f.ResultUser != null)
                {
                    int index = dtgUser.Rows.Add();
                    DataGridViewRow row = dtgUser.Rows[index];
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = f.ResultUser.Username;
                    row.Cells[2].Value = f.ResultUser.Input;
                    row.Tag = f.ResultUser;
                    row.Selected = true;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgUser.SelectedRows == null || dtgUser.SelectedRows.Count == 0)
                return;
            DataGridViewRow row = dtgUser.SelectedRows[0];
            User user = row.Tag as User;
            ShowEditSingle(row, user);
        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Hide();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtgUser_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgUser.SelectedRows == null || dtgUser.SelectedRows.Count == 0 || e.ColumnIndex < 1)
                return;
            DataGridViewRow row = dtgUser.SelectedRows[0];
            User user = row.Tag as User;

            ShowEditSingle(row, user);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<User> lstUser = new List<User>();
            foreach (DataGridViewRow row in dtgUser.Rows)
                if (Convert.ToBoolean(row.Cells[0].Value))
                    lstUser.Add(row.Tag as User);
            if (lstUser.Count == 0)
                return;

            var res = MessageBox.Show("Are you sure to remove selected users?", "Remove?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (User u in lstUser)
                {
                    UserBusiness.Remove(u);
                }
                BindData();
            }
        }

        private void btnInputAll_Click(object sender, EventArgs e)
        {
            List<User> lstUser = new List<User>();
            foreach (DataGridViewRow row in dtgUser.Rows)
                if (Convert.ToBoolean(row.Cells[0].Value))
                    lstUser.Add(row.Tag as User);

            ShowEditMany(lstUser);

            List<User> lstData = UserBusiness.GetAll();
            foreach (User user in lstData)
            {
                foreach (DataGridViewRow row in dtgUser.Rows)
                {
                    User itemRow = row.Tag as User;
                    if (user.Username == itemRow.Username)
                    {
                        row.Cells[2].Value = user.Input;
                        row.Tag = user;
                    }
                }
            }
        }

        private void ShowEditSingle(DataGridViewRow row, User user)
        {
            FormEditUser f = new FormEditUser(user);
            var res = f.ShowDialog();

            List<User> lstData = UserBusiness.GetAll();
            foreach (User u in lstData)
            {
                foreach (DataGridViewRow r in dtgUser.Rows)
                {
                    User itemRow = r.Tag as User;
                    if (u.Username == itemRow.Username)
                    {
                        r.Cells[2].Value = u.Input;
                        r.Tag = u;
                    }
                }
            }
        }

        private void ShowEditMany(List<User> lstUser)
        {
            new FormInputAll(lstUser).ShowDialog();
        }

        private bool _isItemCheckedbox = false;
        private void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isItemCheckedbox)
            {
                foreach (DataGridViewRow row in dtgUser.Rows)
                    row.Cells[0].Value = (sender as CheckBox).Checked;
            }
            _isItemCheckedbox = false;
        }

        private void dtgUser_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dtgUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0)
                return;
            dtgUser.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(dtgUser.Rows[e.RowIndex].Cells[0].Value);
        }

        private void FormUser_Shown(object sender, EventArgs e)
        {
            
        }

        private void FormUser_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                BindData();
        }
    }
}
