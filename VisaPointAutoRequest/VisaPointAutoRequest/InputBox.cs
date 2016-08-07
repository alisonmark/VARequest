using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisaPointAutoRequest
{
    public partial class InputBox : Form
    {
        public FrmMain parent;
        public InputBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            parent.captcha = textBox1.Text;
            this.Dispose();
        }
    }
}
