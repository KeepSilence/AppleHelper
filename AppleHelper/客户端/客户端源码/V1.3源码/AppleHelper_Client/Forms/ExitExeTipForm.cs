using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppleHelper_Client
{
    public partial class ExitExeTipForm : Form
    {
        public ExitExeTipForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 如果为1则最小化程序到系统托盘区，如果为2则退出程序
        /// </summary>
        public int UserChoice{get;protected set;}

        private void ExitExeTipForm_Load(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);
            string userChoice = xml.GetNodeInnerText("/Config/ExeConfig/UserExitChoice", "2");
            if (userChoice == "2")
                radioButtonExitExe.Checked = true;
            else
                radioButtonNotExitExe.Checked = true;
        }

        private void buttonExOk_Click(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);

            if (radioButtonNotExitExe.Checked)
            {
                UserChoice = 1;
                xml.SetNodeInnerText("/Config/ExeConfig/UserExitChoice", "1");
            }
            else
            {
                UserChoice = 2;
                xml.SetNodeInnerText("/Config/ExeConfig/UserExitChoice", "2");
            }

            if(checkBoxNotTipAgain.Checked)
                xml.SetNodeInnerText("/Config/ExeConfig/ExitExeNotTipAgain", "1");
            else
                xml.SetNodeInnerText("/Config/ExeConfig/ExitExeNotTipAgain", "0");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonExCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
