using GenericRpg.Business.Model.Living;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GenericRPG.Global
{
    public class WorldObjectsReferenceHelper
    {
        private static WorldObjectsReferenceHelper current;
        public List<GameObject> Humans { get; set; }
        public List<GameObject> Minerals { get; set; }
        public List<Tribe> Tribes { get; set; }

        private WorldObjectsReferenceHelper()
        {
            Humans = new List<GameObject>();
            Minerals = new List<GameObject>();
            Tribes = new List<Tribe>();
        }

        public static WorldObjectsReferenceHelper Current()
        {
            if (current == null)
            {
                current = new WorldObjectsReferenceHelper();
            }
            return current;
        }

    }
}
