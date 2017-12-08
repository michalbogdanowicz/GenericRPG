using GenericRpg.Business.Model;
using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
   public class Existence
    {
        public List<Phase> Phases { get; set; }


        public Existence() {
            Phases = new List<Phase>();
        }
        /// <summary>
        /// The universe time is not affected by the relativity of einstein, so the time is always the same! LOVELY ( FOR NOW MUHAHAHA)
        /// </summary>
        public UniversalTimeUnitPassReport MakeAnUniversalTimeUntiPass() {
            foreach (Phase phase in Phases)
            {
             return   phase.MankeAnUniversalTimeUntiPass();
            }
            return null;
        }

    }
}
