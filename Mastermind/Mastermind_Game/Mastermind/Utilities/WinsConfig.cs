using System;
using System.IO;
using System.Xml;

namespace Mastermind
{
    class WinsConfig
    {
        private XmlDocument xmldoc; //XMLDocument
        private FileInfo flInfo; //"Wins.xml"
        private FileInfo flInfoDirectory; //"Configs"
        private ulong wins;

        public WinsConfig()
        {
            xmldoc = new XmlDocument();

            LoadXml();

            wins = GetCurrentWins();

            //Handle properties file
            flInfo = new FileInfo(@"Configs/Wins.xml");
            flInfoDirectory = new FileInfo(@"Configs");
            flInfoDirectory.Attributes = FileAttributes.Hidden;
            flInfo.Attributes = FileAttributes.ReadOnly;
        }

        private void CreateXmlDocument()
        {
            //<Wins>"n wins"</Wins>

            XmlNode winNode = xmldoc.CreateElement("Wins");
            xmldoc.AppendChild(winNode);

            winNode.InnerText = "0";

            SaveXml();
        }

        public void AddWin()
        {
            ++wins;
            WriteNewWinCounter();
        }

        private ulong GetCurrentWins()
        {
            XmlNode winNode = xmldoc.FirstChild;
            return ulong.Parse(winNode.InnerText.ToString());
        }

        private void WriteNewWinCounter()
        {
            flInfo.Attributes = FileAttributes.Normal; //Enable write

            XmlNode winNode = xmldoc.FirstChild;

            winNode.InnerText = wins.ToString();
            SaveXml();

            flInfo.Attributes = FileAttributes.ReadOnly; //Disable write
        }

        public ulong GetWins()
        {
            return wins;
        }

        private void LoadXml()
        {
            try
            {
                xmldoc.Load(@"Configs/Wins.xml");
            }
            catch (FileNotFoundException)
            {
                CreateXmlDocument();
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }
        }

        private void SaveXml()
        {
            try
            {
                xmldoc.Save(@"Configs/Wins.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine("XMLException: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }
        }

    }
}
