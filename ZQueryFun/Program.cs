using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using KskPlugInSharedObject;
using OdooPlugIn;
using OdooPlugIn.Model.Mrp;
using OdooWebSvc;
using ZQueryFun.MSDN;
using System.Text.RegularExpressions;

namespace ZQueryFun
{
    class Program
    {
        static void Main(string[] args)
        {
            //OdooQueryContext c = new OdooQueryContext();
            //var q1 = c.users.Where(u => u.Name.Equals("jack") && u.id.Equals(1));

            //var l = q1.ToList();
            //Console.WriteLine(l);

            //IOdooCommon common = XmlRpcProxyGen.Create<IOdooCommon>();
            //common.Url = "http://42.121.111.38:8000/xmlrpc/2/common";
            //int uid = common.authenticate("CharlotDb", "admin", "123456@", null);
            //IOdooObject obj = XmlRpcProxyGen.Create<IOdooObject>();
            //obj.Url = "http://42.121.111.38:8000/xmlrpc/2/object";
            //object[] q = new object[1];
            //q[0] = new object[1] { new object[3] { "product_tmpl_id", "=", "P1" } };

            //XmlRpcStruct[] boms = obj.SearchAndRead("CharlotDb", uid, "123456@", "mrp.bom", "search_read",q, new XmlRpcStruct());
            //string s = "";

            string url = "http://42.121.111.38:8000";
            string db = "CharlotDb";
            string userName = "admin";
            string pwd = "123456@";

            OdooQueryContext qc = new OdooQueryContext(url, db, userName, pwd);
            // List<Bom> boms = qc.boms.Where(b => b.product_nr.Equals("P1")).ToList();
           // List<Bom> boms = qc.boms.Where(b => b.product_nr.Equals("P1;P2")).ToList();
            

            //foreach (Bom b in boms) {
            //    Console.WriteLine(b.code+":"+b.product_nr);
            //}

            //List<BomLine> bomLines = qc.bomLines.Where(l => l.bom_id.Equals(1)).ToList();
            //foreach (BomLine b in bomLines)
            //{
            //    Console.WriteLine(b.bom_display + ":"+b.product_nr);
            //}

            //string dbConnectStr = @"Data Source=Charlot-PC\SQLEXPRESS;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=123456@";
            string dbConnectStr = @"Data Source=WANGSONG-PC;Initial Catalog=Mrp;Persist Security Info=True;User ID=sa;Password=wangsong";

            Odoo o = new Odoo();
            List<string> partNrs = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                partNrs.Add("P" + i);
            }
            ProcessData pd = new ProcessData();
            pd.Data["db"] = dbConnectStr;
            pd.Data["parts"] = partNrs;

            //ProcessResult result = o.CreateBomByParts(pd);

            //ProcessResult presult = o.CreateConfirmedProductionPlans(pd);

             //ProcessResult oresult = o.CreatePurchaseOrderedParts(pd);

            //ProcessResult pvresult = o.CreatePartVendorConfigs(pd);

           ProcessResult coresult = o.CreateOdooOrders(pd);

            Console.Read();
        }
    }
}
