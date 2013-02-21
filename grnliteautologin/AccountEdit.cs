using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GrnLiteAutoLogin
{
    public partial class AccountEdit : Form
    {
        private Account account;

        internal AccountEdit(Account account)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;
            this.account = account;
        }

        /// <summary>
        /// 储存账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAccount_Click(object sender, EventArgs e)
        {
            // 没有输入用户名
            if (txtUsername.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show(Properties.Resources.USERNAME_NOT_INPUT);
                return;
            }
            // 存储账号
            AccountManager am = AccountManager.GetInstance;
            am.EditAccount(this.account.LoginID, this.txtUsername.Text, this.txtPassword.Text, this.account.Enable);
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 画面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountEdit_Load(object sender, EventArgs e)
        {
            this.txtUsername.Text = this.account.LoginID;
        }

    }
}
