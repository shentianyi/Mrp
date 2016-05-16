using KskPlugInSharedObject;
using OdooPlugIn.Model.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Service
{
    public interface IOrderService
    {
        ProcessResult CreatePurchaseOrderLines();
        List<OrderLine> GetPurchaseOrderLines();

    }

}
