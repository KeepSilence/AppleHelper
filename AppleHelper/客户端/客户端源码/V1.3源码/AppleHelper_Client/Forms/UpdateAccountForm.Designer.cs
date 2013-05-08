namespace AppleHelper_Client
{
    partial class UpdateAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAccountForm));
            this.labelAccount = new System.Windows.Forms.Label();
            this.labelPsword = new System.Windows.Forms.Label();
            this.textBoxExAccount = new AppleHelper_Client.TextBoxEx();
            this.textBoxExPsword = new AppleHelper_Client.TextBoxEx();
            this.buttonExCancel = new AppleHelper_Client.ButtonEx();
            this.buttonExUpdate = new AppleHelper_Client.ButtonEx();
            this.SuspendLayout();
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAccount.Location = new System.Drawing.Point(15, 25);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(32, 17);
            this.labelAccount.TabIndex = 3;
            this.labelAccount.Text = "账号";
            // 
            // labelPsword
            // 
            this.labelPsword.AutoSize = true;
            this.labelPsword.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPsword.Location = new System.Drawing.Point(236, 26);
            this.labelPsword.Name = "labelPsword";
            this.labelPsword.Size = new System.Drawing.Size(32, 17);
            this.labelPsword.TabIndex = 4;
            this.labelPsword.Text = "密码";
            // 
            // textBoxExAccount
            // 
            this.textBoxExAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExAccount.EmptyTextTip = null;
            this.textBoxExAccount.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.textBoxExAccount.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBoxExAccount.Location = new System.Drawing.Point(53, 22);
            this.textBoxExAccount.Name = "textBoxExAccount";
            this.textBoxExAccount.Size = new System.Drawing.Size(166, 23);
            this.textBoxExAccount.TabIndex = 1;
            // 
            // textBoxExPsword
            // 
            this.textBoxExPsword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExPsword.EmptyTextTip = null;
            this.textBoxExPsword.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.textBoxExPsword.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBoxExPsword.Location = new System.Drawing.Point(274, 23);
            this.textBoxExPsword.Name = "textBoxExPsword";
            this.textBoxExPsword.Size = new System.Drawing.Size(166, 23);
            this.textBoxExPsword.TabIndex = 2;
            // 
            // buttonExCancel
            // 
            this.buttonExCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExCancel.Location = new System.Drawing.Point(372, 70);
            this.buttonExCancel.Name = "buttonExCancel";
            this.buttonExCancel.Size = new System.Drawing.Size(68, 23);
            this.buttonExCancel.TabIndex = 5;
            this.buttonExCancel.Text = "取消";
            this.buttonExCancel.UseVisualStyleBackColor = true;
            this.buttonExCancel.Click += new System.EventHandler(this.buttonExCancel_Click);
            // 
            // buttonExUpdate
            // 
            this.buttonExUpdate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExUpdate.Location = new System.Drawing.Point(285, 70);
            this.buttonExUpdate.Name = "buttonExUpdate";
            this.buttonExUpdate.Size = new System.Drawing.Size(68, 23);
            this.buttonExUpdate.TabIndex = 0;
            this.buttonExUpdate.Text = "确定";
            this.buttonExUpdate.UseVisualStyleBackColor = true;
            this.buttonExUpdate.Click += new System.EventHandler(this.buttonExUpdate_Click);
            // 
            // UpdateAccountForm
            // 
            this.AcceptButton = this.buttonExUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 114);
            this.Controls.Add(this.buttonExUpdate);
            this.Controls.Add(this.buttonExCancel);
            this.Controls.Add(this.textBoxExPsword);
            this.Controls.Add(this.textBoxExAccount);
            this.Controls.Add(this.labelPsword);
            this.Controls.Add(this.labelAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "账号修改";
            this.Load += new System.EventHandler(this.UpdateAccountForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.Label labelPsword;
        private TextBoxEx textBoxExAccount;
        private TextBoxEx textBoxExPsword;
        private ButtonEx buttonExCancel;
        private ButtonEx buttonExUpdate;
    }
}