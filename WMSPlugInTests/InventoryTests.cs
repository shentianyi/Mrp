using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMSPlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KskPlugInSharedObject;

namespace WMSPlugIn.Tests
{
    [TestClass()]
    public class InventoryTests
    {
        string dbConnectStr= "Data Source=VM08;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=brilliantech123@";

        [TestMethod()]
        public void CreateInventoryTest()
        {
            Inventory iv = new Inventory();
            List<string> partNrs = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                partNrs.Add("P" + i);
            }
            ProcessData pd = new ProcessData();
            pd.Data["db"] = dbConnectStr;
            pd.Data["parts"] = partNrs;
            iv.CreateInventory(pd);

            Assert.Fail();
        }
    }
}