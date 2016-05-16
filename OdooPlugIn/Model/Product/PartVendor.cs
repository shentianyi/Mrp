using CookComputing.XmlRpc;
using OdooPlugIn.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model.Product
{
    [Table("product.supplierinfo")]
    public class PartVendor
    {
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        public int vendor_id { get; set; }

        [Column("name")]
        public string vendor_nr { get; set; }

        [Column("product_tmpl_id")]
        public int product_tmpl_id { get; set; }

        [Column("product_tmpl_id")]
        public string product_nr { get; set; }


        [Column("product_uom")]
        public int product_uom_id { get; set; }

        [Column("product_uom")]
        public string product_uom_nr { get; set; }

        [Column("delay")]
        public int delay { get; set; }

        [Column("min_qty")]
        public float min_qty { get; set; }

        [Column("price")]
        public float price { get; set; }


        public static PartVendor InitWithXmlStruct(XmlRpcStruct xml)
        {
            PartVendor partVendor = new PartVendor();
            partVendor.id = int.Parse(xml["id"].ToString());
            partVendor.vendor_id = int.Parse((xml["name"] as object[])[0].ToString());
            partVendor.vendor_nr = (xml["name"] as object[])[1].ToString();
            partVendor.product_tmpl_id = int.Parse((xml["product_tmpl_id"] as object[])[0].ToString());
            partVendor.product_nr = (xml["product_tmpl_id"] as object[])[1].ToString();



            partVendor.product_uom_id = int.Parse((xml["product_uom"] as object[])[0].ToString());
            partVendor.product_uom_nr = (xml["product_uom"] as object[])[1].ToString();


            partVendor.delay = int.Parse(xml["delay"].ToString());
            partVendor.min_qty = float.Parse(xml["min_qty"].ToString());

            partVendor.price = float.Parse(xml["price"].ToString());

            return partVendor;
        }
    }
}
