﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrpPluginData.Rep.Interface
{
    public interface IDataInventoryRep
    {
        void Insert(List<Data_Inventory> inventories);
    }
}
