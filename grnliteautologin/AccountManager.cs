using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GrnLiteAutoLogin
{
    internal class AccountManager
    {
        private static AccountManager ACCOUNTMANGAGER;

        internal static AccountManager GetInstance
        {
            get
            {
                if (ACCOUNTMANGAGER == null)
                {
                    ACCOUNTMANGAGER = new AccountManager();
                }
                return ACCOUNTMANGAGER;
            }
        }

        internal int Count
        {
            get
            {
                XmlNodeList xnl = xmlAccesser.RootElement.ChildNodes;
                if (xnl.Count > 0)
                {
                    IEnumerator ie = xnl.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        XmlElement x = (XmlElement)ie.Current;
                        XmlNode xn = x.SelectSingleNode(Properties.Resources.ACCOUNT_NO);
                        if (xn == null)
                        {
                            return -1;
                        }
                        string numberString = xn.InnerText;
                        if (string.Empty.Equals(numberString))
                        {
                            return -1;
                        }

                        try
                        {
                            Int16.Parse(numberString);
                        }
                        catch (FormatException)
                        {
                            return -1;
                        }
                    }
                    return xnl.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        private XmlAccesser xmlAccesser;

        #region 构造函数

        /// <summary>
        /// 创建一个AccountManager实例
        /// </summary>
        private AccountManager()
        {
            this.xmlAccesser = new XmlAccesser(Properties.Resources.ACCOUNT_FILENAME, Properties.Resources.ACCOUNT_ROOT);
            this.Format();
            this.Update();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 格式化
        /// </summary>
        private void Format()
        {
            if (!Properties.Resources.ACCOUNT_ROOT.Equals(xmlAccesser.RootElement.Name))
            {
                this.xmlAccesser.RenameRootElement(Properties.Resources.ACCOUNT_ROOT);
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        private void Update()
        {
            XmlNodeList xnl = xmlAccesser.RootElement.ChildNodes;
            if (xnl.Count > 0)
            {
                int i = 1;
                IEnumerator ie = xnl.GetEnumerator();

                while (ie.MoveNext())
                {
                    XmlElement x = (XmlElement)ie.Current;
                    XmlNode xn = x.SelectSingleNode(Properties.Resources.ACCOUNT_NO);
                    if (xn == null)
                    {
                        XmlElement childNo = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_NO);
                        childNo.InnerText = i.ToString();
                        x.AppendChild(childNo);
                        i++;
                    }
                }
                xmlAccesser.Save();
            }
        }
        /// <summary>
        /// 添加一个账号
        /// </summary>
        /// <param name="accountID">账号id</param>
        /// <param name="password">密码</param>
        internal void AddAccount(string accountID, string password, bool enable)
        {
            XmlElement xe = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_NODE);
            XmlElement child1 = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_NO);
            XmlElement child2 = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_ID);
            XmlElement child3 = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_PASSWORD);
            XmlElement child4 = xmlAccesser.XmlDoc.CreateElement(Properties.Resources.ACCOUNT_ENABLE);
            int c = this.Count;
            if (c >= 0)
            {
                child1.InnerText = c.ToString();
            }
            else
            {
                // 文件不符合规则
            }
            child1.InnerText = "???";
            child2.InnerText = accountID;
            child3.InnerText = password;
            if (enable)
            {
                child4.InnerText = "1";
            }
            else
            {
                child4.InnerText = "0";
            }
            xe.AppendChild(child2);
            xe.AppendChild(child3);
            xe.AppendChild(child4);
            xmlAccesser.AppendElement(xe);
        }

        /// <summary>
        /// 添加一个账号
        /// </summary>
        /// <param name="account">账号</param>
        internal void AddAccount(Account account)
        {
            AddAccount(account.LoginID, account.LoginPW, account.Enable);
        }

        /// <summary>
        /// 删除一个账号
        /// </summary>
        /// <param name="accountID">要删除的账号ID</param>
        internal void DeleteAccount(string accountID)
        {
            XmlElement xe = FindAccount(accountID);
            if (xe != null)
            {
                xmlAccesser.RootElement.RemoveChild(xe);
                xmlAccesser.Save();
            }
        }

        /// <summary>
        /// 编辑一个账号
        /// </summary>
        /// <param name="oldAccountID">要编辑的账号</param>
        /// <param name="newAccountID">编辑后的名字</param>
        /// <param name="newPassword">编辑后的密码</param>
        internal void EditAccount(string oldAccountID, string newAccountID, string newPassword, bool newEnable)
        {
            XmlElement xe = FindAccount(oldAccountID);
            if (xe != null)
            {
                XmlElement id = (XmlElement)xe.SelectSingleNode(Properties.Resources.ACCOUNT_ID);
                XmlElement pw = (XmlElement)xe.SelectSingleNode(Properties.Resources.ACCOUNT_PASSWORD);
                XmlElement enable = (XmlElement)xe.SelectSingleNode(Properties.Resources.ACCOUNT_ENABLE);
                id.InnerText = newAccountID;
                pw.InnerText = newPassword;
                if (newEnable)
                {
                    enable.InnerText = "1";
                }
                else
                {
                    enable.InnerText = "0";
                }
                xmlAccesser.Save();
            }
        }

        /// <summary>
        /// 从磁盘读取账号
        /// </summary>
        /// <returns>账号列表</returns>
        internal ArrayList ReadAccount()
        {
            ArrayList al = new ArrayList();
            XmlNodeList xnl = xmlAccesser.RootElement.ChildNodes;
            if (xnl.Count > 0)
            {
                IEnumerator ie = xnl.GetEnumerator();
                while (ie.MoveNext())
                {
                    XmlElement x = (XmlElement)ie.Current;
                    string id = x.SelectSingleNode(Properties.Resources.ACCOUNT_ID).InnerText;
                    string pw = x.SelectSingleNode(Properties.Resources.ACCOUNT_PASSWORD).InnerText;
                    string enableS = x.SelectSingleNode(Properties.Resources.ACCOUNT_ENABLE).InnerText;
                    bool enableB = "0".Equals(enableS) ? false : "1".Equals(enableS) ? true : false;
                    al.Add(new Account(id, pw, enableB));
                }
            }
            return al;
        }

        /// <summary>
        /// 寻找Account
        /// </summary>
        /// <param name="accountID">账号名</param>
        /// <returns></returns>
        private XmlElement FindAccount(string accountID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/");
            sb.Append(Properties.Resources.ACCOUNT_ROOT);
            sb.Append("/");
            sb.Append(Properties.Resources.ACCOUNT_NODE);
            sb.Append("[id='");
            sb.Append(accountID);
            sb.Append("']");
            XmlElement xe = (XmlElement)xmlAccesser.RootElement.SelectSingleNode(sb.ToString());
            return xe;
        }

        #endregion

    }
}
