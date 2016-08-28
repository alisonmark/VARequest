using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace Auto_VAR
{
    public partial class VarTimeInput : UserControl
    {
        public VarTimeInput()
        {
            InitializeComponent();
        }

        public DateTime StartDate { get; set; }

        public bool IsUse
        {
            get { return chbUse.Checked; }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() => { chbUse.Checked = value; }));
                else
                    chbUse.Checked = value;
            }
        }

        public int NumberOfInput
        {
            get { return (int)txtNumUser.Value; }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() => { txtNumUser.Value = value; }));
                else
                    txtNumUser.Value = value;
            }
        }

        public TimeSpan StartTime
        {
            get
            {
                try { return TimeSpan.Parse(txtStartTime.Text); }
                catch { return TimeSpan.Zero; }
            }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() =>
                    {
                        if (TimeSpan.Zero == value)
                            txtStartTime.Text = string.Empty;
                        else
                            txtStartTime.Text = value.ToString();
                    }));
                else
                    if (TimeSpan.Zero == value)
                        txtStartTime.Text = string.Empty;
                    else
                        txtStartTime.Text = value.ToString(@"hh\:mm\:ss\.fff");

            }
        }

        public int Frequency
        {
            get { try { return int.Parse(txtFrequency.Text); } catch { return 0; } }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() => { txtFrequency.Text = value.ToString(); }));
                else
                    txtFrequency.Text = value.ToString();
            }
        }

        public int RepeatInterval
        {
            get { try { return int.Parse(txtRepeatInterval.Text); } catch { return 0; } }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() => { txtRepeatInterval.Text = value.ToString(); }));
                else
                    txtRepeatInterval.Text = value.ToString();
            }
        }

        public int RepeatCount
        {
            get
            {
                int temp = 0;
                if (!int.TryParse(txtRepeatCount.Text, out temp))
                    temp = 0;
                if (temp <= 0)
                    return 1;
                else
                    return temp;
            }
            set
            {
                if (this.ParentForm != null && this.ParentForm.InvokeRequired)
                    Invoke(new ThreadStart(() => { txtRepeatCount.Text = value.ToString(); }));
                else
                    txtRepeatCount.Text = value.ToString();
            }
        }

        public bool Started { get; set; }

        private void txtStartTime_Leave(object sender, EventArgs e)
        {
            if (txtStartTime.Text.EndsWith("."))
                txtStartTime.Text += "000";
            txtStartTime.Text = StartTime.ToString("c");
            if (txtStartTime.Text.EndsWith(".") || txtStartTime.Text.EndsWith(","))
                txtStartTime.Text += "000";
            if (StartTime == TimeSpan.Zero)
                txtStartTime.Text = string.Empty;
        }

        private void chbUse_CheckedChanged(object sender, EventArgs e)
        {
            txtNumUser.Enabled = txtStartTime.Enabled = txtFrequency.Enabled = !IsUse;
            txtRepeatCount.Enabled = txtRepeatInterval.Enabled = !IsUse;
            txtNumUser.ReadOnly = txtStartTime.ReadOnly = txtFrequency.ReadOnly = IsUse;
            txtRepeatCount.ReadOnly = txtRepeatInterval.ReadOnly = IsUse;

            if (chbUse.Checked)
            {
                if (DateTime.Now.TimeOfDay > StartTime)
                    StartDate = DateTime.Now.Date.AddDays(1);
                else
                    StartDate = DateTime.Now.Date;
            }
        }

        public void Reset()
        {
            this.Frequency = 0;
            this.IsUse = false;
            this.NumberOfInput = 0;
            this.RepeatCount = 1;
            this.RepeatInterval = 0;
            this.StartDate = new System.DateTime(((long)(0)));
            this.Started = false;
            this.StartTime = System.TimeSpan.Parse("00:00:00");
        }
    }
}
