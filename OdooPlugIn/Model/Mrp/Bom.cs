using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdooPlugIn.Model.Product;
using OdooPlugIn.Attributes;

namespace OdooPlugIn.Model.Mrp
{

    [OdooTableAttribute("mrp.bom")]
    public class Bom
    {
        public int id { get; set; }
        public string code { get; set; }
        
        public int product_tmpl_id { get; set; }

        public Part product { get; set; }

        List<BomLine> bomLines { get; set; }
    }
}
