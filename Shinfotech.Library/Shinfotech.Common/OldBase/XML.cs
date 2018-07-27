using System;
using System.IO;
using System.Xml;

namespace ShInfoTech.Common
{
    /// <summary>
    /// XML处理类
    /// </summary>
    public class XML
    {
        private XmlDocument xmldoc = new XmlDocument();

        public void ChangeNode(string Name, string Value)
        {
            XmlNode node = this.xmldoc.DocumentElement.SelectSingleNode(Name);
            if (node != null)
            {
                node.InnerText = Value;
            }
        }
        /// <summary>
        /// 生成xml根节点
        /// </summary>
        /// <param name="Name"></param>
        public void CreateRootNode(string Name)
        {
            XmlElement newChild = this.xmldoc.CreateElement("Root");
            this.xmldoc.AppendChild(newChild);
        }
        /// <summary>
        ///  生成xml文件头
        /// </summary>
        /// <param name="Encoding"></param>
        /// <param name="Standalone"></param>
        public void CreateXMLHeader(string Encoding, string Standalone)
        {
            XmlDeclaration newChild = this.xmldoc.CreateXmlDeclaration("1.0", Encoding, Standalone);
            this.xmldoc.AppendChild(newChild);
        }
        /// <summary>
        /// 生成新节点
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void InsertNode(string Name, string Value)
        {
            if ((Name != "") && (Name != null))
            {
                XmlElement documentElement = this.xmldoc.DocumentElement;
                XmlNode newChild = this.xmldoc.CreateNode(XmlNodeType.Element, Name, "");
                newChild.InnerText = Value;
                documentElement.AppendChild(newChild);
            }
        }
        /// <summary>
        ///  加载XML文件
        /// </summary>
        /// <param name="Path"></param>
        public void LoadXml(string Path)
        {
            try
            {
                if (this.SearchXml(Path))
                {
                    this.xmldoc.Load(Path);
                }
                else
                {
                    this.CreateXMLHeader("UTF-8", "yes");
                    this.CreateRootNode("Root");
                }
            }
            catch
            {
                this.CreateXMLHeader("UTF-8", "yes");
                this.CreateRootNode("Root");
            }
        }

        /// <summary>
        /// 返回节点值
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string ReturnValue(string Name)
        {
            XmlElement documentElement = this.xmldoc.DocumentElement;
            XmlNode node = null;
            if (documentElement.HasChildNodes)
            {
                node = documentElement.SelectSingleNode(Name);
            }
            if (node == null)
            {
                return null;
            }
            return node.InnerText;
        }
        /// <summary>
        ///   保存XML到文件
        /// </summary>
        /// <param name="Path"></param>
        public void SaveXml(string Path)
        {
            this.xmldoc.Save(Path);
        }
        /// <summary>
        /// 查找xml文件
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public bool SearchXml(string Path)
        {
            return File.Exists(Path);
        }
        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool SelectNode(string Name)
        {
            XmlElement documentElement = this.xmldoc.DocumentElement;
            XmlNode node = null;
            if (documentElement.HasChildNodes)
            {
                node = documentElement.SelectSingleNode(Name);
            }
            if (node == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void UpdateNode(string Name, string Value)
        {
            XmlNode node = this.xmldoc.DocumentElement.SelectSingleNode(Name);
            if (node != null)
            {
                node.InnerText = Value;
            }
        }
    }
}

