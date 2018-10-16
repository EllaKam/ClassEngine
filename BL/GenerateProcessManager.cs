using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
    public class GenerateProcessManager
    {
        #region Members
        private static readonly object lockOb = new object();
        private static GenerateProcessManager instance;
        public Dictionary<string, Basic> Classes { get; set; }

        public Dictionary<string, XmlNode> ClassesXML { get; set; }

        public  event Action<string> Status;
        public XmlDocument DataXMLDocument { get; private set; }
        #endregion

        #region Construction
        private GenerateProcessManager()
        {
            Classes = new Dictionary<string, Basic>();
            ClassesXML = new Dictionary<string, XmlNode>();
        }
        public static GenerateProcessManager GetManager()
        {
            if (instance == null)
            {
                lock (lockOb)
                {
                    if (instance == null)
                    {
                        instance = new GenerateProcessManager();
                    }
                }
            }
            return instance;
        }
        #endregion

        #region Public Methods
        public void ClassesCreater(int count, string directoryPath)
        {
            InitDirectory(directoryPath);
            SendStatus($"Directory {directoryPath} initialisation" );
            int current = 0;
            for (int i = 0; i < count; i++)
            {
                foreach (var basicClass in Classes)
                {
                    Basic basicRes = basicClass.Value.BasicCreaterWithInitialization();
                    string filePath = GetFilePath(directoryPath, basicClass.Key);
                    basicRes.CreatedXMLFile(filePath);
                    SendStatus($"Class {basicClass.Key} created");
                    ++current;
                }
            }
            SendStatus($"{current} clases created" );
        }

       
        public void LoadClassData(string pathXmlClassData)
        {
            DataXMLDocument = new XmlDocument();
            DataXMLDocument.LoadXml(pathXmlClassData);
             
            foreach (XmlNode xmlNode in DataXMLDocument.FirstChild.ChildNodes)
            {
                if (!ClassesXML.ContainsKey(xmlNode.Name))
                {
                    ClassesXML.Add(xmlNode.Name, xmlNode);
                }
                if (!Classes.ContainsKey(xmlNode.Name))
                {
                    Classes.Add(xmlNode.Name, new Basic(xmlNode.Name));
                }
            }
        }

        public XmlNode GetNodeByClass(string className)
        {
            XmlNode node = null;
            ClassesXML.TryGetValue(className, out node);
            return node;
        }

        public Basic GetBasicClass(XmlNode node)
        {
            Basic simple = null;
            if (!Classes.TryGetValue(node.Name, out simple))
            {
                Classes.Add(node.Name, new Basic(node.Name));
            }
            return simple;
        }
       
        public void LoadRules(string pathXmlRules)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(pathXmlRules);

            foreach (XmlNode xmlNode in xmlDoc.FirstChild.ChildNodes)
            {
                Basic basic = Classes[xmlNode.Name];
                basic.CreateRules(xmlNode);
            }
        }

        
        private void InitDirectory(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                string problemInfo = $"Directory problem {directoryPath}";
                SendStatus(problemInfo);
                throw new Exception(problemInfo, e);
            }
        }
        public void SendStatus(string text)
        {
            if (Status != null)
            {
                Status(text);
            }
        }
        #endregion

        #region Private Methods
        private string GetFilePath(string directoryPath, string fileName)
        {
            return Path.Combine(directoryPath, $"{fileName}_{DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss_fff")}.xml");
        }

        #endregion
    }
}
