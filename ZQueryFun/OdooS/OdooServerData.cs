using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZQueryFun.OdooS
{
    public class OdooServerData<T> : IQueryable<T>
    {
        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public OdooServerData() {
            Provider = new OdooQueryProvider();
            Expression = Expression.Constant(this);
        }

        public OdooServerData(OdooQueryProvider provider, Expression expression) {

            Provider = provider;
            Expression = expression;
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
        }

    }
}
