using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericRpg.Business.Model.Things
{
    public enum TreeType {
        Oak
    }

    class Tree : UniversalObject
    {
        public TreeType Type { get; set; }

        public Tree() {
            
            base.Attributes.Weight = 10;
        }
    }
}
