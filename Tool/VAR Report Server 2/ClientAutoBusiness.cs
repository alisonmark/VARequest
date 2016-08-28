using RemoteContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VAR_Report_Server
{
    public class ClientAutoBusiness
    {
        public const string DataFile = "clientauto.var";
        public static List<ClientAuto> Load()
        {
            if (!File.Exists(DataFile))
            {
                var f = File.Create(DataFile);
                f.Flush();
                f.Close();
                return new List<ClientAuto>();
            }

            try
            {
                List<ClientAuto> lstReport = new List<ClientAuto>();

                XmlDocument doc = new XmlDocument();
                doc.Load(DataFile);
                XmlNode root = doc.DocumentElement;

                XmlNodeList lstReportNode = root.SelectNodes("//ClientAuto");
                foreach (XmlNode node in lstReportNode)
                {
                    try
                    {
                        ClientAuto r = new ClientAuto();
                        r.Username = node.SelectSingleNode("Username").InnerXml.Trim();
                        r.TimeInfo = node.SelectSingleNode("TimeInfo").InnerXml.Trim();
                        r.Input = node.SelectSingleNode("Input").InnerXml.Trim();

                        lstReport.Add(r);
                    }
                    catch (Exception ex) { }
                }
                return lstReport;
            }
            catch (Exception ex)
            {
                return new List<ClientAuto>();
            }
        }

        public static void Save(List<ClientAuto> lstReport)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("root");
            doc.AppendChild(root);

            foreach (ClientAuto r in lstReport)
            {
                XmlNode e = doc.CreateElement("ClientAuto");
                XmlNode username = doc.CreateElement("Username");
                XmlNode timeInfo = doc.CreateElement("TimeInfo");
                XmlNode input = doc.CreateElement("Input");

                e.AppendChild(username);
                e.AppendChild(timeInfo);
                e.AppendChild(input);

                root.AppendChild(e);

                username.InnerXml = r.Username;
                timeInfo.InnerXml = r.TimeInfo;
                input.InnerXml = r.Input;
            }

            doc.Save(DataFile);
        }

        public static List<BotInfo> GetBotInfo(ClientAuto client)
        {
            List<BotInfo> list = new List<BotInfo>();

            string[] timeData = client.TimeInfo.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string data in timeData)
            {
                BotInfo obj = new BotInfo();
                try
                {
                    string[] time = data.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    obj.Checked = time[0] == "1";
                    obj.NumberOfInput = int.Parse(time[1]);
                    obj.StartTime = TimeSpan.Parse(time[2]);
                    obj.Frequency = int.Parse(time[3]);
                    obj.RepeatInterval = int.Parse(time[4]);
                    obj.RepeatCount = int.Parse(time[5]);
                    list.Add(obj);
                }
                catch { }
            }
            return list;
        }

        public static List<Input> GetInput(ClientAuto client)
        {
            List<Input> list = new List<Input>();

            string[] inputData = client.Input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string data in inputData)
            {
                string str = data.Trim();
                string[] arr = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                int purpose = -1;
                if (!string.IsNullOrEmpty(str) && (arr.Length != 10 || !int.TryParse(arr[0], out purpose)))
                    continue;

                Input input = new Input();
                input.InputString = str;
                input.UpdateTime = DateTime.Now;

                list.Add(input);
            }
            return list;
        }
    }
}
