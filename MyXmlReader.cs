using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace MapleLauncher_Console
{
    public class MyXmlReader
    {
        private string file;
        private Hashtable wzHash;

        public string Path
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }

        public Hashtable getHashFromXml()
        {
            wzHash = new Hashtable();
            using (XmlReader reader = XmlReader.Create(file))
            {   

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "WZ"))
                    {
                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                switch (reader.Name)
                                {
                                    case "Character":                    
                                        wzHash.Add(reader.Name.ToString(), reader.Value.ToString());        
                                        break;

                                    case "Skill":
                                        wzHash.Add(reader.Name.ToString(), reader.Value.ToString());
                                        break;  
                                }
                            }
                            reader.MoveToElement();
                        }
                    }
                }            
            }
            return wzHash;
        }
    }
}
