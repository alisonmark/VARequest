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
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Setting.DecaptchaTimeout = (int)txtDecaptchaTimeout.Value;
            Setting.DelayEachStep = (int)txtDelayEachStep.Value;
            Setting.DelaySubmit = (int)txtDelaySubmit.Value;
            Setting.PrepareTime = (int)txtPrepareSeconds.Value;
            Setting.RetrySubmitCount = (int)txtRetrySubmitCount.Value;
            Setting.SessionTimeout = (int)txtSessionTimeout.Value;
            Setting.SessionTime = (int)txtSessionTime.Value;

            Setting.MaxCaptchaManual = (int)txtMaxCaptchaManual.Value;

            Setting.ForceSubmitCount = (int)txtForceSubmitCount.Value;
            Setting.ForceSubmitDelay = (int)txtForceSubmitDelay.Value;

            Setting.UsernameDbc = txtUsername0.Text;
            Setting.PasswordDbc = txtPassword0.Text;

            Setting.UsernameDeCaptcher = txtUsername1.Text;
            Setting.PasswordDeCaptcher = txtPassword1.Text;

            Setting.UsernameImagetyperz = txtUsername2.Text;
            Setting.PasswordImagetyperz = txtPassword2.Text;

            Setting.UsernameShanibpo = txtUsername3.Text;
            Setting.PasswordShanibpo = txtPassword3.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            txtDecaptchaTimeout.Text = Setting.DecaptchaTimeout.ToString();
            txtDelayEachStep.Text = Setting.DelayEachStep.ToString();
            txtDelaySubmit.Text = Setting.DelaySubmit.ToString();
            txtPrepareSeconds.Text = Setting.PrepareTime.ToString();
            txtRetrySubmitCount.Text = Setting.RetrySubmitCount.ToString();
            txtSessionTimeout.Text = Setting.SessionTimeout.ToString();
            txtSessionTime.Text = Setting.SessionTime.ToString();
            txtMaxCaptchaManual.Text = Setting.MaxCaptchaManual.ToString();

            txtForceSubmitCount.Text = Setting.ForceSubmitCount.ToString();
            txtForceSubmitDelay.Text = Setting.ForceSubmitDelay.ToString();

            txtUsername0.Text = Setting.UsernameDbc;
            txtPassword0.Text = Setting.PasswordDbc;

            txtUsername1.Text = Setting.UsernameDeCaptcher;
            txtPassword1.Text = Setting.PasswordDeCaptcher;

            txtUsername2.Text = Setting.UsernameImagetyperz;
            txtPassword2.Text = Setting.PasswordImagetyperz;

            txtUsername3.Text = Setting.UsernameShanibpo;
            txtPassword3.Text = Setting.PasswordShanibpo;
        }
    }
}
