using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMSPlugIn.Data.Interface;

namespace WMSPlugIn.Data.Implement
{
    public class SettingRep : BaseRep, ISettingRep
    {
        public SettingRep(IUnitOfWork unit) : base(unit) { }

        public Plugin_WMS_Setting Load()
        {
            return this.context.Plugin_WMS_Setting.FirstOrDefault();
        }
    }
}
