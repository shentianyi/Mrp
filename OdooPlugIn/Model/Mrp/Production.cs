using CookComputing.XmlRpc;
using OdooPlugIn.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OdooPlugIn.Model.Mrp
{
    [Table("mrp.production")]
    public class Production
    {
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("product_tmpl_id")]
        public int product_tmpl_id { get; set; }

        [Column("product_tmpl_id")]
        public string product_nr { get; set; }
        
        [Column("product_uom")]
        public int product_uom_id { get; set; }

        [Column("product_uom")]
        public string product_uom_nr { get; set; }

        [Column("state")]
        public string state { get; set; }

        [Column("date_planned")]
        public DateTime date_planned { get; set; }

        [Column("product_qty")]
        public float product_qty { get; set; }

        [Column("bom_id")]
        public int bom_id { get; set; }

        [Column("bom_id")]
        public string bom_code { get; set; }



        public static Production InitWithXmlStruct(XmlRpcStruct xml)
        {
            Production production = new Production();
            production.id = int.Parse(xml["id"].ToString());
            production.name = xml["name"].ToString();
            production.product_tmpl_id = int.Parse((xml["product_tmpl_id"] as object[])[0].ToString());
            production.product_nr = (xml["product_tmpl_id"] as object[])[1].ToString();
            production.product_uom_id = int.Parse((xml["product_uom"] as object[])[0].ToString());
            production.product_uom_nr = (xml["product_uom"] as object[])[1].ToString();
            production.state = xml["state"].ToString();
            production.date_planned = DateTime.Parse(xml["date_planned"].ToString()).ToLocalTime();
            production.product_qty = float.Parse(xml["product_qty"].ToString());

            production.bom_id = int.Parse((xml["bom_id"] as object[])[0].ToString());
            // like [P2-BOM] P2
            string combainedCode = (xml["bom_id"] as object[])[1].ToString();
            Regex r = new Regex(@"\[(.*)\]");
            Match m = r.Match(combainedCode);
            if (m.Success) {
                production.bom_code = m.Value.ToString().TrimEnd(']').TrimStart('[');
            }
            return production;
        }
    }
}
