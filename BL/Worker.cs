using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
    public class Worker
    {
        #region Members
        public event Action<string> ProcessResult;
        #endregion
        public void LoadClass(string filePath)
        {
            var xmlDocData = new XmlDocument();
            xmlDocData.Load(filePath);
            ParaseXML(xmlDocData, string.Empty);
            
        }

        private void ParaseXML(XmlNode xmlParrent, string parrent)
        {
            foreach (XmlNode xmlNode in xmlParrent.FirstChild.ChildNodes)
            {
                Basic basic = GenerateProcessManager.GetManager().Classes[xmlParrent.FirstChild.Name].BasicCreater();
                Basic basicChild = null;


                FieldInfo fieldParam = null;
                if (basic.Parameters.TryGetValue(xmlNode.Name, out fieldParam))
                {
                    FieldInfo field = basic.Parameters[xmlNode.Name];
                    field.Data = field.DataConvertor(xmlNode.InnerText);
                    field.Run(ProcessResult, parrent);
                }
                else
                {
                    if (basic.ComplexParameters.TryGetValue(xmlNode.FirstChild.Name, out basicChild))
                    {
                        ParaseXML(xmlNode,$"{parrent} {xmlParrent.FirstChild.Name}");
                    }
                }

            }
        }
    }
}
