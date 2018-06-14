using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shell
{
  [Serializable]
  [XmlRoot("Command")]
  public class Command
  {
    static Regex regPath = new Regex("-p {0,}\"(.[^\"]+)\"");
    static Regex regDettached = new Regex("-d {0,}\"(.[^\"]+)\"");

    public Command()
    {
    }
    public Command(string str)
    {
      Name = str.Split(' ')[0];
      //Find path
      var matchResult = regPath.Match(str);
      if (matchResult.Success)
      {
        Path = matchResult.Groups[1].Value;
      }
      //Find dettach
      matchResult = regDettached.Match(str);
      bool bDettached = false;
      if (matchResult.Success)
      {
        bool.TryParse(matchResult.Groups[1].Value, out bDettached);
      }
      Dettached = bDettached;
    }
    [XmlElement("Name")]
    public string Name { get; set; }
    [XmlElement("Path")]
    public string Path { get; set; }
    [XmlElement("Dettached")]
    public bool Dettached { get; set; }
  }
}
