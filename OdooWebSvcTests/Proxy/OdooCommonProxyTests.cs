using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdooWebSvc.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc.Proxy.Tests
{
    [TestClass()]
    public class OdooCommonProxyTests
    {
        string url = "http://42.121.111.38:8000";
        [TestMethod()]
        public void OdooCommonProxyTest()
        {
            OdooCommonProxy proxy = new OdooCommonProxy(url);

            string uurl = proxy.Url;
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            string s = "Odoo 权限验证失败，请检查服务器、用户名、密码等配置";
            string es = string.Empty;

            OdooCommonProxy proxy = new OdooCommonProxy(url);
            int userId = proxy.Authenticate("Junnuo", "admin", "123456@");
            try
            {
                int userId2 = proxy.Authenticate("Junnuo", "admin", "123456@1");
            }
            catch (Exception e)
            {
                es = e.Message;
            }

            Assert.AreEqual(s, es);
        }
    }
}