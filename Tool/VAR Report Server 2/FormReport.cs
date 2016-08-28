using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using RemoteContract;
using System.Threading;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.IO;

namespace VAR_Report_Server
{
    public partial class FormReport : Form
    {
        private List<Report> _reportList = new List<Report>();
        private List<Report> _importList = new List<Report>();

        private Thread _threadAutoExport;
        private object _lockListReport = new object();

        private Dictionary<string, User> _userList = new Dictionary<string, User>();
        private Dictionary<string, ListViewItem> _userItem = new Dictionary<string, ListViewItem>();

        protected bool _isShowingImport
        {
            get
            {
                bool res = false;
                if (InvokeRequired)
                    Invoke(new ThreadStart(() => { res = cbbView.SelectedIndex == 1; }));
                else
                    res = cbbView.SelectedIndex == 1;
                return res;
            }
        }

        private Thread _threadCheckin;
        private object _lockUser = new object();
        private bool _isRunning = false;

        public FormReport()
        {
            InitializeComponent();
        }

        TcpChannel tcpChannel;
        private void StartRemoteServer()
        {
            int port = -1;
            if (!int.TryParse(txtPort.Text, out port) || port < 1024 || port > 49151)
            {
                MessageBox.Show("ServerPort phải là số nguyên từ 1024 đến 49151");
                return;
            }

            try
            {
                BinaryServerFormatterSinkProvider bp = new BinaryServerFormatterSinkProvider();
                ClientIPServerSinkProvider csp = new ClientIPServerSinkProvider();
                csp.Next = bp;
                Hashtable ht = new Hashtable();
                ht.Add("port", port); // Your remoting port number

                tcpChannel = new TcpChannel(ht, null, csp);
                ChannelServices.RegisterChannel(tcpChannel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(ResultServer), RemoteSetting.REMOTE_SERVER_NAME, WellKnownObjectMode.SingleCall);

                _isRunning = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try { if (_threadCheckin != null) _threadCheckin.Abort(); }
            catch { }

            _threadCheckin = new Thread(() =>
            {
                while (_isRunning)
                {
                    Thread.Sleep(500);
                    lock (_lockUser)
                    {
                        foreach (User user in _userList.Values)
                        {
                            if (user.IsOnline && DateTime.Now - user.CheckinTime > TimeSpan.FromSeconds(20))
                            {
                                user.IsOnline = false;
                                FormClientManagement.ClientDisconnect(user.Username);
                                UpdateListViewUser(user);
                            }
                        }
                    }
                }
            });
            _threadCheckin.Start();

            _threadAutoExport = new Thread(() =>
            {
                while (_isRunning)
                {
                    Thread.Sleep(120 * 60000);
                    try
                    {
                        lock (_lockListReport)
                        {
                            if (_reportList.Count > 0)
                            {
                                string folder = "AutoSave";
                                if (!Directory.Exists(folder))
                                    Directory.CreateDirectory(folder);
                                string file = string.Format("{0}/{1}.xml", folder, DateTime.Now.ToString("yyyy_MM_dd_HH_mm"));
                                ReportUtils.Export(_reportList, file);
                                _reportList.Clear();
                                BindDataToReportTable(_reportList);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            });
            _threadAutoExport.Start();

            btnStart.Text = "Stop";
            lblStatus.Text = "Server is running !";

            MessageBox.Show("Server Started");
        }

        private void StopRemoteServer()
        {
            _isRunning = false;

            try { if (_threadCheckin != null) _threadCheckin.Abort(); }
            catch { }

            try { if (_threadAutoExport != null) _threadAutoExport.Abort(); }
            catch { }

            ChannelServices.UnregisterChannel(tcpChannel);

            foreach (User user in _userList.Values)
            {
                user.IsOnline = false;
                UpdateListViewUser(user);
            }

            btnStart.Text = "Start";
            lblStatus.Text = "Server has stopped !";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_isRunning)
                StopRemoteServer();
            else
                StartRemoteServer();
        }

        private void UpdateListViewUser(User user)
        {
            Invoke(new ThreadStart(() =>
            {
                ListViewItem item = _userItem[user.Username];
                item.SubItems[1].Text = user.IPAddress;
                item.SubItems[2].Text = user.Success;
                item.ToolTipText = CreateTooltip(user);
                item.ImageIndex = user.IsOnline ? 1 : 0;
            }));
        }

        private void InitListViewUser()
        {
            lvUser.Columns.Add("Name", lvUser.Width - 45 - 5);
            lvUser.Columns.Add("IP Address", 0);
            lvUser.Columns.Add("Succeeded", 45, HorizontalAlignment.Center);
        }

        private void InitFunctionPointer()
        {
            UserLogin = new FpUserLogin((username) =>
            {
                if (!_isRunning) return "stopped";
                User user = UserBusiness.GetUser(username);
                if (user == null)
                {
                    ClientAuto client = FormClientManagement.GetClient(username);
                    if (client != null)
                    {
                        UserBusiness.Insert(new User { Username = client.Username });
                        user = UserBusiness.GetUser(username);
                    }
                    else
                        return "User không tồn tại";
                }

                username = user.Username;

                if (_userList.ContainsKey(username) && _userList[username].IsOnline == true)
                    return "User này đang Online";

                User loginUser = new User
                {
                    Username = username,
                    IPAddress = CallContext.GetData("ClientIPAddress").ToString(),
                    LoginTime = DateTime.Now,
                    CheckinTime = DateTime.Now,
                    IsOnline = true
                };

                if (!_userList.ContainsKey(username))
                {
                    Invoke(new ThreadStart(() =>
                    {
                        ListViewItem lvItem = new ListViewItem(loginUser.Username, loginUser.IsOnline ? 1 : 0);
                        lvItem.ToolTipText = CreateTooltip(loginUser);
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, loginUser.IPAddress));
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, loginUser.Success));
                        lvUser.Items.Add(lvItem);

                        lock (_lockUser) { _userItem[username] = lvItem; }
                    }));
                }
                else
                {
                    loginUser.SubmitCount = _userList[username].SubmitCount;
                    loginUser.SuccessCount = _userList[username].SuccessCount;
                    UpdateListViewUser(loginUser);
                }

                lock (_lockUser) { _userList[username] = loginUser; }
                return user.Username;
            });

            UserCheckin = new FpUserCheckin((string username) =>
            {
                if (!_isRunning) return "stopped";
                if (!_userList.ContainsKey(username))
                    return "Invalid Session";
                lock (_lockUser)
                {
                    _userList[username].IsOnline = true;
                    _userList[username].CheckinTime = DateTime.Now;
                }
                UpdateListViewUser(_userList[username]);
                return string.Empty;
            });

            UserLogout = new FpUserLogout((string username) =>
            {
                if (!_userList.ContainsKey(username))
                    return;
                _userList[username].IsOnline = false;
                UpdateListViewUser(_userList[username]);
            });

            AddResult = new FpSendResult((Report r) =>
            {
                Invoke(new ThreadStart(() =>
                    {
                        if (_userList.ContainsKey(r.Name))
                        {
                            User user = _userList[r.Name];
                            user.SubmitCount++;
                            if (r.Result == "Success") user.SuccessCount++;
                            UpdateListViewUser(user);
                        }
                        lock (_lockListReport)
                        {
                            _reportList.Add(r);
                        }

                        if (!_isShowingImport)
                        {
                            AddRowToReportTable(r, true);
                        }
                    }));
            });
        }

        public static FpUserLogin UserLogin { get; set; }
        public static FpUserCheckin UserCheckin { get; set; }
        public static FpSendResult AddResult { get; set; }
        public static FpUserLogout UserLogout { get; set; }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            lvUser.Columns[0].Width = lvUser.Width - lvUser.Columns[2].Width - 5;
        }

        private string CreateTooltip(User user)
        {
            return string.Format("Tên: {0}\r\n"
                                            + "Trạng thái: {1}\r\n"
                                            + "Địa chỉ IP: {2}\r\n"
                                            + "Đăng nhập: {3}\r\n"
                                            + "Thành công: {4}\r\n"
                                            , user.Username, user.IsOnline ? "Online" : "Offline", user.IPAddress, user.LoginTime.ToString("dd/MM/yyyy HH:mm:ss"), user.Success);
        }

        private void InitPurposeOfStay()
        {
            for (int i = 0; i < _listVisaTypeKey.Count; i++)
            {
                _listVisaType.Add(new KeyValueItem(_listVisaTypeKey[i].ToString(), _listVisaTypeValue[i]));
            }
            List<KeyValueItem> listData = new List<KeyValueItem> { new KeyValueItem("-1", "All") };
            listData.AddRange(_listVisaType.FindAll((item) => item.Key == "7" || item.Key == "14" || item.Key == "2"));

            cbbPurposeOfStay.DataSource = listData;
            cbbPurposeOfStay.ValueMember = "Key";
            cbbPurposeOfStay.DisplayMember = "Value";
        }

        private void InitComboboxResult()
        {
            cbbSuccess.SelectedIndex = 0;
            //List<string> lstData = new List<string>
            //{
            //    "All",
            //    "Success",
            //    "Failed",
            //    "Not open"
            //};
            //cbbSuccess.DataSource = lstData;
        }

        private void FormReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Exit program?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                if (_isRunning)
                    StopRemoteServer();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
                e.Cancel = true;
        }

        private List<KeyValueItem> _listVisaType = new List<KeyValueItem>();

        //public static List<string> _listVisaTypeValue  = new List<string> { "Long-term visa for Business", "Long-term visa for Other Purposes", "Application for Permanent Residence Permit", "Application for a long term residence for the purpose of employment \"Blue Card\"", "Long-term visa for purposes- health/culture/sport", "Long-term residence permit - family reunification", "Long-term residence permit for study", "Long-term residence permit - scientific research", "Long-term visa - family reunification", "Long-term visa for study", "Long-term visa – scientific research", "Transfer of a valid visa to a new travel document", "Long-term residence permit - Employment card" };

        //public static List<int> ListPurposeOfStayKey = new List<int> { 3, 6, 7, 10, 13, 14, 15, 16, 2, 4, 12, 9, 19 };

        public static List<string> _listVisaTypeValue = new List<string> { "Application for Permanent Residence Permit", "Long-term residence permit", "Long-term visa", "Long-term visa for Business" };

        public static List<int> _listVisaTypeKey = new List<int> { 7, 20, 18, 3 };

        private void FormReport_Load(object sender, EventArgs e)
        {
            FormClientManagement.LoadClientData();
            _formClientAuto.Show();
            _formClientAuto.Visible = false;

            InitFunctionPointer();
            InitPurposeOfStay();
            InitListViewUser();
            InitComboboxResult();

            cbbView.SelectedIndex = 0;
        }

        private bool _isFiltering = false;
        private string _filterName;
        private string _filterPurpose;
        private string _filterResult;
        private void btnFilter_Click(object sender, EventArgs e)
        {
            _isFiltering = true;
            _filterName = txtName.Text.Trim();
            _filterPurpose = ((KeyValueItem)cbbPurposeOfStay.SelectedItem).Key == "-1" ? string.Empty : ((KeyValueItem)cbbPurposeOfStay.SelectedItem).Value;
            _filterResult = cbbSuccess.Text == "All" ? string.Empty : cbbSuccess.Text;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isShow = string.IsNullOrEmpty(_filterName) || row.Cells[1].Value.ToString().ToLower().Contains(_filterName.ToLower());
                isShow &= string.IsNullOrEmpty(_filterPurpose) || row.Cells[5].Value.ToString().ToLower().Contains(_filterPurpose.ToLower());
                isShow &= string.IsNullOrEmpty(_filterResult) || row.Cells[4].Value.ToString().ToLower().Contains(_filterResult.ToLower());

                row.Visible = isShow;
            }
        }

        Form2 f = new Form2();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 7)
            //{
            //    f.webBrowser1.DocumentText = ((Report)dataGridView1.Rows[e.RowIndex].Tag).ResultHtml;
            //    f.ShowDialog();
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _isFiltering = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Visible = true;
        }

        FormUser _formUser = new FormUser();
        private void btnUserList_Click(object sender, EventArgs e)
        {
            _formUser.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Xml files (*.xml)|*.xml";
            dialog.Multiselect = false;
            var res = dialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                _importList = ReportUtils.Import(file);
                BindDataToReportTable(_importList);
                cbbView.SelectedIndex = 1;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Xml files (*.xml)|*.xml";
            var res = dialog.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                lock (_lockListReport)
                {
                    ReportUtils.Export(_reportList, file);
                }
            }
        }

        private void BindDataToReportTable(List<Report> source)
        {
            dataGridView1.Rows.Clear();
            foreach (Report r in source)
            {
                AddRowToReportTable(r);
            }

            if (dataGridView1.SortedColumn != null)
            {
                ListSortDirection direction = ListSortDirection.Ascending;
                switch (dataGridView1.SortOrder)
                {
                    case SortOrder.Ascending: direction = ListSortDirection.Ascending; break;
                    case SortOrder.Descending: direction = ListSortDirection.Descending; break;
                }
                dataGridView1.Sort(dataGridView1.SortedColumn, direction);
            }
        }

        private void AddRowToReportTable(Report r, bool isSort = false)
        {
            DataGridViewRow newRow = dataGridView1.Rows[dataGridView1.Rows.Add()];
            newRow.Tag = r;
            newRow.Cells[0].Value = dataGridView1.Rows.Count;

            newRow.Cells[1].Value = r.Name;
            newRow.Cells[1].ToolTipText = r.Name;

            newRow.Cells[2].Value = r.SentTime.ToString("dd/MM/yyyy");
            newRow.Cells[2].ToolTipText = r.SentTime.ToString("dd/MM/yyyy");

            newRow.Cells[3].Value = r.SentTime.ToString("HH:mm:ss");
            newRow.Cells[3].ToolTipText = r.SentTime.ToString("HH:mm:ss");

            newRow.Cells[4].Value = r.Result;
            newRow.Cells[4].ToolTipText = r.Result;

            newRow.Cells[5].Value = r.OpenTime;
            newRow.Cells[5].ToolTipText = r.OpenTime;

            newRow.Cells[6].Value = r.NotOpenTime;
            newRow.Cells[6].ToolTipText = r.NotOpenTime;

            newRow.Cells[7].Value = _listVisaType.First((x) => x.Key == r.PurposeOfStay.ToString()).Value;
            newRow.Cells[7].ToolTipText = _listVisaType.First((x) => x.Key == r.PurposeOfStay.ToString()).Value;

            newRow.Cells[8].Value = r.Input;
            newRow.Cells[8].ToolTipText = r.Input;

            newRow.Cells[9].Value = r.ErrorMessage;
            newRow.Cells[9].ToolTipText = r.ErrorMessage;

            if (isSort)
            {
                if (dataGridView1.SortedColumn != null)
                {
                    ListSortDirection direction = ListSortDirection.Ascending;
                    switch (dataGridView1.SortOrder)
                    {
                        case SortOrder.Ascending: direction = ListSortDirection.Ascending; break;
                        case SortOrder.Descending: direction = ListSortDirection.Descending; break;
                    }
                    dataGridView1.Sort(dataGridView1.SortedColumn, direction);
                }

                if (_isFiltering)
                {
                    bool isShow = string.IsNullOrEmpty(_filterName) || newRow.Cells[1].Value.ToString().ToLower().Contains(_filterName.ToLower());
                    isShow &= string.IsNullOrEmpty(_filterPurpose) || newRow.Cells[7].Value.ToString().ToLower().Contains(_filterPurpose.ToLower());
                    isShow &= string.IsNullOrEmpty(_filterResult) || newRow.Cells[4].Value.ToString().ToLower().Contains(_filterResult.ToLower());
                    newRow.Visible = isShow;
                }
            }

            if (!string.IsNullOrEmpty(r.OpenTime.Trim()))
            {
                string subject = string.Format("Đặt lịch hẹn thành công! OpenTime: {0}", r.OpenTime);
                string body = string.Format(@"Client: {0}
                                            SentTime: {1}
                                            Result: {2}
                                            OpenTime: {3}
                                            PurposeOfStay: {4}
                                            Input: {5}"
                                            , r.Name, r.SentTime, r.Result, r.OpenTime, newRow.Cells[7].Value, r.Input);
                SendMail(subject, body);
            }
        }

        private void cbbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isShowingImport)
                BindDataToReportTable(_importList);
            else
                BindDataToReportTable(_reportList);
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            if (_isShowingImport)
            {
                _importList.Clear();
                BindDataToReportTable(_importList);
            }
            else
            {
                lock (_lockListReport)
                {
                    if (_reportList.Count > 0)
                    {
                        var res = MessageBox.Show("Toàn bộ dữ liệu hiện tại sẽ bị xoá.", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == System.Windows.Forms.DialogResult.OK)
                        {
                            _reportList.Clear();
                            BindDataToReportTable(_reportList);
                        }
                    }
                }
            }
        }

        private FormClientManagement _formClientAuto = new FormClientManagement();
        private void btnClientAutoList_Click(object sender, EventArgs e)
        {
            if (!_formClientAuto.Visible)
                _formClientAuto.Show();
        }

        private void SendMail(string subject, string body)
        {
            new Thread(() =>
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Sender: {0}\nTo: {1}\nError: {2}", senderUser, receiverUser, ex
                        .Message), "Gửi email thất bại.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }).Start();
        }

        private void btnMailSetting_Click(object sender, EventArgs e)
        {
            new FrmSetting().ShowDialog();
        }
    }

    internal class KeyValueItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValueItem() { }
        public KeyValueItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public static class VarState
    {
        public static string Running = "Running";
        public static string Ready = "Ready";
        public static string Success = "Success";
        public static string Failed = "Failed";
        public static string Error = "Error";
        public static string NotOpen = "NotOpen";
        public static string NotSubmit = "NotSubmit";
        public static string InvalidCaptcha = "InvalidCaptcha";
        public static string ForceStopped = "ForceStopped";
    }
}
