using OdooPlugIn.Model.Mrp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Service
{
    public interface IProductionService
    {
        List<Production> GetConfirmed(string from_date, string to_date);
    }
}
