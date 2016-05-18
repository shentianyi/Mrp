﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Proxy;
using OdooPlugIn.Model.Purchase;
using OdooPlugIn.Model.Product;

namespace OdooPlugIn
{
    public class OdooQueryContext
    {
        public static string Url { get; set; }
        public static string Db { get; set; }
        public static string UserName { get; set; }
        public static string Pwd { get; set; }

        public OdooQueryContext() { }
        public OdooQueryContext(string url, string db, string userName, string pwd)
        {
            Url = url;
            Db = db;
            UserName = userName;
            Pwd = pwd;
        }

        public OdooQueryable<Bom> boms = new OdooQueryable<Bom>();
        public OdooQueryable<BomLine> bomLines = new OdooQueryable<BomLine>();
        public OdooQueryable<Production> productions = new OdooQueryable<Production>();
        public OdooQueryable<OrderLine> orderLines = new OdooQueryable<OrderLine>();
        public OdooQueryable<PartVendor> partVendors = new OdooQueryable<PartVendor>();

        internal static object Execute(Expression expression, bool IsEnumerable)
        {
            OdooExpressionVisitor v = new OdooExpressionVisitor();
            v.Translate(expression);

            OdooCommonProxy cp = new OdooCommonProxy(Url);
            int uid = cp.Authenticate(Db, UserName, Pwd);
            OdooObjectProxy op = new OdooObjectProxy(Url);

            MethodInfo mi = typeof(OdooObjectProxy).GetMethod("SearchAndRead");
            MethodInfo mii = mi.MakeGenericMethod(Type.GetType(v.ModelName));
            return mii.Invoke(op, new object[6] { Db, uid, Pwd, v.TableName, v.ModelName, v.Filters.ToArray() });
        }

        public int Create<T>(T obj)
        {
            OdooCommonProxy cp = new OdooCommonProxy(Url);
            int uid = cp.Authenticate(Db, UserName, Pwd);
            OdooObjectProxy op = new OdooObjectProxy(Url);

            return op.Create<T>(Db, uid, Pwd, obj);
        }

    }
}
