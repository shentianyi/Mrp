using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrpPluginData
{
    public class PlugSetting
    {
        private static Dictionary<string, string> setting;
        public static Dictionary<string, string> Setting
        {
            get
            {
                return setting;
            }
            set { setting = value; }
        }

        public static void InitSetting(Dictionary<string, string> s) {
            setting = s;
        }
    }
}
