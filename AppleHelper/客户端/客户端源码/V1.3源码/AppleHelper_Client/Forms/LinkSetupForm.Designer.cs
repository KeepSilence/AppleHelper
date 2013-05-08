namespace AppleHelper_Client
{
    partial class LinkSetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkSetupForm));
            this.labelAppIlink = new System.Windows.Forms.Label();
            this.labelAppIIlink = new System.Windows.Forms.Label();
            this.textBoxExAppI = new AppleHelper_Client.TextBoxEx();
            this.textBoxExAppII = new AppleHelper_Client.TextBoxEx();
            this.buttonExSetup = new AppleHelper_Client.ButtonEx();
            this.buttonExCancel = new AppleHelper_Client.ButtonEx();
            this.SuspendLayout();
            // 
            // labelAppIlink
            // 
            this.labelAppIlink.AutoSize = true;
            this.labelAppIlink.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAppIlink.Location = new System.Drawing.Point(25, 31);
            this.labelAppIlink.Name = "labelAppIlink";
            this.labelAppIlink.Size = new System.Drawing.Size(80, 17);
            this.labelAppIlink.TabIndex = 0;
            this.labelAppIlink.Text = "应用Ⅰ链接：";
            // 
            // labelAppIIlink
            // 
            this.labelAppIIlink.AutoSize = true;
            this.labelAppIIlink.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAppIIlink.Location = new System.Drawing.Point(25, 75);
            this.labelAppIIlink.Name = "labelAppIIlink";
            this.labelAppIIlink.Size = new System.Drawing.Size(80, 17);
            this.labelAppIIlink.TabIndex = 2;
            this.labelAppIIlink.Text = "应用Ⅱ链接：";
            // 
            // textBoxExAppI
            // 
            this.textBoxExAppI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExAppI.EmptyTextTip = null;
            this.textBoxExAppI.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.textBoxExAppI.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBoxExAppI.Location = new System.Drawing.Point(118, 29);
            this.textBoxExAppI.Name = "textBoxExAppI";
            this.textBoxExAppI.Size = new System.Drawing.Size(382, 23);
            this.textBoxExAppI.TabIndex = 4;
            // 
            // textBoxExAppII
            // 
            this.textBoxExAppII.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxExAppII.EmptyTextTip = null;
            this.textBoxExAppII.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.textBoxExAppII.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBoxExAppII.Location = new System.Drawing.Point(118, 73);
            this.textBoxExAppII.Name = "textBoxExAppII";
            this.textBoxExAppII.Size = new System.Drawing.Size(382, 23);
            this.textBoxExAppII.TabIndex = 5;
            // 
            // buttonExSetup
            // 
            this.buttonExSetup.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExSetup.Location = new System.Drawing.Point(339, 117);
            this.buttonExSetup.Name = "buttonExSetup";
            this.buttonExSetup.Size = new System.Drawing.Size(68, 23);
            this.buttonExSetup.TabIndex = 0;
            this.buttonExSetup.Text = "应用";
            this.buttonExSetup.UseVisualStyleBackColor = true;
            this.buttonExSetup.Click += new System.EventHandler(this.buttonExSetup_Click);
            // 
            // buttonExCancel
            // 
            this.buttonExCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExCancel.Location = new System.Drawing.Point(432, 117);
            this.buttonExCancel.Name = "buttonExCancel";
            this.buttonExCancel.Size = new System.Drawing.Size(68, 23);
            this.buttonExCancel.TabIndex = 8;
            this.buttonExCancel.Text = "取消";
            this.buttonExCancel.UseVisualStyleBackColor = true;
            this.buttonExCancel.Click += new System.EventHandler(this.buttonExCancel_Click);
            // 
            // LinkSetupForm
            // 
            this.AcceptButton = this.buttonExSetup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 151);
            this.Controls.Add(this.buttonExCancel);
            this.Controls.Add(this.buttonExSetup);
            this.Controls.Add(this.textBoxExAppII);
            this.Controls.Add(this.textBoxExAppI);
            this.Controls.Add(this.labelAppIIlink);
            this.Controls.Add(this.labelAppIlink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LinkSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下载链接设置";
            this.Load += new System.EventHandler(this.downloadSetupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAppIlink;
        private System.Windows.Forms.Label labelAppIIlink;
        private TextBoxEx textBoxExAppI;
        private TextBoxEx textBoxExAppII;
        private ButtonEx buttonExSetup;
        private ButtonEx buttonExCancel;
    }
}