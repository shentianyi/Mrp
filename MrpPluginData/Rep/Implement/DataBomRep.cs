using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrpPluginData.Rep.Implement;

namespace MrpPluginData.Rep.Interface
{
    public class DataBomRep : BaseRep, IDataBomRep
    {
        public DataBomRep(IUnitOfWork unit) : base(unit) { }

        public void Insert(List<Data_Bom> boms)
        {
            this.context.Data_Bom.InsertAllOnSubmit(boms);
        }
    }
}
