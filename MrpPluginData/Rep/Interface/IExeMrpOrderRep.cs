using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData.Rep.Interface
{
    public interface IExeMrpOrderRep
    {
        List<Exe_MrpOrder> GroupByVendorAndOrderDate();
        List<Exe_MrpOrder> ByVendorAndOrderDate(string vendorId, DateTime orderDate);
        List<Exe_MrpOrder> All();
    }
}
