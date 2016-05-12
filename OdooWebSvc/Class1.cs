using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc
{
    public class Class1
    {
        public void T() {
            IOdooCommon oc = XmlRpcProxyGen.Create<IOdooCommon>();
            oc.Url = "http://42.121.111.38:8000/xmlrpc/2/common";
            

          var xps= oc.version();
            string s = "";
        }
    }
}
