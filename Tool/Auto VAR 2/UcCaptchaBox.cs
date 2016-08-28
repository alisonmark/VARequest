using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Auto_VAR
{
    public partial class UcCaptchaBox : UserControl
    {
        public UcCaptchaBox()
        {
            InitializeComponent();
        }

        private bool _running = false;
        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }

        public void Start(VarItem item, Bitmap bmp)
        {
            _item = item;
            _running = true;

            txtCaptcha.Clear();
            ptbCaptcha.Image = new Bitmap(bmp);

            int count = Setting.CaptchaRefreshTime;
            btnAcceptCaptcha.Text = string.Format("{0}", count);
            btnAcceptCaptcha.Enabled = true;
            new Thread(() =>
            {
                try
                {
                    while (_running && count-- > 0)
                    {
                        Thread.Sleep(1000);
                        Invoke(new ThreadStart(() =>
                        {
                            btnAcceptCaptcha.Text = string.Format("{0}", count);
                        }));
                    }
                }
                catch { _running = false; }

                if (_running)
                {
                    Invoke(new ThreadStart(() =>
                    {
                        btnAcceptCaptcha.Enabled = false;
                        btnAcceptCaptcha.Text = "0";
                        if (_item != null)
                            _item.CaptchaManual = txtCaptcha.Text;
                    }));
                    _running = false;
                }
            }).Start();
        }

        private VarItem _item;

        private void btnAcceptCaptcha_Click(object sender, EventArgs e)
        {
            if (_running)
            {
                if (_item != null)
                    _item.CaptchaManual = txtCaptcha.Text;

                btnAcceptCaptcha.Enabled = false;
                btnAcceptCaptcha.Text = "0";
                _running = false;
            }
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
                if (_running)
                {
                    if (_item != null)
                        _item.CaptchaManual = txtCaptcha.Text;

                    btnAcceptCaptcha.Enabled = false;
                    btnAcceptCaptcha.Text = "0";
                    _running = false;
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                txtCaptcha.Clear();
            }
        }
    }
}
