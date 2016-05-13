using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OdooTableAttribute : Attribute
    {
        public string tableName { get; set; }
        public OdooTableAttribute(string tableName) {
            this.tableName = tableName;
        }
    }
}
