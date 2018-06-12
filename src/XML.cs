using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shell
{
  static class XML
  {
    static void WriteXML(List<Item> list, string strPath)
    {
      XmlSerializer x = new XmlSerializer(typeof(Item));
      TextWriter writer = new StreamWriter(strPath);
      x.Serialize(writer, list);
    }

    List<Item> ReadXML(string strPath)
    {
      XmlSerializer x = new XmlSerializer(typeof(Item));
      TextReader reader = new StreamReader(strPath);

      return x.Deserialize(reader);
    }
  }
}
