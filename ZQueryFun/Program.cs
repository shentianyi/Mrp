using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZQueryFun
{
    class Program
    {
        static void Main(string[] args)
        {
            Context c = new Context();
            c.Users.Where(u => u.Name.Equals(""));
        }
    }
}
