using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrpPluginData.Rep.Implement;

namespace MrpPluginData.Rep.Interface
{
    public class DataPartVendorConfigRep : BaseRep, IDataPartVendorConfigRep
    {
        public DataPartVendorConfigRep(IUnitOfWork unit) : base(unit) { }

        public void Insert(List<Data_PartVendorConfig> partVendorConfig)
        {
            this.context.Data_PartVendorConfig.InsertAllOnSubmit(partVendorConfig);
        }
    }
}
