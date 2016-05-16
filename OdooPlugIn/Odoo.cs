using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;
using OdooPlugIn.Service;

namespace OdooPlugIn
{
    public class Odoo
    {
        public ProcessResult CreateBomByParts(ProcessData data) {
            IBomService bs = new BomService(data.Data["db"] as string);
            return bs.CreateBomByParts(data.Data["parts"] as List<string>);
        }

        public ProcessResult CreateConfirmedProductionPlan(ProcessData data) {
            IProductionService ps = new ProductionService(data.Data["db"] as string);
            return ps.CreateProuduction();
        }

        public ProcessResult CreatePurchaseOrderedPart(ProcessData data)
        {
            IOrderService ps = new OrderService(data.Data["db"] as string);
            return ps.CreatePurchaseOrderLines();
        }
    }
}
