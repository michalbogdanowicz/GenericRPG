using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Creators
{
    class HumanGeneration
    {
        public void CreateObject(string prefabName)
        {
            GameObject newObject =
               GameObject.Instantiate(Resources.Load(prefabName)) as GameObject;
        }
    }
}
