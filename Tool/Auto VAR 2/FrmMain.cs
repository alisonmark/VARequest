using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using CaptchaUtils;
using RemoteContract;

namespace Auto_VAR
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            pnlMain.HorizontalScroll.Enabled = false;
            pnlMain.HorizontalScroll.Visible = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.T:
                    new FrmSetting().ShowDialog();
                    break;
                case Keys.Control | Keys.A:
                    Start();
                    break;
                case Keys.Control | Keys.S:
                    Stop();
                    break;
                case Keys.Control | Keys.O:
                    OpenFileDialog f = new OpenFileDialog();
                    f.Multiselect = false;
                    var res = f.ShowDialog();

                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(f.FileName))
                            {
                                ImportFromFile(f.FileName);
                            }
                        }
                        catch { }
                        UpdateMainTable();
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        FrmWebBrowser _frmWeb = new FrmWebBrowser();

        Thread _threadCheckin;

        bool _isAlreadyStart = false;

        VarBot _mainBot = new VarBot();

        List<BotInfo> _listBotInfo = new List<BotInfo>();

        List<VarTimeInput> _listInputControl = new List<VarTimeInput>();
        List<VarItem> _listItem = new List<VarItem>();

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                VarItem tool = dtgInput.Rows[e.RowIndex].Tag as VarItem;
                _frmWeb.webBrowser1.DocumentText = tool.LastResponse;
                _frmWeb.Show();
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            _listInputControl = new List<VarTimeInput>
            {
                varTimeInput1, 
                varTimeInput2, 
                varTimeInput3, 
                varTimeInput4, 
                varTimeInput5, 
                varTimeInput6, 
                varTimeInput7, 
                varTimeInput8, 
                varTimeInput9, 
                varTimeInput10, 
                varTimeInput11, 
                varTimeInput12,
                varTimeInput13
            };

            _mainBot.SetListControl(_listInputControl);
            Setting.UseProxy = loadProxyToolStripMenuItem.Checked;

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        Invoke(new ThreadStart(() =>
                        {
                            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
                        }));
                    }
                    catch { }
                }
            }).Start();

            _threadCheckin = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(2000);
                        if (Client.Connected)
                        {
                            try
                            {
                                string res = Client.Checkin();
                                if (!string.IsNullOrEmpty(res))
                                {
                                    Stop();
                                    ShowConfirmRetryLogin();
                                }

                            }
                            catch (Exception ex)
                            {
                                Stop();
                                ShowConfirmRetryLogin();
                            }
                        }
                    }
                }));
            _threadCheckin.Start();

            mnsManualCaptcha.Checked = Setting.CaptchaManual == 1;
        }

        private void Start()
        {
            if (_mainBot != null)
            {
                _mainBot.Stop();
            }

            _mainBot.Start();

            Invoke(new ThreadStart(() =>
            {
                mnsStart.Enabled = false;
                mnsStop.Enabled = true;
                lblStatus.Text = "Running";
            }));
        }

        private void Stop()
        {
            if (_mainBot != null)
            {
                _mainBot.Stop();
            }

            Invoke(new ThreadStart(() =>
            {
                mnsStart.Enabled = true;
                mnsStop.Enabled = false;
                lblStatus.Text = "Stopped";
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (mnsStart.Enabled)
            {
                _isAlreadyStart = true;
                Start();
            }
            else
            {
                _isAlreadyStart = false;
                Stop();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new VarTimeInput() { NumberOfInput = 0, Frequency = 0 };
            pnlMain.Controls.Add(item);
            _listInputControl.Add(item);
        }

        private void btnLoadInput_Click(object sender, EventArgs e)
        {
#if TESTMODE
            GenerateTestData(); 
#else
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = false;
            var res = f.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (!string.IsNullOrEmpty(f.FileName))
                    {
                        ImportFromFile(f.FileName);
                    }
                }
                catch { }
            }
#endif
            UpdateMainTable();

        }

        private void btnSaveInput_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(f.FileName))
                    ExportToFile(f.FileName);
            }
        }

        Random random = new Random();
        private void GenerateTestData()
        {
            for (int i = 0; i < 10; i++)
            {
                VarObject obj = new VarObject();
                obj.Citizenship = new KeyValueItem("39", "Vietnam (Việt Nam)");
                obj.CountryOfBirth = new KeyValueItem("39", "Vietnam (Việt Nam)");
                obj.DateOfBirth = "02/09/199" + i;
                obj.Email = string.Format("test{1:000}_{0:00}@gmail.com", i, random.Next(100, 999));
                obj.Embassy = new KeyValueItem("129", "Vietnam (Việt Nam) - Hanoj");
                obj.FamilyName = string.Format("family{0:00}", i);
                obj.Name = string.Format("test{0:00}", i);
                obj.Gender = "Male";
                obj.Passport = string.Format("B{0:00000}", random.Next(10000, 99999));
                obj.Phone = "9852115745";
                obj.PurposeOfStay = new KeyValueItem("3", "Long-term visa for Business");
                obj.Status = VarState.Ready;

                VarItem item = new VarItem(obj);
                _listItem.Add(item);
            }
        }

        private void UpdateMainTable()
        {
            dtgInput.Rows.Clear();
            foreach (VarItem item in _listItem)
            {
                int rowIndex = dtgInput.Rows.Add(dtgInput.Rows.Count, item.Item.ToString(), item.Item.Status, item.Item.CaptchaText, string.Empty);
                DataGridViewRow row = dtgInput.Rows[rowIndex];
                row.Tag = item;
                item.Tag = row;

                item.ShowMessage = (sender, msg) =>
                {
                    new Thread(() =>
                        {
                            VarItem currItem = sender as VarItem;
                            DataGridViewRow currRow = (DataGridViewRow)currItem.Tag;
                            Invoke(new ThreadStart(() =>
                            {
                                currRow.Cells[1].Value = currItem.Item.ToString();
                                currRow.Cells[2].Value = currItem.Item.Status;
                                currRow.Cells[3].Value = currItem.Item.RequestTime == DateTime.MinValue ? string.Empty : currItem.Item.RequestTime.ToString("HH:mm:ss fff");
                                currRow.Cells[4].Value = currItem.Item.NotOpenTime;
                                currRow.Cells[5].Value = currItem.Item.OpenTime;
                                currRow.Cells[6].Value = currItem.Item.CaptchaText;
                                currRow.Cells[7].Value = currItem.CurrentProxy;
                                currRow.Cells[8].Value = msg;
                            }));
                        }).Start();
                };
            }
        }

        private void UpdateScheduleTable()
        {
            foreach (VarTimeInput control in _listInputControl)
            {
                control.Reset();
            }

            for (int i = _listInputControl.Count; i < _listBotInfo.Count; i++)
            {
                var item = new VarTimeInput() { NumberOfInput = 0, Frequency = 0 };
                pnlMain.Controls.Add(item);
                _listInputControl.Add(item);
            }

            for (int i = 0; i < _listBotInfo.Count; i++)
            {
                var control = _listInputControl[i];
                var bot = _listBotInfo[i];

                control.NumberOfInput = bot.NumberOfInput;
                control.StartTime = bot.StartTime;
                control.StartDate = bot.StartDate;
                control.RepeatCount = bot.RepeatCount;
                control.RepeatInterval = bot.RepeatInterval;
                control.Frequency = bot.Frequency;

                control.IsUse = bot.Checked;
            }
        }

        private void UpdateScheduleList()
        {
            _listBotInfo.Clear();
            for (int i = 0; i < _listInputControl.Count; i++)
            {
                var control = _listInputControl[i];
                if (control.StartTime != TimeSpan.Zero)
                {
                    BotInfo bot = new BotInfo
                    {
                        NumberOfInput = control.NumberOfInput,
                        StartTime = control.StartTime,
                        StartDate = control.StartDate,
                        RepeatCount = control.RepeatCount,
                        RepeatInterval = control.RepeatInterval,
                        Frequency = control.Frequency,

                        Checked = control.IsUse
                    };
                    _listBotInfo.Add(bot);
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Client.Connected)
            {
                try { Client.StopRemotingClient(); }
                catch { }
            }
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void dtgInput_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Checks for correct column index
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                VarItem item = dtgInput.Rows[e.RowIndex].Tag as VarItem;
                if (item.Item.Status != VarState.NotSubmit)
                    return;
                //Create the ContextStripMenu for Creating the PO Sub Form
                ContextMenuStrip Menu = new ContextMenuStrip();
                ToolStripMenuItem MenuOpenPO = new ToolStripMenuItem("Xác nhận đăng ký lại");
                MenuOpenPO.MouseDown += new MouseEventHandler((o, s) =>
                {
                    item.Step7_PreviewInfo();
                });
                Menu.Items.AddRange(new ToolStripItem[] { MenuOpenPO });

                //Assign created context menu strip to the DataGridView
                dtgInput.ContextMenuStrip = Menu;
            }

            else
                dtgInput.ContextMenuStrip = null;
        }

        #region MenuItems
        private void mnsStart_Click(object sender, EventArgs e)
        {
            if (mnsStart.Enabled)
            {
                _isAlreadyStart = true;
                Start();
            }
        }

        private void mnsStop_Click(object sender, EventArgs e)
        {
            if (!mnsStart.Enabled)
            {
                _isAlreadyStart = false;
                Stop();
            }
        }

        private void mnsLoadInput_Click(object sender, EventArgs e)
        {
#if TESTMODE
            GenerateTestData();
#else
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = false;
            var res = f.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (!string.IsNullOrEmpty(f.FileName))
                    {
                        ImportFromFile(f.FileName);
                    }
                }
                catch { }
            }
#endif
            UpdateMainTable();
        }

        private void mnsSaveInput_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(f.FileName))
                    ExportToFile(f.FileName);
            }
        }

        private void mnsExit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void mnsAddNewRow_Click(object sender, EventArgs e)
        {
            var item = new VarTimeInput() { NumberOfInput = 0, Frequency = 0 };
            pnlMain.Controls.Add(item);
            _listInputControl.Add(item);
        }

        private void mnsChooseService0_Click(object sender, EventArgs e)
        {
            VarItem.CaptchaProviderIndex = 0;
            mnsChooseService0.Checked = true;
            mnsChooseService1.Checked = false;
            mnsChooseService2.Checked = false;
            mnsChooseService3.Checked = false;
        }

        private void mnsChooseService1_Click(object sender, EventArgs e)
        {
            VarItem.CaptchaProviderIndex = 1;
            mnsChooseService0.Checked = false;
            mnsChooseService1.Checked = true;
            mnsChooseService2.Checked = false;
            mnsChooseService3.Checked = false;
        }

        private void mnsChooseService2_Click(object sender, EventArgs e)
        {
            VarItem.CaptchaProviderIndex = 2;
            mnsChooseService0.Checked = false;
            mnsChooseService1.Checked = false;
            mnsChooseService2.Checked = true;
            mnsChooseService3.Checked = false;
        }

        private void mnsChooseService3_Click(object sender, EventArgs e)
        {
            VarItem.CaptchaProviderIndex = 3;
            mnsChooseService0.Checked = false;
            mnsChooseService1.Checked = false;
            mnsChooseService2.Checked = false;
            mnsChooseService3.Checked = true;
        }


        private void mnsManualCaptcha_Click(object sender, EventArgs e)
        {
            mnsManualCaptcha.Checked = !mnsManualCaptcha.Checked;
            Setting.CaptchaManual = mnsManualCaptcha.Checked ? 1 : 0;
        }

        private void mnsCheckService0_Click(object sender, EventArgs e)
        {
            try
            {
                double balance = CaptchaUtils.DeathByCaptchaDecaptcher.GetBalance(Setting.UsernameDbc, Setting.PasswordDbc);
                MessageBox.Show("Balance: " + balance.ToString(), "www.deathbycaptcha.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnsCheckService1_Click(object sender, EventArgs e)
        {
            try
            {
                string balance = CaptchaUtils.DeCaptcherDecaptcher.GetBalance(Setting.UsernameDeCaptcher, Setting.PasswordDeCaptcher);
                MessageBox.Show("Balance: " + balance.ToString(), "de-captcher.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnsCheckService2_Click(object sender, EventArgs e)
        {
            try
            {
                string balance = CaptchaUtils.ImageTyperzDecaptcher.GetBalance(Setting.UsernameImagetyperz, Setting.PasswordImagetyperz);
                MessageBox.Show("Balance: " + balance.ToString(), "www.imagetyperz.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnsCheckService3_Click(object sender, EventArgs e)
        {
            try
            {
                string balance = CaptchaUtils.ShanibpoDecaptcher.GetBalance(Setting.UsernameShanibpo, Setting.PasswordShanibpo);
                MessageBox.Show("Balance: " + balance.ToString(), "www.shanibpo.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnsSetting_Click(object sender, EventArgs e)
        {
            new FrmSetting().ShowDialog();
        }

        private void useToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.UseProxy = loadToolStripMenuItem.Checked;
        }

        private void loadProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmProxy().ShowDialog();
        }

        private void mnsImportSchedule_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = false;
            var res = f.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (!string.IsNullOrEmpty(f.FileName))
                    {
                        ImportScheduleFromFile(f.FileName);
                    }
                }
                catch { }
            }

            UpdateScheduleTable();
        }

        private void mnsExportSchedule_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(f.FileName))
                    ExportScheduleToFile(f.FileName);
            }
        } 
        #endregion

        #region Utils
        private void ImportFromFile(string fileName)
        {
            _listItem.Clear();
            using (StreamReader r = new StreamReader(fileName))
            {
                string line = string.Empty;
                while (!r.EndOfStream)
                {
                    line = r.ReadLine();
                    VarObject obj = VarObject.FromString(line);
                    if (obj != null)
                    {
                        VarItem item = new VarItem(obj);
                        item.StartManualCaptcha = (s, e) =>
                        {
                            if (!ucCaptchaBox1.Running)
                            {
                                VarItem.CountManual++;
                                Invoke(new ThreadStart(() => { ucCaptchaBox1.Start((VarItem)s, e); }));
                                while (ucCaptchaBox1.Running) ;
                                VarItem.CountManual--;
                            }
                            else if (!ucCaptchaBox2.Running)
                            {
                                VarItem.CountManual++;
                                Invoke(new ThreadStart(() => { ucCaptchaBox2.Start((VarItem)s, e); }));
                                while (ucCaptchaBox2.Running) ;
                                VarItem.CountManual--;
                            }
                            else if (!ucCaptchaBox3.Running)
                            {
                                VarItem.CountManual++;
                                Invoke(new ThreadStart(() => { ucCaptchaBox3.Start((VarItem)s, e); }));
                                while (ucCaptchaBox3.Running) ;
                                VarItem.CountManual--;
                            }
                        };

                        _listItem.Add(item);
                    }
                }
            }
            _mainBot.SetListItem(_listItem);
        }

        private void ImportFromString(string content)
        {
            _listItem.Clear();
            string[] lines = content.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); ;
            foreach (string line in lines)
            {
                VarObject obj = VarObject.FromString(line);
                if (obj != null)
                {
                    VarItem item = new VarItem(obj);
                    item.StartManualCaptcha = (s, e) =>
                    {
                        if (!ucCaptchaBox1.Running)
                        {
                            VarItem.CountManual++;
                            Invoke(new ThreadStart(() => { ucCaptchaBox1.Start((VarItem)s, e); }));
                            while (ucCaptchaBox1.Running) ;
                            VarItem.CountManual--;
                        }
                        else if (!ucCaptchaBox2.Running)
                        {
                            VarItem.CountManual++;
                            Invoke(new ThreadStart(() => { ucCaptchaBox2.Start((VarItem)s, e); }));
                            while (ucCaptchaBox2.Running) ;
                            VarItem.CountManual--;
                        }
                        else if (!ucCaptchaBox3.Running)
                        {
                            VarItem.CountManual++;
                            Invoke(new ThreadStart(() => { ucCaptchaBox3.Start((VarItem)s, e); }));
                            while (ucCaptchaBox3.Running) ;
                            VarItem.CountManual--;
                        }
                    };

                    _listItem.Add(item);
                }
            }
            _mainBot.SetListItem(_listItem);
        }

        private void ExportToFile(string fileName)
        {
            using (StreamWriter w = new StreamWriter(fileName))
            {
                foreach (VarItem item in _listItem)
                {
                    w.WriteLine(item.Item);
                }
            }
        }

        private void ImportScheduleFromFile(string fileName)
        {
            _listBotInfo.Clear();
            using (StreamReader r = new StreamReader(fileName))
            {
                string line = string.Empty;
                while (!r.EndOfStream)
                {
                    line = r.ReadLine();
                    BotInfo obj = GetBotFromString(line);
                    if (obj != null)
                    {
                        _listBotInfo.Add(obj);
                    }
                }
            }
            _mainBot.SetListItem(_listItem);
        }

        private void ExportScheduleToFile(string fileName)
        {
            UpdateScheduleList();
            using (StreamWriter w = new StreamWriter(fileName))
            {
                foreach (BotInfo item in _listBotInfo)
                {
                    w.WriteLine(ConvertBotToString(item));
                }
            }
        }

        private void ImportScheduleFromString(string content)
        {
            _listBotInfo.Clear();
            string[] lines = content.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); ;
            foreach (string line in lines)
            {
                BotInfo obj = GetBotFromString(line);
                if (obj != null)
                {
                    _listBotInfo.Add(obj);
                }
            }
            _mainBot.SetListItem(_listItem);
        }

        private void ShowStatus(string mess)
        {
            if (InvokeRequired)
            {
                Invoke(new ThreadStart(() =>
                {
                    lblStatus.Text = mess;
                }));
            }
            else
            {
                lblStatus.Text = mess;
            }
        }

        private void ShowMessage(string mess)
        {
            if (InvokeRequired)
            {
                Invoke(new ThreadStart(() =>
                {
                    MessageBox.Show(mess);
                }));
            }
            else
            {
                MessageBox.Show(mess);
            }
        }

        private void ShowConfirmRetryLogin()
        {
            Invoke(new ThreadStart(() =>
            {
                var res = MessageBox.Show("Mất kết nối với máy chủ. Bạn có muốn thử kết nối lại không?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        var loginRes = Client.Checkin();
                        if (string.IsNullOrEmpty(loginRes))
                        {
                            if (_isAlreadyStart)
                                Start();
                        }
                        else
                        {
                            MessageBox.Show(loginRes);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowConfirmRetryLogin();
                    }
                }
                else
                {
                    this.Close();
                }
            }));
        }

        private BotInfo GetBotFromString(string str)
        {
            try
            {
                BotInfo obj = new BotInfo();
                string[] time = str.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                obj.Checked = time[0] == "1";
                obj.NumberOfInput = int.Parse(time[1]);
                obj.StartTime = TimeSpan.Parse(time[2]);
                obj.Frequency = int.Parse(time[3]);
                obj.RepeatInterval = int.Parse(time[4]);
                obj.RepeatCount = int.Parse(time[5]);

                return obj;
            }
            catch { }
            return null;
        }

        private string ConvertBotToString(BotInfo bot)
        {
            return string.Format("{0};{1};{2};{3};{4};{5}"
                                , bot.Checked ? 1 : 0
                                , bot.NumberOfInput
                                , bot.StartTime.ToString(@"hh\:mm\:ss\.fff")
                                , bot.Frequency
                                , bot.RepeatInterval
                                , bot.RepeatCount);
        } 
        #endregion

        private void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);
                ImportFromString(text);
                UpdateMainTable();
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    ImportFromFile(files[0]);
                    UpdateMainTable();
                }
            }
        }

        private void fromTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmScheduleBox box = new FrmScheduleBox();
            if (box.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImportScheduleFromString(FrmScheduleBox.ScheduleText);
                UpdateScheduleTable();
            }
        }

        private void dtgInput_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);
                ImportFromString(text);
                UpdateMainTable();
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    ImportFromFile(files[0]);
                    UpdateMainTable();
                }
            }
        }

        private void pnlMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.Data.GetData(DataFormats.Text);
                ImportFromString(text);
                UpdateMainTable();
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    ImportFromFile(files[0]);
                    UpdateMainTable();
                }
            }
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void InitCaptCharBox()
        {
            
        }
    }
}
