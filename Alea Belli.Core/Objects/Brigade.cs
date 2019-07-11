﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alea_Belli.Core.Objects
{
    public class Brigade : AbstractObject
    {
        private List<Regiment> regiments = new List<Regiment>();

        public int BrigadeId { get; set; }
        public List<Regiment> Regiments
        {
            get
            {
                return regiments;
            }
        }

    }
}
