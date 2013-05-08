using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Forms;
using System.IO;
namespace AppleHelper_Client
{
    public class XmlConfigHelper
    {
        private string _xmlFileName;

        public string XmlFileName
        {
            get { return _xmlFileName; }
            set { _xmlFileName = value; }
        }

        public XmlConfigHelper() { }

        public XmlConfigHelper(string xmlFileName)
        {
            this._xmlFileName = xmlFileName;
        }

        /// <summary>
        /// 生成XML模板配置文件
        /// </summary>
        public bool GenerateXmlConfigFile(string xmlSavePath)
        {
            try
            {
                XDocument xdoc = new XDocument(

                    new XElement("Config",
                        new XElement("ExeConfig",
                            new XElement("UserExitChoice"),
                            new XElement("ExitExeNotTipAgain")
                        ),
                        new XElement("DownloadConfig",
                            new XElement("AppI"),
                            new XElement("AppII"),
                            new XElement("iTunesVersion"),
                            new XElement("PostNextAccount"),
                            new XElement("ToDownloadPage"),
                            new XElement("ShotedToLoadNextAccount")
                            )
                        )
                );

                xdoc.Save(xmlSavePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取XML文件中指定节点的值
        /// 如果该节点不存在或没有值则返回一个默认的值
        /// </summary>
        public string GetNodeInnerText(string xpath, string defaultValue = "")
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_xmlFileName);
                XmlNode resultNode = doc.SelectSingleNode(xpath);
                if (resultNode != null)     //节点存在
                {
                    if (!string.IsNullOrEmpty(resultNode.InnerText))
                        return resultNode.InnerText;
                    else
                        return defaultValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 设置XML文件中指定节点的值
        /// </summary>
        public bool SetNodeInnerText(string xpath, string value)
        {
            if (!string.IsNullOrEmpty(_xmlFileName) && !File.Exists(_xmlFileName))
                GenerateXmlConfigFile(_xmlFileName);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_xmlFileName);
                XmlNode resultNode = doc.SelectSingleNode(xpath);
                if (resultNode != null)     //节点存在
                {
                    resultNode.InnerText = value;
                    doc.Save(_xmlFileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
