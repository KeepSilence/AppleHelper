namespace AppleHelper_Client
{
    partial class ExitExeTipForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitExeTipForm));
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.labelChoiceTip = new System.Windows.Forms.Label();
            this.radioButtonNotExitExe = new System.Windows.Forms.RadioButton();
            this.radioButtonExitExe = new System.Windows.Forms.RadioButton();
            this.checkBoxNotTipAgain = new System.Windows.Forms.CheckBox();
            this.buttonExCancel = new AppleHelper_Client.ButtonEx();
            this.buttonExOk = new AppleHelper_Client.ButtonEx();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxIcon.Image")));
            this.pictureBoxIcon.Location = new System.Drawing.Point(36, 21);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(47, 44);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            // 
            // labelChoiceTip
            // 
            this.labelChoiceTip.AutoSize = true;
            this.labelChoiceTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelChoiceTip.Location = new System.Drawing.Point(104, 21);
            this.labelChoiceTip.Name = "labelChoiceTip";
            this.labelChoiceTip.Size = new System.Drawing.Size(164, 17);
            this.labelChoiceTip.TabIndex = 2;
            this.labelChoiceTip.Text = "您点击了关闭按钮，您是想：";
            // 
            // radioButtonNotExitExe
            // 
            this.radioButtonNotExitExe.AutoSize = true;
            this.radioButtonNotExitExe.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonNotExitExe.Location = new System.Drawing.Point(107, 44);
            this.radioButtonNotExitExe.Name = "radioButtonNotExitExe";
            this.radioButtonNotExitExe.Size = new System.Drawing.Size(206, 21);
            this.radioButtonNotExitExe.TabIndex = 3;
            this.radioButtonNotExitExe.Text = "最小化到系统托盘区，不退出程序";
            this.radioButtonNotExitExe.UseVisualStyleBackColor = true;
            // 
            // radioButtonExitExe
            // 
            this.radioButtonExitExe.AutoSize = true;
            this.radioButtonExitExe.Checked = true;
            this.radioButtonExitExe.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonExitExe.Location = new System.Drawing.Point(107, 71);
            this.radioButtonExitExe.Name = "radioButtonExitExe";
            this.radioButtonExitExe.Size = new System.Drawing.Size(74, 21);
            this.radioButtonExitExe.TabIndex = 4;
            this.radioButtonExitExe.TabStop = true;
            this.radioButtonExitExe.Text = "退出程序";
            this.radioButtonExitExe.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotTipAgain
            // 
            this.checkBoxNotTipAgain.AutoSize = true;
            this.checkBoxNotTipAgain.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxNotTipAgain.Location = new System.Drawing.Point(12, 116);
            this.checkBoxNotTipAgain.Name = "checkBoxNotTipAgain";
            this.checkBoxNotTipAgain.Size = new System.Drawing.Size(75, 21);
            this.checkBoxNotTipAgain.TabIndex = 4;
            this.checkBoxNotTipAgain.TabStop = false;
            this.checkBoxNotTipAgain.Text = "不在提示";
            this.checkBoxNotTipAgain.UseVisualStyleBackColor = true;
            // 
            // buttonExCancel
            // 
            this.buttonExCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExCancel.Location = new System.Drawing.Point(263, 114);
            this.buttonExCancel.Name = "buttonExCancel";
            this.buttonExCancel.Size = new System.Drawing.Size(68, 23);
            this.buttonExCancel.TabIndex = 1;
            this.buttonExCancel.Text = "取消";
            this.buttonExCancel.UseVisualStyleBackColor = true;
            this.buttonExCancel.Click += new System.EventHandler(this.buttonExCancel_Click);
            // 
            // buttonExOk
            // 
            this.buttonExOk.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExOk.Location = new System.Drawing.Point(176, 114);
            this.buttonExOk.Name = "buttonExOk";
            this.buttonExOk.Size = new System.Drawing.Size(68, 23);
            this.buttonExOk.TabIndex = 0;
            this.buttonExOk.Text = "确定";
            this.buttonExOk.UseVisualStyleBackColor = true;
            this.buttonExOk.Click += new System.EventHandler(this.buttonExOk_Click);
            // 
            // ExitExeTipForm
            // 
            this.AcceptButton = this.buttonExCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 149);
            this.Controls.Add(this.buttonExOk);
            this.Controls.Add(this.buttonExCancel);
            this.Controls.Add(this.checkBoxNotTipAgain);
            this.Controls.Add(this.radioButtonExitExe);
            this.Controls.Add(this.radioButtonNotExitExe);
            this.Controls.Add(this.labelChoiceTip);
            this.Controls.Add(this.pictureBoxIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExitExeTipForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关闭提示";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ExitExeTipForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label labelChoiceTip;
        private System.Windows.Forms.RadioButton radioButtonNotExitExe;
        private System.Windows.Forms.RadioButton radioButtonExitExe;
        private System.Windows.Forms.CheckBox checkBoxNotTipAgain;
        private ButtonEx buttonExCancel;
        private ButtonEx buttonExOk;
    }
}