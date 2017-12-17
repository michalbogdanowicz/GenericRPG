using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GenericRPG.Global
{
     public class Tribe
    {
        public UnityEngine.Color Color;
        /// <summary>
        /// TribeID
        /// </summary>
        public int Id { get; set; }

        public Tribe(int id, UnityEngine.Color color) {
            this.Id = id;
            this.Color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            Tribe tribe = obj as Tribe;
            if ( tribe == null) { return false; }
           
            if ( this.Id == tribe.Id) { return true; }
            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
