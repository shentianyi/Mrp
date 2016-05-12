using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooWebSvc.Proxy
{
    public class OdooProxy
    {
        public string Url { get; set; }
        public string Db { get; set; }
        public int UserId { get; set; }
        public string Pwd { get; set; }


        public OdooProxy() { }

        public OdooProxy(string odooUrl)
        {
            this.Url = odooUrl;
        }
    }
}
