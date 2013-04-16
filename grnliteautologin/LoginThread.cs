using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Net;

namespace GrnLiteAutoLogin
{
    public delegate void LoginEventHandler(string sParam);
    internal class LoginThread
    {
        private static LoginEventHandler OnLoginEventHandler;

        private static Thread T;
        internal LoginThread()
        {
        }
        
        internal void run(object o)
        {
            if (T == null)
            {
                T = new Thread(new ParameterizedThreadStart(th));
                T.Start(o);
            }
            else
            {
                if (ThreadState.Stopped == T.ThreadState)
                {
                    T = new Thread(new ParameterizedThreadStart(th));
                    T.Start(o);
                }
                else
                {
                    OnLoginEventHandler(Message.BUSY_NOW_R() + "\r\n");
                }
            }
        }

        private void th(object o)
        {
            ArrayList accountList = (ArrayList)o;
            for (int i = 0; i < accountList.Count; i++)
            {
                Account account = (Account)accountList[i];
                OnLoginEventHandler("准备访问账号：" + account.LoginID + "\r\n");
                Thread.Sleep(50);
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.CYBOZUSH_GRN_ADDRESS);
                sb.Append("?");
                sb.Append(Properties.Resources.CYBOZUSH_GRN_IDF);
                sb.Append("&_account=");
                sb.Append(account.LoginID);
                sb.Append("&_password=");
                sb.Append(account.LoginPW);
                HttpAccesser ha = new HttpAccesser();
                ha.AccessUrl = sb.ToString();
                ha.AccessMethod = HttpAccesser.ACCESS_METHOD.GET;
                ha.ContentType = Properties.Resources.CONTENT_TYPE1;
                ha.IsUseCookie = true;
                try
                {
                    OnLoginEventHandler("正在尝试访问网站...\r\n");
                    ha.access();
                    Thread.Sleep(2000);
                    if (ha.ResponseText.Contains("<title>门户</title>"))
                    {
                        DateTime dt = new DateTime();
                        dt = DateTime.Parse(ha.ResponseDate);
                        OnLoginEventHandler(dt.ToString("yyyy年MM月dd日HH时mm分ss秒") + " 访问成功！\r\n");
                    }
                    else
                    {
                        OnLoginEventHandler("访问失败！\r\n");
                    }
                    Thread.Sleep(50);
                }
                catch (HttpAccesser.UncompleteSettingException unse)
                {
                    OnLoginEventHandler(unse.ToString() + "\r\n" + "访问失败！\r\n");
                    Thread.Sleep(50);
                }
                catch (WebException we)
                {
                    we.ToString();
                    OnLoginEventHandler("访问网站失败，有可能是网络连接问题" + "\r\n");
                    Thread.Sleep(50);
                }
            }
            OnLoginEventHandler("访问操作结束\r\n");
            Thread.Sleep(50);
        }

        internal event LoginEventHandler Login
        {
            add
            {
                OnLoginEventHandler += new LoginEventHandler(value);
            }
            remove
            {
                OnLoginEventHandler -= new LoginEventHandler(value);
            }
        }
    }
}
