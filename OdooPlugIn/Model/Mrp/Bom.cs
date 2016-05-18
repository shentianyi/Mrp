using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdooPlugIn.Model.Product;
using OdooPlugIn.Attributes;
using CookComputing.XmlRpc;
using OdooPlugIn.Helper;

namespace OdooPlugIn.Model.Mrp
{

    [Table("mrp.bom")]
    public class Bom
    {
        [Column("id")]
        public int id { get; set; }

        [Column("code")]
        public string code { get; set; }


        [Column("product_tmpl_id")]
        public int product_tmpl_id { get; set; }

        [Column("product_tmpl_id")]
        public string product_nr { get; set; }


        [Column("product_uom")]
        public int product_uom_id { get; set; }

        [Column("product_uom")]
        public string product_uom_nr { get; set; }


        public static Bom InitWithXmlStruct(XmlRpcStruct xml)
        {
            Bom bom = new Bom();
            bom.id =int.Parse( xml["id"].ToString());
            bom.code = xml["code"].ToString();
            bom.product_tmpl_id = int.Parse((xml["product_tmpl_id"] as object[])[0].ToString());
            bom.product_nr = OdooFieldValueHelper.ParsePartNr((xml["product_tmpl_id"] as object[])[1].ToString());


            bom.product_uom_id = int.Parse((xml["product_uom"] as object[])[0].ToString());
            bom.product_uom_nr = (xml["product_uom"] as object[])[1].ToString();

            return bom;
        }
    }
}
