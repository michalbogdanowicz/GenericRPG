using Assets.Scripts.GenericRPG.Global;
using GenericRpg.Business.Model.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model.Living
{
    public enum Role {
        Worker,
        Fighter
    }

    public class Human : Being
    {

        float attackResetTimer = 0;
        public Stockpile resourcesCarried;

        Vector2 direction;
        private SpriteRenderer currentRenderer;
        private HumanTarget humanTarget;
        public float SpeedModifier;
        private MineralTarget mineralTarget;
        private ActionChosen actionChosen;
        public float AgressionRange;
        public Role Role;
        // Use this for initialization
        new void Start()
        {
            base.Start();
            currentRenderer = gameObject.GetComponent<SpriteRenderer>();
            ResetSprite();
            resourcesCarried = new Stockpile();
          //  Role = Role.Fighter;
        }
        float ShowNormalAfterLevelUpTime;
        protected override void ShowLevelUp()
        {
            base.ShowLevelUp();
            var currentRenderer = gameObject.GetComponent<SpriteRenderer>();
            currentRenderer.color = Color.yellow;
            ShowNormalAfterLevelUpTime = Time.time + 0.15f;
            needToGoNormal = true;
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
        private class MineralTarget
        {
            public Mineral Mineral { get; set; }
            public float Distance { get; set; }
            public MineralTarget(Mineral mineral, float distance)
            {
                this.Mineral = mineral;
                this.Distance = distance;
            }
        }

        private float MovementSpeed()
        {
            return SpeedModifier * base.Attributes.Speed.Value;
        }

        private void GetHumanTarget()
        {
            float minDistanceSoFar = float.MaxValue;
            humanTarget = null;

            foreach (GameObject xxx in WorldObjectsReferenceHelper.Current().Humans)
            {
                if (xxx != null)
                {
                    Human currentlyChecked = xxx.GetComponent<Human>();
                    if (currentlyChecked != null && currentlyChecked != this && currentlyChecked.Tribe != this.Tribe && currentlyChecked.IsAlive)
                    {
                        float distance = Vector2.Distance(transform.position, currentlyChecked.transform.position);
                        if (distance < minDistanceSoFar && distance < AgressionRange)
                        {
                            minDistanceSoFar = distance;
                            humanTarget = new HumanTarget(currentlyChecked, minDistanceSoFar);
                        }
                    }
                }
            }

        }

        public void ToDamangedHuman()
        {
            currentRenderer.color = Color.red;
        }

        public void ResetSprite()
        {
            if (Tribe != null)
            {
                currentRenderer.color = base.Tribe.Color;
            }
            else
            {
                currentRenderer.color = Color.white;
            }
        }
        float whenToGoWhite = 0;
        private bool needToGoNormal = false;
        private float WhenToThinkAboutDecision = 0;

        private void BackToNormalColorIfNeeded()
        {
            if (needToGoNormal && ( whenToGoWhite < Time.time  || ShowNormalAfterLevelUpTime < Time.time))
            {
                needToGoNormal = false;
                ResetSprite();
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
                actionChosen = ChooseAction();
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
                case ActionChosen.ReactToGathering: ReactToGather(); break;
                default: throw new NotImplementedException(String.Format("This action is not implemnted yet : {0}", actionChosen.ToString()));
            }
        }

        private float gatheringResetTimer = 0;

        private void ReactToGather()
        {
            if (mineralTarget != null && mineralTarget.Mineral != null && mineralTarget.Mineral)
            {
                if (mineralTarget.Distance < 2)
                {
                    if (Time.time > gatheringResetTimer)
                    {
                        Mine(mineralTarget.Mineral);
                        gatheringResetTimer = Time.time + 2f; // every 2 seconds 1 peace of somehting. Need to add a level up. for this skill.
                    }

                    if (base.Tribe != null &&( resourcesCarried.Copper > 2 || resourcesCarried.Iron > 2 || resourcesCarried.Wood > 2))
                    {
                        DepositMatsToTribe();
                    }
                }
                else
                {
                    MoveTo(mineralTarget.Mineral.transform.position);
                }
            }
            else
            {
                WhenToThinkAboutDecision = Time.time - 1; // reset the choose on who to rape.
            }
        }

        private void DepositMatsToTribe()
        {
            this.Tribe.Stockpitle.AddResources(this.resourcesCarried);
            this.resourcesCarried.Empty();
        }

        private void MoveTo(Vector2 target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, MovementSpeed());
        }

        private void Mine(Mineral mineral)
        {
            switch (mineral.Type)
            {
                case MineralType.Copper: mineral.Amount--; resourcesCarried.Copper++; break;
                case MineralType.Iron: mineral.Amount--; resourcesCarried.Iron++; break;
                case MineralType.Unknown: throw new NotImplementedException("Impossible!");
            }

        }

        private void ReactToEnemy()
        {
            if (humanTarget != null && humanTarget.Human != null && humanTarget.Human.IsAlive)
            {
                if (humanTarget.Distance < ((base.EquippedWeapon ?? base.BaseWeapon).Range))
                {
                    if (Time.time > attackResetTimer)
                    {
                        base.Attack(humanTarget.Human);
                        attackResetTimer = Time.time + (base.EquippedWeapon ?? base.BaseWeapon).RewindPeriod;
                    }
                }
                else
                {
                    MoveTo(humanTarget.Human.transform.position);
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

        private ActionChosen ChooseAction()
        {
            if (Role == Role.Fighter)
            {
                this.AgressionRange = float.MaxValue;
                GetHumanTarget();
                if (humanTarget != null) { return ActionChosen.ReactToEnemy; }
            }
            else if (Role == Role.Worker)
            {
                FindMineralTarget();
                if (mineralTarget != null) { return ActionChosen.ReactToGathering; }

            }
            return ActionChosen.Wander;
        }

        private void FindMineralTarget()
        {
            mineralTarget = null;
            float minDistanceSoFar = float.MaxValue;
            foreach (var obj in WorldObjectsReferenceHelper.Current().Minerals)
            {
                if (obj != null)
                {
                    Mineral currentlyChecked = obj.GetComponent<Mineral>();
                    if (currentlyChecked != null && currentlyChecked != this)
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
        }

        public override void ShowHit()
        {
            this.ToDamangedHuman();
            whenToGoWhite = Time.time + 0.15f;
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
