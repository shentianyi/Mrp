using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrpPluginData.Rep.Implement;

namespace MrpPluginData.Rep.Interface
{
    public class ExeMrpOrderRep : BaseRep, IExeMrpOrderRep
    {
        public ExeMrpOrderRep(IUnitOfWork unit) : base(unit) { }

        public List<Exe_MrpOrder> All()
        {
          return  this.context.Exe_MrpOrder.ToList();
        }

        public List<Exe_MrpOrder> ByVendorAndOrderDate(string vendorId, DateTime orderDate)
        {
            return this.context.Exe_MrpOrder.Where(o => o.vendorId.Equals(vendorId) && o.orderDate.Equals(orderDate)).ToList();
        }

        public List<Exe_MrpOrder> GroupByVendorAndOrderDate()
        {
            List<Exe_MrpOrder> orders = new List<Exe_MrpOrder>();
            //var i= this.context.Exe_MrpOrder.GroupBy(o => new { o.vendorId, o.orderDate })
            //     .Select(o=>o).AsQueryable();
            // return i;
            var q = from o in this.context.Exe_MrpOrder
                    group o by new { o.vendorId, o.orderDate }
            into g
                    select new   {
                        vendorId=g.Key.vendorId,
                        orderDate=g.Key.orderDate
                    };
            foreach (var qq in q)
            {
                orders.Add(new Exe_MrpOrder() { vendorId = qq.vendorId, orderDate = qq.orderDate });
                
            }
            return orders;
        }
    }
}
