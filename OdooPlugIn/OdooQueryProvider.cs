using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OdooPlugIn
{
    public class OdooQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            return (IQueryable)Activator.CreateInstance(typeof(OdooQueryable<>).MakeGenericType(expression.Type), new object[] { this, expression });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new OdooQueryable<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return OdooQueryContext.Execute(expression,false);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");
            
            return (TResult)OdooQueryContext.Execute(expression,IsEnumerable);
        }
        
    }
}
