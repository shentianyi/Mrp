using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;
using OdooPlugIn.Model.Mrp;

namespace OdooPlugIn.Service
{
    public interface IBomService
    {
        List<BomLine> GetOdooBomLineByBomId(int bomId);

        List<Bom> GetOdooBomByParts(List<string> partNrs);

        ProcessResult CreateBomByParts(List<string> partNrs);
    }
}
