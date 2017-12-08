using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    public class LivingTarget
    {
        public Being  Being { get; set; }
        public double Distance { get; set; }
        public Phase PhaseOfExistance { get; set; }
    }
}
