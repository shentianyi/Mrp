using OdooPlugIn.Attributes;
using OdooPlugIn.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model.Mrp
{
    [OdooTableAttribute("mpr.bom.line")]
    public class BomLine:Query
    {
        public int id { get; set; }
        public int bom_id { get; set; }
        public int product_id { get; set; }

        public Part product { get; set; }

        public Bom bom { get; set; }
    }
}
