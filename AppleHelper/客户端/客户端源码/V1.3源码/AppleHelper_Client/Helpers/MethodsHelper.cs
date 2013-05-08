using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace AppleHelper_Client
{
    public class MethodsHelper
    {
        /// <summary>
        /// 高效率等待，避免UI假死
        /// </summary>
        public static void HighEffeciencyWait(long ms)
        {
            Application.DoEvents();
            ms = -10000 * ms;
            IntPtr hWaitableTiemr = Win32.CreateWaitableTimer(null, false, null);
            Win32.SetWaitableTimer(hWaitableTiemr, ref ms, 0, null, null, false);
            IntPtr[] pHandles = new IntPtr[1];
            pHandles[0] = hWaitableTiemr;
            while (Win32.MsgWaitForMultipleObjects(1, pHandles, false, Win32.INFINITE, Win32.QS_ALLINPUT) != Win32.WAIT_OBJECT_0)
            {
                Application.DoEvents();
            }
            Win32.CancelWaitableTimer(hWaitableTiemr);
            Win32.CloseHandle(hWaitableTiemr);
            Application.DoEvents();
        }

        /// <summary>
        /// 点击指定句柄的控件
        /// </summary>
        public static void ClickControl(IntPtr hwnd)
        {
            int clickPoint = 5 + 5 * 65536;
            Win32.PostMessage(hwnd, Win32.WM_MOUSEMOVE, 2, clickPoint);
            Win32.PostMessage(hwnd, Win32.WM_LBUTTONDOWN, 1, clickPoint);
            Win32.PostMessage(hwnd, Win32.WM_LBUTTONUP, 0, clickPoint);
        }

        /// <summary>
        /// 向指定句柄的Edit控件投递文本
        /// </summary>
        public static void PostText(IntPtr hwnd, string text)
        {
            if (text.Length != 0)
            {
                char[] chars = text.ToCharArray();
                foreach (char ch in chars)
                {
                    Win32.PostMessage(hwnd, Win32.WM_CHAR, Convert.ToInt32(ch), 0);
                }
            }
        }

        /// <summary>
        /// 清空指定句柄的Edit控件的内容
        /// </summary>
        public static void ClearTextBoxText(IntPtr hwnd)
        {
            Win32.SendMessage(hwnd, Win32.EM_SETSEL, 0, -1);   //全选
            Win32.SendMessage(hwnd, Win32.WM_CLEAR, 0, 0);     //删除
        }

        /// <summary>
        /// 获取当前命名空间指定路径所嵌入的图片资源
        /// </summary>
        public static Image GetImageFormResourceStream(string imagePath)
        {
            return Image.FromStream(
                Assembly.GetExecutingAssembly().
                GetManifestResourceStream(
                MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + imagePath));
        }

        /// <summary>
        /// 在TextBox中只允许输入正整数
        /// </summary>
        public static void OnlyEnterInt(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 获取指定句柄控件(外部进程控件)的标题文本
        /// </summary>
        public static string GetWindowText(IntPtr hwnd)
        {
            // Allocate correct string length first
            int length = Win32.SendMessage(hwnd, Win32.WM_GETTEXTLENGTH, 0, 0);
            StringBuilder sb = new StringBuilder(length + 1);
            Win32.SendMessage(hwnd, Win32.WM_GETTEXT, (IntPtr)sb.Capacity, sb);
            return sb.ToString();
        }

        /// <summary>
        /// 获取指定窗体的截图
        /// </summary>
        public static Bitmap GetWindowCapture(IntPtr hWnd)
        {
            IntPtr hscrdc = Win32.GetWindowDC(hWnd);
            Win32.Rect wndRect = new Win32.Rect();
            Win32.GetWindowRect(hWnd, ref wndRect);
            int width = wndRect.Right - wndRect.Left;
            int height = wndRect.Bottom - wndRect.Top;
            IntPtr hbitmap = Win32.CreateCompatibleBitmap(hscrdc, width, height);
            IntPtr hmemdc = Win32.CreateCompatibleDC(hscrdc);
            Win32.SelectObject(hmemdc, hbitmap);
            Win32.PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            Win32.DeleteDC(hscrdc);
            Win32.DeleteDC(hmemdc);
            return bmp;
        }

        /// <summary>
        /// 判断指定的窗体是否最小化
        /// </summary>
        public static bool WindowIsMinimized(IntPtr hwnd)
        {
            //get the width and height of  window
            Win32.Rect wndRect = new Win32.Rect();
            Win32.GetWindowRect(hwnd, ref wndRect);
            int width = wndRect.Right - wndRect.Left;
            int height = wndRect.Bottom - wndRect.Top;

            //当窗体最小化时其Left与Top的值都为-32000
            if (wndRect.Left == -32000 && wndRect.Top == -32000)
                return true;
            else
                return false;
        }
    }
}
