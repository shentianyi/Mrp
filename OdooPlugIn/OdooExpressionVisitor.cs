using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using OdooPlugIn.Attributes;
using OdooPlugIn.Service;

namespace OdooPlugIn
{
    public class OdooExpressionVisitor : ExpressionVisitor
    {
        public string ModelName { get; set; }
        public string TableName { get; set; }
        public List<object[]> Filters { get; set; }
        public OdooExpressionVisitor() {

            this.TableName = string.Empty;
            this.Filters = new List<object[]>();
        }


        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {

            if (expression.Method.Name == "Where" && expression.Method.DeclaringType == typeof(Queryable))
            {
                var arg1 = expression.Arguments[0];
                var arg2 = expression.Arguments[1];
                Visit(arg1);
                
                Visit(arg2);


            }
            else if (expression.Method.Name == "Equals")
            {
                var invoker = expression.Object;
                Visit(invoker);

                var propertyAttr = (invoker as MemberExpression).Member.GetCustomAttributes(
                                   typeof(ColumnAttribute), false)
                                   .FirstOrDefault();
                if (propertyAttr != null)
                {
                    var value = TryEvaluate(expression.Arguments[0]);
                    string svalue = value.ToString();
                    if (svalue.Contains(";"))
                    {
                        string[] values = svalue.Split(';');
                        object[] ovs = new object[values.Length];
                        for (var i = 0; i < ovs.Length; i++)
                        {
                            ovs[i] = values[i];
                        }

                        this.Filters.Add(new object[3] { (propertyAttr as ColumnAttribute).Name, "=", ovs });
                    }
                    else
                    {
                        this.Filters.Add(new object[3] { (propertyAttr as ColumnAttribute).Name, "=", value });
                    }
                    
                }
            }
            else if (expression.Method.Name == "Contains"
             && expression.Method.DeclaringType == typeof(string))
            {
                var invoker = expression.Object;
                Visit(invoker);
            }
            else if (expression.Method.Name == "StartsWith"
                && expression.Method.DeclaringType == typeof(string))
            {
                var invoker = expression.Object;
                Visit(invoker);
            }

            return expression;
        }

        static object TryEvaluate(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                return (ConstantExpression)expression;
            }
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var member = (MemberExpression)expression;

                var target = member.Expression != null ? TryEvaluate(member.Expression) : null;
                var pi = member.Member as PropertyInfo;
                var vvv = (target as ConstantExpression).Value;
                //// var pi = target.GetType().GetProperty(member.Member.Name);
                //var pii = vvv.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(pi.Name));
                //if (pii != null)
                //{
                //    var pppp = pii.GetValue(vvv, null);
                //}
                if (pi != null)
                {
                    return pi.GetValue(vvv, null);
                }

                var fi = member.Member as FieldInfo;
                if (fi != null)
                {
                    return fi.GetValue(target);
                }
            }
            if (expression.NodeType == ExpressionType.Call)
            {
                var call = (MethodCallExpression)expression;
                var target = call.Object != null ? TryEvaluate(call.Object) : null;
                return call.Method.Invoke(target, call.Arguments.Select(TryEvaluate).ToArray());
            }

            throw new NotSupportedException("not support type!");
        }

        protected override Expression VisitConstant(ConstantExpression expression)
        {
            var query = expression.Value as IQueryable;
            if (query != null)
            {
                var type = query.ElementType;
                this.ModelName = type.ToString();

                object tableAttr = type.GetCustomAttributes(
                                        typeof(TableAttribute), false)
                                        .FirstOrDefault();
                if (tableAttr != null)
                {
                    this.TableName = (tableAttr as TableAttribute).Name;
                }
            }
            return expression;
        }

        protected override Expression VisitLambda<T>(Expression<T> expression)
        {
            /*这里通常是筛选的正文*/
            var body = expression.Body;
            Visit(body);
            return expression;
        }
        


        public void Translate(Expression expression)
        {
            Visit(expression);
        }
        


    }
}
