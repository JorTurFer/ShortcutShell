using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shell
{
  [Serializable]
  [XmlRoot("Item")]
  public class Item
  {
    [XmlElement("Name")]
    public string Name { get; set; }
    [XmlElement("Path")]
    public string Path { get; set; }
  }
}
