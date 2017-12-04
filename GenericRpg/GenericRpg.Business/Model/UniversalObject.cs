using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    class UniversalObject : IPosition
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public string GlobalGeneralName { get; set; }

        public Attributes Attributes { get; set; }


    }
}
