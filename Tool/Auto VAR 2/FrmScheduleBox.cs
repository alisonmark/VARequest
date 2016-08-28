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
    public partial class FrmScheduleBox : Form
    {
        public FrmScheduleBox()
        {
            ScheduleText = string.Empty;
            InitializeComponent();
        }

        public static string ScheduleText { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ScheduleText = txtSchedule.Text;
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
