using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrpPluginData.Rep.Implement;

namespace MrpPluginData.Rep.Interface
{
    public class DataProductionPlanRep : BaseRep, IDataProductionPlanRep
    {
        public DataProductionPlanRep(IUnitOfWork unit) : base(unit) { }

        public void Insert(List<Data_ProductionPlan> productionPlans)
        {
            this.context.Data_ProductionPlan.InsertAllOnSubmit(productionPlans);
        }
    }
}
