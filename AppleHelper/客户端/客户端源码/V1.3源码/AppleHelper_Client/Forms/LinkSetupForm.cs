using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AppleHelper_Client
{
    public partial class LinkSetupForm : Form
    {
        public LinkSetupForm()
        {
            InitializeComponent();
        }

        public string AppIlink { get; set; }
        public string AppIIlink { get; set; }

        // 将http(s)://.....格式的链接转换为 itunes://格式的下载链接
        public static string TranslateUrlToAppLink(string link)
        {
            if (!string.IsNullOrEmpty(link) && link.Contains("http://"))
                return link.Replace("http://", "itunes://");
            else if (!string.IsNullOrEmpty(link) && link.Contains("https://"))
                return link.Replace("https://", "itunes://");
            else
                return string.Empty;
        }

        // 将itunes://....格式的下载链接还原为标准的https://.....格式
        public static string RestoreAppLinkToUrl(string translatedLink)
        {
            if (translatedLink.Length != 0)
                return translatedLink.Replace("itunes://", "https://");
            else
                return string.Empty;
        }

        //判断指定的字符串是否为url
        public static bool StringIsUrl(string targetStr)
        {
            if (targetStr.ToLower().StartsWith("http://") || targetStr.ToLower().StartsWith("https://"))
                return true;
            else
                return false;
        }

        private void downloadSetupForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AppIlink))
                textBoxExAppI.Text = RestoreAppLinkToUrl(AppIlink);
            if (!string.IsNullOrEmpty(AppIIlink))
                textBoxExAppII.Text = RestoreAppLinkToUrl(AppIIlink);
        }

        //取消
        private void buttonExCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //应用
        private void buttonExSetup_Click(object sender, EventArgs e)
        {
            if (textBoxExAppI.Text.Trim().Length == 0
                && textBoxExAppII.Text.Trim().Length == 0)
            {
                MessageBox.Show(
                    "请输入下载链接！",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else if (textBoxExAppI.Text.Trim().Length != 0 && !StringIsUrl(textBoxExAppI.Text.Trim())
               || textBoxExAppII.Text.Trim().Length != 0 && !StringIsUrl(textBoxExAppII.Text.Trim()))
            {
                MessageBox.Show(
                    "设置的链接格式不正确，请检查后重新输入！",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            //将标准下载链接转换为itunes格式的链接便于传递给主窗体
            //将当前设置的链接写至 .\Config.xml 配置文件中
            string configXmlFile = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(configXmlFile);

            if (!File.Exists(configXmlFile))
                xml.GenerateXmlConfigFile(configXmlFile);

            if (StringIsUrl(textBoxExAppI.Text.Trim()))
            {
                AppIlink = TranslateUrlToAppLink(textBoxExAppI.Text.Trim());
                xml.SetNodeInnerText("/Config/DownloadConfig/AppI", textBoxExAppI.Text.Trim());
            }
            if (StringIsUrl(textBoxExAppII.Text.Trim()))
            {
                AppIIlink = TranslateUrlToAppLink(textBoxExAppII.Text.Trim());
                xml.SetNodeInnerText("/Config/DownloadConfig/AppII", textBoxExAppII.Text.Trim());
            }

            //设置成功标志
            this.DialogResult = DialogResult.OK;

        }
    }
}
