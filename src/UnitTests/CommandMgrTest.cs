using System.IO;
using Shell;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
  [TestClass, TestCategory("CommandMgrTest")]
  public class CommandMgrTest
  {
    [TestMethod, TestCategory("CommandMgrTest")]
    public void AddTest()
    {
      //Clean the files 
      if (File.Exists(XMLTest.XMLPATH))
        File.Delete(XMLTest.XMLPATH);

      var lstCommands = CommandMgr.GetCommands();
      Assert.AreEqual(0, lstCommands.Count, "Error cleaning the CommandMgr");
      CommandMgr.AddCommand("test1", "test1.exe");
      CommandMgr.AddCommand("test2", "test2.exe");
      Assert.AreEqual(2, CommandMgr.GetCommands().Count, "Error adding commands");
    }
    [TestMethod, TestCategory("CommandMgrTest")]
    public void PathByNameTest()
    {
      Assert.AreEqual("test1.exe", CommandMgr.GetCommandPathByName("test1"), "Error, getting the path");
      Assert.AreEqual("test2.exe", CommandMgr.GetCommandPathByName("test2"), "Error, getting the path");
    }
    [TestMethod, TestCategory("CommandMgrTest")]
    public void NameByPathTest()
    {
      Assert.AreEqual("test1", CommandMgr.GetCommandNameByPath("test1.exe"), "Error, getting the name");
      Assert.AreEqual("test2", CommandMgr.GetCommandNameByPath("test2.exe"), "Error, getting the name");
    }
    [TestMethod, TestCategory("CommandMgrTest")]
    public void RemoveTest()
    {
      var lstCommands = CommandMgr.GetCommands();
      Assert.AreEqual(2, lstCommands.Count, "Error, cleaned CommandMgr");
      CommandMgr.RemoveCommand("test1");
      CommandMgr.RemoveCommand("test2");
      Assert.AreEqual(0, CommandMgr.GetCommands().Count, "Error removing commands");
    }
  }
}
