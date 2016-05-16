using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZQueryFun
{
    [Category("User")]
    public class User
    {
        [Property("userId")]
        public int id { get; set; }
        /*模型类型，用来构造查询*/
        [Property("userPrincipalName")]
        public string UserPrincipalName { get; set; }
        [Property("cn")]
        public string Name { get; set; }
    }

    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string categoryName)
        {
            Name = categoryName;
        }
        public string Name { get; private set; }
    }

    public class PropertyAttribute : Attribute
    {
        public PropertyAttribute(string propertyName)
        {
            Name = propertyName;
        }
        public string Name { get; private set; }
    }
}
