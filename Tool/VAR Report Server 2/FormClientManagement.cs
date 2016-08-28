using RemoteContract;
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
    public partial class FormClientManagement : Form
    {
        public FormClientManagement()
        {
            InitializeComponent();
        }

        private static List<ClientAuto> _listClient = new List<ClientAuto>();

        #region Remote Function
        public static int SendCommand(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                if (client.Command.Count > 0)
                    return (int)client.Command.Dequeue();
                return (int)ClientCommand.None;
            }
            return (int)ClientCommand.None;
        }

        public static void LoginClient(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Connected = true;
                client.Command.Enqueue(ClientCommand.UpdateInput);
                client.Command.Enqueue(ClientCommand.UpdateTime);
                UpdateRow(client);
            }
        }

        public static void ReLoginClient(string username, bool isStarted)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Connected = true;
                client.Started = isStarted;
                client.Command.Enqueue(ClientCommand.UpdateInput);
                client.Command.Enqueue(ClientCommand.UpdateTime);
                UpdateRow(client);
            }
        }

        public static void LogoutClient(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Connected = false;
                UpdateRow(client);
            }
        }

        public static bool CheckinClient(string username, bool isStarted)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Started = isStarted;
                client.Connected = true;
                UpdateRow(client);
                return true;
            }
            return false;
        }

        public static void StartClient(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Started = true;
                UpdateRow(client);
            }
        }

        public static void StopClient(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Started = false;
                UpdateRow(client);
            }
        }

        public static List<BotInfo> GetBotInfo(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                return ClientAutoBusiness.GetBotInfo(client);
            }
            return new List<BotInfo>();
        }

        public static List<Input> GetInputList(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                return ClientAutoBusiness.GetInput(client);
            }
            return new List<Input>();
        }

        public static void ClientDisconnect(string username)
        {
            ClientAuto client = GetClient(username);
            if (client != null)
            {
                client.Connected = false;
                UpdateRow(client);
            }
        }

        #endregion

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string key = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (key == string.Empty)
                    row.Visible = true;
                else if (row.Cells[1].Value.ToString().ToLower().Contains(key))
                    row.Visible = true;
                else
                    row.Visible = false;
            }
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            FormAddClientAuto form = new FormAddClientAuto();
            var res = form.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                AddRow(FormAddClientAuto.Client);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var lst = GetSelectedRows();
            foreach (DataGridViewRow row in lst)
                RemoveRow(row);
        }

        private void btnStartClient_Click(object sender, EventArgs e)
        {
            var lst = GetSelectedRows();
            foreach (DataGridViewRow row in lst)
                StartRow(row);
        }

        private void btnStopClient_Click(object sender, EventArgs e)
        {
            var list = GetSelectedRows();
            foreach (DataGridViewRow row in list)
                StopRow(row);
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            var list = GetSelectedRows();
            List<ClientAuto> listClient = new List<ClientAuto>();
            foreach (DataGridViewRow row in list)
                listClient.Add(row.Tag as ClientAuto);

            FormSetTime f = new FormSetTime(listClient);
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                foreach (DataGridViewRow row in list)
                {
                    UpdateRow(row.Tag as ClientAuto);
                }
            }
        }

        private void btnSetInput_Click(object sender, EventArgs e)
        {
            var list = GetSelectedRows();
            List<ClientAuto> listClient = new List<ClientAuto>();
            foreach (DataGridViewRow row in list)
                listClient.Add(row.Tag as ClientAuto);

            FormSetInput f = new FormSetInput(listClient);
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                foreach (DataGridViewRow row in list)
                {
                    UpdateRow(row.Tag as ClientAuto);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 3)
            {
                ClientAuto client = dataGridView1.Rows[e.RowIndex].Tag as ClientAuto;
                var started = dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                if (started == "Start")
                {
                    client.Started = true;
                    client.Command.Enqueue(ClientCommand.Start);
                }
                else
                {
                    client.Started = false;
                    client.Command.Enqueue(ClientCommand.Stop);
                }
                UpdateRow(client);
            }
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Cells[0].Value = chbSelectAll.Checked;
        }

        private void FormClientManagement_Load(object sender, EventArgs e)
        {
            _listClient = ClientAutoBusiness.Load();
            foreach (ClientAuto item in _listClient)
            {
                AddRow(item, false);
            }
        }

        private void FormClientManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientAutoBusiness.Save(_listClient);
            e.Cancel = true;
            this.Visible = false;
        }

        private void AddRow(ClientAuto client, bool isAdd = true)
        {
            int rowIndex = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            row.Tag = client;
            client.Tag = row;

            row.Cells[0].Value = false;
            row.Cells[1].Value = client.Username;
            row.Cells[2].Value = client.Connected;
            row.Cells[3].Value = (client.Started && client.Connected) ? "Stop" : "Start";
            row.Cells[4].Value = client.TimeInfo;
            row.Cells[5].Value = client.Input;

            if (isAdd)
                _listClient.Add(client);
        }

        private void UpdateClientRow(DataGridViewRow row, ClientAuto updater)
        {
            ClientAuto current = row.Tag as ClientAuto;
            current.Username = updater.Username;
            current.Input = updater.Input;
            current.TimeInfo = updater.TimeInfo;

            UpdateRow(current);
        }

        private void RemoveRow(DataGridViewRow row)
        {
            _listClient.Remove(row.Tag as ClientAuto);
            dataGridView1.Rows.Remove(row);
        }

        private static void StartRow(DataGridViewRow row)
        {
            ClientAuto client = row.Tag as ClientAuto;
            client.Started = true;
            client.Command.Enqueue(ClientCommand.Start);
        }

        private static void StopRow(DataGridViewRow row)
        {
            ClientAuto client = row.Tag as ClientAuto;
            client.Started = false;
            client.Command.Enqueue(ClientCommand.Stop);
        }

        private static void UpdateRow(ClientAuto updater)
        {
            DataGridViewRow row = updater.Tag as DataGridViewRow;
            row.Cells[1].Value = updater.Username;
            row.Cells[2].Value = updater.Connected;
            row.Cells[3].Value = (updater.Started && updater.Connected) ? "Stop" : "Start";
            row.Cells[4].Value = updater.TimeInfo;
            row.Cells[5].Value = updater.Input;
        }

        public static ClientAuto GetClient(string username)
        {
            ClientAuto client = null;
            try
            {
                client = _listClient.First(x => username.ToLower() == x.Username.ToLower());
            }
            catch { }
            return client;
        }

        public static bool ClientExists(string username)
        {
            return _listClient.Exists(x => x.Username == username);
        }

        private List<DataGridViewRow> GetSelectedRows()
        {
            List<DataGridViewRow> list = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                    list.Add(row);
            }
            return list;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 3)
                return;

            var f = new FormEditClientAuto(dataGridView1.Rows[e.RowIndex].Tag as ClientAuto);
            var res = f.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                UpdateRow(dataGridView1.Rows[e.RowIndex].Tag as ClientAuto);
            }
        }

        private void FormClientManagement_VisibleChanged(object sender, EventArgs e)
        {

        }

        public static void LoadClientData()
        {
            _listClient = ClientAutoBusiness.Load();
        }
    }
}
