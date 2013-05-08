namespace AppleHelper_Client
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.buttonVerify = new System.Windows.Forms.Button();
            this.labelMachineId = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.textBoxMachineId = new System.Windows.Forms.TextBox();
            this.glassButtonExCopyMachineId = new AppleHelper_Client.GlassButtonEx();
            this.textBoxKey = new AppleHelper_Client.TextBoxEx();
            this.glassButtonExOpenHelp = new AppleHelper_Client.GlassButtonEx();
            this.glassButtonExAbout = new AppleHelper_Client.GlassButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExCopyMachineId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExOpenHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonVerify
            // 
            this.buttonVerify.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonVerify.Location = new System.Drawing.Point(226, 306);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 23);
            this.buttonVerify.TabIndex = 0;
            this.buttonVerify.Text = "注册";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // labelMachineId
            // 
            this.labelMachineId.AutoSize = true;
            this.labelMachineId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMachineId.Location = new System.Drawing.Point(22, 35);
            this.labelMachineId.Name = "labelMachineId";
            this.labelMachineId.Size = new System.Drawing.Size(44, 17);
            this.labelMachineId.TabIndex = 1;
            this.labelMachineId.Text = "机器码";
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKey.Location = new System.Drawing.Point(22, 73);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(44, 17);
            this.labelKey.TabIndex = 2;
            this.labelKey.Text = "注册码";
            // 
            // textBoxMachineId
            // 
            this.textBoxMachineId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMachineId.Location = new System.Drawing.Point(86, 32);
            this.textBoxMachineId.Name = "textBoxMachineId";
            this.textBoxMachineId.ReadOnly = true;
            this.textBoxMachineId.Size = new System.Drawing.Size(175, 23);
            this.textBoxMachineId.TabIndex = 3;
            // 
            // glassButtonExCopyMachineId
            // 
            this.glassButtonExCopyMachineId.BackColor = System.Drawing.Color.Transparent;
            this.glassButtonExCopyMachineId.DialogResult = System.Windows.Forms.DialogResult.None;
            this.glassButtonExCopyMachineId.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.glassButtonExCopyMachineId.Image = ((System.Drawing.Image)(resources.GetObject("glassButtonExCopyMachineId.Image")));
            this.glassButtonExCopyMachineId.Location = new System.Drawing.Point(271, 32);
            this.glassButtonExCopyMachineId.Name = "glassButtonExCopyMachineId";
            this.glassButtonExCopyMachineId.Size = new System.Drawing.Size(23, 23);
            this.glassButtonExCopyMachineId.TabIndex = 5;
            this.glassButtonExCopyMachineId.TabStop = false;
            this.glassButtonExCopyMachineId.ToolTipText = "复制机器码";
            this.glassButtonExCopyMachineId.Click += new System.EventHandler(this.glassButtonExCopyMachineId_Click);
            // 
            // textBoxKey
            // 
            this.textBoxKey.AllowDrop = true;
            this.textBoxKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxKey.EmptyTextTip = "请输入注册码或将授权文件拖至此处";
            this.textBoxKey.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.textBoxKey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxKey.Location = new System.Drawing.Point(86, 70);
            this.textBoxKey.Multiline = true;
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(215, 210);
            this.textBoxKey.TabIndex = 4;
            this.textBoxKey.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxKey_DragDrop);
            this.textBoxKey.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxKey_DragEnter);
            // 
            // glassButtonExOpenHelp
            // 
            this.glassButtonExOpenHelp.BackColor = System.Drawing.Color.Transparent;
            this.glassButtonExOpenHelp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.glassButtonExOpenHelp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.glassButtonExOpenHelp.Image = ((System.Drawing.Image)(resources.GetObject("glassButtonExOpenHelp.Image")));
            this.glassButtonExOpenHelp.Location = new System.Drawing.Point(193, 305);
            this.glassButtonExOpenHelp.Name = "glassButtonExOpenHelp";
            this.glassButtonExOpenHelp.Size = new System.Drawing.Size(24, 24);
            this.glassButtonExOpenHelp.TabIndex = 6;
            this.glassButtonExOpenHelp.TabStop = false;
            this.glassButtonExOpenHelp.ToolTipText = "获取帮助";
            this.glassButtonExOpenHelp.Click += new System.EventHandler(this.glassButtonExOpenHelp_Click);
            // 
            // glassButtonExAbout
            // 
            this.glassButtonExAbout.BackColor = System.Drawing.Color.Transparent;
            this.glassButtonExAbout.DialogResult = System.Windows.Forms.DialogResult.None;
            this.glassButtonExAbout.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.glassButtonExAbout.Image = ((System.Drawing.Image)(resources.GetObject("glassButtonExAbout.Image")));
            this.glassButtonExAbout.Location = new System.Drawing.Point(160, 305);
            this.glassButtonExAbout.Name = "glassButtonExAbout";
            this.glassButtonExAbout.Size = new System.Drawing.Size(24, 24);
            this.glassButtonExAbout.TabIndex = 7;
            this.glassButtonExAbout.TabStop = false;
            this.glassButtonExAbout.ToolTipText = "关于";
            this.glassButtonExAbout.Click += new System.EventHandler(this.glassButtonExAbout_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonVerify;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 358);
            this.Controls.Add(this.glassButtonExAbout);
            this.Controls.Add(this.glassButtonExOpenHelp);
            this.Controls.Add(this.glassButtonExCopyMachineId);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.textBoxMachineId);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.labelMachineId);
            this.Controls.Add(this.buttonVerify);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppleHelper";
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExCopyMachineId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExOpenHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glassButtonExAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.Label labelMachineId;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox textBoxMachineId;
        private TextBoxEx textBoxKey;
        private GlassButtonEx glassButtonExCopyMachineId;
        private GlassButtonEx glassButtonExOpenHelp;
        private GlassButtonEx glassButtonExAbout;

    }
}

