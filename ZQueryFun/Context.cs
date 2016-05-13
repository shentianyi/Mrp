using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZQueryFun
{
    public class Context
    {
        public IEnumerable<User> Users { get;   set; }
       
        //public Context()
        //{
        //    var provider = new DSProvider();
        //    Users = new OLC.LINQ.Queryable<User>(provider);
        //}
    }
}
