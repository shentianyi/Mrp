﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WMSPlugIn.Model
{

    [DataContract]
    public class PartStock
    {
        [DataMember]
        public string PartId { get; set; }

        [DataMember]
        public string Uom { get; set; }

        [DataMember]
        public string FIFO { get; set; }

        [DataMember]
        public string ExpireDate { get; set; }

        [DataMember]
        public float Qty { get; set; }
    }
}
