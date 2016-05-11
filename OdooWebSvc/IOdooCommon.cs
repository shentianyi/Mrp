using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc
{
    public interface IOdooCommon : IXmlRpcProxy
    {
        [XmlRpcMethod("authenticate")]
        int authenticate(string database, string username, string password,Array dummy);

        [XmlRpcMethod("version")]
        XmlRpcStruct version();
    }
}
