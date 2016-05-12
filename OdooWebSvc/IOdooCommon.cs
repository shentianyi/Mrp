using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc
{
    public interface IOdooCommon : IXmlRpcProxy
    {
        [XmlRpcMethod("version")]
        XmlRpcStruct version();

        [XmlRpcMethod("authenticate")]
        int authenticate(string database, string username, string password,Array dummy);
    }
}
