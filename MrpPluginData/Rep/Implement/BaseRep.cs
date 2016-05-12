using MrpPluginData.Rep.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrpPluginData.Rep.Implement
{
    public class BaseRep:IBaseRep
    {
        protected MrpDataClassesDataContext context;
        public BaseRep(IUnitOfWork unit) {
            this.context = unit as MrpDataClassesDataContext;
        }
    }
}
