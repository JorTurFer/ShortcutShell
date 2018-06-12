using System;
using System.Collections.Generic;
using System.Linq;

namespace Shell
{
  public static class CommandMgr
  {
    /// <summary>
    /// Path of the XML file
    /// </summary>
    const string XMLPATH = "./Commands.xml";
    /// <summary>
    /// Command collection
    /// </summary>
    static List<Item> m_lstItems = XML.Deserialize(XMLPATH);
    /// <summary>
    /// Add command to the list
    /// </summary>
    /// <param name="strName">Command name</param>
    /// <param name="strPath">Execution path</param>
    public static void AddCommand(string strName, string strPath)
    {      
      Item newItem = new Item();
      newItem.Name = strName;
      newItem.Path = strPath;
      m_lstItems.Add(newItem);
      XML.Serialization(m_lstItems,XMLPATH);
    }
    /// <summary>
    /// Remove command from the list
    /// </summary>
    /// <param name="strName">Comand name</param>
    public static void RemoveCommand(string strName)
    {
      Item newItem = m_lstItems.Where(x=>string.Equals(x.Name,strName,StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
      if (newItem != null)
      {
        m_lstItems.Remove(newItem);
        XML.Serialization(m_lstItems, XMLPATH);
      }
    }
    /// <summary>
    /// Get the command list
    /// </summary>
    /// <returns></returns>
    public static List<Item> GetCommands()
    {
      return m_lstItems;
    }
    /// <summary>
    /// Get the command path by command name
    /// </summary>
    /// <param name="strName">Command name</param>
    /// <returns></returns>
    public static string GetCommandPathByName(string strName)
    {
      return m_lstItems.Where(x => string.Equals(x.Name, strName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Path;
    }
    /// <summary>
    /// Get the command name by path
    /// </summary>
    /// <param name="strPath">Execution path</param>
    /// <returns></returns>
    public static string GetCommandNameByPath(string strPath)
    {
      return m_lstItems.Where(x => string.Equals(x.Path, strPath, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()?.Name;
    }
  }
}
