using MrpPluginData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Helper;
using KskPlugInSharedObject;
using MrpPluginData;
using MrpPluginData.Rep.Interface;

namespace OdooPlugIn.Service
{
    public class ProductionService : ServiceBase, IProductionService
    {
        public ProductionService() { }
        public ProductionService(string dbConnectString) : base(dbConnectString) {

        }

        public ProcessResult CreateProuduction()
        {
            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {

                    List<Data_ProductionPlan> productionPlans = new List<Data_ProductionPlan>();
                    IDataProductionPlanRep rep = new DataProductionPlanRep(unit);


                    foreach (var production in this.GetConfirmed())
                    {
                        productionPlans.Add(new Data_ProductionPlan()
                        {
                            planId = production.name,
                            assemblyPartId = production.product_nr,
                            time = production.date_planned,
                            quantity = production.product_qty,
                            bomId = production.bom_code
                        });
                    }

                    rep.Insert(productionPlans);
                    unit.Submit();
                    result.ResultCode = 1;
                    result.ReturnedValues.Add("productionPlans", productionPlans);
                }
            }
            catch (Exception e)
            {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }

        public List<Production> GetConfirmed()
        {
            List<Production> productions = QueryContextHelper.GetOdooContext().productions.Where(p => p.state.Equals("confirmed")).ToList();
            return productions;
        }
    }
}
