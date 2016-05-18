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
        /// <summary>
        /// PP
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProcessResult CreateConfirmedProductionPlans(ProcessData data)
        {
            IProductionService ps = new ProductionService(data.Data["db"] as string);
            return ps.CreateProuduction();

        }

        /// <summary>
        /// BOM
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProcessResult CreateBomByParts(ProcessData data) {
            IBomService bs = new BomService(data.Data["db"] as string);
            return bs.CreateBomByParts(data.Data["parts"] as List<string>);
        }

        /// <summary>
        /// ORDER
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProcessResult CreatePurchaseOrderedParts(ProcessData data)
        {
            IOrderService ps = new OrderService(data.Data["db"] as string);
            return ps.CreatePurchaseOrderLines();
        }

        /// <summary>
        /// VENDOR
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProcessResult CreatePartVendorConfigs(ProcessData data)
        {
            IPartService bs = new PartService(data.Data["db"] as string);
            return bs.CreatePartVendors(data.Data["parts"] as List<string>);
        }

        /// <summary>
        /// PO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProcessResult CreateOdooOrders(ProcessData data) {
            IOrderService os = new OrderService(data.Data["db"] as string);
            return os.CreateOdooOrders();
        }


    }
}
