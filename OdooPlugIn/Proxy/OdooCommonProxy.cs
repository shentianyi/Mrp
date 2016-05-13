using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using OdooPlugIn.Exceptions;
using OdooWebSvc;

namespace OdooPlugIn.Proxy
{
    public class OdooCommonProxy:OdooProxy
    {
        public IOdooCommon odooCommon { get; set; }
        public OdooCommonProxy(string odooUrl):base(odooUrl) {
            this.Url = string.Format("{0}/xmlrpc/2/common", this.Url);
            this.odooCommon=  XmlRpcProxyGen.Create<IOdooCommon>();
            this.odooCommon.Url = Url;
        }

        public int Authenticate(string db, string userName, string pwd)
        {
            try
            {
                int userId = this.odooCommon.authenticate(db, userName, pwd, null);
                return userId;
            }
            catch
            {
                throw new OdooAuthException();
            }
        }
    }
}