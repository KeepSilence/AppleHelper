using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Linq;

namespace AppleHelper_Client
{
    public partial class MainForm : Form
    {
        #region Field


        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Properites

        public string CurrentID { get; set; }
        public string CurrentPsword { get; set; }
        public string CurrentComment { get; set; }

        public string AppIlink { get; set; }
        public string AppIIlink { get; set; }

        public iTunesVersion Version { get; set; }

        #endregion

        #region Common

        #region Verify Method

        private void VerifyApplication_MainForm()   //利用此函数随机验证程序的合法性
        {
            bool isValid = VerifyLicenseFile_MainForm();
            if (!isValid)
                Application.Exit();
        }

        private bool VerifyLicenseFile_MainForm()
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
                    bool isValid = RSAHelper.SignatureDeformatter(Program.PUBLICKEY, key, GenerateMachineId_MainForm());
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

        private string GenerateMachineId_MainForm()
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

        #region Initializtion & Verify

        private void MainForm_Load(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            SetAllPanelsLocationAndSize();
            GetConfigFromXmlFile();
            glassButtonExTopMost.Image = MethodsHelper.GetImageFormResourceStream("Res.topmost.png");
        }

        private void GetConfigFromXmlFile()
        {//根据XML配置文件进行初始化
            string xmlFile = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFile);
            if (!File.Exists(xmlFile))
                xml.GenerateXmlConfigFile(xmlFile);

            string url_AppI = xml.GetNodeInnerText("/Config/DownloadConfig/AppI");
            if (!string.IsNullOrEmpty(url_AppI) && LinkSetupForm.StringIsUrl(url_AppI))
                AppIlink = LinkSetupForm.TranslateUrlToAppLink(url_AppI);

            string url_AppII = xml.GetNodeInnerText("/Config/DownloadConfig/AppII");
            if (!string.IsNullOrEmpty(url_AppII) && LinkSetupForm.StringIsUrl(url_AppII))
                AppIIlink = LinkSetupForm.TranslateUrlToAppLink(url_AppII);

            //iTunes Version
            string iTuensVersion = xml.GetNodeInnerText("/Config/DownloadConfig/iTunesVersion", "B");
            if (iTuensVersion.Equals("A"))
            {
                comboBoxiTunesVersion.SelectedIndex = 0;
                Version = iTunesVersion.V10_5_0_142;
            }
            else if (iTuensVersion.Equals("B"))
            {
                comboBoxiTunesVersion.SelectedIndex = 1;
                Version = iTunesVersion.V10_5_1_42;
            }


            //下载功能的三CheckBox选项配置
            string isPostNextAccount = xml.GetNodeInnerText("/Config/DownloadConfig/PostNextAccount", "0");
            if (isPostNextAccount == "1")
                checkBoxPostNextAccount.Checked = true;
            else
                checkBoxPostNextAccount.Checked = false;

            string isToDownloadPage = xml.GetNodeInnerText("/Config/DownloadConfig/ToDownloadPage", "0");
            if (isToDownloadPage == "1")
                checkBoxToDownloadPage.Checked = true;
            else
                checkBoxToDownloadPage.Checked = false;

            string isShotedToLoadNextAccount = xml.GetNodeInnerText("/Config/DownloadConfig/ShotedToLoadNextAccount", "0");
            if (isShotedToLoadNextAccount == "1")
                checkBoxShotedToLoadNextAccount.Checked = true;
            else
                checkBoxShotedToLoadNextAccount.Checked = false;
        }

        #endregion

        #region Common Private

        private void SetAllPanelsLocationAndSize()
        {
            this.Width = 627;
            this.Height = 347;
            Point location = new Point(-4, menuMain.Bottom + 1);
            Size size = new Size(
                ClientRectangle.Width + (-2 * location.X),
                ClientRectangle.Height - menuMain.Height);

            panelDownload.Location = location;
            panelDownload.Size = size;

            panelPostComment.Location = location;
            panelPostComment.Size = size;

            panelImgCheck.Location = location;
            panelImgCheck.Size = size;

            panelImgView.Location = location;
            panelImgView.Size = size;

            panelSetup.Location = location;
            panelSetup.Size = size;
        }

        private bool ExportListviewToExcel(ListView listView, string strFileName)
        {//将listview中的数据导出到Excel表格文件中，导出的数据为去除了第一列索引的数据

            int rowNum = listView.Items.Count;
            if (rowNum <= 0 || string.IsNullOrEmpty(strFileName))
            {
                return false;
            }

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
                return false;
            }

            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);

            //将ListView的列名(去除第一列的索引列)导入Excel表第一行
            int rowIndex = 1;
            int columnIndex = 0;
            int columnNum = listView.Columns.Count;
            for (int index = 1; index < columnNum; index++)
            {
                columnIndex++;
                xlApp.Cells[rowIndex, columnIndex] = listView.Columns[index].Text;
            }

            //将ListView中的数据(去除第一列的数据)导入Excel中
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 1; j < columnNum; j++)
                {
                    columnIndex++;

                    //注意这个在导出的时候加了“\t” 的目的就是避免导出的数据显示为科学计数法。可以放在每行的首尾。
                    xlApp.Cells[rowIndex, columnIndex] = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
                }
                Application.DoEvents();
            }

            //例外需要说明的是用strFileName,Excel.XlFileFormat.xlExcel9795保存方式时 
            //当你的Excel版本不是95、97 而是2003、2007 时导出的时候会报一个错误：异常来自 HRESULT:0x800A03EC。
            //解决办法就是换成strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal。
            xlBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            xlApp = null;
            xlBook = null;
            return true;
        }

        private void ShowMainForm()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            notifyIconTray.Visible = true;
            this.ShowInTaskbar = true;
        }

        private void HideMainForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            notifyIconTray.Visible = true;
            this.ShowInTaskbar = false;
        }

        #endregion

        #region Common Events

        private void menuMainHelp_Click(object sender, EventArgs e)
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

        private void menuMainAboutMe_Click(object sender, EventArgs e)
        {
            using (AboutForm about = new AboutForm())
            {
                about.ShowDialog();
            }
        }

        private void menuMainExitApplication_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void menuMainFeedback_Click(object sender, EventArgs e)
        {
            string feedbackName = AppDomain.CurrentDomain.BaseDirectory + "Feedback.exe";
            if (!File.Exists(feedbackName))
            {
                MessageBox.Show("反馈失败，未找到相应的反馈辅助程序FeedBack.exe！",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {

                using (System.Diagnostics.Process feedbackExe = new System.Diagnostics.Process())
                {
                    feedbackExe.StartInfo.FileName = feedbackName;
                    feedbackExe.StartInfo.Arguments = "AppleHelper";
                    feedbackExe.Start();
                }
            }
        }

        //主菜单-下载
        private void menuMainDownload_Click(object sender, EventArgs e)
        {
            panelDownload.Visible = true;
            panelPostComment.Visible = false;
            panelImgCheck.Visible = false;
            panelImgView.Visible = false;
            panelSetup.Visible = false;
        }

        //主菜单-评论
        private void menuMainPostCommentary_Click(object sender, EventArgs e)
        {
            panelDownload.Visible = false;
            panelPostComment.Visible = true;
            panelImgCheck.Visible = false;
            panelImgView.Visible = false;
            panelSetup.Visible = false;
        }

        //主菜单-截图检查
        private void menuMainImgCheck_Click(object sender, EventArgs e)
        {
            panelDownload.Visible = false;
            panelPostComment.Visible = false;
            panelImgCheck.Visible = true;
            panelImgView.Visible = false;
            panelSetup.Visible = false;
        }

        //主菜单-截图查看
        private void menuMainImgView_Click(object sender, EventArgs e)
        {
            panelDownload.Visible = false;
            panelPostComment.Visible = false;
            panelImgCheck.Visible = false;
            panelImgView.Visible = true;
            panelSetup.Visible = false;
        }

        //主菜单-设置
        private void menuMainSetup_Click(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);
            string userChoice = xml.GetNodeInnerText("/Config/ExeConfig/UserExitChoice", "2");
            if (userChoice == "2")
                radioButtonExitExeSetup.Checked = true;
            else
                radioButtonNotExitExeSetup.Checked = true;

            panelDownload.Visible = false;
            panelPostComment.Visible = false;
            panelImgCheck.Visible = false;
            panelImgView.Visible = false;
            panelSetup.Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);

            //为0则表示显示关闭窗体提示 为1则不显示
            string formCloseNotTipAgain = xml.GetNodeInnerText("/Config/ExeConfig/ExitExeNotTipAgain", "0");
            if (formCloseNotTipAgain == "0")
            {
                ExitExeTipForm tipForm = new ExitExeTipForm();
                tipForm.ShowDialog();
                if (tipForm.DialogResult == DialogResult.OK)
                {
                    if (tipForm.UserChoice == 1)
                    {
                        HideMainForm();
                        e.Cancel = true;
                    }
                    else
                    {
                        Application.ExitThread();
                    }
                }
                else  //点击了窗体的关闭按钮或取消按钮
                {
                    e.Cancel = true;
                }
            }
            else
            {
                string userChoice = xml.GetNodeInnerText("/Config/ExeConfig/UserExitChoice", "2");
                if (userChoice == "1")
                {
                    HideMainForm();
                    e.Cancel = true;
                }
                else
                {
                    Application.ExitThread();
                }
            }
        }

        private void notifyIconTray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    ShowMainForm();
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                HideMainForm();
        }

        //托盘菜单-显示窗体
        private void menuContextTrayShowMainForm_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                ShowMainForm();
        }

        //托盘菜单-关于
        private void menuContextTrayAbout_Click(object sender, EventArgs e)
        {
            menuMainAboutMe_Click(sender, e);
        }

        //托盘菜单--退出
        private void menuContextTrayExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        #endregion

        #endregion

        #region Download

        #region Private

        private bool ImportExcelToListviewDownload(string fileName)
        {//导入账号Excel数据到listviewDownload中，缺点为加载第一张的表的数据

            if (!System.IO.File.Exists(fileName))
                return false;
            string strConn = string.Empty;  //获取连接字符串
            if (fileName.ToLower().Contains(".xlsx"))
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else if (fileName.ToLower().Contains(".xls"))
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else
                return false;
            strConn = string.Format(strConn, fileName);

            try
            {
                using (var conn = new OleDbConnection(strConn))
                {
                    string sql = "select * from [{0}]";
                    conn.Open();
                    using (OleDbCommand oldCmd = conn.CreateCommand())
                    {
                        //获取第一张表名
                        DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                          new object[] { null, null, null, "TABLE" });

                        string name = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                        sql = string.Format(sql, name);
                        oldCmd.CommandText = sql;
                        OleDbDataReader reader = oldCmd.ExecuteReader();

                        //序号，iTunes账号，密码
                        listViewAccount_Download.Items.Clear();
                        int index = 0;
                        while (reader.Read())
                        {
                            index++;
                            ListViewItem item = new ListViewItem(new string[] { index.ToString(), reader[0].ToString(), reader[1].ToString() });
                            listViewAccount_Download.Items.Add(item);
                        }
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void OpenAppDownloadPage(string applink)
        {//在iTunes打开指定的链接

            if (applink != null && applink.Contains("itunes://"))
                System.Diagnostics.Process.Start(applink);
        }

        private bool PostIdAndPsword()
        {
            IntPtr idTextBoxHwnd = Hwnds.IDTextBoxHwnd(Version);
            IntPtr pswordTextBoxHwnd = Hwnds.PswordTextBoxHwnd(Version);

            //先清空原来的账号
            MethodsHelper.ClearTextBoxText(idTextBoxHwnd);
            MethodsHelper.ClearTextBoxText(pswordTextBoxHwnd);

            //投递账号密码
            if (!string.IsNullOrEmpty(CurrentID)
                && !string.IsNullOrEmpty(CurrentPsword))
            {
                MethodsHelper.PostText(idTextBoxHwnd, CurrentID.Trim());
                MethodsHelper.PostText(pswordTextBoxHwnd, CurrentPsword.Trim());
                MethodsHelper.ClickControl(Hwnds.StartLoadItunes(Version));
                return true;
            }
            else
                return false;
        }

        private void UpdateCurrentAccountAndDownloadProgress()
        {//动态更新当前的账号，密码与进度显示
            if (listViewAccount_Download.Items.Count == 0)
            {
                CurrentID = string.Empty;
                CurrentPsword = string.Empty;
                labelCurrentProgress.Text = "0 / 0";
                return;
            }
            if (listViewAccount_Download.SelectedIndices != null && listViewAccount_Download.SelectedIndices.Count > 0)
            {
                CurrentID = listViewAccount_Download.SelectedItems[0].SubItems[1].Text.Trim();
                CurrentPsword = listViewAccount_Download.SelectedItems[0].SubItems[2].Text.Trim();
                labelCurrentProgress.Text = Convert.ToString(listViewAccount_Download.SelectedItems[0].Index + 1) +
                                                " / " + listViewAccount_Download.Items.Count.ToString();
            }
        }

        private void WaitAccountPostSucceeded(string id)
        {//等待指定的账号投递成功
            if (string.IsNullOrEmpty(id))
                return;

            string buttonText = MethodsHelper.GetWindowText(Hwnds.OpenDialogBtnHwnd(Version));
            while (buttonText != id.Trim() + " 显示我的 iTunes Store 帐户")
            {
                buttonText = MethodsHelper.GetWindowText(Hwnds.OpenDialogBtnHwnd(Version));
                Thread.Sleep(5);
                Application.DoEvents();
            }
        }

        #endregion

        #region ContextMenu

        //弹出菜单-导入账号
        private void menuContext_ImportAccount_Download_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string fileName = IOHelper.ShowOpenFileDialog();
            bool result = ImportExcelToListviewDownload(fileName);
            if (result)
            {
                listViewAccount_Download.Items[0].Selected = true;
                UpdateCurrentAccountAndDownloadProgress();
            }
        }

        //弹出菜单-导出账号
        private void menuContext_ExportAccount_Download_Click(object sender, EventArgs e)
        {
            string filePath = IOHelper.ShowSaveFileDialog();
            bool isSucceed = ExportListviewToExcel(this.listViewAccount_Download, filePath);
            if (isSucceed)
                MessageBox.Show("账号导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //弹出菜单-修改此项
        private void menuContext_UpdateAccount_Download_Click(object sender, EventArgs e)
        {
            using (UpdateAccountForm updateForm = new UpdateAccountForm())
            {
                updateForm.Account = listViewAccount_Download.SelectedItems[0].SubItems[1].Text;
                updateForm.Psword = listViewAccount_Download.SelectedItems[0].SubItems[2].Text;
                updateForm.TopMost = true;
                updateForm.ShowDialog();
                if (updateForm.DialogResult == DialogResult.OK)
                {
                    listViewAccount_Download.SelectedItems[0].SubItems[1].Text = updateForm.Account;
                    listViewAccount_Download.SelectedItems[0].SubItems[2].Text = updateForm.Psword;
                }
            }
        }

        //弹出菜单-删除此项
        private void menuContext_DeleteAccount_Download_Click(object sender, EventArgs e)
        {
            if (listViewAccount_Download.SelectedIndices != null
                && listViewAccount_Download.SelectedIndices.Count > 0)
            {
                int oldIndex = listViewAccount_Download.SelectedItems[0].Index;
                listViewAccount_Download.Items.Remove(listViewAccount_Download.Items[oldIndex]);
                listViewAccount_Download.Items[oldIndex + 1].Selected = true;
                UpdateCurrentAccountAndDownloadProgress();
            }
        }

        //弹出菜单-标记此项
        private void menuContext_MakeupAccount_Download_Click(object sender, EventArgs e)
        {
            if (listViewAccount_Download.SelectedIndices.Count > 0)
            {
                listViewAccount_Download.Items[listViewAccount_Download.SelectedItems[0].Index].Tag = 1;
                listViewAccount_Download.Items[listViewAccount_Download.SelectedItems[0].Index].ForeColor = Color.Red;
            }
        }

        //弹出菜单-取消标记
        private void menuContext_CancelMakeup_Download_Click(object sender, EventArgs e)
        {
            if (listViewAccount_Download.SelectedIndices.Count > 0)
            {
                listViewAccount_Download.Items[listViewAccount_Download.SelectedItems[0].Index].Tag = null;
                listViewAccount_Download.Items[listViewAccount_Download.SelectedItems[0].Index].ForeColor = SystemColors.WindowText;
            }
        }

        //弹出菜单-清空
        private void menuContext_ClearAll_Download_Click(object sender, EventArgs e)
        {
            listViewAccount_Download.Items.Clear();
            UpdateCurrentAccountAndDownloadProgress();
        }

        //账号弹出菜单时对相关菜单选项禁止
        private void contextMenuDownload_Opening(object sender, CancelEventArgs e)
        {
            if (listViewAccount_Download.Items.Count > 0)
            {
                menuContext_ExportAccount_Download.Enabled = true;  //导出账号
                menuContext_ClearAll_Download.Enabled = true; //清空列表

            }
            else
            {
                menuContext_ExportAccount_Download.Enabled = false;
                menuContext_ClearAll_Download.Enabled = false;
            }

            if (listViewAccount_Download.SelectedIndices.Count > 0)
            {
                menuContext_DeleteAccount_Download.Enabled = true;   //删除此项
                menuContext_UpdateAccount_Download.Enabled = true;   //修改此项

                //未标记
                if (listViewAccount_Download.Items[listViewAccount_Download.SelectedItems[0].Index].Tag == null)
                {
                    menuContext_MakeupAccount_Download.Enabled = true;           //标记此项
                    menuContext_CancelMakeup_Download.Enabled = false;     //取消标记
                }
                else
                {
                    menuContext_MakeupAccount_Download.Enabled = false;
                    menuContext_CancelMakeup_Download.Enabled = true;
                }
            }
            else
            {
                menuContext_DeleteAccount_Download.Enabled = false;
                menuContext_UpdateAccount_Download.Enabled = false;
                menuContext_MakeupAccount_Download.Enabled = false;
                menuContext_CancelMakeup_Download.Enabled = false;
            }
        }

        //动态更改账号与密码及当前进度
        private void listViewAccountDownload_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentAccountAndDownloadProgress();
        }

        //拖放账号表格文件到列表框中
        private void listViewAccountDownload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listViewAccountDownload_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                if (fileName.ToLower().Contains(".xls") || fileName.ToLower().Contains(".xlsx"))
                {
                    bool result = ImportExcelToListviewDownload(fileName);
                    if (result)
                    {
                        listViewAccount_Download.Items[0].Selected = true;
                        UpdateCurrentAccountAndDownloadProgress();
                    }
                }
            }
        }

        //禁止调整所有的列的宽度
        private void listViewAccountDownload_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex > listViewAccount_Download.Columns.Count - 1)
                return;
            e.Cancel = true;
            e.NewWidth = listViewAccount_Download.Columns[e.ColumnIndex].Width;
        }

        #endregion

        #region Events

        //窗口置顶设置
        private void glassButtonExTopMost_Click(object sender, EventArgs e)
        {
            if (TopMost)
            {
                TopMost = false;
                glassButtonExTopMost.Image = MethodsHelper.GetImageFormResourceStream("Res.notopmost.png");
            }
            else
            {
                TopMost = true;
                glassButtonExTopMost.Image = MethodsHelper.GetImageFormResourceStream("Res.topmost.png");
            }
        }

        //下载链接设置
        private void glassButtonExDownloadSetup_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            using (LinkSetupForm downloadForm = new LinkSetupForm())
            {
                downloadForm.TopMost = true;

                //将主窗体从配置文件中读取到的链接传递给链接设置窗体
                downloadForm.AppIlink = this.AppIlink;
                downloadForm.AppIIlink = this.AppIIlink;
                if (downloadForm.ShowDialog() == DialogResult.OK)
                {
                    //将更新后的链接传递给主窗体
                    this.AppIlink = downloadForm.AppIlink;
                    this.AppIIlink = downloadForm.AppIIlink;
                }
            }
        }

        //登录
        private void buttonExPostAccount_Click(object sender, EventArgs e)
        {
            //登陆对话框没有打开，先打开再Post
            if (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero)
            {
                if (Hwnds.iTunesHwnd(Version) != IntPtr.Zero
                    && !MethodsHelper.WindowIsMinimized(Hwnds.iTunesHwnd(Version)))    //找到了iTunes窗口，并且不是最小化
                {
                    IntPtr openDialogBtnHwnd = Hwnds.OpenDialogBtnHwnd(Version);
                    MethodsHelper.ClickControl(openDialogBtnHwnd);

                    //等待登陆窗体出现
                    do
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    } while (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero);
                }
            }

            if (checkBoxPostNextAccount.Checked)
                buttonExNextAccount_Click(sender, e);

            PostIdAndPsword();

            //是否在账号登录成功后直接跳转到下载应用I的页面
            if (checkBoxToDownloadPage.Checked)
            {
                WaitAccountPostSucceeded(CurrentID);
                OpenAppDownloadPage(AppIlink);
            }
        }

        //账号信息
        private void btnIDInformation_Download_Click(object sender, EventArgs e)
        {
            //登陆对话框没有打开，先打开再Post
            if (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero)
            {
                if (Hwnds.iTunesHwnd(Version) != IntPtr.Zero
                    && !MethodsHelper.WindowIsMinimized(Hwnds.iTunesHwnd(Version)))    //找到了iTunes窗口，并且不是最小化
                {
                    IntPtr openDialogBtnHwnd = Hwnds.OpenDialogBtnHwnd(Version);
                    MethodsHelper.ClickControl(openDialogBtnHwnd);

                    //等待登陆窗体出现
                    do
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    } while (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero);
                }
            }

            PostIdAndPsword();
        }

        //应用I
        private void buttonExAppI_Click(object sender, EventArgs e)
        {
            OpenAppDownloadPage(AppIlink);
        }

        //应用II
        private void buttonExAppII_Click(object sender, EventArgs e)
        {
            OpenAppDownloadPage(AppIIlink);
        }

        //自动截图
        private void buttonExAutoShoted_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();

            if (string.IsNullOrEmpty(CurrentID))
                return;

            IntPtr hiTunes = Hwnds.iTunesHwnd(Version);
            if (hiTunes != IntPtr.Zero)
            {
                //将截图所得的最终文件放置此文件夹中
                string captureFolder = AppDomain.CurrentDomain.BaseDirectory + "截图";
                if (!Directory.Exists(captureFolder))
                    Directory.CreateDirectory(captureFolder);

                Bitmap iTunesBmp = MethodsHelper.GetWindowCapture(hiTunes);
                if (iTunesBmp != null && !MethodsHelper.WindowIsMinimized(hiTunes))  //当iTunes最小化时不进行截图
                {
                    //图片截取的起点与大小
                    Point resultBmpStartPoint = new Point(185, 70);
                    Win32.Rect wndRect = new Win32.Rect();
                    Win32.GetWindowRect(hiTunes, ref wndRect);
                    int width = wndRect.Right - wndRect.Left;
                    Size resultBmpSize = new Size(width - resultBmpStartPoint.X, 222);
                    Bitmap resultBmp = iTunesBmp.Clone(new Rectangle(resultBmpStartPoint, resultBmpSize), iTunesBmp.PixelFormat);

                    if (resultBmp != null)
                    {
                        pictureBoxShotImg_ImgView.BackgroundImage = resultBmp;
                        resultBmp.Save(captureFolder + "/" + CurrentID.Trim() + ".jpg");
                    }

                    //是否自动截图后自动登录下个账号
                    if (checkBoxShotedToLoadNextAccount.Checked)
                    {
                        //登陆对话框没有打开，先打开再Post
                        if (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero)
                        {
                            if (Hwnds.iTunesHwnd(Version) != IntPtr.Zero
                                && !MethodsHelper.WindowIsMinimized(Hwnds.iTunesHwnd(Version)))    //找到了iTunes窗口，并且不是最小化
                            {
                                IntPtr openDialogBtnHwnd = Hwnds.OpenDialogBtnHwnd(Version);
                                MethodsHelper.ClickControl(openDialogBtnHwnd);

                                //等待登陆窗体出现
                                do
                                {
                                    Thread.Sleep(5);
                                    Application.DoEvents();
                                } while (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero);
                            }
                        }

                        if (checkBoxPostNextAccount.Checked)
                            buttonExNextAccount_Click(sender, e);

                        PostIdAndPsword();

                        //是否在账号登录成功后直接跳转到下载应用I的页面
                        if (checkBoxToDownloadPage.Checked)
                        {
                            WaitAccountPostSucceeded(CurrentID);
                            OpenAppDownloadPage(AppIlink);
                        }
                    }
                }
            }
        }

        //手动截图
        private void buttonExManualShoted_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string screenshot = AppDomain.CurrentDomain.BaseDirectory + "ScreenShot.dll";
            if (File.Exists(screenshot))
            {
                if (!string.IsNullOrEmpty(CurrentID))
                    Clipboard.SetText(CurrentID);

                using (System.Diagnostics.Process exe = new System.Diagnostics.Process())
                {
                    exe.StartInfo.UseShellExecute = false;
                    exe.StartInfo.FileName = screenshot;
                    exe.Start();
                }
            }
            else
            {
                MessageBox.Show("手动截图失败，未找到相应的截图程序ScreenShot.dll！",
                    "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        //账号向上
        private void buttonExLastAccount_Click(object sender, EventArgs e)
        {
            if (listViewAccount_Download.Items.Count == 0)
                return;

            if (listViewAccount_Download.SelectedIndices.Count > 0)
            {
                int oldIndex = listViewAccount_Download.SelectedItems[0].Index;

                if (oldIndex == 0)    //最顶端
                {
                    listViewAccount_Download.Select();
                    return;
                }
                else
                {
                    listViewAccount_Download.Items[oldIndex - 1].Selected = true;
                    listViewAccount_Download.EnsureVisible(oldIndex - 1);
                    listViewAccount_Download.Select();
                }
            }
        }

        //账号向下
        private void buttonExNextAccount_Click(object sender, EventArgs e)
        {
            if (listViewAccount_Download.Items.Count == 0)
                return;
            if (listViewAccount_Download.SelectedIndices.Count > 0)
            {
                int oldIndex = listViewAccount_Download.SelectedItems[0].Index;

                if (oldIndex == listViewAccount_Download.Items.Count - 1)    //最底端
                {
                    listViewAccount_Download.Select();
                    return;
                }
                else
                {
                    listViewAccount_Download.Items[oldIndex + 1].Selected = true;
                    listViewAccount_Download.EnsureVisible(oldIndex + 1);
                    listViewAccount_Download.Select();
                }
            }

        }

        //配置修改-连续登陆选项
        private void checkBoxPostNextAccount_CheckedChanged(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);
            if (checkBoxPostNextAccount.Checked)
                xml.SetNodeInnerText("/Config/DownloadConfig/PostNextAccount", "1");
            else
                xml.SetNodeInnerText("/Config/DownloadConfig/PostNextAccount", "0");
        }

        //配置修改-登录之后直接跳至下载页面
        private void checkBoxToDownloadPage_CheckedChanged(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);
            if (checkBoxToDownloadPage.Checked)
                xml.SetNodeInnerText("/Config/DownloadConfig/ToDownloadPage", "1");
            else
                xml.SetNodeInnerText("/Config/DownloadConfig/ToDownloadPage", "0");
        }

        //配置修改-自动截图后自动登录下个账号
        private void checkBoxShotedToLoadNextAccount_CheckedChanged(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);
            if (checkBoxShotedToLoadNextAccount.Checked)
                xml.SetNodeInnerText("/Config/DownloadConfig/ShotedToLoadNextAccount", "1");
            else
                xml.SetNodeInnerText("/Config/DownloadConfig/ShotedToLoadNextAccount", "0");
        }

        #endregion

        #endregion

        #region Comment

        #region Private

        private bool ImportExcelToListviewID_Comment(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
                return false;
            string strConn = string.Empty;  //获取连接字符串
            if (fileName.ToLower().Contains(".xlsx"))
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else if (fileName.ToLower().Contains(".xls"))
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else
                return false;
            strConn = string.Format(strConn, fileName);

            try
            {
                using (var conn = new OleDbConnection(strConn))
                {
                    string sql = "select * from [{0}]";
                    conn.Open();
                    using (OleDbCommand oldCmd = conn.CreateCommand())
                    {
                        //获取第一张表名
                        DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                          new object[] { null, null, null, "TABLE" });

                        string name = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                        sql = string.Format(sql, name);
                        oldCmd.CommandText = sql;
                        OleDbDataReader reader = oldCmd.ExecuteReader();

                        //序号，iTunes账号，密码
                        listViewID_Comment.Items.Clear();
                        int index = 0;
                        while (reader.Read())
                        {
                            index++;
                            ListViewItem item = new ListViewItem(new string[] { index.ToString(), reader[0].ToString(), reader[1].ToString() });
                            listViewID_Comment.Items.Add(item);
                        }
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private bool ImportExcelToListViewComment_Comment(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
                return false;
            string strConn = string.Empty;  //获取连接字符串
            if (fileName.ToLower().Contains(".xlsx"))
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else if (fileName.ToLower().Contains(".xls"))
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else
                return false;
            strConn = string.Format(strConn, fileName);

            try
            {
                using (var conn = new OleDbConnection(strConn))
                {
                    string sql = "select * from [{0}]";
                    conn.Open();
                    using (OleDbCommand oldCmd = conn.CreateCommand())
                    {
                        //获取第一张表名
                        DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                          new object[] { null, null, null, "TABLE" });

                        string name = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                        sql = string.Format(sql, name);
                        oldCmd.CommandText = sql;
                        OleDbDataReader reader = oldCmd.ExecuteReader();

                        //序号,评语
                        listViewComment_Comment.Items.Clear();
                        int index = 0;
                        while (reader.Read())
                        {
                            index++;
                            ListViewItem item = new ListViewItem(new string[] { index.ToString(), reader[0].ToString() });
                            listViewComment_Comment.Items.Add(item);
                        }
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void UpdateCurrentAccountAndCommentProgress()
        {
            if (listViewID_Comment.Items.Count == 0)
            {
                CurrentID = string.Empty;
                CurrentPsword = string.Empty;
                labelIDProgress_Comment.Text = "0 / 0";
            }
            else if (listViewID_Comment.SelectedIndices != null
                && listViewID_Comment.SelectedIndices.Count > 0)
            {
                CurrentID = listViewID_Comment.SelectedItems[0].SubItems[1].Text.Trim();
                CurrentPsword = listViewID_Comment.SelectedItems[0].SubItems[2].Text.Trim();
                labelIDProgress_Comment.Text = Convert.ToString(listViewID_Comment.SelectedItems[0].Index + 1) +
                                                " / " + listViewID_Comment.Items.Count.ToString();
            }

            if (listViewComment_Comment.Items.Count == 0)
            {
                CurrentComment = string.Empty;
                labelCommentProgress_Comment.Text = "0 / 0";
            }
            else if (listViewComment_Comment.SelectedIndices != null
                && listViewComment_Comment.SelectedIndices.Count > 0)
            {
                CurrentComment = listViewComment_Comment.SelectedItems[0].SubItems[1].Text.Trim();
                labelCommentProgress_Comment.Text = Convert.ToString(listViewComment_Comment.SelectedItems[0].Index + 1) +
                                                " / " + listViewComment_Comment.Items.Count.ToString();
            }

            if (!string.IsNullOrEmpty(CurrentID))
                labelCurrentIDInfo_Comment.Text = CurrentID;
            else
                labelCurrentIDInfo_Comment.Text = string.Empty;
        }

        #endregion

        #region ID Context Menu

        //账号导入
        private void menuContext_ImportAccount_Comment_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string fileName = IOHelper.ShowOpenFileDialog();
            bool result = ImportExcelToListviewID_Comment(fileName);
            if (result)
            {
                listViewID_Comment.Items[0].Selected = true;
                UpdateCurrentAccountAndCommentProgress();
            }
        }

        //账号导出
        private void menuContext_ExportAccount_Comment_Click(object sender, EventArgs e)
        {
            string filePath = IOHelper.ShowSaveFileDialog();
            bool isSucceed = ExportListviewToExcel(this.listViewID_Comment, filePath);
            if (isSucceed)
                MessageBox.Show("账号导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //删除此项
        private void menuContext_DeleteAccount_Comment_Click(object sender, EventArgs e)
        {
            if (listViewID_Comment.SelectedIndices != null
                && listViewID_Comment.SelectedIndices.Count > 0)
            {
                int oldIndex = listViewID_Comment.SelectedItems[0].Index;
                listViewID_Comment.Items.Remove(listViewID_Comment.Items[oldIndex]);
                listViewID_Comment.Items[oldIndex + 1].Selected = true;
                UpdateCurrentAccountAndCommentProgress();
            }
        }

        //标记此项
        private void menuContext_MakeupAccount_Comment_Click(object sender, EventArgs e)
        {
            if (listViewID_Comment.SelectedIndices.Count > 0)
            {
                listViewID_Comment.Items[listViewID_Comment.SelectedItems[0].Index].Tag = 1;
                listViewID_Comment.Items[listViewID_Comment.SelectedItems[0].Index].ForeColor = Color.Red;
            }
        }

        //取消标记
        private void menuContext_CancelMakeup_Comment_Click(object sender, EventArgs e)
        {
            if (listViewID_Comment.SelectedIndices.Count > 0)
            {
                listViewID_Comment.Items[listViewID_Comment.SelectedItems[0].Index].Tag = null;
                listViewID_Comment.Items[listViewID_Comment.SelectedItems[0].Index].ForeColor = SystemColors.WindowText;
            }
        }

        //清空
        private void menuContext_ClearAll_Comment_Click(object sender, EventArgs e)
        {
            listViewID_Comment.Items.Clear();
            UpdateCurrentAccountAndCommentProgress();
        }

        //账号弹出菜单时对相关菜单选项禁止
        private void contextMenuIDPostComment_Opening(object sender, CancelEventArgs e)
        {
            if (listViewID_Comment.Items.Count > 0)
            {
                menuContext_ExportAccount_Comment.Enabled = true;  //导出账号
                menuContext_ClearAllID_Comment.Enabled = true; //清空列表
            }
            else
            {
                menuContext_ExportAccount_Comment.Enabled = false;
                menuContext_ClearAllID_Comment.Enabled = false;
            }

            if (listViewID_Comment.SelectedIndices.Count > 0)
            {
                menuContext_DeleteAccount_Comment.Enabled = true;   //删除此项

                //未标记
                if (listViewID_Comment.Items[listViewID_Comment.SelectedItems[0].Index].Tag == null)
                {
                    menuContext_MakeupAccount_Comment.Enabled = true;           //标记此项
                    menuContext_CancelMakeup_Comment.Enabled = false;     //取消标记
                }
                else
                {
                    menuContext_MakeupAccount_Comment.Enabled = false;
                    menuContext_CancelMakeup_Comment.Enabled = true;
                }
            }
            else
            {
                menuContext_DeleteAccount_Comment.Enabled = false;
                menuContext_MakeupAccount_Comment.Enabled = false;
                menuContext_CancelMakeup_Comment.Enabled = false;
            }
        }

        private void listViewIDPostComment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentAccountAndCommentProgress();
        }

        //拖放Excel文件至列表框内
        private void listViewIDPostComment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listViewIDPostComment_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                if (fileName.ToLower().Contains(".xls") || fileName.ToLower().Contains(".xlsx"))
                {
                    bool result = ImportExcelToListviewID_Comment(fileName);
                    if (result)
                    {
                        listViewID_Comment.Items[0].Selected = true;
                        UpdateCurrentAccountAndCommentProgress();
                    }
                }
            }
        }

        #endregion

        #region Comment Context Menu

        //评语导入
        private void menuContext_ImportComment_Comment_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string fileName = IOHelper.ShowOpenFileDialog();
            bool result = ImportExcelToListViewComment_Comment(fileName);
            if (result)
            {
                listViewComment_Comment.Items[0].Selected = true;
                UpdateCurrentAccountAndCommentProgress();
            }
        }

        //复制评语
        private void toolmenuContext_CopyComment_Comment_Click(object sender, EventArgs e)
        {
            if (listViewComment_Comment.SelectedIndices != null
                && listViewComment_Comment.SelectedIndices.Count > 0)
            {
                string comment = listViewComment_Comment.SelectedItems[0].SubItems[1].Text;
                Clipboard.SetText(comment);
            }
        }

        //清空列表
        private void menuContext_ClearAllComment_Comment_Click(object sender, EventArgs e)
        {
            listViewComment_Comment.Items.Clear();
            UpdateCurrentAccountAndCommentProgress();
        }

        private void contextMenuCommentPostComment_Opening(object sender, CancelEventArgs e)
        {
            if (listViewComment_Comment.Items.Count > 0)
                menuContext_ClearAllComment_Comment.Enabled = true;
            else
                menuContext_ClearAllComment_Comment.Enabled = false;

            if (listViewComment_Comment.SelectedIndices != null
               && listViewComment_Comment.SelectedIndices.Count > 0)
                menuContext_CopyComment_Comment.Enabled = true;
            else
                menuContext_CopyComment_Comment.Enabled = false;
        }

        private void listViewComment_Comment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCurrentAccountAndCommentProgress();

            if (listViewComment_Comment.SelectedIndices != null
                && listViewComment_Comment.SelectedIndices.Count > 0)
            {
                string comment = listViewComment_Comment.SelectedItems[0].SubItems[1].Text;
                Clipboard.SetText(comment);
            }
        }

        //拖放账号表格文件到列表框中
        private void listViewComment_Comment_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listViewComment_Comment_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                if (fileName.ToLower().Contains(".xls") || fileName.ToLower().Contains(".xlsx"))
                {
                    bool result = ImportExcelToListViewComment_Comment(fileName);
                    if (result)
                    {
                        listViewComment_Comment.Items[0].Selected = true;
                        UpdateCurrentAccountAndCommentProgress();
                    }
                }
            }
        }

        #endregion

        #region Events

        //登录
        private void btnPostID_Comment_Click(object sender, EventArgs e)
        {
            //登陆对话框没有打开，先打开再Post
            if (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero)
            {
                if (Hwnds.iTunesHwnd(Version) != IntPtr.Zero
                    && !MethodsHelper.WindowIsMinimized(Hwnds.iTunesHwnd(Version)))    //找到了iTunes窗口，并且不是最小化
                {
                    IntPtr openDialogBtnHwnd = Hwnds.OpenDialogBtnHwnd(Version);
                    MethodsHelper.ClickControl(openDialogBtnHwnd);

                    //等待登陆窗体出现
                    do
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    } while (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero);
                }
            }

            //是否自动登录下一个账号(连续登陆)
            if (checkBoxPostNextID_Comment.Checked)
                btnNextID_Comment_Click(sender, e);

            PostIdAndPsword();

            //是否在账号登录成功后直接跳转到下载应用I的页面
            if (checkBoxToDownloadPage_Comment.Checked)
            {
                WaitAccountPostSucceeded(CurrentID);
                OpenAppDownloadPage(AppIlink);

            }
        }

        //账号信息
        private void btnIDInformation_Comment_Click(object sender, EventArgs e)
        {
            //登陆对话框没有打开，先打开再Post
            if (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero)
            {
                if (Hwnds.iTunesHwnd(Version) != IntPtr.Zero
                    && !MethodsHelper.WindowIsMinimized(Hwnds.iTunesHwnd(Version)))    //找到了iTunes窗口，并且不是最小化
                {
                    IntPtr openDialogBtnHwnd = Hwnds.OpenDialogBtnHwnd(Version);
                    MethodsHelper.ClickControl(openDialogBtnHwnd);

                    //等待登陆窗体出现
                    do
                    {
                        Thread.Sleep(5);
                        Application.DoEvents();
                    } while (Hwnds.InputAccountDialogHwnd(Version) == IntPtr.Zero);
                }
            }

            PostIdAndPsword();
        }

        //手动截图
        private void btnManualShot_Comment_Click(object sender, EventArgs e)
        {
            buttonExManualShoted_Click(sender, e);
        }

        //下个账号
        private void btnNextID_Comment_Click(object sender, EventArgs e)
        {
            if (listViewID_Comment.Items.Count != 0)
            {
                if (listViewID_Comment.SelectedIndices.Count > 0)
                {
                    int oldIndex = listViewID_Comment.SelectedItems[0].Index;

                    if (oldIndex == listViewID_Comment.Items.Count - 1)    //最底端
                    {
                        listViewID_Comment.Select();
                    }
                    else
                    {
                        listViewID_Comment.Items[oldIndex + 1].Selected = true;
                        listViewID_Comment.EnsureVisible(oldIndex + 1);
                        listViewID_Comment.Select();
                    }
                }
            }
        }

        //应用I
        private void btnOpenAppI_Comment_Click(object sender, EventArgs e)
        {
            OpenAppDownloadPage(AppIlink);
        }

        //应用II
        private void btnOpenAppII_Comment_Click(object sender, EventArgs e)
        {
            OpenAppDownloadPage(AppIIlink);
        }

        //下载链接设置
        private void glassBtnAppLinkSetup_Comment_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            using (LinkSetupForm downloadForm = new LinkSetupForm())
            {
                downloadForm.TopMost = true;

                //将主窗体从配置文件中读取到的链接传递给链接设置窗体
                downloadForm.AppIlink = this.AppIlink;
                downloadForm.AppIIlink = this.AppIIlink;
                if (downloadForm.ShowDialog() == DialogResult.OK)
                {
                    //将更新后的链接传递给主窗体
                    this.AppIlink = downloadForm.AppIlink;
                    this.AppIIlink = downloadForm.AppIIlink;
                }
            }
        }

        #endregion

        #endregion

        #region Image Check

        #region Private

        private bool ImportExcelToListViewImgCheck(string fileName)
        {//导入账号Excel数据到listviewImgCheck中，缺点为加载第一张的表的数据

            if (!System.IO.File.Exists(fileName))
                return false;
            string strConn = string.Empty;  //获取连接字符串
            if (fileName.ToLower().Contains(".xlsx"))
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else if (fileName.ToLower().Contains(".xls"))
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=Excel 8.0;";
            else
                return false;
            strConn = string.Format(strConn, fileName);

            try
            {
                using (var conn = new OleDbConnection(strConn))
                {
                    string sql = "select * from [{0}]";
                    conn.Open();
                    using (OleDbCommand oldCmd = conn.CreateCommand())
                    {
                        //获取第一张表名
                        DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                          new object[] { null, null, null, "TABLE" });

                        string name = excelSchema.Rows[0]["TABLE_NAME"].ToString();
                        sql = string.Format(sql, name);
                        oldCmd.CommandText = sql;
                        OleDbDataReader reader = oldCmd.ExecuteReader();

                        //序号，iTunes账号，密码
                        listViewAccount_ImgCheck.Items.Clear();
                        int index = 0;
                        while (reader.Read())
                        {
                            index++;
                            ListViewItem item = new ListViewItem(new string[] { index.ToString(), reader[0].ToString(), "未检查" });
                            listViewAccount_ImgCheck.Items.Add(item);
                        }
                        reader.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private List<string> GetFolderAllFile(string folder)
        {
            if (folder.Length != 0)
            {
                DirectoryInfo folderInfo = new DirectoryInfo(folder);
                List<string> fileList = new List<string>();
                foreach (FileInfo file in folderInfo.GetFiles())
                {
                    fileList.Add(file.Name);
                }
                return fileList;
            }
            else
                return null;
        }

        /// <summary>
        /// 存在表格中但不存在截图文件夹内的账号（截图缺失）
        /// </summary>
        private List<string> GetIdNotExistInImgFolder()
        {
            if (listViewAccount_ImgCheck.Items.Count != 0)
            {
                List<string> idNotExistList = new List<string>();
                foreach (ListViewItem item in listViewAccount_ImgCheck.Items)
                {
                    if (item.SubItems[2].Text.Trim() == "不存在")
                    {
                        idNotExistList.Add(item.SubItems[1].Text.Trim());
                    }
                }
                return idNotExistList;
            }
            else
                return null;
        }

        /// <summary>
        /// 存在截图文件夹内但不存在表格内的账号(截图无效或者多余的)
        /// </summary>
        private List<string> GetInvalidImgList()
        {
            string folder = textBoxExImgFolder.Text;
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
            {
                List<string> invalidImgList = new List<string>();    //多余截图,无效截图文件列表
                List<string> imgFileList = GetFolderAllFile(folder);
                bool isExist = false;
                foreach (string img in imgFileList)
                {
                    foreach (ListViewItem item in listViewAccount_ImgCheck.Items)
                    {
                        isExist = false;
                        string id = item.SubItems[1].Text.Trim() + ".jpg";
                        if (id.Equals(img))
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                        invalidImgList.Add(img);
                }
                return invalidImgList;
            }
            else
                return null;
        }

        /// <summary>
        /// 检查listview中ID所对应的图片是否存在文件夹内
        /// </summary>
        private void CheckIDExistInFolder(string folder)
        {
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
            {
                List<string> imgFileList = GetFolderAllFile(folder);
                if (imgFileList != null)
                {
                    foreach (ListViewItem item in listViewAccount_ImgCheck.Items)
                    {
                        string id = item.SubItems[1].Text.Trim() + ".jpg";
                        foreach (string imgfile in imgFileList)
                        {
                            if (id == imgfile)
                            {
                                item.SubItems[2].Text = "存在";
                                item.ForeColor = SystemColors.WindowText;
                                break;
                            }
                            else
                            {
                                item.SubItems[2].Text = "不存在";
                                item.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateSatusLabelInfo()
        {
            int idNum = 0, imgNum = 0;
            if (listViewAccount_ImgCheck.Items.Count > 0)
                idNum = listViewAccount_ImgCheck.Items.Count;
            if (textBoxExImgFolder.Text.Length != 0)
            {
                List<string> imgFileList = GetFolderAllFile(textBoxExImgFolder.Text);
                if (imgFileList != null && imgFileList.Count > 0)
                    imgNum = imgFileList.Count;
            }
            statusImgCheckResultLabel.Text = "账号ID个数：" + idNum.ToString() +
                "  截图文件夹文件个数： " + imgNum.ToString();
        }

        private void UpdateCheckResultInfo()
        {
            //显示账号总数与截图文件总数
            int idNum = 0, imgNum = 0;
            if (listViewAccount_ImgCheck.Items.Count > 0)
                idNum = listViewAccount_ImgCheck.Items.Count;
            if (textBoxExImgFolder.Text.Length != 0)
            {
                List<string> imgFileList = GetFolderAllFile(textBoxExImgFolder.Text);
                if (imgFileList != null && imgFileList.Count > 0)
                    imgNum = imgFileList.Count;
            }
            string checkResultInfo = "总账号数：" + idNum.ToString() + "\r\n" +
                "总截图数：" + imgNum.ToString() + "\r\n";

            //显示缺少的截图文件
            List<string> idNotExistList = GetIdNotExistInImgFolder();
            if (idNotExistList != null && idNotExistList.Count != 0)
            {
                checkResultInfo += "截图文件缺失个数：" + idNotExistList.Count.ToString() + "\r\n";
                foreach (string id in idNotExistList)
                {
                    checkResultInfo += id + "\r\n";
                }
            }
            else
            {
                checkResultInfo += "截图文件缺失个数：" + "0" + "\r\n";
            }

            //显示多余的、无效的截图文件
            if (checkBoxCheckInvalidImg.Checked)
            {
                List<string> invalidImgList = GetInvalidImgList();
                if (invalidImgList != null && invalidImgList.Count != 0)
                {
                    checkResultInfo += "无效截图文件个数：" + invalidImgList.Count.ToString() + "\r\n";
                    foreach (string invalidImg in invalidImgList)
                    {
                        checkResultInfo += invalidImg + "\r\n";
                    }
                }
                else
                {
                    checkResultInfo += "无效截图文件个数：" + "0" + "\r\n";
                }

            }

            textBoxExImgCheckResult.Text = checkResultInfo;
        }

        #endregion

        #region ContextMenu

        //账号导入
        private void menuContext_ImportAccount_ImgCheck_Click(object sender, EventArgs e)
        {
            VerifyApplication_MainForm();
            string fileName = IOHelper.ShowOpenFileDialog();
            bool result = ImportExcelToListViewImgCheck(fileName);
            if (result)
                UpdateSatusLabelInfo();
        }

        //账号导出
        private void menuContext_ExportAccoount_ImgCheck_Click(object sender, EventArgs e)
        {
            string filePath = IOHelper.ShowSaveFileDialog();

            bool isSucceed = ExportListviewToExcel(this.listViewAccount_ImgCheck, filePath);
            if (isSucceed)
                MessageBox.Show("账号导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //复制账号
        private void menuContext_CopyID_ImgCheck_Click(object sender, EventArgs e)
        {
            if (listViewAccount_ImgCheck.SelectedIndices != null
                && listViewAccount_ImgCheck.SelectedIndices.Count > 0)
            {
                string id = listViewAccount_ImgCheck.SelectedItems[0].SubItems[1].Text;
                Clipboard.SetText(id);
            }
        }

        //删除此项
        private void menuContext_DeleteCurrentID_ImgCheck_Click(object sender, EventArgs e)
        {
            if (listViewAccount_ImgCheck.SelectedIndices != null
                && listViewAccount_ImgCheck.SelectedIndices.Count > 0)
            {
                int index = listViewAccount_ImgCheck.SelectedItems[0].Index;
                listViewAccount_ImgCheck.Items.Remove(listViewAccount_ImgCheck.Items[index]);
                UpdateSatusLabelInfo();
            }
        }

        //清空列表
        private void menuContext_ClearAll_ImgCheck_Click(object sender, EventArgs e)
        {
            listViewAccount_ImgCheck.Items.Clear();
            UpdateSatusLabelInfo();
        }

        //弹出菜单打开
        private void contextMenuImgCheck_Opening(object sender, CancelEventArgs e)
        {
            if (listViewAccount_ImgCheck.Items.Count > 0)
            {
                menuContext_ExportAccoount_ImgCheck.Enabled = true;
                menuContext_ClearAll_ImgCheck.Enabled = true;
            }
            else
            {
                menuContext_ExportAccoount_ImgCheck.Enabled = false;
                menuContext_ClearAll_ImgCheck.Enabled = false;
            }

            if (listViewAccount_ImgCheck.SelectedIndices != null
               && listViewAccount_ImgCheck.SelectedIndices.Count > 0)
            {
                menuContext_CopyID_ImgCheck.Enabled = true;
                menuContext_DeleteCurrentID_ImgCheck.Enabled = true;
            }
            else
            {
                menuContext_CopyID_ImgCheck.Enabled = false;
                menuContext_DeleteCurrentID_ImgCheck.Enabled = false;
            }
        }

        //拖放账号表格文件到列表框中
        private void listViewAccount_ImgCheck_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listViewAccount_ImgCheck_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                if (fileName.ToLower().Contains(".xls") || fileName.ToLower().Contains(".xlsx"))
                {
                    bool result = ImportExcelToListViewImgCheck(fileName);
                    if (result)
                        UpdateSatusLabelInfo();
                }
            }
        }

        #endregion

        #region Events

        //选择截图文件夹
        private void glassButtonExAddImgFolder_Click(object sender, EventArgs e)
        {
            string imgFolder = IOHelper.ShowFolderBrowserDialog();
            textBoxExImgFolder.Text = imgFolder;
            UpdateSatusLabelInfo();
        }

        //截图检查
        private void buttonExStartImgCheck_Click(object sender, EventArgs e)
        {
            string folder = textBoxExImgFolder.Text;
            if (folder.Length != 0)
            {
                statusImgCheckResultLabel.Text = "正在进行截图检查...";
                textBoxExImgCheckResult.Clear();
                Application.DoEvents();

                CheckIDExistInFolder(folder);
                UpdateCheckResultInfo();

                statusImgCheckResultLabel.Text = "截图检查完成";
            }
            else
            {
                MessageBox.Show("请选择截图所在的文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //实现拖放截图文件夹
        private void textBoxExImgFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBoxExImgFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = paths[0];

                if (Directory.Exists(fileName))
                {
                    textBoxExImgFolder.Text = fileName.Trim();
                    UpdateSatusLabelInfo();
                }
            }
        }


        #endregion

        #endregion

        #region Setup

        //重置
        private void buttonExReset_Click(object sender, EventArgs e)
        {
            radioButtonExitExeSetup.Checked = true;
            comboBoxiTunesVersion.SelectedIndex = 1;
            Version = iTunesVersion.V10_5_1_42;
        }

        //应用
        private void buttonExApplySetup_Click(object sender, EventArgs e)
        {
            string xmlFileName = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";
            XmlConfigHelper xml = new XmlConfigHelper(xmlFileName);

            if (radioButtonExitExeSetup.Checked)
                xml.SetNodeInnerText("/Config/ExeConfig/UserExitChoice", "2");
            else
                xml.SetNodeInnerText("/Config/ExeConfig/UserExitChoice", "1");

            if (comboBoxiTunesVersion.SelectedIndex == 0)
            {
                Version = iTunesVersion.V10_5_0_142;
                xml.SetNodeInnerText("/Config/DownloadConfig/iTunesVersion", "A");
            }
            else if (comboBoxiTunesVersion.SelectedIndex == 1)
            {
                Version = iTunesVersion.V10_5_1_42;
                xml.SetNodeInnerText("/Config/DownloadConfig/iTunesVersion", "B");
            }

            menuMainDownload_Click(sender, e);    //跳至下载页面
        }

        //返回
        private void buttonExBacktoDownload_Click(object sender, EventArgs e)
        {
            menuMainDownload_Click(sender, e);    //跳至下载页面
        }

        #endregion

    }
}
