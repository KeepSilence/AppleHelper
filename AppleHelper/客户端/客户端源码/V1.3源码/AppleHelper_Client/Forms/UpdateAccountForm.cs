using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppleHelper_Client
{
    public partial class UpdateAccountForm : Form
    {
        public UpdateAccountForm()
        {
            InitializeComponent();
        }

        public string Account { get; set; }
        public string Psword { get; set; }

        private void UpdateAccountForm_Load(object sender, EventArgs e)
        {
            textBoxExAccount.Text = Account;
            textBoxExPsword.Text = Psword;
        }

        private void buttonExUpdate_Click(object sender, EventArgs e)
        {
            Account = textBoxExAccount.Text.Trim();
            Psword = textBoxExPsword.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonExCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
