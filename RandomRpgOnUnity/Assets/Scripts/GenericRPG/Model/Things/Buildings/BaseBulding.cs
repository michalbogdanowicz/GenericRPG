using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model;
using GenericRpg.Business.Model.Living;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GenericRPG.Model.Things.Buildings
{
    public class BaseBulding : UniversalObject
    {
        private SpriteRenderer currentRenderer;
        private float WhenHumanIsReady;
        private bool IsBuildingAHuman;
        public GameObject TheHuman;
        // Use this for initialization
        void Start()
        {
            currentRenderer = gameObject.GetComponent<SpriteRenderer>();
            currentRenderer.color = base.Tribe.Color;
            //
            SpawnHuman();
            SpawnHuman();
            SpawnHuman();
            SpawnHuman();
            WhenHumanIsReady = Time.time + 4;
        }

        public GameObject SpawnHuman()
        {
            GameObject theObj = Instantiate(TheHuman, transform.position, Quaternion.identity);
            Human script = theObj.GetComponent<Human>();
            script.Tribe = base.Tribe;
            WorldObjectsReferenceHelper.Current().Humans.Add(theObj);
            return theObj;
        }

        // Update is called once per frame
        void Update()
        {
            // build stuff
            if (WhenHumanIsReady < Time.time)
            {
                SpawnHuman();
                WhenHumanIsReady = Time.time + 4;

            }

        }
    }
}
