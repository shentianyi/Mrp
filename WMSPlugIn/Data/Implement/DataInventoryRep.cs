using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSPlugIn.Data.Interface;

namespace WMSPlugIn.Data.Implement
{
  public  class DataInventoryRep:BaseRep, IDataInventoryRep
    {
        public DataInventoryRep(IUnitOfWork unit) : base(unit) { }

        public void Insert(List<Data_Inventory> inventories)
        {
            this.context.Data_Inventory.InsertAllOnSubmit(inventories);
        }
    }
}
