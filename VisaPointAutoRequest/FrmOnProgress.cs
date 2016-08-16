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
    public partial class FrmOnProgress : Form
    {
        private int _delayMilisecs;

        private FrmOnProgress()
        {
            InitializeComponent();
        }

        public FrmOnProgress(int delayMilisecs, string task)
        {
            InitializeComponent();
            this._delayMilisecs = delayMilisecs;

            bw.WorkerReportsProgress = true;
            prgDelay.Maximum = 100;
            prgDelay.Step = 1;

            lblText.Text = string.Format("Waiting {0} miliseconds. Tasks: {1}", _delayMilisecs, task);
        }

        private void FrmOnProgress_Load(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int secs = _delayMilisecs / 100;
            int i;
            for (i = 1; i < 100; i++)
            {
                System.Threading.Thread.Sleep(secs);
                bw.ReportProgress(i);
            }
            System.Threading.Thread.Sleep(_delayMilisecs - secs * 99);
            bw.ReportProgress(i);
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgDelay.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
