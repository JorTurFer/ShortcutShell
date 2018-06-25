using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Shell
{
  public static class CommandMgr
  {
    /// <summary>
    /// Path of the XML file
    /// </summary>
    public static string strXmlPath { get; private set; }
    /// <summary>
    /// Command collection
    /// </summary>
    static List<Command> m_lstItems;
    /// <summary>
    /// Load the commands from XML file
    /// </summary>
    /// <param name="strPath">Path of XML</param>
    public static void Load(string strPath)
    {
      strXmlPath = strPath;
      m_lstItems = XML.Deserialize(strXmlPath);
    }
    /// <summary>
    /// Add command to the list
    /// </summary>
    /// <param name="strName">Command name</param>
    /// <param name="strPath">Execution path</param>
    public static void AddCommand(Command command)
    {
      m_lstItems.Add(command);
      XML.Serialization(m_lstItems, strXmlPath);
    }
    /// <summary>
    /// Remove command from the list
    /// </summary>
    /// <param name="strName">Comand name</param>
    public static void RemoveCommand(Command command)
    {
      Command newItem = m_lstItems.Where(x => string.Equals(x.Name, command.Name, StringComparison.InvariantCultureIgnoreCase)).First();
      m_lstItems.Remove(newItem);
      XML.Serialization(m_lstItems, strXmlPath);
    }
    /// <summary>
    /// Get the command list
    /// </summary>
    /// <returns></returns>
    public static List<Command> GetCommands()
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
    public static Command GetCommandByName(string strName)
    {
      return m_lstItems.Where(x => string.Equals(x.Name, strName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
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
    /// <summary>
    /// Check if the command exists previously
    /// </summary>
    /// <param name="strName">Command Name</param>
    /// <returns></returns>
    public static bool Exists(Command command)
    {
      return m_lstItems.Exists(x => x.Name == command.Name);
    }
  }
}
