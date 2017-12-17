using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericRpg.Business.Model.Things
{
    public enum MineralType {
        
        Unknown,
        Iron,
        Copper

    }

    class Mineral : UniversalObject
    {
      public  MineralType Type;
        public long Amount;
    }
}
