using OdooPlugIn.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model.Purchase
{
    [Table("purchase.order")]
    public class Order
    {
        [Column("id")]
        public int id { get; set; }
    }
}
