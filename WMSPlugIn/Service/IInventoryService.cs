﻿using KskPlugInSharedObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSPlugIn.Service
{
    public interface IInventoryService
    {
        ProcessResult GetAndCreatePartsInventory(List<string> PartNr);
    }
}
