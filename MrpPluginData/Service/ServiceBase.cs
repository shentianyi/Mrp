using MrpPluginData;
using MrpPluginData.Rep.Implement;
using MrpPluginData.Rep.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrpPluginData.Service
{
    public class ServiceBase
    {
       

        private string dbConnectString;
        private MrpDataClassesDataContext dbContext;
        public string DbConnectString { get { return dbConnectString; } }



        public MrpDataClassesDataContext DbContext
        {
            get
            {
                //if (dbContext == null)
                //{
                    dbContext = new MrpDataClassesDataContext(this.DbConnectString);
                //}
                return dbContext;
            }
        }
        public ServiceBase() { }
        public ServiceBase(string dbConnectString)
        {
            this.dbConnectString = dbConnectString;
            
            PlugSetting.InitSetting(GetSetting());
        }

        public Dictionary<string,string> GetSetting()
        {
            using (IUnitOfWork unit = this.DbContext)
            {
                ISettingRep rep = new SettingRep(unit);
                Dictionary<string,string> setting = rep.AllToDic();
                if (setting == null || setting.Count==0)
                {
                    throw new Exception("没有在数据库中维护WMS的配置");
                }
                else {
                    return setting;
                }
            }
        }
    }
}
