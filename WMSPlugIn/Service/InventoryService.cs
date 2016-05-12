using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KskPlugInSharedObject;
using WMSPlugIn.WmsRest;
using MrpPluginData;
using MrpPluginData.Rep.Interface;
using MrpPluginData.Rep.Implement;
using MrpPluginData.Service;
using WMSPlugIn.Model;

namespace WMSPlugIn.Service
{
    public class InventoryService : ServiceBase, IInventoryService
    {
        public InventoryService() { }
        public InventoryService(string dbConnectString):base(dbConnectString) {
            
        }

        /// <summary>
        /// Get Part Inventory from WMS & Insert into Mrp DB
        /// </summary>
        /// <param name="partNrs">part nr list</param>
        /// <returns>ResultCode 1 stands for success, 0 for fail</returns>
        public ProcessResult GetAndCreatePartsInventory(List<string> partNrs)
        {
            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {
                    if (partNrs != null && partNrs.Count > 0)
                    {
                        List<Data_Inventory> inventories = new List<Data_Inventory>();
                        IDataInventoryRep rep = new DataInventoryRep(unit);


                        foreach (var stock in this.GetPartsInventory(partNrs))
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
                        result.ReturnedValues.Add("inventoris", inventories);
                    }
                    else
                    {
                        result.Msgs.Add("Part Nr List is empty");
                    }
                }
            }
            catch (Exception e)
            {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }

        public List<PartStock> GetPartsInventory(List<string> partNrs)
        {
            var wmsRestClient = new WmsRestClient(PlugSetting.Setting["wms_host"]);
            var req = wmsRestClient.GetRequest(PlugSetting.Setting["wms_inventory_api"]);
            req.AddParameter("part_nrs", string.Join(";", partNrs.ToArray()));
            var res = wmsRestClient.Execute(req);
            List<PartStock> stocks = JsonUtil.parse<List<PartStock>>(res.Content);
            return stocks;
        }
    }
}
