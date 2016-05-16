using KskPlugInSharedObject;
using OdooPlugIn.Model.Mrp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Service
{
    public interface IProductionService
    {
        ProcessResult CreateProuduction();
        List<Production> GetConfirmed();
    }
}
