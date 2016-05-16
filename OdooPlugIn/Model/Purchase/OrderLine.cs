﻿using CookComputing.XmlRpc;
using OdooPlugIn.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model.Purchase
{
    [Table("purchase.order.line")]
    public class OrderLine
    {
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("state")]
        public string state { get; set; }


        [Column("order_id")]
        public int order_id { get; set; }

        [Column("order_id")]
        public string order_nr { get; set; }

        [Column("partner_id")]
        public int partner_id { get; set; }

        [Column("partner_id")]
        public string partner_nr { get; set; }


        [Column("product_id")]
        public int product_id { get; set; }

        [Column("product_id")]
        public string product_nr { get; set; }


        [Column("product_qty")]
        public float product_qty { get; set; }

        [Column("date_planned")]
        public DateTime date_planned { get; set; }

        [Column("product_uom")]
        public int product_uom_id { get; set; }

        [Column("product_uom")]
        public string product_uom_nr { get; set; }



        public static OrderLine InitWithXmlStruct(XmlRpcStruct xml)
        {
            OrderLine orderLine = new OrderLine();
            orderLine.id = int.Parse(xml["id"].ToString());
            orderLine.name = xml["name"].ToString();
            orderLine.state = xml["state"].ToString();

            orderLine.order_id = int.Parse((xml["order_id"] as object[])[0].ToString());
            orderLine.order_nr = (xml["order_id"] as object[])[1].ToString();


            orderLine.partner_id = int.Parse((xml["partner_id"] as object[])[0].ToString());
            orderLine.partner_nr = (xml["partner_id"] as object[])[1].ToString();

            orderLine.product_id = int.Parse((xml["product_id"] as object[])[0].ToString());
            orderLine.product_nr = (xml["product_id"] as object[])[1].ToString();

            orderLine.product_uom_id = int.Parse((xml["product_uom"] as object[])[0].ToString());
            orderLine.product_uom_nr = (xml["product_uom"] as object[])[1].ToString();


            orderLine.date_planned = DateTime.Parse(xml["date_planned"].ToString()).ToLocalTime();
            orderLine.product_qty = float.Parse(xml["product_qty"].ToString());

            return orderLine;
        }

    }
}
