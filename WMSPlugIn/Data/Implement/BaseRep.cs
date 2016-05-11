using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSPlugIn.Data.Interface;

namespace WMSPlugIn.Data.Implement
{
    public class BaseRep:IBaseRep
    {
        protected MrpDataClassesDataContext context;
        public BaseRep(IUnitOfWork unit) {
            this.context = unit as MrpDataClassesDataContext;
        }
    }
}
