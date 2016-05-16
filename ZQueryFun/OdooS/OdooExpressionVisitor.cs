using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ZQueryFun.OdooS
{
    public class OdooExpressionVisitor : ExpressionVisitor
    {
        private StringBuilder sb = new StringBuilder();
        public string TableName { get; set; }
        public List<object[]> Filters { get; set; }
        public OdooExpressionVisitor()
        {
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
                sb.Append("(");

                Visit(arg2);
                sb.Append(")");

            }
            else if (expression.Method.Name == "Equals") {
                var invoker = expression.Object;
                Visit(invoker);

                var propertyAttr = (invoker as MemberExpression).Member.GetCustomAttributes(
                                   typeof(PropertyAttribute), false)
                                   .FirstOrDefault();
                if (propertyAttr != null)
                {
                    this.Filters.Add(new object[3] { (propertyAttr as PropertyAttribute).Name, "=", (expression.Arguments[0] as ConstantExpression).Value });
                }
                string value = string.Format("=*{0}*",
    (expression.Arguments[0] as ConstantExpression).Value);
                sb.Append(value);
            } 
            else if (expression.Method.Name == "Contains"
             && expression.Method.DeclaringType == typeof(string))
            {
                var invoker = expression.Object;
                Visit(invoker);
                //已经确定参数是字符串
                string value = string.Format("=*{0}*",
                    (expression.Arguments[0] as ConstantExpression).Value);
                sb.Append(value);
            }
            else if (expression.Method.Name == "StartsWith"
                && expression.Method.DeclaringType == typeof(string))
            {
                var invoker = expression.Object;
                Visit(invoker);
                string value = string.Format("={0}*",
                    (expression.Arguments[0] as ConstantExpression).Value);
                sb.Append(value);
            }

            return expression;
        }

        protected override Expression VisitConstant(ConstantExpression expression)
        {
            var query = expression.Value as IQueryable;
            if (query != null)
            {
                var type = query.ElementType;
                object categoryAttr = type.GetCustomAttributes(
                                        typeof(CategoryAttribute), false)
                                        .FirstOrDefault();
                if (categoryAttr != null)
                {
                    //如果是类型特性
                    sb.AppendFormat("(objectClass={0})",
                                 (categoryAttr as CategoryAttribute).Name);
                }
            }
            return expression;
        }

        protected override Expression VisitUnary(UnaryExpression expression)
        {
            /*访问一元表达式*/
            var opdexpression = GetOperand(expression);
            Visit(opdexpression);
            return expression;
        }

        public static Expression GetOperand(Expression expression)
        {
            /*如果是Quot表达式就把获取其操作数*/
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = (expression as UnaryExpression).Operand;
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

        protected override Expression VisitMember(MemberExpression expression)
        {
            //获取直接应用的Property特性
            var propertyAttr = expression.Member.GetCustomAttributes(
                                    typeof(PropertyAttribute), false)
                                    .FirstOrDefault();
            if (propertyAttr != null)
            {
                sb.Append((propertyAttr as PropertyAttribute).Name);
            }
            return expression;
        }


        public string Translate(Expression expression)
        {
            Visit(expression);
            return string.Format("(&{0})", sb.ToString());
        }

        public void Clear()
        {
            sb.Clear();
        }


    }
}
