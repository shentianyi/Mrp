using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData.Rep.Interface
{
    public interface IDataOrderedPartRep
    {
        void Insert(List<Data_OrderedPart> orderedParts);
    }
}