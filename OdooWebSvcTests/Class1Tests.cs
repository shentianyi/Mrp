﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdooWebSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc.Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void TTest()
        {
            string s = "1";
            Class1 c = new Class1();
            c.T();
            Assert.Fail();
        }
    }
}