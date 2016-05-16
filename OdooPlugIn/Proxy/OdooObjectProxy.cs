using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public List<T> SearchAndRead<T>(string db, int uid, string pwd, string table, string model, object[] filters)
        {
            object[] f = new object[0];
            if (filters.Length > 0)
            {
                f = new object[1] { filters };
            }
            XmlRpcStruct[] xmls = this.odooObject.SearchAndRead(db, uid, pwd, table, "search_read", f, new XmlRpcStruct());
            List<T> results = new List<T>();
            if (xmls.Length > 0)
            {
                foreach (XmlRpcStruct xml in xmls)
                {
                    Type t = Type.GetType(model);
                    T m = (T)t.InvokeMember("InitWithXmlStruct", BindingFlags.InvokeMethod | BindingFlags.Public |
                         BindingFlags.Static, null, null, new object[1] { xml });
                    results.Add(m);
                }
            }
            return results;
        }
    }
}
