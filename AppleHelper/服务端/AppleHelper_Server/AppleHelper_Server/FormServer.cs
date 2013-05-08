using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AppleHelper_Server
{
    public partial class FormServer : Form
    {
        #region Fields

        private const string PRIVATEKEY = "<RSAKeyValue><Modulus>qg8EuUeCHtTdF3IVa73sR83FUbGa6IaqIvPRGzlYyEiYbH4YUQQTglglqX/wW+AJIXBa0xuOjPDt1Gg5bAQh6R/daaFiM2olq2f6RPOkRk3/xlEz0DVt3/NPq9Je6bLs2gfyElvkPUDkbGY01OginVL9lQlzIw0vXFcA266V8ak=</Modulus><Exponent>AQAB</Exponent><P>4Dbfmr4/pVqeD3gDg+/nI44JTdcYJPU3BzkZT5bgjLD0nsVFwEuq5hA8ljQwzHMU842qH2pIURwY6E4hQkzBEw==</P><Q>wiq915VR3a7Q0ahxXs/T/pqcWF2D9wZBwjGhH4uRIDmPKu3rSj10h+sFK/9eJ5sVibBrzmIbMVXJ1E6YBFrV0w==</Q><DP>z24Uyl2OFlVSe68lWW+eIEesleYUAUUXZshNgVNtZNvlQ+nwEOowLm4BD1kzeEJTtMtwA2ZZhyh+ZpS5slO+Hw==</DP><DQ>exs+YoF59mPwYf26Y7LBZpwApMYl9WkyKD3vOmDzEstrynwuwlNUnxiTnuG6gMIDVdlVgPUS6KTL4qAyIhC1oQ==</DQ><InverseQ>qlgc6vHrqxA9RTTfFuyRSlczDzsOZ2+oYLEuFiGasA8T1W8G0yHmy1ifGFLhJQSOozf4Ki4LgdocnFu7NcEsxQ==</InverseQ><D>WFV4xK0KP+2d8mHGErMSmrGd/zNcHdBla8ZuJdAB9/cNhb1x8BNH9vrGhWb5eR/EXmQ4KVQ1FazIPP7uZNc4rUAXIBaJaTvdBOufFF7R+CCLnGJ2MZ/ONxwh2ekMLeQhLp8ilnNl+vXlO8r5BlckkAnLcMDyOqhch1/VB7Ios2k=</D></RSAKeyValue>";
        private const string MYHDDSERIALNUMBER = "WD-WXG1A31R2018";

        #endregion

        #region Constructor

        public FormServer()
        {
            InitializeComponent();
        }

        #endregion

        private void FormServer_Load(object sender, EventArgs e)
        {
            CheckHddSerialNumber();
        }

        private void buttonGenerateKey_Click(object sender, EventArgs e)
        {
            textBoxKey.Text = RSAHelper.SignatureFormatter(PRIVATEKEY, textBoxMachineId.Text.Trim());
            if (string.IsNullOrEmpty(textBoxKey.Text))
                MessageBox.Show("加密失败了，呜呜.......肿么办？");
        }

        private void CheckHddSerialNumber()
        {
            string serialNum = ComputerInfoHelper.GetHddInfo_SerialNumber(0).Trim();
            if (!string.Equals(serialNum, MYHDDSERIALNUMBER))
                Application.Exit();
        }


        private void buttonWriteLicense_Click(object sender, EventArgs e)
        {
            string licenseFolderPath = AppDomain.CurrentDomain.BaseDirectory + "License";
            if (!Directory.Exists(licenseFolderPath))
                Directory.CreateDirectory(licenseFolderPath);

            string filePath = licenseFolderPath + @"\License.txt";
            WriteDataToFile(filePath, textBoxKey.Text);

            System.Diagnostics.Process.Start("explorer.exe", licenseFolderPath);

        }

        private void WriteDataToFile(string filePath, string data)
        {
            try
            {
                using (FileStream fstream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fstream))
                    {
                        writer.WriteLine(data);
                    }
                }
            }
            catch
            {
                MessageBox.Show("写入 Lisence.txt 文件失败，请检查Lisence文件夹是否存在或重试！");
            }
        }
    }
}
