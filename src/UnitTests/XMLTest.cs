using System.Collections.Generic;
using Shell;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
  [TestClass]
  public class XMLTest
  {
    public TestContext TestContext { get; set; }
    public static readonly string XMLPATH = "./Commands.xml";
    [TestMethod]
    public void Generation()
    {
      //Generation of XML
      List<Item> lstItem = new List<Item>();
      lstItem.Add(new Item() { Name = "test1", Path = "test1.exe" });
      lstItem.Add(new Item() { Name = "test2", Path = "test2.exe" });
      XML.Serialization(lstItem, XMLPATH);
      //Read XML
      List<Item> lstRead = XML.Deserialize(XMLPATH);
      //Asserts
      Assert.AreEqual(lstItem.Count, lstRead.Count, "Error in List.Count");
      TestContext.WriteLine($"lstItem.Count {lstItem.Count}");
      TestContext.WriteLine($"lstRead.Count {lstRead.Count}");
      for (int i = 0; i < lstItem.Count; i++)
      {
        Assert.AreEqual(lstItem[i].Name, lstRead[i].Name, "Error in Name");
        Assert.AreEqual(lstItem[i].Path, lstRead[i].Path, "Error in Path");
        TestContext.WriteLine($"lstItem.Name = {lstItem[i].Name}");
        TestContext.WriteLine($"lstRead.Name = {lstRead[i].Name}");
        TestContext.WriteLine($"lstItem.Path = {lstItem[i].Path}");
        TestContext.WriteLine($"lstRead.Path = {lstRead[i].Path}");
      }
    }
  }
}
