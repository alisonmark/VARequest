using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Auto_VAR
{
    public partial class FrmCaptcharBox : Form
    {
        public FrmCaptcharBox(VarItem varItem, Bitmap bmp)
        {
            InitializeComponent();

            _item = varItem;

            ptbCaptcha.Image = bmp;

            int count = Setting.CaptchaRefreshTime;

            this.Text = count.ToString();
            btnOK.Text = string.Format("OK ({0})", count);

            _timer = new Thread(() =>
                {
                    try
                    {
                        while (count-- > 0)
                        {
                            Thread.Sleep(1000);
                            Invoke(new ThreadStart(() =>
                            {
                                this.Text = count.ToString();
                                btnOK.Text = string.Format("OK ({0})", count);
                            }));
                        }
                    }
                    catch { }

                    Invoke(new ThreadStart(() =>
                    {
                        btnOK.PerformClick();
                    }));
                });
            _timer.Start();
        }

        private VarItem _item = null;
        private Thread _timer;

        private void btnOK_Click(object sender, EventArgs e)
        {
            _item.CaptchaManual = txtCaptcha.Text;
            this.Close();
        }

        private void txtCaptcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void txtCaptcha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                txtCaptcha.Clear();
            }
        }

        private void FrmCaptcharBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { _timer.Abort(); }
            catch { }
        }
    }
}
