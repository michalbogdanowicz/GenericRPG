﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model.Reports
{
   public class AttackReport : ActionReport
    {
        public bool Hits { get; set; }
        public Tuple<Point, Point> attackPath { get; set; }
    }
}
