using KskPlugInSharedObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMSPlugIn.Service;

namespace WMSPlugIn
{
    public class Inventory
    {
        public ProcessResult CreateInventory(ProcessData data)
        {
            IInventoryService ins = new InventoryService(data.Data["db"] as string);
            return ins.GetAndCreatePartsInventory(data.Data["parts"] as List<string>);
        }
    }
}
