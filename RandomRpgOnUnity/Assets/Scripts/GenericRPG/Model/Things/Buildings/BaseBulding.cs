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
            SpawnHuman();
            WhenHumanIsReady = 0;
        }

        public GameObject SpawnHuman()
        {
            
            GameObject theObj = Instantiate(TheHuman, transform.position, Quaternion.identity);
            Human human = theObj.GetComponent<Human>();
            human.Tribe = base.Tribe;
            WorldObjectsReferenceHelper.Current().Humans.Add(theObj);
            if (this.Tribe.CurrentWorkers < this.Tribe.MaxWorkers)
            {
                this.Tribe.CurrentWorkers++;
                human.Role = Role.Worker;
            }
            else
            {
                human.Role = Role.Fighter;
            }
            return theObj;
        }

        // Update is called once per frame
        void Update()
        {
            // build stuff
            if (WhenHumanIsReady <= Time.time && Tribe.Stockpitle.Iron >= 5)
            {
                Tribe.Stockpitle.Iron -= 5;
                SpawnHuman();
                WhenHumanIsReady = Time.time + 2;
            }

        }
    }
}
