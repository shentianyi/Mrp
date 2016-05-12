using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData.Rep.Interface
{
    public interface ISettingRep
    {
        List<Sys_Plugin_Setting> All();

        Dictionary<string, string> AllToDic();
        
    }
}