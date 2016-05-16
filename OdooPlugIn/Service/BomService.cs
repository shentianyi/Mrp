using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KskPlugInSharedObject;
using MrpPluginData;
using MrpPluginData.Rep.Interface;
using MrpPluginData.Service;
using OdooPlugIn.Model.Mrp;
using OdooPlugIn.Helper;

namespace OdooPlugIn.Service
{
    public class BomService : ServiceBase, IBomService
    {

        //public string partNrString = string.Empty;
        public string PartNrString { get; set; }
        public int bomId { get; set; }
        public BomService() { }
        public BomService(string dbConnectString) : base(dbConnectString) {
          
        }

        public ProcessResult CreateBomByParts(List<string> partNrs)
        {

            ProcessResult result = new ProcessResult() { ResultCode = 0, Msgs = new List<string>() };
            try
            {
                using (IUnitOfWork unit = this.DbContext)
                {
                    if (partNrs != null && partNrs.Count > 0)
                    {
                        List<Data_Bom> boms = new List<Data_Bom>();
                        IDataBomRep rep = new DataBomRep(unit);


                        foreach (var bom in this.GetOdooBomByParts(partNrs))
                        {
                            List<BomLine> bomLines = this.GetOdooBomLineByBomId(bom.id);
                            foreach (BomLine bl in bomLines)
                            {
                                boms.Add(new Data_Bom()
                                {
                                    bomId = bom.code,
                                    assemblyPartId = bom.product_nr,
                                    materialPartId = bl.product_nr,
                                    quantity = bl.product_qty,
                                    uom = bl.product_uom_nr
                                });
                            }
                        }

                        rep.Insert(boms);
                        unit.Submit();
                        result.ResultCode = 1;
                        result.ReturnedValues.Add("boms", boms);
                    }
                    else
                    {
                        result.Msgs.Add("Part Nr List is empty");
                    }
                }
            }
            catch (Exception e)
            {
                result.ResultCode = 0;
                result.Msgs.Add(e.Message);
            }
            return result;
        }

        public List<Bom> GetOdooBomByParts(List<string> partNrs)
        {
            this.PartNrString = string.Join(";", partNrs.ToArray());
            List<Bom> boms = QueryContextHelper.GetOdooContext().boms.Where(b => b.product_nr.Equals(this.PartNrString)).ToList();
            return boms;
        }

        public List<BomLine> GetOdooBomLineByBomId(int bomId)
        {
            this.bomId = bomId;
            List<BomLine> bomLines = QueryContextHelper.GetOdooContext().bomLines.Where(b => b.bom_id.Equals(this.bomId)).ToList();
            return bomLines;
        }

       
    }
}
