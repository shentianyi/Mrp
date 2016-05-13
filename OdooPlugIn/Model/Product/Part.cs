using OdooPlugIn.Attributes;
using OdooPlugIn.Model.Mrp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model.Product
{
    [OdooTableAttribute("product.product")]
   public class Part
    {
        public int id { get; set; }
        public DateTime create_date { get; set; }
        public string name_template { get; set; }

        public int product_tmpl_id { get; set; }

        public List<Bom> boms { get; set; }
    }
}
