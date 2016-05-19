using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc
{
    public interface IOdooObject : IXmlRpcProxy
    {
        [XmlRpcMethod("execute_kw")]
        int Create(string dbname, int uid, string pwd, string modelName, string method, XmlRpcStruct[] strct);
      //  [XmlRpcMethod("execute_kw")]
     //   int Creates(string dbname, int uid, string pwd, string modelName, string method, XmlRpcStruct[][] strct);
        [XmlRpcMethod("execute_kw")]
        XmlRpcStruct[] Read(string dbname, int uid, string pwd, string modelName, string method, int[] ids, XmlRpcStruct strct);
        [XmlRpcMethod("execute_kw")]
        int[] Search(string dbname, int uid, string pwd, string modelName, string method, Object[] filters);
        [XmlRpcMethod("execute_kw")]
        bool Unlink(string dbname, int uid, string pwd, string modelName, string method, int[] ids);
        //[XmlRpcMethod("execute_kw")]
        //bool Write(string dbname, int uid, string pwd, string modelName, string method, int[] ids, XmlRpcStruct strct);
        [XmlRpcMethod("execute_kw")]
        bool Write(string dbname, int uid, string pwd, string modelName, string method, object [] ids);
        [XmlRpcMethod("execute_kw")]
        XmlRpcStruct[] SearchAndRead(string dbname, int uid, string pwd, string modelName, string method, object[] ids, XmlRpcStruct strct);
    }
}
