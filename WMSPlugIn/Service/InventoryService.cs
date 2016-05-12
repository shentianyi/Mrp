using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KskPlugInSharedObject;
using WMSPlugIn.Data;
using WMSPlugIn.WmsRest;
using WMSPlugIn.Data.Implement;
using WMSPlugIn.Data.Interface;

namespace WMSPlugIn.Service
{
    public class InventoryService :Service, IInventoryService
    {
        public InventoryService() { }
        public InventoryService(string dbConnectString):base(dbConnectString) {
            
        }

        public ProcessResult GetAndCreatePartsInventory(List<string> PartNrs)
        {
            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>()};
            try {
                using (IUnitOfWork unit = this.DbContext)
                {
                    if (PartNrs != null && PartNrs.Count > 0)
                    {
                        List<Data_Inventory> inventories = new List<Data_Inventory>();
                        IDataInventoryRep rep = new DataInventoryRep(unit);
                        var wmsRestClient = new WmsRestClient(Service.WMSSetting.host);
                        var req = wmsRestClient.GetRequest(Service.WMSSetting.inventoryApi);
                        req.AddParameter("part_nrs", string.Join(";",PartNrs.ToArray()));
                        var res = wmsRestClient.Execute(req);
                        List<PartStock> stocks = JsonUtil.parse<List<PartStock>>(res.Content);

                        foreach (var stock in stocks)
                        {
                            inventories.Add(new Data_Inventory()
                            {
                                partId = stock.PartId,
                                uom = stock.Uom,
                                FIFO = DateTime.Parse(stock.FIFO),
                                expireDate = DateTime.Parse(stock.ExpireDate),
                                quantity = stock.Qty
                            });
                        }

                        rep.Insert(inventories);
                        unit.Submit();
                        result.ResultCode = 1;
                        
                    }
                    else {
                        result.Msgs.Add("Part Nr List is empty");
                    }
                }
            } catch (Exception e) {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }
    }
}
