using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommandLine;

namespace Shell
{
  [Serializable]
  [XmlRoot("Command")]
  public class Command
  {
    [XmlElement("Name")]
    [Option('n', "Name",
            Required = true,           
            HelpText = "Command Name")]
    public string Name { get; set; }
    [XmlElement("Path")]
    [Option('p', "Path",
            Required = true,
            HelpText = "Command Path")]
    public string Path { get; set; }
    [XmlElement("Dettached")]
    [Option('d', "dettached",
            Default = false,
            HelpText = "Execute Dettached")]
    public bool Dettached { get; set; }
  }
}
