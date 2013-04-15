using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace GrnLiteAutoLogin
{
    public partial class Main : Form
    {
        private string logMessage;
        private LoginThread lt;

        #region 构造函数
        public Main()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;
        }
        #endregion

        #region 事件

        private void Main_Load(object sender, EventArgs e)
        {
            // 读取账号信息
            this.ReadAccounts();

            // 读取是否自动运行
            cbAutoRun.Checked = SettingManager.GetInstance.AutoRun;

            // 自动运行
            if (cbAutoRun.Checked)
            {
                this.AutoRun();
            }

        }

        private void btnRunLogin_Click(object sender, EventArgs e)
        {
            this.AutoRun();
            //this.outputArea.AppendText(AccountManager.GetInstance.Count.ToString());
        }

        protected void TextShow(object sender, EventArgs e)
        {
            this.outputArea.AppendText(logMessage);
        }

        private void appendAccountBtn_Click(object sender, EventArgs e)
        {
            AccountCreate ae = new AccountCreate();
            ae.ShowDialog();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            if (p != null)
            {
                p.Kill();
            }
        }

        private void checkedListBox_accounts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Account currentSelected = (Account)checkedListBox_accounts.SelectedItem;
            if (currentSelected == null)
            {
                return;
            }
            currentSelected.Enable = !currentSelected.Enable;

            AccountManager am = AccountManager.GetInstance;
            am.EditAccount(currentSelected.LoginID, currentSelected.LoginID, currentSelected.LoginPW, currentSelected.Enable);
        }

        /// <summary>
        /// 激活窗体时的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Activated(object sender, EventArgs e)
        {
            this.ReLoadAccounts();
        }

        /// <summary>
        /// 账号被选中时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox_accounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox_accounts.SelectedItem == null)
            {
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
            }
            else
            {
                btnEditAccount.Enabled = true;
                btnDeleteAccount.Enabled = true;
            }
        }

        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            Account a = (Account)checkedListBox_accounts.SelectedItem;
            if (a == null)
            {
                return;
            }
            AccountEdit ae = new AccountEdit(a);
            ae.ShowDialog();
        }

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            Account a = (Account)checkedListBox_accounts.SelectedItem;
            if (a == null)
            {
                btnDeleteAccount.Enabled = false;
                return;
            }
            AccountManager am = AccountManager.GetInstance;
            am.DeleteAccount(a.LoginID);
            this.ReLoadAccounts();
        }

        /// <summary>
        /// 保存是否自动运行的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            SettingManager.GetInstance.AutoRun = cbAutoRun.Checked;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 从磁盘读取账号显示在列表中
        /// </summary>
        private void ReadAccounts()
        {
            AccountManager am = AccountManager.GetInstance;
            ArrayList accountList = am.ReadAccount();
            checkedListBox_accounts.Items.Clear();
            for (int i = 0; i < accountList.Count; i++)
            {
                checkedListBox_accounts.Items.Add((Account)accountList[i], ((Account)accountList[i]).Enable);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        private ArrayList GetEnableAccount()
        {
            ArrayList al = new ArrayList();
            CheckedListBox.CheckedItemCollection c = checkedListBox_accounts.CheckedItems;
            foreach (Account a in c)
            {
                al.Add(a);
            }
            return al;
        }
        
        /// <summary>
        /// 自动运行登录
        /// </summary>
        private void AutoRun()
        {
            if (this.lt == null)
            {
                this.lt = new LoginThread();
                lt.Login += this.OnLogin;
            }
            try
            {
                ArrayList accountList = this.GetEnableAccount();
                if (accountList.Count > 0)
                {
                    lt.run(accountList);
                }
                else
                {
                    // 账号未输入
                    outputArea.AppendText(Properties.Resources.NONE_ACCOUNT + "\r\n");
                }
            }
            catch (Exception ee)
            {
                outputArea.AppendText(ee.ToString() + "\r\n");
            }
        }


        /// <summary>
        /// 重新读入账号
        /// </summary>
        private void ReLoadAccounts()
        {
            this.ReadAccounts();
            if (checkedListBox_accounts.SelectedItem == null)
            {
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
            }
            else
            {
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
            }
        }

        private void OnLogin(string s)
        {
            logMessage = s;
            Object[] list = { this, System.EventArgs.Empty };
            this.outputArea.BeginInvoke(new EventHandler(TextShow), list);
        }
        #endregion

    }
}
