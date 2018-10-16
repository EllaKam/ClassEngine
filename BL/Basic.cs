using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
    public class Basic
    {
        #region Members
        public Dictionary<string, FieldInfo> Parameters { get; set; }
        public Dictionary<string, Basic> ComplexParameters { get; set; }
        private XmlNode XmlRules;
        private string className;
        #endregion
        #region Construction
        public Basic(String name)
        {
            className = name;
            Parameters = new Dictionary<string, FieldInfo>();
            ComplexParameters = new Dictionary<string, Basic>();
            XmlDocument xmlDoc = GenerateProcessManager.GetManager().DataXMLDocument;
            XmlNode xmlSimpe = GenerateProcessManager.GetManager().GetNodeByClass(name);
            foreach (XmlNode innerNode in xmlSimpe.ChildNodes)
            {
                var node = xmlDoc.SelectSingleNode($"//{ innerNode.InnerText}");
                if (node == null)
                {
                    Parameters.Add(innerNode.Name, new FieldInfo(className, innerNode.Name, innerNode.InnerText));
                }
                else
                {
                    Basic basic =  GenerateProcessManager.GetManager().GetBasicClass(node);
                    ComplexParameters.Add(node.Name, basic);
                }
            }
        }
        #endregion

        #region Public Methods
        public Basic BasicCreater()
        {
            Basic basic = new Basic(className);
            basic.CreateRules(this.XmlRules);
            return basic;
        }
        public Basic BasicCreaterWithInitialization()
        {
            Basic basic = new Basic(className);

            foreach ( Basic basicItem in basic.ComplexParameters.Values)
            {
                basicItem.Initialization();
            }
            foreach (var param in basic.Parameters.Values)
            {
                param.Initialization();
            }
            return basic;
        }

        public void Initialization()
        {
            foreach (var param in Parameters.Values)
            {
                param.Initialization();
            }
        }

        public void CreatedXMLFile(string filePath)
        {
          
            XmlDocument finalDocument = NodeParse();
            try
            {
                finalDocument.Save(filePath); ;
            }
            catch (Exception e)
            {
                GenerateProcessManager.GetManager().SendStatus($"File {filePath} saving problem");
            }
        }

    
        public void CreateRules(XmlNode rule)
        {
            XmlRules = rule;
            if (XmlRules == null)
            {
                return;
            }
            foreach (XmlNode innerNode in XmlRules.ChildNodes)
            {
                FieldInfo param = Parameters[innerNode.Name];
                param.CreateOperationInfo(innerNode.InnerText);
            }
        }
        public XmlDocument NodeParse()
        {
            XmlNode xmlProperties = GenerateProcessManager.GetManager().GetNodeByClass(className);
            XmlDocument finalDocument = new XmlDocument();
            finalDocument.LoadXml(xmlProperties.OuterXml);

            foreach (XmlNode xmlNode in xmlProperties.ChildNodes)
            {
                XmlNode innerNode = finalDocument.SelectSingleNode($"//{xmlNode.Name}");
                if (innerNode != null)
                {
                    FieldInfo fieldInfo = null;
                    if (Parameters.TryGetValue(xmlNode.Name, out fieldInfo))
                    {
                        innerNode.InnerText = fieldInfo.Data.ToString();
                    }
                    else if (ComplexParameters.ContainsKey(xmlNode.InnerText))
                    {
                        Basic b = null;
                        if (ComplexParameters.TryGetValue(xmlNode.InnerText, out b))
                        {
                            XmlDocument partional = b.NodeParse();
                            innerNode.InnerText = "";
                            innerNode.AppendChild(finalDocument.ImportNode(partional.DocumentElement, true));
                        }

                    }
                }
            }
            return finalDocument;
        }

        #endregion

        #region PrivateMethods


        #endregion

    }
}
