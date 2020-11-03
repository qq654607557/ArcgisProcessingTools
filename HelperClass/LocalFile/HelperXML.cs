using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HelperClass.LocalFile
{
    public class HelperXML
    {
        #region 对节点操作
        /// <summary>
                /// 增加一个新节点
                /// </summary>
                /// <param name="filePath">xml文件名</param>
                /// <param name="xPath"></param>
                /// <param name="xmlNode">新增加的节点</param>
                /// <returns></returns>
        public static bool AppendChild(string filePath, string xPath, XmlNode xmlNode)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);//从xPath路径中加载一个xml文件
                XmlNode xn = xmldoc.SelectSingleNode(xPath);//查找匹配第一个xml节点
                XmlNode n = xmldoc.ImportNode(xmlNode, true);//把节点导入新节点
                xn.AppendChild(n);
                xmldoc.Save(filePath);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        /// <summary>
                /// 删除指定节点下所有的子节点
                /// </summary>
                /// <param name="filePath"></param>
                /// <param name="xPath"></param>
                /// <returns></returns>
        public static bool DeleteAllChild(string filePath, string xPath)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                XmlElement root = xmldoc.DocumentElement;
                xmldoc.Load(filePath);//从xPath路径中加载一个xml文件
                XmlNode xn = xmldoc.SelectSingleNode(xPath);//查找匹配第一个xml节点
                xn.RemoveAll();
                xmldoc.Save(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
                /// 移除指定节点的子节点 
                /// </summary>           
                /// <param name="filePath">xml文件名</param>
                /// <param name="xPath">被删除节点的父节点xPath路径</param>
                /// <param name="xPathChi">被删除节点的的xPath路径</param>
                /// <returns></returns> 
        public static bool DeleteChild(string filePath, string xPath, string xPathChi)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                XmlElement root = xmldoc.DocumentElement;
                xmldoc.Load(filePath);//从xPath路径中加载一个xml文件
                XmlNode xn = xmldoc.SelectSingleNode(xPath);//查找匹配第一个xml节点
                XmlNode n = xmldoc.SelectSingleNode(xPathChi);//查找匹配xml子节点
                xn.RemoveChild(n);
                xmldoc.Save(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        #endregion

        /// <summary>
                /// 获取指定路径节点中所有子节点的值
                /// </summary>
                /// <param name="filePath"></param>
                /// <param name="xPath"></param>
                /// <returns></returns>
        public static List<string> GetListValues(string filePath, string xPath)
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                XmlElement newNode = (XmlElement)xn;//要读取的节点转换为元素                      
                foreach (XmlNode tempNode in newNode)
                {
                    XmlNode Node = tempNode.ChildNodes[0];
                    string nodeName = Node.InnerText;//取子节点的值
                    string u = nodeName;
                    list.Add(u);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }


        /// <summary>
               /// 获取指定节点的指定属性
               /// </summary>
               /// <param name="filePath"></param>
               /// <param name="xPath"></param>
               /// <param name="attName">属性集合</param>
               /// <returns></returns>
        public static List<string> GetListAttribute(string filePath, string xPath, params string[] attName)
        {
            List<string> list = new List<string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                XmlElement newNode = (XmlElement)xn;//要读取的节点转换为元素                   
                for (int i = 0; i < attName.Length; i++)
                {
                    string stratt = newNode.GetAttribute(attName[i]);
                    list.Add(stratt);
                }
            }
            catch (Exception)
            {

                return null;
            }
            return list;
        }


        /// <summary>
               /// 给节点增加(修改)属性
               /// </summary>
               /// <param name="filePath"></param>
               /// <param name="xPath"></param>
               /// <param name="attName">string</param>
               /// <param name="attValue">string</param>
               /// <returns></returns>
        public static bool UpdateAttribute(string filePath, string xPath, string attName, string attValue)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化xml文件
                xmldoc.Load(filePath);//根据路径加载xml文件
                XmlNode xn = xmldoc.SelectSingleNode(xPath);//查找一个xml节点         
                XmlAttribute xa = xmldoc.CreateAttribute(attName);
                xa.InnerText = attValue;
                xn.Attributes.Append(xa);
                xmldoc.Save(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
               /// 删除指定名称的属性
               /// </summary>
               /// <param name="filePath"></param>
               /// <param name="attName">节点名称</param>
               /// <returns></returns>
        public static bool DeleteAttribute(string filePath, string attName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlElement root = doc.DocumentElement;//获取根节点
                XmlNodeList nodelist = root.ChildNodes;//获取所有节点列表
                XmlNode node = null;
                foreach (XmlNode tempNode in nodelist)
                {
                    if (tempNode.NodeType == XmlNodeType.Element)
                    {
                        if (tempNode.ChildNodes[0].InnerText == attName)
                        {
                            node = tempNode;
                            break;
                        }
                    }
                }
                if (node != null)
                {
                    root.RemoveChild(node);
                    doc.Save(filePath);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
               /// 更新(增加 修改)节点内容
               /// </summary>
               /// <param name="filePath"></param>
               /// <param name="xPath"></param>
               /// <param name="value"></param>
               /// <returns></returns>
        public static bool UpateNodeInnerText(string filePath, string xPath, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode(xPath);//根据路径选择节点
                XmlElement xe = (XmlElement)xn;
                xe.InnerText = value;
                doc.Save(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public static XmlDocument OpenXML(string filePath)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();//实例化一个xml文件类
                xmldoc.Load(filePath);
                return xmldoc;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
                /// 获取指定路径节点中所有子节点的值
                /// </summary>
                /// <param name="filePath"></param>
                /// <param name="xPath"></param>
                /// <returns></returns>
        public static string GetValue(XmlDocument xmldoc, string xPath)
        {
            try
            {
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                return xn.InnerText;
                //xn.Name;
            }
            catch
            {
                return null;
            }
        }

    }
}
