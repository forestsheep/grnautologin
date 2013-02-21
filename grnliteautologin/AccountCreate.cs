using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GrnLiteAutoLogin
{
    /// <summary>
    /// 创建账号的画面窗体
    /// </summary>
    public partial class AccountCreate : Form
    {
        public AccountCreate()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;
        }

        private void btnSaveAccount_Click(object sender, EventArgs e)
        {
            // 没有输入用户名
            if (txtUsername.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show(Properties.Resources.USERNAME_NOT_INPUT);
                return;
            }
            // 存储账号
            Account account = new Account(txtUsername.Text, txtPassword.Text, false);
            AccountManager.GetInstance.AddAccount(account);
            this.Close();
            this.Dispose();
        }

        private void AccountEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
