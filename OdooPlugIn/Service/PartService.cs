using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;
using MrpPluginData;
using MrpPluginData.Rep.Interface;
using MrpPluginData.Service;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Helper;
using OdooPlugIn.Model.Product;

namespace OdooPlugIn.Service
{
    public class PartService : ServiceBase, IPartService
    {
         
        public string PartNrString { get; set; } 

        public PartService() { }
        public PartService(string dbConnectString) : base(dbConnectString) {
          
        }

        public ProcessResult CreatePartVendors(List<string> partNrs)
        {

            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {
                    if (partNrs != null && partNrs.Count > 0)
                    {
                        List<Data_PartVendorConfig> partVendorConfigs = new List<Data_PartVendorConfig>();
                        IDataPartVendorConfigRep rep = new DataPartVendorConfigRep(unit);


                        foreach (var partVendor in this.GetPartVendorByParts(partNrs))
                        {
                            partVendorConfigs.Add(new Data_PartVendorConfig()
                            {
                                partId = partVendor.product_nr,
                                vendorId = partVendor.vendor_nr,
                                leadTime = partVendor.delay,
                                moq = partVendor.min_qty
                            });
                        }

                        rep.Insert(partVendorConfigs);
                        unit.Submit();
                        result.ResultCode = 1;
                        result.ReturnedValues.Add("partVendorConfigs", partVendorConfigs);
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

        public List<PartVendor> GetPartVendorByParts(List<string> partNrs)
        {
            this.PartNrString = string.Join(";", partNrs.ToArray());
            List<PartVendor> partVendors = QueryContextHelper.GetOdooContext().partVendors.Where(pv => pv.product_nr.Equals(this.PartNrString)).ToList();
            return partVendors;
        }

        
    }
}
