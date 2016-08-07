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
    public partial class Response : Form
    {
        public Response()
        {
            InitializeComponent();
        }

        public void setRes(String content)
        {
            txtRes.Text = content;
            wb.DocumentText = content;
        }
    }
}
