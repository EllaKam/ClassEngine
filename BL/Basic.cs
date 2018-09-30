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

        private XmlNode xmlProperties;
        private XmlNode XmlRules;
        private string className;
        #endregion
        #region Construction
        public Basic(String name, XmlNode xmlSimpe)
        {
            xmlProperties = xmlSimpe;
            className = name;
            Parameters = new Dictionary<string, FieldInfo>();
            foreach (XmlNode innerNode in xmlSimpe.ChildNodes)
            {
                Parameters.Add(innerNode.Name, new FieldInfo(className, innerNode.Name, innerNode.InnerText));
            }
        }
        #endregion

        #region Public Methods
        public Basic BasicCreater()
        {
            Basic basic = new Basic(className, xmlProperties);
            basic.CreateRules(this.XmlRules);
            return basic;
        }
        public Basic BasicCreaterWithInitialization()
        {
            Basic basic = new Basic(className, xmlProperties);
            foreach (var param in basic.Parameters.Values)
            {
                param.Initialization();
            }
            return basic;
        }
        public void CreatedXMLFile(string filePath)
        {
            XmlDocument finalDocument = new XmlDocument();
            finalDocument.LoadXml(xmlProperties.OuterXml);
            foreach (XmlNode xmlNode in xmlProperties.ChildNodes)
            {
                XmlNode innerNode = finalDocument.SelectSingleNode($"//{xmlNode.Name}");
                if (innerNode != null)
                {
                    innerNode.InnerText = Parameters[xmlNode.Name].Data.ToString();
                }
            }
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
        #endregion

    }
}
