using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrpPluginData.Rep.Implement;

namespace MrpPluginData.Rep.Interface
{
    public class DataOrderedPartRep : BaseRep, IDataOrderedPartRep
    {
        public DataOrderedPartRep(IUnitOfWork unit) : base(unit) { }

        public void Insert(List<Data_OrderedPart> orderedParts)
        {
            this.context.Data_OrderedPart.InsertAllOnSubmit(orderedParts);
        }
    }
}
