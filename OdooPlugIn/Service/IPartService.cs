using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Model.Product;

namespace OdooPlugIn.Service
{
    public interface IPartService
    { 
        List<PartVendor> GetPartVendorByParts(List<string> partNrs);

        ProcessResult CreatePartVendors(List<string> partNrs);
    }
}
