using OdooPlugIn.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace OdooPlugIn.Model.Purchase
{
    [Table("purchase.order")]
    public class Order
    {
        [Column("id")]
        public int id { get; set; }

        [Column("partner_id")]
        public int partner_id { get; set; }

        [Column("partner_id")]
        public string partner_nr { get; set; }

        [Column("date_order")]
        public DateTime date_order { get; set; }

        public XmlRpcStruct ConvertToXml() {
            XmlRpcStruct xml = new XmlRpcStruct();
            xml.Add("partner_id", this.partner_id.ToString());
            xml.Add("date_order",this.date_order.ToString());
            return xml;
        }
    }
}
