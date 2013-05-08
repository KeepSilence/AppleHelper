namespace AppleHelper_Server
{
    partial class FormServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlServer = new System.Windows.Forms.TabControl();
            this.tabPageUserVerify = new System.Windows.Forms.TabPage();
            this.groupBoxUserInfo = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBoxMachineIdToAdd = new System.Windows.Forms.TextBox();
            this.labelMachineIdToAdd = new System.Windows.Forms.Label();
            this.labelUserTotalNum = new System.Windows.Forms.Label();
            this.textBoxQQ = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxNickName = new System.Windows.Forms.TextBox();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.labelTip = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelQQ = new System.Windows.Forms.Label();
            this.labelNickName = new System.Windows.Forms.Label();
            this.groupBoxCreateKey = new System.Windows.Forms.GroupBox();
            this.buttonWriteLicense = new System.Windows.Forms.Button();
            this.buttonGenerateKey = new System.Windows.Forms.Button();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.textBoxMachineId = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelMachineId = new System.Windows.Forms.Label();
            this.tabPageUserManagement = new System.Windows.Forms.TabPage();
            this.tabControlServer.SuspendLayout();
            this.tabPageUserVerify.SuspendLayout();
            this.groupBoxUserInfo.SuspendLayout();
            this.groupBoxCreateKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlServer
            // 
            this.tabControlServer.Controls.Add(this.tabPageUserVerify);
            this.tabControlServer.Controls.Add(this.tabPageUserManagement);
            this.tabControlServer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControlServer.Location = new System.Drawing.Point(1, 0);
            this.tabControlServer.Name = "tabControlServer";
            this.tabControlServer.SelectedIndex = 0;
            this.tabControlServer.Size = new System.Drawing.Size(685, 419);
            this.tabControlServer.TabIndex = 0;
            // 
            // tabPageUserVerify
            // 
            this.tabPageUserVerify.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageUserVerify.Controls.Add(this.groupBoxUserInfo);
            this.tabPageUserVerify.Controls.Add(this.groupBoxCreateKey);
            this.tabPageUserVerify.Location = new System.Drawing.Point(4, 26);
            this.tabPageUserVerify.Name = "tabPageUserVerify";
            this.tabPageUserVerify.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUserVerify.Size = new System.Drawing.Size(677, 389);
            this.tabPageUserVerify.TabIndex = 0;
            this.tabPageUserVerify.Text = "用户激活";
            // 
            // groupBoxUserInfo
            // 
            this.groupBoxUserInfo.Controls.Add(this.checkBox2);
            this.groupBoxUserInfo.Controls.Add(this.checkBox1);
            this.groupBoxUserInfo.Controls.Add(this.textBoxMachineIdToAdd);
            this.groupBoxUserInfo.Controls.Add(this.labelMachineIdToAdd);
            this.groupBoxUserInfo.Controls.Add(this.labelUserTotalNum);
            this.groupBoxUserInfo.Controls.Add(this.textBoxQQ);
            this.groupBoxUserInfo.Controls.Add(this.textBoxEmail);
            this.groupBoxUserInfo.Controls.Add(this.textBoxNickName);
            this.groupBoxUserInfo.Controls.Add(this.buttonAddUser);
            this.groupBoxUserInfo.Controls.Add(this.labelTip);
            this.groupBoxUserInfo.Controls.Add(this.labelEmail);
            this.groupBoxUserInfo.Controls.Add(this.labelQQ);
            this.groupBoxUserInfo.Controls.Add(this.labelNickName);
            this.groupBoxUserInfo.Location = new System.Drawing.Point(324, 16);
            this.groupBoxUserInfo.Name = "groupBoxUserInfo";
            this.groupBoxUserInfo.Size = new System.Drawing.Size(338, 360);
            this.groupBoxUserInfo.TabIndex = 1;
            this.groupBoxUserInfo.TabStop = false;
            this.groupBoxUserInfo.Text = "用户信息";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(253, 187);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 21);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "同步";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(253, 133);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(71, 21);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "同步QQ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBoxMachineIdToAdd
            // 
            this.textBoxMachineIdToAdd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMachineIdToAdd.Location = new System.Drawing.Point(77, 187);
            this.textBoxMachineIdToAdd.Name = "textBoxMachineIdToAdd";
            this.textBoxMachineIdToAdd.Size = new System.Drawing.Size(170, 23);
            this.textBoxMachineIdToAdd.TabIndex = 14;
            // 
            // labelMachineIdToAdd
            // 
            this.labelMachineIdToAdd.AutoSize = true;
            this.labelMachineIdToAdd.Location = new System.Drawing.Point(12, 190);
            this.labelMachineIdToAdd.Name = "labelMachineIdToAdd";
            this.labelMachineIdToAdd.Size = new System.Drawing.Size(44, 17);
            this.labelMachineIdToAdd.TabIndex = 13;
            this.labelMachineIdToAdd.Text = "机器码";
            // 
            // labelUserTotalNum
            // 
            this.labelUserTotalNum.AutoSize = true;
            this.labelUserTotalNum.Location = new System.Drawing.Point(74, 232);
            this.labelUserTotalNum.Name = "labelUserTotalNum";
            this.labelUserTotalNum.Size = new System.Drawing.Size(32, 17);
            this.labelUserTotalNum.TabIndex = 12;
            this.labelUserTotalNum.Text = "人数";
            // 
            // textBoxQQ
            // 
            this.textBoxQQ.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxQQ.Location = new System.Drawing.Point(77, 75);
            this.textBoxQQ.Name = "textBoxQQ";
            this.textBoxQQ.Size = new System.Drawing.Size(170, 23);
            this.textBoxQQ.TabIndex = 11;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxEmail.Location = new System.Drawing.Point(77, 131);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(170, 23);
            this.textBoxEmail.TabIndex = 10;
            // 
            // textBoxNickName
            // 
            this.textBoxNickName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNickName.Location = new System.Drawing.Point(77, 35);
            this.textBoxNickName.Name = "textBoxNickName";
            this.textBoxNickName.Size = new System.Drawing.Size(170, 23);
            this.textBoxNickName.TabIndex = 9;
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Location = new System.Drawing.Point(172, 229);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(75, 23);
            this.buttonAddUser.TabIndex = 4;
            this.buttonAddUser.Text = "添加";
            this.buttonAddUser.UseVisualStyleBackColor = true;
            // 
            // labelTip
            // 
            this.labelTip.AutoSize = true;
            this.labelTip.Location = new System.Drawing.Point(12, 232);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(44, 17);
            this.labelTip.TabIndex = 3;
            this.labelTip.Text = "用户数";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(24, 134);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(32, 17);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "邮箱";
            // 
            // labelQQ
            // 
            this.labelQQ.AutoSize = true;
            this.labelQQ.Location = new System.Drawing.Point(28, 78);
            this.labelQQ.Name = "labelQQ";
            this.labelQQ.Size = new System.Drawing.Size(28, 17);
            this.labelQQ.TabIndex = 1;
            this.labelQQ.Text = "QQ";
            // 
            // labelNickName
            // 
            this.labelNickName.AutoSize = true;
            this.labelNickName.Location = new System.Drawing.Point(24, 38);
            this.labelNickName.Name = "labelNickName";
            this.labelNickName.Size = new System.Drawing.Size(32, 17);
            this.labelNickName.TabIndex = 0;
            this.labelNickName.Text = "昵称";
            // 
            // groupBoxCreateKey
            // 
            this.groupBoxCreateKey.Controls.Add(this.buttonWriteLicense);
            this.groupBoxCreateKey.Controls.Add(this.buttonGenerateKey);
            this.groupBoxCreateKey.Controls.Add(this.textBoxKey);
            this.groupBoxCreateKey.Controls.Add(this.textBoxMachineId);
            this.groupBoxCreateKey.Controls.Add(this.labelKey);
            this.groupBoxCreateKey.Controls.Add(this.labelMachineId);
            this.groupBoxCreateKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxCreateKey.Location = new System.Drawing.Point(7, 16);
            this.groupBoxCreateKey.Name = "groupBoxCreateKey";
            this.groupBoxCreateKey.Size = new System.Drawing.Size(300, 360);
            this.groupBoxCreateKey.TabIndex = 0;
            this.groupBoxCreateKey.TabStop = false;
            this.groupBoxCreateKey.Text = "注册码生成";
            // 
            // buttonWriteLicense
            // 
            this.buttonWriteLicense.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonWriteLicense.Location = new System.Drawing.Point(175, 312);
            this.buttonWriteLicense.Name = "buttonWriteLicense";
            this.buttonWriteLicense.Size = new System.Drawing.Size(75, 23);
            this.buttonWriteLicense.TabIndex = 11;
            this.buttonWriteLicense.Text = "License";
            this.buttonWriteLicense.UseVisualStyleBackColor = true;
            this.buttonWriteLicense.Click += new System.EventHandler(this.buttonWriteLicense_Click);
            // 
            // buttonGenerateKey
            // 
            this.buttonGenerateKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGenerateKey.Location = new System.Drawing.Point(74, 312);
            this.buttonGenerateKey.Name = "buttonGenerateKey";
            this.buttonGenerateKey.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerateKey.TabIndex = 10;
            this.buttonGenerateKey.Text = "注册码";
            this.buttonGenerateKey.UseVisualStyleBackColor = true;
            this.buttonGenerateKey.Click += new System.EventHandler(this.buttonGenerateKey_Click);
            // 
            // textBoxKey
            // 
            this.textBoxKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxKey.Location = new System.Drawing.Point(74, 79);
            this.textBoxKey.Multiline = true;
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(176, 210);
            this.textBoxKey.TabIndex = 9;
            // 
            // textBoxMachineId
            // 
            this.textBoxMachineId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMachineId.Location = new System.Drawing.Point(74, 33);
            this.textBoxMachineId.Name = "textBoxMachineId";
            this.textBoxMachineId.Size = new System.Drawing.Size(176, 23);
            this.textBoxMachineId.TabIndex = 8;
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKey.Location = new System.Drawing.Point(24, 79);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(44, 17);
            this.labelKey.TabIndex = 7;
            this.labelKey.Text = "注册码";
            // 
            // labelMachineId
            // 
            this.labelMachineId.AutoSize = true;
            this.labelMachineId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMachineId.Location = new System.Drawing.Point(24, 33);
            this.labelMachineId.Name = "labelMachineId";
            this.labelMachineId.Size = new System.Drawing.Size(44, 17);
            this.labelMachineId.TabIndex = 6;
            this.labelMachineId.Text = "机器码";
            // 
            // tabPageUserManagement
            // 
            this.tabPageUserManagement.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageUserManagement.Location = new System.Drawing.Point(4, 26);
            this.tabPageUserManagement.Name = "tabPageUserManagement";
            this.tabPageUserManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUserManagement.Size = new System.Drawing.Size(677, 409);
            this.tabPageUserManagement.TabIndex = 1;
            this.tabPageUserManagement.Text = "用户管理";
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 424);
            this.Controls.Add(this.tabControlServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppleHelper--服务端";
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.tabControlServer.ResumeLayout(false);
            this.tabPageUserVerify.ResumeLayout(false);
            this.groupBoxUserInfo.ResumeLayout(false);
            this.groupBoxUserInfo.PerformLayout();
            this.groupBoxCreateKey.ResumeLayout(false);
            this.groupBoxCreateKey.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlServer;
        private System.Windows.Forms.TabPage tabPageUserVerify;
        private System.Windows.Forms.TabPage tabPageUserManagement;
        private System.Windows.Forms.GroupBox groupBoxCreateKey;
        private System.Windows.Forms.Button buttonGenerateKey;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxMachineId;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelMachineId;
        private System.Windows.Forms.GroupBox groupBoxUserInfo;
        private System.Windows.Forms.TextBox textBoxQQ;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxNickName;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelQQ;
        private System.Windows.Forms.Label labelNickName;
        private System.Windows.Forms.Label labelUserTotalNum;
        private System.Windows.Forms.TextBox textBoxMachineIdToAdd;
        private System.Windows.Forms.Label labelMachineIdToAdd;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonWriteLicense;
    }
}

