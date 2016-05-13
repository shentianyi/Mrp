using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model
{
    public class Query : IQuery
    {
        public  List<T> Search<T>()
        {
            throw new NotImplementedException();
        }

        public List<T> SearchAndRead<T>()
        {
            throw new NotImplementedException();
        }
    }
}
