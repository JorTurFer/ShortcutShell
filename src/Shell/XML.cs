using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Shell
{
  /// <summary>
  /// XML Helper
  /// </summary>
  public static class XML
  {/// <summary>
  /// Deserialize the list from the XML File
  /// </summary>
  /// <param name="strFileName">XML Path</param>
  /// <returns></returns>
    public static List<Command> Deserialize(string strFileName)
    {
      try
      {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Command>));
        using (TextReader reader = new StreamReader(strFileName))
        {
          object obj = deserializer.Deserialize(reader);
          reader.Close();
          return (List<Command>)obj;
        }
      }
      catch 
      {
        return new List<Command>();
      }
    }
    /// <summary>
    /// Serialize the List<Item> into a XML file
    /// </summary>
    /// <param name="lstItems">List of Items</param>
    /// <param name="strFileName">XML File path</param>
    public static void Serialization(List<Command> lstItems, string strFileName)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(List<Command>));
      using (var stream = File.OpenWrite(strFileName))
      {
        serializer.Serialize(stream, lstItems);
      }
    }
  }
}
