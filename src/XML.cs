using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Shell
{
  static class XML
  {
    public static List<Item> Deserialize(string a_fileName)
    {
      try
      {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Item>));
        using (TextReader reader = new StreamReader(a_fileName))
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

    public static void Serialization(List<Item> a_stations, string a_fileName)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(List<Item>));
      using (var stream = File.OpenWrite(a_fileName))
      {
        serializer.Serialize(stream, a_stations);
      }
    }
  }
}
