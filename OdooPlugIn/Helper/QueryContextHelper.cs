using MrpPluginData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Helper
{
    public class QueryContextHelper
    {
        public static OdooQueryContext GetOdooContext()
        {
            OdooQueryContext qc = new OdooQueryContext(PlugSetting.Setting["odoo_host"],
               PlugSetting.Setting["odoo_db"],
               PlugSetting.Setting["odoo_user"],
               PlugSetting.Setting["odoo_pwd"]);
            return qc;
        }
    }
}
