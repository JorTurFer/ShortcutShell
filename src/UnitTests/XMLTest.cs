using System.Collections.Generic;
using Shell;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
  [TestClass]
  public class XMLTest
  {
    const string XMLPATH = "./Commands.xml";
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
      Assert.AreEqual(lstItem.Count, lstRead.Count,"Error in List.Count");
      for (int i = 0; i < lstItem.Count; i++)
      {
        Assert.AreEqual(lstItem[i].Name, lstRead[i].Name,"Error in Name");
        Assert.AreEqual(lstItem[i].Path, lstRead[i].Path,"Error in Path");
      }

    }
  }
}
