using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdooPlugIn.Exceptions
{
    public class OdooAuthException:Exception
    {
        public OdooAuthException() : base("Odoo 权限验证失败，请检查服务器、用户名、密码等配置") { }
    }
}
