using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AppleHelper_Client
{
    public class IOHelper
    {
        /// <summary>
        ///返回从指定的文本文件读取的内容
        /// </summary>
        /// <param name="filePath">文本文件路径(包括文件名)</param>
        public static string ReadTextFormFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        string resultStr = string.Empty;
                        string lineStr = reader.ReadLine();
                        while (lineStr != null)
                        {
                            resultStr += lineStr;
                            lineStr = reader.ReadLine();
                        }
                        return resultStr;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将指定的文本写入打文本文件中
        /// </summary>
        /// <param name="filePath">待写入的文件路径(包括文件名)</param>
        /// <param name="data">待写入的内容</param>
        public static void WriteTextToFile(string filePath, string data)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(data);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 显示文件打开对话框，成功返回选择打开的文件路径
        /// </summary>
        public static string ShowOpenFileDialog()
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openDialog.Title = "请选择Excel表格文件";
                openDialog.Filter = "Excel表格文件(*.xls;*.xlsx)|*.xls;*.xlsx|(所有文件(*.*)|*.*";
                openDialog.DereferenceLinks = true;
                if (openDialog.ShowDialog() == DialogResult.OK)
                    return openDialog.FileName;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 显示文件保存对话框，成功返回文件保存的路径
        /// </summary>
        public static string ShowSaveFileDialog()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveDialog.Title = "导出表格";
                saveDialog.Filter = "Excel表格文件(*.xls)|*.xls";
                saveDialog.DefaultExt = ".xls";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                    return saveDialog.FileName;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 显示文件夹浏览对话框，成功返回文件保存的路径
        /// </summary>
        public static string ShowFolderBrowserDialog()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "请选择文件夹";
                folderDialog.ShowNewFolderButton = true;
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    return folderDialog.SelectedPath;
                else
                    return string.Empty;
            }
        }
    }
}
