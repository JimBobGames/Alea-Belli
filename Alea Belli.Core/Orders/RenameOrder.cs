﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alea_Belli.Core.Orders
{
    public class RenameOrder : AbstractOrder
    {
        public int TargetId { get; set; }
        public string NewName { get; set; }
    }

}
