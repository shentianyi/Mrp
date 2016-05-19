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

        [Column("origin")]
        public string origin { get; set; }


        [Column("state")]
        public string state { get; set; }

        [Column("partner_id")]
        public string partner_nr { get; set; }

        [Column("date_order")]
        public DateTime date_order { get; set; }


        public static Order InitWithXmlStruct(XmlRpcStruct xml)
        {
            Order order = new Order();
            order.id = int.Parse(xml["id"].ToString()); 
            order.state = xml["state"].ToString();

            order.origin = xml["origin"].ToString();

            order.partner_id = int.Parse((xml["partner_id"] as object[])[0].ToString());
            order.partner_nr = (xml["partner_id"] as object[])[1].ToString();
             

            return order;
        }

        public XmlRpcStruct ConvertToXml() {
            XmlRpcStruct xml = new XmlRpcStruct();
            xml.Add("partner_id", this.partner_id.ToString());
            xml.Add("date_order",this.date_order.ToString());
            xml.Add("origin", this.origin);
            return xml;
        }

        public XmlRpcStruct GetCancelXml() {
            XmlRpcStruct xml = new XmlRpcStruct();
            xml.Add("state", "cancel");
            
            return xml;
        }
    }
}
