using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData.Rep.Interface
{
    public interface IDataProductionPlanRep
    {
        void Insert(List<Data_ProductionPlan> productionPlans);
    }
}
