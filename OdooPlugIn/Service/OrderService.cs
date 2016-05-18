using MrpPluginData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Helper;
using KskPlugInSharedObject;
using MrpPluginData;
using MrpPluginData.Rep.Interface;
using OdooPlugIn.Model.Purchase;
using OdooPlugIn.Model.Product;

namespace OdooPlugIn.Service
{
    public class OrderService : ServiceBase, IOrderService
    {
        public string productNr { get; set; }
        public string vendorNr { get; set; }

        public OrderService() { }
        public OrderService(string dbConnectString) : base(dbConnectString)
        {

        }

        public ProcessResult CreatePurchaseOrderLines()
        {
            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {

                    List<Data_OrderedPart> orderedParts = new List<Data_OrderedPart>();
                    IDataOrderedPartRep rep = new DataOrderedPartRep(unit);


                    foreach (var orderLine in this.GetPurchaseOrderLines())
                    {
                        orderedParts.Add(new Data_OrderedPart()
                        {
                            partId = orderLine.product_nr,
                            sourceId = orderLine.partner_nr,
                            quantity = orderLine.product_qty,
                            arriveTime = orderLine.date_planned
                        });
                    }

                    rep.Insert(orderedParts);
                    unit.Submit();
                    result.ResultCode = 1;
                    result.ReturnedValues.Add("orderedParts", orderedParts);
                }
            }
            catch (Exception e)
            {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }

        public List<OrderLine> GetPurchaseOrderLines()
        {
            List<OrderLine> orderLines = QueryContextHelper.GetOdooContext().orderLines.Where(o => o.state.Equals("purchase")).ToList();
            return orderLines;
        }


        public ProcessResult CreateOdooOrders()
        {
            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {
                    IExeMrpOrderRep rep = new ExeMrpOrderRep(unit);

                    var groupMrpOrders = rep.GroupByVendorAndOrderDate();
                    OdooQueryContext qc = QueryContextHelper.GetOdooContext();


                    foreach (var gmo in groupMrpOrders)
                    {
                        var i = gmo.vendorId;
                        List<Exe_MrpOrder> mrpOrders = rep.ByVendorAndOrderDate(gmo.vendorId, gmo.orderDate);
                        //// create order
                        // find part vendor
                        Exe_MrpOrder order = mrpOrders.First();
                        this.productNr = order.partId;
                        this.vendorNr = order.vendorId;

                        List<PartVendor> pvs = qc.partVendors.Where(v => v.product_nr.Equals(this.productNr) && v.vendor_nr.Equals(this.vendorNr)).ToList();
                        if (pvs.Count > 0)
                        {
                            PartVendor pv = pvs.First();
                            Order odooOrder = new Order() { partner_id = pv.vendor_id, date_order = order.orderDate };
                            int orderId = qc.Create<Order>(odooOrder);
                            if (orderId > 0)
                            {
                                // create oder line
                                foreach (var mrpOrder in mrpOrders)
                                {
                                    this.productNr = mrpOrder.partId;
                                    this.vendorNr = mrpOrder.vendorId;
                                    List<PartVendor> _pvs = qc.partVendors.Where(v => v.product_nr.Equals(this.productNr) && v.vendor_nr.Equals(this.vendorNr)).ToList();
                                    if (_pvs.Count > 0)
                                    {
                                        PartVendor _pv = _pvs.First();
                                        OrderLine ol = new OrderLine()
                                        {
                                            order_id = orderId,
                                            date_planned = mrpOrder.requiredDate,
                                            product_id = _pv.product_tmpl_id,
                                            product_uom_id = _pv.product_uom_id,
                                            product_qty = (float)mrpOrder.quantity,
                                            price_unit = _pv.price,
                                            product_nr = _pv.product_nr
                                        };
                                        qc.Create<OrderLine>(ol);
                                    }
                                }
                            }
                        }
                    }

                    result.ResultCode = 1;
                    result.ReturnedValues.Add("groupMrpOrders", groupMrpOrders);
                }
            }
            catch (Exception e)
            {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }
    }
}
