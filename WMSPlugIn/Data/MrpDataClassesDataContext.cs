using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSPlugIn.Data
{
    public partial class MrpDataClassesDataContext : IUnitOfWork
    {
        public void Submit()
        {
            this.SubmitChanges();
        }
    }
}
