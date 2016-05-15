using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }
    }
}
