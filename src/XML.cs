using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Shell
{
  /// <summary>
  /// XML Helper
  /// </summary>
  static class XML
  {/// <summary>
  /// Deserialize the list from the XML File
  /// </summary>
  /// <param name="strFileName">XML Path</param>
  /// <returns></returns>
    public static List<Item> Deserialize(string strFileName)
    {
      try
      {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Item>));
        using (TextReader reader = new StreamReader(strFileName))
        {
          object obj = deserializer.Deserialize(reader);
          reader.Close();
          return (List<Item>)obj;
        }
      }
      catch 
      {
        return new List<Item>();
      }
    }
    /// <summary>
    /// Serialize the List<Item> into a XML file
    /// </summary>
    /// <param name="lstItems">List of Items</param>
    /// <param name="strFileName">XML File path</param>
    public static void Serialization(List<Item> lstItems, string strFileName)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
      using (var stream = File.OpenWrite(strFileName))
      {
        serializer.Serialize(stream, lstItems);
      }
    }
  }
}
