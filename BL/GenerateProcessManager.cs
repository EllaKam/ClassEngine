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
        public  event Action<string> Status;
        #endregion

        #region Construction
        private GenerateProcessManager()
        {
            Classes = new Dictionary<string, Basic>();
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
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(pathXmlClassData);
            foreach (XmlNode xmlNode in xmlDoc.FirstChild.ChildNodes)
            {
                Classes.Add(xmlNode.Name, new Basic(xmlNode.Name, xmlNode));
            }
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
