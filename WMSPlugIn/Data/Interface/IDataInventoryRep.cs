using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSPlugIn.Data.Interface
{
    public interface IDataInventoryRep
    {
        void Insert(List<Data_Inventory> inventories);
    }
}
