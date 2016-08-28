using RemoteContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO.Compression;

namespace VAR_Report_Server
{
    public class ReportUtils
    {
        public static List<Report> Import(string fileName)
        {
            if (!File.Exists(fileName))
                throw new Exception("File is not exist !");

            List<Report> lstReport = new List<Report>();

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.DocumentElement;

            XmlNodeList lstReportNode = root.SelectNodes("//Report");
            foreach (XmlNode node in lstReportNode)
            {
                try
                {
                    Report r = new Report();
                    r.Name = node.SelectSingleNode("Name").InnerXml.Trim();
                    r.PurposeOfStay = int.Parse(node.SelectSingleNode("PurposeOfStay").InnerXml.Trim());
                    r.SentTime = DateTime.Parse(node.SelectSingleNode("SentTime").InnerXml.Trim());
                    r.Result = node.SelectSingleNode("Success").InnerXml.Trim();
                    r.Input = node.SelectSingleNode("Input").InnerXml.Trim();
                    r.OpenTime = node.SelectSingleNode("OpenTime").InnerXml.Trim();
                    r.NotOpenTime = node.SelectSingleNode("NotOpenTime").InnerXml.Trim();

                    lstReport.Add(r);
                }
                catch (Exception ex) { }
            }
            return lstReport;
        }

        public static void Export(List<Report> lstReport, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("root");
            doc.AppendChild(root);

            foreach (Report r in lstReport)
            {
                XmlNode e = doc.CreateElement("Report");
                XmlNode name = doc.CreateElement("Name");
                XmlNode purposeOfStay = doc.CreateElement("PurposeOfStay");
                XmlNode sentTime = doc.CreateElement("SentTime");
                XmlNode success = doc.CreateElement("Success");
                XmlNode input = doc.CreateElement("Input");
                XmlNode openTime = doc.CreateElement("OpenTime");
                XmlNode notOpenTime = doc.CreateElement("NotOpenTime");

                e.AppendChild(name);
                e.AppendChild(purposeOfStay);
                e.AppendChild(sentTime);
                e.AppendChild(success);
                e.AppendChild(input);
                e.AppendChild(openTime);
                e.AppendChild(notOpenTime);

                root.AppendChild(e);

                name.InnerXml = r.Name;
                purposeOfStay.InnerXml = r.PurposeOfStay.ToString();
                sentTime.InnerXml = r.SentTime.ToString();
                success.InnerXml = r.Result.ToString();
                input.InnerXml = r.Input;
                openTime.InnerXml = r.OpenTime;
                notOpenTime.InnerXml = r.NotOpenTime;
            }

            doc.Save(fileName);
        }

        public static string Base64Encode(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
                return string.Empty;
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
