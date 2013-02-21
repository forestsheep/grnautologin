using System;

namespace GrnLiteAutoLogin
{
    internal class Account
    {
        #region 成员变量
        private string loginID;
        private string loginPW;
        private bool enable;
        #endregion

        #region 属性
        internal string LoginID
        {
            get { return loginID; }
            set { loginID = value; }
        }


        internal string LoginPW
        {
            get { return loginPW; }
            set { loginPW = value; }
        }

        internal bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
        #endregion

        #region 构造函数
        internal Account()
        {
        }

        internal Account(string loginID, string loginPW, bool enable)
        {
            this.loginID = loginID;
            this.loginPW = loginPW;
            this.enable = enable;
        }

        internal Account(string loginID, string loginPW)
            : this(loginID, loginPW, true)
        {
        }
        #endregion

        #region method
        public override string ToString()
        {
            return this.loginID;
        }
        #endregion
    }
}