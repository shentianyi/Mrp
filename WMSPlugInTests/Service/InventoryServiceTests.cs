using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMSPlugIn.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KskPlugInSharedObject;

namespace WMSPlugIn.Service.Tests
{
    [TestClass()]
    public class InventoryServiceTests
    {
        //string dbConnectStr= "Data Source=VM08;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=brilliantech123@";
        string dbConnectStr = "Data Source=WANGSONG-PC;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=wangsong";

        [TestMethod()]
        public void InventoryServiceTest()
        {
            string dbs = "hello db";
            InventoryService iv = new InventoryService(dbs);
            Assert.AreEqual(dbs, iv.DbConnectString);
        }

        [TestMethod()]
        public void GetAndCreatePartsInventoryTest()
        {
            IInventoryService iv = new InventoryService(dbConnectStr);
            List<string> partNrs = new List<string>();
            for (int i = 0; i < 100; i++) {
                partNrs.Add("P"+i);
            }
            ProcessResult result= iv.GetAndCreatePartsInventory(partNrs);
            Assert.AreEqual(result.ResultCode, 1);
        }
    }
}