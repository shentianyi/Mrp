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

namespace OdooPlugIn.Service
{
    public class OrderService : ServiceBase, IOrderService
    {
        public OrderService() { }
        public OrderService(string dbConnectString) : base(dbConnectString) {

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
                           partId=orderLine.product_nr,
                           sourceId=orderLine.partner_nr,
                           quantity=orderLine.product_qty,
                           arriveTime=orderLine.date_planned
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
    }
}
