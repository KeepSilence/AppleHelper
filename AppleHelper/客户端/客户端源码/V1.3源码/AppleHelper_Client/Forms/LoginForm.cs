using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace AppleHelper_Client
{
    public partial class LoginForm : Form
    {
        #region Field

        private const byte TOTAL_INPUT_TIEMS = 9;  //允许错误的输入10次注册码

        #endregion

        #region Constructor

        public LoginForm()
        {
            InitializeComponent();
            textBoxMachineId.Text = GenerateMachineId_LoginForm();
        }

        #endregion

        #region Property

        //已经错误输入的次数
        private byte InputedTime { get; set; }

        #endregion

        #region Private

        private string GenerateMachineId_LoginForm()
        {
            string hddSerialNum = ComputerInfoHelper.GetHddInfo_SerialNumber(0);

            hddSerialNum = hddSerialNum.Replace("0", "E");
            hddSerialNum = hddSerialNum.Replace("1", "Y");
            hddSerialNum = hddSerialNum.Replace("2", "F");
            hddSerialNum = hddSerialNum.Replace("3", "V");
            hddSerialNum = hddSerialNum.Replace("4", "U");
            hddSerialNum = hddSerialNum.Replace("5", "H");
            hddSerialNum = hddSerialNum.Replace("6", "S");
            hddSerialNum = hddSerialNum.Replace("7", "G");
            hddSerialNum = hddSerialNum.Replace("8", "D");
            hddSerialNum = hddSerialNum.Replace("9", "A");
            return hddSerialNum;
        }

        #endregion

        #region Events

        private void glassButtonExCopyMachineId_Click(object sender, EventArgs e)
        {
            if (textBoxMachineId.Text.Length != 0)
                Clipboard.SetText(textBoxMachineId.Text.Trim());
        }

        private void glassButtonExAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm about = new AboutForm())
            {
                about.ShowDialog();
            }
        }

        private void glassButtonExOpenHelp_Click(object sender, EventArgs e)
        {
            string helpFile = AppDomain.CurrentDomain.BaseDirectory + "AppleHelper帮助说明.doc";
            if (File.Exists(helpFile))
                try
                {
                    System.Diagnostics.Process.Start(helpFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            else
                MessageBox.Show("未找到帮助文件---AppleHelper帮助说明.doc",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            //错误输入次数限制
            if (InputedTime >= TOTAL_INPUT_TIEMS)
            {
                Application.Exit();
                return;
            }

            bool isValid = RSAHelper.SignatureDeformatter(Program.PUBLICKEY, textBoxKey.Text, GenerateMachineId_LoginForm());
            if (isValid)
            {
                this.DialogResult = DialogResult.OK;

                string licenseFilePath = AppDomain.CurrentDomain.BaseDirectory + "License.txt";
                IOHelper.WriteTextToFile(licenseFilePath, textBoxKey.Text);

                this.Close();
            }
            else
            {
                InputedTime++;
            }
        }

        private void textBoxKey_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                string fileSuffix = fileName.Substring(fileName.Length - 3, 3);
                if (fileSuffix.ToLower().Equals("txt"))
                    textBoxKey.Text = IOHelper.ReadTextFormFile(fileName);
            }
        }

        private void textBoxKey_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion

    }
}
