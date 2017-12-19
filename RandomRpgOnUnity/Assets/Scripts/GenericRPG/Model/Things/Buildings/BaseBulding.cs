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
            SpawnWorker();
            SpawnWorker();
            SpawnWorker();
            SpawnWorker();
            SpawnWorker();
            WorkerTimer = 0;
            BowManTimer = 0;
            SwordmanTimer = 0;
        }

        public GameObject SpawnHuman()
        {

            GameObject theObj = Instantiate(TheHuman, transform.position, Quaternion.identity);
            Human human = theObj.GetComponent<Human>();
            human.Tribe = base.Tribe;
            WorldObjectsReferenceHelper.Current().Humans.Add(theObj);
            return theObj;
        }

        private void SpawnWorker()
        {
            GameObject theObj = SpawnHuman();
            Human human = theObj.GetComponent<Human>();
            human.Role = Role.Worker;
        }

        public void SpawnBowMan()
        {
            GameObject theObj = SpawnHuman();
            Human human = theObj.GetComponent<Human>();
            human.EquippedWeapon = new Weapon(PresetWeapons.Bow);
        }

        private void SpawnSuperBowMan()
        {
            GameObject theObj = SpawnHuman();
            Human human = theObj.GetComponent<Human>();
            human.EquippedWeapon = new Weapon(PresetWeapons.Bow);
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
            human.Attributes.LevelUp();
        }

        private void SpawnSwordMan()
        {
            GameObject theObj = SpawnHuman();
            Human human = theObj.GetComponent<Human>();
            human.EquippedWeapon = new Weapon(PresetWeapons.Sword);
        }

        private float WorkerTimer;
        private float BowManTimer;
        private float SwordmanTimer;

        // Update is called once per frame
        void Update()
        {
            // build stuff
            if (Tribe.CurrentWorkers < Tribe.MaxWorkers && Tribe.Stockpile.Iron >= 5 && WorkerTimer <= Time.time)
            {
                Tribe.Stockpile.Iron -= 5;
                SpawnWorker();
                Tribe.CurrentWorkers++;
                WorkerTimer = Time.time + 0.1f;
            }

            if (Tribe.Stockpile.Iron >= 20 && WorkerTimer <= Time.time)
            {
                Tribe.Stockpile.Iron -= 20;
                SpawnBowMan();
                BowManTimer = Time.time + 3;
            }
            if (Tribe.Stockpile.Iron >= 10 && SwordmanTimer <= Time.time)
            {
                Tribe.Stockpile.Iron -= 10;
                SpawnSwordMan();
                SwordmanTimer = Time.time + 2;
            }
            if (Tribe.Stockpile.Iron >= 100)
            {
                Tribe.Stockpile.Iron -= 100;
                SpawnSuperBowMan();
            }
            

        }

   
    }
}

