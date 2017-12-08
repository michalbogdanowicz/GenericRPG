using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    public class UniversalObject : IPosition
    {
      public Point Position { get; set; }


        public string GlobalGeneralName { get; set; }

        public Attributes Attributes { get; set; }

        public virtual ActionReport DoAnythingYoucanDoOrWantTo(Phase phase) {
            // of course if you are an inaminate onbject you have no wil, but you still might be able to do something...
            // and you need the phase 
            return null;
        }

    }
}
