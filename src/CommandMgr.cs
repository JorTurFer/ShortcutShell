using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
  public static class CommandMgr
  {
    const string XMLPATH = "./Commands.xml";

    static List<Item> m_lstItems = XML.Deserialize(XMLPATH);

    public static void AddCommand(string strName, string strPath)
    {      
      Item newItem = new Item();
      newItem.Name = strName;
      newItem.Path = strPath;
      m_lstItems.Add(newItem);
      XML.Serialization(m_lstItems,XMLPATH);
    }
    public static void RemoveCommand(string strName)
    {
      Item newItem = m_lstItems.Where(x=>string.Equals(x.Name,strName,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
      if (newItem != null)
      {
        m_lstItems.Remove(newItem);
        XML.Serialization(m_lstItems, XMLPATH);
      }
    }

    public static List<Item> GetCommandNames()
    {
      return m_lstItems;
    }

    public static string GetCommandPath(string strName)
    {
      return m_lstItems.Where(x => string.Equals(x.Name, strName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Path;
    }
  }
}
