/*
- Mỗi input sẽ tương ứng với 1 bot. 
- Mỗi khi đến giờ hẹn của 1 dòng hẹn bất kỳ (một dòng hẹn có thể yêu cầu nhiều bot), sẽ xảy ra các TH:
 + Bot running hoặc success => bỏ qua, ko gọi.
 + Bot ko running và ko success
    * Nếu còn session (dựa vào khoảng cách giữa lần chạy gần nhất đến hiện tại - thời gian tồn tại session sẽ xác định sau) thì chạy Previous.
    * Nếu mất session thì sẽ chạy từ bước load visapoint.eu/form (nếu nó tự direct về disclamer thì vẫn phải chạy qua disclamer và action)
 - Phần mềm sẽ lấy đủ số bot thỏa mãn tại thời điểm hẹn để đem đi chạy, ko đủ thì lấy tối đa có thể.
 */
using RemoteContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Visa_Appointment_Request;

namespace Auto_VAR
{
    public class VarBot
    {
        private bool _isRunning = false;
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        private Dictionary<DateTime, int> _dictTime = new Dictionary<DateTime, int>();
        private Dictionary<DateTime, bool> _dictRunned = new Dictionary<DateTime, bool>();

        private List<VarItem> _listItem = new List<VarItem>();
        public void SetListItem(List<VarItem> list)
        {
            _listItem = list;
        }

        private object _lockList = new object();

        private List<VarTimeInput> _listControl = new List<VarTimeInput>();
        public void SetListControl(List<VarTimeInput> list)
        {
            _listControl = list;
        }

        private int _countToUpdate = 0;

        private Thread _threadMain;

        private EventHandler _finish = new EventHandler((s, o) => { });
        public void SetFinish(EventHandler fn)
        {
            _finish = fn;
        }

        private void UpdateDictTime()
        {
            _dictTime.Clear();
            foreach (VarTimeInput item in _listControl)
            {
                if (item.IsUse)
                {
                    bool isChecked = false;

                    DateTime startTime = item.StartDate.Add(item.StartTime);

                    for (int i = 0; i < item.RepeatCount; i++)
                    {
                        for (int j = 0; j < item.NumberOfInput; j++)
                        {
                            DateTime time = startTime.AddMinutes(i * item.RepeatInterval)
                                                        .AddMilliseconds(j * item.Frequency);

                            if (!_dictTime.ContainsKey(time))
                            {
                                _dictTime[time] = 0;
                                _dictRunned[time] = false;
                            }
                            _dictTime[time]++;

                            double t = (DateTime.Now.AddMilliseconds(Setting.PrepareTime) - time).TotalMilliseconds;
                            if (t > 1000)
                                _dictRunned[time] = true;

                            if (!isChecked && !_dictRunned[time])
                            {
                                isChecked = true;
                            }
                        }
                    }

                    if (item.IsUse != isChecked)
                        item.IsUse = isChecked;
                }
            }
            List<DateTime> lstRunned = _dictRunned.Keys.ToList();
            foreach (DateTime time in lstRunned)
            {
                if (!_dictTime.ContainsKey(time))
                    _dictRunned.Remove(time);
            }
        }

        public void Start()
        {
            _isRunning = true;
            _threadMain = new Thread(() =>
                {
                    while (_isRunning)
                    {
                        if (++_countToUpdate > 100)
                        {
                            UpdateDictTime();
                            _countToUpdate = 0;
                        }

                        Thread.Sleep(10);
                        foreach (KeyValuePair<DateTime, int> time in _dictTime)
                        {
                            if (!_dictRunned[time.Key])
                            {
                                double t = (DateTime.Now.AddMilliseconds(Setting.PrepareTime) - time.Key).TotalMilliseconds;
                                if (t >= -50 && t < 20)
                                {
                                    _dictRunned[time.Key] = true;
                                    int numberOfInput = _dictTime[time.Key];
                                    DateTime eventTime = time.Key;
                                    for (int c = 0; c < numberOfInput; c++)
                                        new Thread(() =>
                                        {
                                            List<VarItem> listItemToRun = new List<VarItem>();
                                            lock (_lockList)
                                            {
                                                foreach (VarItem item in _listItem)
                                                {
                                                    if (listItemToRun.Count >= 1)
                                                        break;
                                                    if (item.Item.Status != VarState.Success && item.Item.Status != VarState.Running && item.IsRunning == false)
                                                    {
                                                        item.IsRunning = true;
                                                        listItemToRun.Add(item);
                                                    }
                                                }
                                            }
                                            if (!_isRunning || listItemToRun.Count == 0) return;

                                            listItemToRun[0].RunBySession();

                                            while (DateTime.Now < eventTime && _isRunning) Thread.Sleep(10);
                                            if (_isRunning)
                                            {
                                                if (listItemToRun[0].Item.Status == VarState.Running)
                                                    listItemToRun[0].Step3_SendCaptcha();
                                            }
                                            else
                                                listItemToRun[0].Item.Status = VarState.ForceStopped;

                                            if (listItemToRun[0].Item.Status == VarState.NotOpen)
                                            {
                                                string mess = string.Format("{0} - Time: {1}", listItemToRun[0].Item, listItemToRun[0].Item.NotOpenTime);
                                                LogUtils.WriteLogHistory(mess, listItemToRun[0].Item.Status);
                                            }
                                            else
                                                LogUtils.WriteLogHistory(listItemToRun[0].Item.ToString(), listItemToRun[0].Item.Status);

                                            if (Client.Connected)
                                            {
                                                List<Report> listReportToSend = new List<Report>();
                                                foreach (VarItem item in listItemToRun)
                                                {
                                                    if (item.Item.Status == VarState.NotOpen)
                                                    {
                                                        if (listReportToSend.Count((x) => x.Result == VarState.NotOpen) == 0)
                                                            listReportToSend.Add(CreateReport(item.Item));
                                                    }
                                                    else
                                                    {
                                                        listReportToSend.Add(CreateReport(item.Item));
                                                    }
                                                }
                                                foreach (Report r in listReportToSend)
                                                {
                                                    try { Client.SendResult(r); }
                                                    catch (Exception ex) { LogUtils.WriteLog("Send Report Err: " + ex.ToString(), "Err"); }
                                                }
                                            }
                                            foreach (VarItem item in listItemToRun)
                                            {
                                                item.IsRunning = false;
                                            }
                                        }).Start();

                                }
                            }
                        }
                    }
                });
            _threadMain.Start();
        }

        private Report CreateReport(VarObject item)
        {
            return new Report()
            {
                Input = CreateInputString(item),
                Name = Client.Username,
                NotOpenTime = item.Status == VarState.NotOpen ? item.NotOpenTime : string.Empty,
                OpenTime = item.OpenTime,
                PurposeOfStay = int.Parse(item.PurposeOfStay.Key),
                Result = item.Status,
                ResultHtml = string.Empty,
                SentTime = DateTime.Now,
                ErrorMessage = item.ErrorMsg
            };
        }

        public void Stop()
        {
            _isRunning = false;
            try
            {
                _threadMain.Abort();
            }
            catch { }
        }

        public static string CreateInputString(VarObject info)
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}", info.PurposeOfStay.Key, info.Name, info.FamilyName, info.Gender, info.DateOfBirth, info.Passport, info.Email, info.Phone, info.ErrorMsg);
        }
    }
}
