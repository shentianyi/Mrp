using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using OdooWebSvc;

namespace OdooPlugIn.Proxy
{
    public class OdooObjectProxy : OdooProxy
    {
        
        public IOdooObject odooObject { get; set; }
        public OdooObjectProxy(string odooUrl) : base(odooUrl)
        {
            this.Url = string.Format("{0}/xmlrpc/2/object", this.Url);
            this.odooObject = XmlRpcProxyGen.Create<IOdooObject>();
            this.odooObject.Url = Url;
        }


    }
}
