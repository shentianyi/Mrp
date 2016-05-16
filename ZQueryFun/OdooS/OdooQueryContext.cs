using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZQueryFun.OdooS
{
    public class OdooQueryContext
    {

        public OdooServerData<User> users = new OdooServerData<User>();

        internal static object Execute(Expression expression, bool IsEnumerable)
        {
            OdooExpressionVisitor v = new OdooExpressionVisitor();
            string s = v.Translate(expression);
            
            
            return new List<User>() { new User() { id = 99 } };
        }
    }
}
