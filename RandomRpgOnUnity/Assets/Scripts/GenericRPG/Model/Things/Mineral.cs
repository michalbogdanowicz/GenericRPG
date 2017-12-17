using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model.Things
{
    public enum MineralType {
        
        Unknown,
        Iron,
        Copper

    }

  public  class Mineral : UniversalObject
    {
      public  MineralType Type;
        public long Amount;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if ( Amount < 0) { GameObject.Destroy(this.gameObject); }
        }
    }
}
