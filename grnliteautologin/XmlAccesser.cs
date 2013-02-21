using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace GrnLiteAutoLogin
{
    public class XmlAccesser
    {
        #region 成员函数
        private string filename;
        private XmlDocument xdoc;
        private XmlElement rootElement;
        #endregion

        #region 属性
        /// <summary>
        /// 取得根节点
        /// </summary>
        public XmlElement RootElement
        {
            get { return rootElement; }
        }

        /// <summary>
        /// 取得XMLDocument的实例
        /// </summary>
        public XmlDocument XmlDoc
        {
            get { return xdoc; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 创建一个XML访问器
        /// </summary>
        /// <param name="filename">XML文件名</param>
        /// <param name="rootElementName">如果没有文件时命名的根目录名</param>
        public XmlAccesser(string filename, string rootElementName)
        {
            this.filename = filename;
            bool isFileExist = File.Exists(filename);
            this.xdoc = new XmlDocument();
            try
            {
                if (!isFileExist)
                {
                    this.rootElement = this.xdoc.CreateElement(rootElementName);
                    this.xdoc.AppendChild(this.rootElement);
                    this.xdoc.Save(this.filename);
                }
                else
                {
                    this.xdoc.Load(this.filename);
                    this.rootElement = this.xdoc.DocumentElement;
                }
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 创建一个XML访问器,提供默认的根节点名叫&lt;xml&gt;
        /// </summary>
        /// <param name="filename"></param>
        public XmlAccesser(string filename)
            : this(filename, "xml")
        {
        }
        #endregion

        #region 成员方法

        /// <summary>
        /// 根元素改名，会删除所有内容
        /// </summary>
        /// <param name="newName">要改成的名字</param>
        public void RenameRootElement(string newName)
        {
            try
            {
                this.XmlDoc.RemoveAll();
                XmlElement xe = this.XmlDoc.CreateElement(newName);
                this.XmlDoc.AppendChild(xe);
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 添加一个元素（根目录下）
        /// </summary>
        /// <param name="elementName">要添加的元素名</param>
        public void AppendElement(string elementName)
        {
            AppendElement(elementName, null);
        }

        /// <summary>
        /// 添加一个有内容文本元素（根目录下）
        /// </summary>
        /// <param name="elementName">元素名</param>
        /// <param name="innerText">内容文本</param>
        public void AppendElement(string elementName, string innerText)
        {
            try
            {
                XmlElement node = this.XmlDoc.CreateElement(elementName);
                if (!(innerText == null || string.Empty.Equals(innerText.Trim())))
                {
                    node.InnerText = innerText;
                }
                this.RootElement.AppendChild(node);
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 添加一个元素（根目录下）
        /// </summary>
        /// <param name="xmlElement">需要添加的元素</param>
        public void AppendElement(XmlElement xmlElement)
        {
            try
            {
                this.RootElement.AppendChild(xmlElement);
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 删除一个元素（根目录下,删除找到的第一个元素）
        /// </summary>
        /// <param name="elementName">要删除的元素名</param>
        public void RemoveElement(string elementName)
        {
            try
            {
                XmlNode xn = this.RootElement.SelectSingleNode(elementName);
                if (xn != null)
                {
                    this.RootElement.RemoveChild(xn);
                }
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 修改元素的文本内容（根目录下，修改找到的第一个元素）
        /// </summary>
        /// <param name="elementName">元素名</param>
        /// <param name="text">文本内容</param>
        public void EditElementText(string elementName, string text)
        {
            try
            {
                XmlElement xe = (XmlElement)this.RootElement.SelectSingleNode(elementName);
                if (xe != null)
                {
                    xe.InnerText = text;
                }
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 寻找并修改文本内容
        /// </summary>
        /// <param name="xPath">寻找的路径及表达式</param>
        /// <param name="tagName">修改的节点名</param>
        /// <param name="updateString">文本内容</param>
        public void UpdateElementText(string xPath, string tagName, string updateString)
        {
            try
            {
                XmlElement xe = (XmlElement)RootElement.SelectSingleNode(xPath);
                XmlElement childNode = (XmlElement)xe.SelectSingleNode(tagName);
                childNode.InnerText = updateString;
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        /// <summary>
        /// 保存XML
        /// </summary>
        public void Save()
        {
            try
            {
                this.XmlDoc.Save(this.filename);
            }
            catch (Exception e)
            {
                //TODO write into log
                e.ToString();
            }
        }

        #endregion
    }
}
