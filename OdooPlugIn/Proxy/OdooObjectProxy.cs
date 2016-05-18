using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CookComputing.XmlRpc;
using OdooPlugIn.Attributes;
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

        public int Create<T>(string db, int uid, string pwd, T obj)
        {
            int id = 0;
            Type type = typeof(T);
            string modelName = GetModelName<T>(obj);
            MethodInfo mi = type.GetMethod("ConvertToXml");
            XmlRpcStruct xml = mi.Invoke(obj,null) as XmlRpcStruct;
            id = this.odooObject.Create(db, uid, pwd, modelName, "create",new XmlRpcStruct[1] { xml });

            return id;
        }

        public void Creates<T>(string db, int uid, string pwd, List<T> obj)
        {
            Type type = typeof(T);
            string modelName = GetModelName<T>(obj.First());
            MethodInfo mi = type.GetMethod("ConvertToXmls");
            XmlRpcStruct[] xmls = mi.Invoke(null,new object[1] { obj}) as XmlRpcStruct[];
            this.odooObject.Creates(db, uid, pwd, modelName, "create", new XmlRpcStruct[1][] { xmls });
        }

        public string GetModelName<T>(T obj) {
            Type type = typeof(T);
            object tableAttr = type.GetCustomAttributes(
                                       typeof(TableAttribute), false)
                                       .FirstOrDefault();
            string modelName = string.Empty;
            if (tableAttr != null)
            {
                modelName = (tableAttr as TableAttribute).Name;
            }
            return modelName;
        }
    }
}
