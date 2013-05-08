using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AppleHelper_Client
{
    static class Program
    {
        #region Fileds

        public const string PUBLICKEY = "<RSAKeyValue><Modulus>qg8EuUeCHtTdF3IVa73sR83FUbGa6IaqIvPRGzlYyEiYbH4YUQQTglglqX/wW+AJIXBa0xuOjPDt1Gg5bAQh6R/daaFiM2olq2f6RPOkRk3/xlEz0DVt3/NPq9Je6bLs2gfyElvkPUDkbGY01OginVL9lQlzIw0vXFcA266V8ak=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        #endregion

        #region Private

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RunApplication();
        }

        private static void RunApplication()
        {
            //forbid loginform run twice
            Mutex mutex = new Mutex(false, "LoginFormMutex");
            bool running = !mutex.WaitOne(0, false);

            //之前没有程序运行
            if (!running)
            {
                bool canLogin = VerifyLicenseFile_Program();
                if (canLogin)
                    Application.Run(new MainForm());
                else
                    RunNewApplicationWithLogin();
            }
            else
            {
                RunOriginalApplication("AppleHelper");
            }
        }

        private static void RunNewApplicationWithLogin()
        {
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            if (loginForm.DialogResult == DialogResult.OK)
            {
                MainForm mainForm = new MainForm();
                Application.Run(mainForm);
            }
        }

        private static void RunOriginalApplication(string originalFormText)
        {
            IntPtr hWnd = Win32.FindWindow(null, originalFormText);
            if (hWnd != IntPtr.Zero)
            {
                Win32.SendMessage(hWnd, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                Win32.SetWindowPos(hWnd, Win32.HWND_TOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE + Win32.SWP_NOSIZE);
                Win32.SetWindowPos(hWnd, Win32.HWND_NOTOPMOST, 0, 0, 0, 0, Win32.SWP_NOMOVE + Win32.SWP_NOSIZE);
            }
            Environment.Exit(0);
        }

        private static bool VerifyLicenseFile_Program()
        {
            string lisenceFilePath = AppDomain.CurrentDomain.BaseDirectory + @"/License.txt";
            if (!File.Exists(lisenceFilePath))
            {
                return false;
            }
            else
            {
                string key = IOHelper.ReadTextFormFile(lisenceFilePath);
                if (string.IsNullOrEmpty(key))
                {
                    return false;
                }
                else
                {
                    bool isValid = RSAHelper.SignatureDeformatter(PUBLICKEY, key, GenerateMachineId_Program());
                    if (isValid)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        private static string GenerateMachineId_Program()
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
    }
}
