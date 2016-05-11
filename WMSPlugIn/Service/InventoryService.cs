using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KskPlugInSharedObject;
using WMSPlugIn.Data;
using WMSPlugIn.WmsRest;

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

                        var wmsRestClient = new WmsRestClient(Service.WMSSetting.host);
                        var req = wmsRestClient.GetRequest(Service.WMSSetting.inventoyApi);
                        req.AddParameter("part_nr", PartNrs);
                        var res = wmsRestClient.Execute(req);
                        List<PartStock> stocks = JsonUtil.parse<List<PartStock>>(res.Content);

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
