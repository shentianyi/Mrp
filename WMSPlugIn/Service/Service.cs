using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSPlugIn.Data;
using WMSPlugIn.Data.Implement;
using WMSPlugIn.Data.Interface;

namespace WMSPlugIn.Service
{
    public class Service
    {
        private static Plugin_WMS_Setting setting;
        public static Plugin_WMS_Setting WMSSetting
        {
            get {return setting; }
            set { setting = value; }
        }

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
        public Service() { }
        public Service(string dbConnectString)
        {
            this.dbConnectString = dbConnectString;
            if (WMSSetting == null)
            {
                WMSSetting = GetSetting();
            }
        }

        public Plugin_WMS_Setting GetSetting()
        {
            using (IUnitOfWork unit = this.DbContext)
            {
                ISettingRep rep = new SettingRep(unit);
                Plugin_WMS_Setting setting = rep.Load();
                if (setting == null)
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
