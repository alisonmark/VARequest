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
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Setting.MailSenderUsername = txtMailSenderUsername.Text;
            Setting.MailSenderPassword = txtMailSenderPassword.Text;

            Setting.MailReceiverUsername = txtMailReceiverUsername.Text;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            txtMailSenderUsername.Text = Setting.MailSenderUsername;
            txtMailSenderPassword.Text = Setting.MailSenderPassword;

            txtMailReceiverUsername.Text = Setting.MailReceiverUsername;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SendMail("Test sending mail!", "Send a mail from VAR Report Server v2 at " + DateTime.Now);
        }

        private void SendMail(string subject, string body)
        {
            string senderUser = Setting.MailSenderUsername;
            string receiverUser = Setting.MailReceiverUsername;
            string senderPass = Setting.MailSenderPassword;
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(senderUser, receiverUser);
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.Host = "smtp.googlemail.com";
                mail.Subject = subject;
                mail.Body = body;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(senderUser, senderPass);
                client.Send(mail);

                MessageBox.Show("Test Successful!", "Mail Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Sender: {0}\nTo: {1}\nError: {2}", senderUser, receiverUser, ex
                    .Message), "Gửi email thất bại.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
