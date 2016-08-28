using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Auto_VAR
{
    public partial class FrmProxy : Form
    {
        public FrmProxy()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<WebProxy> list = new List<WebProxy>();
                string[] data = txtProxy.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string d in data)
                {
                    string[] part = d.Trim().Split(';');

                    if (part.Length > 1)
                    {
                        WebProxy proxy = new WebProxy(part[0]);
                        proxy.Credentials = new NetworkCredential(part[1], part[2]);
                        list.Add(proxy);
                    }
                    else
                    {
                        list.Add(new WebProxy(d.Trim()));
                    }

                }

                Setting.ProxyList = list;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dữ liệu proxy không đúng định dạng !");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProxy_Load(object sender, EventArgs e)
        {
            string str = string.Empty;

            foreach (WebProxy proxy in Setting.ProxyList)
            {
                NetworkCredential credential = (NetworkCredential)proxy.Credentials;
                if (credential == null)
                    str += str += string.Format("{0}:{1}\r\n", proxy.Address.Host, proxy.Address.Port);
                else
                    str += string.Format("{0}:{1};{2};{3}\r\n", proxy.Address.Host, proxy.Address.Port, credential.UserName, credential.Password);
            }
            txtProxy.Text = str;
        }
    }
}
