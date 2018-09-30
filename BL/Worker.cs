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
            foreach (XmlNode xmlNode in xmlDocData.FirstChild.ChildNodes)
            {
                Basic basic =   GenerateProcessManager.GetManager().Classes[xmlDocData.FirstChild.Name].BasicCreater();
                FieldInfo item = basic.Parameters[xmlNode.Name];
                item.Data = item.DataConverter(xmlNode.InnerText);
                item.Run(ProcessResult);
            }
        }
    }
}
