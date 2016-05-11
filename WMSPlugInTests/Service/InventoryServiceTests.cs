using Microsoft.VisualStudio.TestTools.UnitTesting;
using WMSPlugIn.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSPlugIn.Service.Tests
{
    [TestClass()]
    public class InventoryServiceTests
    {
        [TestMethod()]
        public void InventoryServiceTest()
        {
            string dbs = "hello db";
            InventoryService iv = new InventoryService(dbs);
            Assert.AreEqual(dbs, iv.DbConnectString);
        }
    }
}