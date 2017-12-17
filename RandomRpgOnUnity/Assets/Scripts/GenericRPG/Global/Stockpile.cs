using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GenericRPG.Global
{
    public class Stockpile
    {
        public long Wood = 0;
        public long Iron = 0;
        public long Copper = 0;

        public void Empty() {
            Wood = 0;
            Iron = 0;
            Copper = 0;
        }

        public void AddResources(Stockpile stockpile) {
            Wood += stockpile.Wood;
            Iron += stockpile.Iron;
            Copper += stockpile.Copper;
        }


    }
}
