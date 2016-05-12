using KskPlugInSharedObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSPlugIn.Model;

namespace WMSPlugIn.Service
{
    public interface IInventoryService
    {
        List<PartStock> GetPartsInventory(List<string> partNrs);
        ProcessResult GetAndCreatePartsInventory(List<string> partNrs);
    }
}
