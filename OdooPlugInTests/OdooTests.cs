using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdooPlugIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;

namespace OdooPlugIn.Tests
{

    [TestClass()]
    public class OdooTests
    {
        // string dbConnectStr = "Data Source=VM08;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=brilliantech123@";
        string dbConnectStr = @"Data Source=Charlot-PC\SQLEXPRESS;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=123456@";

        [TestMethod()]
        public void CreateBomByPartsTest()
        {
            Odoo o = new Odoo();
            List<string> partNrs = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                partNrs.Add("P" + i);
            }
            ProcessData pd = new ProcessData();
            pd.Data["db"] = dbConnectStr;
            pd.Data["parts"] = partNrs;

         ProcessResult result=   o.CreateBomByParts(pd);
            Assert.AreEqual(result.ResultCode, 1);
        }
    }
}