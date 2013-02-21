using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GrnLiteAutoLogin
{
    class SettingManager
    {
        private static SettingManager SETTINGMAGAGE;

        internal static SettingManager GetInstance
        {
            get
            {
                if (SETTINGMAGAGE == null)
                {
                    SETTINGMAGAGE = new SettingManager();
                }
                return SETTINGMAGAGE;
            }
        }

        private XmlAccesser xmlAccesser;

        #region 构造函数

        /// <summary>
        /// 创建一个SettingManager实例
        /// </summary>
        private SettingManager()
        {
            xmlAccesser = new XmlAccesser(Properties.Resources.SETTING_FILENAME, Properties.Resources.SETTING_ROOT);
            // 格式化
            if (!Properties.Resources.SETTING_ROOT.Equals(xmlAccesser.RootElement.Name))
            {
                xmlAccesser.RenameRootElement(Properties.Resources.SETTING_ROOT);
            }
            if (xmlAccesser.RootElement.SelectSingleNode("/" + Properties.Resources.SETTING_ROOT + "/" + Properties.Resources.SETTING_AUTORUN) == null)
            {
                xmlAccesser.AppendElement(Properties.Resources.SETTING_AUTORUN);
            }
        }
        #endregion


        #region 属性
        internal bool AutoRun
        {
            get
            {
                XmlElement xe = (XmlElement)xmlAccesser.RootElement.SelectSingleNode("/" + Properties.Resources.SETTING_ROOT + "/" + Properties.Resources.SETTING_AUTORUN);
                if (!"0".Equals(xe.InnerText) && !"1".Equals(xe.InnerText))
                {
                    xe.InnerText = "0";
                    xmlAccesser.Save();
                }

                if ("0".Equals(xe.InnerText))
                {
                    return false;
                }
                else if ("1".Equals(xe.InnerText))
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    xmlAccesser.EditElementText(Properties.Resources.SETTING_AUTORUN, "1");
                }
                else
                {
                    xmlAccesser.EditElementText(Properties.Resources.SETTING_AUTORUN, "0");
                }
            }
        }
        #endregion
    }
}
