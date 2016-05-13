using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Model
{
    public interface IQuery
    {
        List<T> Search<T>();
        List<T> SearchAndRead<T>();
    }
}
