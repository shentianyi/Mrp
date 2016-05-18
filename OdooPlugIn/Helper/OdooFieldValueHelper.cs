using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OdooPlugIn.Helper
{
   public class OdooFieldValueHelper
    {
        public static string ParsePartNr(string partNr) {
            string _partNr = partNr;
            if (partNr.Contains("[")) {
                Regex r = new Regex(@"\w+");
                Match m = r.Match(partNr);
                if (m.Success)
                {
                    _partNr = m.NextMatch().Value.ToString();
                }
            }
            return _partNr;
        }
    }
}
