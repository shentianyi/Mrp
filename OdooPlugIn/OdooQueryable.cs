using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OdooPlugIn
{
    public class OdooQueryable<T> : IQueryable<T>
    {
        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public OdooQueryable() {
            Provider = new OdooQueryProvider();
            Expression = Expression.Constant(this);
        }

        public OdooQueryable(OdooQueryProvider provider, Expression expression) {

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
