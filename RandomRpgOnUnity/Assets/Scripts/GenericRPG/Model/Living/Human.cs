﻿using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model.Living
{
    public class ResourcesCarried
    {
     public   long Wood = 0;
        public long Iron = 0;
        public long Copper = 0;
    }

    public class Human : Being
    {
        float attackResetTimer = 0;
        public ResourcesCarried resourcesCarried;
        public Sprite DamagedHuman;
        public Sprite NormalHuman;
        public Sprite Tribe1Human;
        public Sprite Tribe2Human;
        public Sprite Tribe3Human;
        public Sprite Tribe4Human;
        Vector2 direction;
        private SpriteRenderer currentRenderer;
        private HumanTarget humanTarget;
        public float SpeedModifier;
        private MineralTarget mineralTarget;
        private ActionChosen actionChosen;
        public float AgressionRange;
        // Use this for initialization
        new void Start()
        {
            base.Start();
            currentRenderer = gameObject.GetComponent<SpriteRenderer>();
            LoadTribeSprite();
            resourcesCarried = new ResourcesCarried();
        }

        private class HumanTarget
        {
            public Human Human { get; set; }
            public float Distance { get; set; }
            public HumanTarget(Human human, float distance)
            {
                this.Human = human;
                this.Distance = distance;
            }

        }
        private class MineralTarget {
            public Mineral Mineral { get; set; }
            public float Distance { get; set; }
            public MineralTarget(Mineral mineral, float distance)
            {
                this.Mineral = mineral;
                this.Distance = distance;
            }
        }
        private void GetHumanTarget()
        {
            float minDistanceSoFar = float.MaxValue;
            humanTarget = null;

            foreach (GameObject xxx in WorldObjectsReferenceHelper.Current().Humans)
            {
                if (xxx != null )
                {
                    Human currentlyChecked = xxx.GetComponent<Human>();
                    if (currentlyChecked != null && currentlyChecked != this && currentlyChecked.Tribe != this.Tribe && currentlyChecked.IsAlive)
                    {
                        float distance = Vector2.Distance(transform.position, currentlyChecked.transform.position);
                        if (distance < minDistanceSoFar && distance < AgressionRange)
                        {
                            minDistanceSoFar = distance;
                            humanTarget = new HumanTarget(currentlyChecked,minDistanceSoFar);
                        }
                    }
                }
            }
         
        }

        public void ToDamangedHuman()
        {
            currentRenderer.sprite = DamagedHuman;
        }

        public void LoadTribeSprite()
        {

            switch (base.Tribe)
            {
                case Tribe.NoTribe: currentRenderer.sprite = NormalHuman; break;
                case Tribe.Tribe1: currentRenderer.sprite = Tribe1Human; break;
                case Tribe.Tribe2: currentRenderer.sprite = Tribe2Human; break;
                case Tribe.Tribe3: currentRenderer.sprite = Tribe3Human; break;
                case Tribe.Tribe4: currentRenderer.sprite = Tribe4Human; break;
                default: throw new ArgumentException("Impossible tribre for now, insert new stuff!");
            }
        }
        float whenToGoWhile = 0;
        private bool needToGoNormal = false;
        private float WhenToThinkAboutDecision = 0;

        private void BackToNormalColorIfNeeded()
        {
            if (needToGoNormal && whenToGoWhile < Time.time)
            {
                needToGoNormal = false;
                LoadTribeSprite();
            }
        }
        /// <summary>
        /// Is dieing?
        /// </summary>
        /// <returns></returns>
        private bool ManageDeath()
        {
            if (!IsAlive)
            {
                ToDamangedHuman();
                GameObject.Destroy(gameObject.GetComponent<Rigidbody2D>());
                GameObject.Destroy(gameObject.GetComponent<BoxCollider2D>());

                this.Decompose();
                if (this.IsDecomposed)
                {
                    GameObject.Destroy(gameObject);
                    return true;
                }
                return true;
            }
            return false;
        }
        private enum ActionChosen
        {
            ReactToEnemy,
            ReactToGathering,
            ReactToCraftingNeed,
            ReactToBuildingNeed,
            Wander
        }

        // Update is called once per frame
        void Update()
        {
            BackToNormalColorIfNeeded();
            if (ManageDeath()) { return; };
            // decide what to do whene there is something to do, for example the target died.
            if (WhenToThinkAboutDecision < Time.time)
            {
                WhenToThinkAboutDecision = Time.time + UnityEngine.Random.Range(1, 1.8f);
                actionChosen = ChooseAnAction();
            }
            // 
            Act();
       
            
        }

        private void Act()
        {
            switch (actionChosen)
            {
                case ActionChosen.Wander: WalkAround(); break;
                case ActionChosen.ReactToEnemy: ReactToEnemy(); break;
                case ActionChosen.ReactToBuildingNeed: throw new NotImplementedException("Building not implemented"); break;
                case ActionChosen.ReactToCraftingNeed: throw new NotImplementedException("Crafting not implemented"); break;
                case ActionChosen.ReactToGathering: Gather(); break;
                default: throw new NotImplementedException(String.Format("This action is not implemnted yet : {0}",actionChosen.ToString()));
            }
        }

        private float gatheringResetTimer = 0;

        private void Gather()
        {
            if (mineralTarget != null && mineralTarget.Mineral != null && mineralTarget.Mineral)
            {
                if (mineralTarget.Distance < (this.CurrentWeapon.Range * 0.2f))
                {
                    if (Time.time > gatheringResetTimer)
                    {
                        Mine(mineralTarget.Mineral);
                        gatheringResetTimer = Time.time + this.CurrentWeapon.RewindPeriod;
                    }
                }
                else
                {
                    Vector2 targetPosition = mineralTarget.Mineral.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Attributes.Speed.Value);
                }
            }
            else
            {
                WhenToThinkAboutDecision = Time.time - 1; // reset the choose who to rape.
            }
        }

        private void Mine(Mineral mineral)
        {
            switch (mineral.Type)
            {
                case MineralType.Copper: mineral.Amount-- ; resourcesCarried.Copper++; break;
                case MineralType.Iron: mineral.Amount--; resourcesCarried.Iron++; break;
                case MineralType.Unknown: throw new NotImplementedException("Impossible!");
            }
            
        }

        private void ReactToEnemy()
        {
            if (humanTarget != null && humanTarget.Human != null && humanTarget.Human.IsAlive)
            {
                if (humanTarget.Distance < (this.CurrentWeapon.Range))
                {
                    if (Time.time > attackResetTimer)
                    {
                        base.Attack(humanTarget.Human);
                        attackResetTimer = Time.time + this.CurrentWeapon.RewindPeriod;
                    }
                }
                else
                {
                    Vector2 targetPosition = humanTarget.Human.transform.position;
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Attributes.Speed.Value);
                }
            }
            else
            {
                WhenToThinkAboutDecision = Time.time - 1; // reset the choose who to rape.
            }
        }
        private float timeToChooseAnotherDirection = 0;

        private void WalkAround()
        {
            if (timeToChooseAnotherDirection < Time.time)
            {
                direction = UnityEngine.Random.insideUnitCircle.normalized;
                timeToChooseAnotherDirection = Time.time + UnityEngine.Random.Range(0.8f, 2);
            }
    
            transform.Translate(direction * Time.deltaTime);
        }

        private ActionChosen ChooseAnAction()
        {
            GetHumanTarget();
            if ( humanTarget != null) { return ActionChosen.ReactToEnemy; }
            FindMineralTarget();
            if ( mineralTarget != null) { return ActionChosen.ReactToGathering; }
            return ActionChosen.Wander;

        }

        private void FindMineralTarget()
        {
            mineralTarget = null;
            float minDistanceSoFar = float.MaxValue;
            foreach (var obj in WorldObjectsReferenceHelper.Current().Minerals)
            {
                Mineral currentlyChecked = obj.GetComponent<Mineral>();
                if (currentlyChecked != null && currentlyChecked != this )
                {
                    float distance = Vector2.Distance(transform.position, currentlyChecked.transform.position);
                    if (distance < minDistanceSoFar)
                    {
                        minDistanceSoFar = distance;
                        mineralTarget = new MineralTarget(currentlyChecked, distance);
                    }
                }
            }
        }

        public override void ShowHit()
        {
            this.ToDamangedHuman();
            whenToGoWhile = Time.time + 0.15f;
            needToGoNormal = true;
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {

        }


        private Vector3 GetNewDirection()
        {
            int d = UnityEngine.Random.Range(1, 4);
            switch (d)
            {
                case 1: return Vector2.up;
                case 2: return Vector2.down;
                case 3: return Vector2.left;
                case 4: return Vector2.right;
                default: throw new System.Exception("Impossible");
            }
        }
    }
}
