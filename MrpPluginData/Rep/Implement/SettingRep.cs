using MrpPluginData.Rep.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData.Rep.Implement
{
    public class SettingRep : BaseRep, ISettingRep
    {
        public SettingRep(IUnitOfWork unit) : base(unit) { }


        public List<Sys_Plugin_Setting> All()
        {
          return  this.context.Sys_Plugin_Setting.ToList();
        }

        public Dictionary<string, string> AllToDic()
        {
            List<Sys_Plugin_Setting> settings = this.All();
            Dictionary<string, string> settingDic = new Dictionary<string, string>();
            settings.ForEach(s =>
            {
                settingDic[s.name] = s.value;
            });
            return settingDic;
        }
    }
}
