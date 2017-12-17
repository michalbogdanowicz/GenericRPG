using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model
{
    public class UniversalObject : MonoBehaviour 
    {
        /// <summary>
        /// Should have a name...
        /// </summary>
        public string GlobalGeneralName { get; set; }

        public Attributes Attributes { get; set; }
        public Tribe Tribe;

        public virtual void DoAnythingYoucanDoOrWantTo() {
            // of course if you are an inaminate onbject you have no wil, but you still might be able to do something...
            // and you need the phase 
        
        }

        public virtual void ShowHit()
        {
           
        }
    }
}
