using OdooPlugIn.Attributes;
using OdooPlugIn.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using OdooPlugIn.Helper;

namespace OdooPlugIn.Model.Mrp
{
    [Table("mrp.bom.line")]
    public class BomLine
    {
        [Column("id")]
        public int id { get; set; }
        [Column("bom_id")]
        public int bom_id { get; set; }
        [Column("bom_id")]
        public string bom_display { get; set; }

        [Column("product_qty")]
        public float product_qty { get; set; }

        [Column("product_id")]
        public int product_id { get; set; }
        [Column("product_id")]
        public string product_nr { get; set; }



        [Column("product_uom")]
        public int product_uom_id { get; set; }

        [Column("product_uom")]
        public string product_uom_nr { get; set; }

        public static BomLine InitWithXmlStruct(XmlRpcStruct xml)
        {
            BomLine bomLine = new BomLine();
            bomLine.id = int.Parse(xml["id"].ToString());

            bomLine.bom_id = int.Parse((xml["bom_id"] as object[])[0].ToString());
            bomLine.bom_display = (xml["bom_id"] as object[])[1].ToString();
            
            bomLine.product_id = int.Parse((xml["product_id"] as object[])[0].ToString());
            bomLine.product_nr = OdooFieldValueHelper.ParsePartNr( (xml["product_id"] as object[])[1].ToString());

            bomLine.product_qty = float.Parse(xml["product_qty"].ToString());

            bomLine.product_uom_id = int.Parse((xml["product_uom"] as object[])[0].ToString());
            bomLine.product_uom_nr = (xml["product_uom"] as object[])[1].ToString();

            return bomLine;
        }
    }
}
