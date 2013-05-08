using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppleHelper_Client
{
    /// <summary>
    /// iTunes句柄类
    /// </summary>
    public class Hwnds
    {
        #region Public

        /// <summary>
        /// iTunes句柄
        /// </summary>
        public static IntPtr iTunesHwnd(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetiTunesHwnd_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetiTunesHwnd_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        /// <summary>
        /// 右侧登录按钮句柄
        /// </summary>
        public static IntPtr OpenDialogBtnHwnd(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetOpenDialogBtnHwnd_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetOpenDialogBtnHwnd_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        /// <summary>
        /// 账号密码输入对话框句柄
        /// </summary>
        public static IntPtr InputAccountDialogHwnd(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetInputAccountDialogHwnd_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetInputAccountDialogHwnd_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        /// <summary>
        /// AppleID 输入框句柄
        /// </summary>
        public static IntPtr IDTextBoxHwnd(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetIDTextBoxHwnd_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetIDTextBoxHwnd_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        /// <summary>
        /// AppleID 的密码输入框句柄
        /// </summary>
        public static IntPtr PswordTextBoxHwnd(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetPswordTextBoxHwnd_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetPswordTextBoxHwnd_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        /// <summary>
        /// 登录对话框的登录按钮句柄
        /// </summary>
        public static IntPtr StartLoadItunes(iTunesVersion version)
        {
            IntPtr hwnd = IntPtr.Zero;
            switch (version)
            {
                case iTunesVersion.V10_5_0_142:
                    hwnd = GetStartLoadItunes_A();
                    break;
                case iTunesVersion.V10_5_1_42:
                    hwnd = GetStartLoadItunes_B();
                    break;
                default:
                    break;
            }
            return hwnd;
        }

        #endregion

        #region iTunesVerison A: V10_5_0_142

        private static IntPtr GetiTunesHwnd_A()
        {
            return Win32.FindWindow("ITWindow", "iTunes");
        }

        private static IntPtr GetOpenDialogBtnHwnd_A()
        {
            IntPtr hWnd = GetiTunesHwnd_A();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 29; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetInputAccountDialogHwnd_A()
        {
            return Win32.FindWindow("iTunesCustomModalDialog", "iTunes");
        }

        private static IntPtr GetIDTextBoxHwnd_A()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_A();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 7; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetPswordTextBoxHwnd_A()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_A();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 14; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetStartLoadItunes_A()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_A();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 18; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        #endregion

        #region iTuensVerison B: V10_5_1_42

        private static IntPtr GetiTunesHwnd_B()
        {
            return Win32.FindWindow("iTunes", "iTunes");
        }

        private static IntPtr GetOpenDialogBtnHwnd_B()
        {
            IntPtr hWnd = GetiTunesHwnd_B();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 29; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetInputAccountDialogHwnd_B()
        {
            return Win32.FindWindow("iTunesCustomModalDialog", "iTunes");
        }

        private static IntPtr GetIDTextBoxHwnd_B()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_B();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 7; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetPswordTextBoxHwnd_B()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_B();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 14; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        private static IntPtr GetStartLoadItunes_B()
        {
            IntPtr hWnd = GetInputAccountDialogHwnd_B();
            hWnd = Win32.GetWindow(hWnd, Win32.GW_CHILD);
            for (int i = 0; i < 18; i++)
            {
                hWnd = Win32.GetWindow(hWnd, Win32.GW_HWNDNEXT);
            }
            return hWnd;
        }

        #endregion
    }
}
