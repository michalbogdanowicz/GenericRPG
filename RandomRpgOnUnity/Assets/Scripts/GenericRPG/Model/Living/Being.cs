﻿using Assets.Scripts.GenericRPG.Global;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GenericRpg.Business.Model.Living
{


    public class Being : UniversalObject
    {

        public bool IsAlive
        {
            get
            {
                if (IsALivingBeing)
                {
                    return (base.Attributes.Durability ?? -1) > 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsDecomposed
        {
            get
            {
                if (!IsALivingBeing) { throw new InvalidOperationException("Impossible to have a non living being decomposed"); }
                return base.Attributes.Durability <= -200;
            }
        }

        protected void Decompose()
        {
            this.Attributes.Durability -= 1;
        }
        public bool NeedToEat { get; set; }

        public decimal MassToEatDaily { get; set; }

        public bool IsALivingBeing { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Weapon BaseWeapon { get; set; }


        public override void Start()
        {
            base.Start();
            BaseWeapon = new Weapon(PresetWeapons.Fists);

        
            base.Attributes.Heigt = 1.80m;// m stands for decimal not for meters.
            base.Attributes.Weight = 70;
            // default values for attributes of beings.
            base.Attributes.Intelligence = 10;
            base.Attributes.Mindfullness = 10;
            base.Attributes.Personality = 10;
            base.Attributes.Reactivity = 10;
            base.Attributes.Strength = 10;
            base.Attributes.Durability = 10;
            base.Attributes.Speed = 0.02f;
            IsALivingBeing = true;
            //this.Tribe = Tribe.NoTribe;
        }

        public override void DoAnythingYoucanDoOrWantTo()
        {
            DecideWhatTodo();
        }



        public virtual void DecideWhatTodo()
        {

        }

        private void MoveAround()
        {

        }

        private void MoveToward()
        {

        }


        public void Attack(UniversalObject target)
        {
            // first if it hits...
            // % based on attributes
            // of myself and 
            int chanceOfHitting = (base.Attributes.Reactivity.Value - (target.Attributes.Reactivity.Value / 2));
            chanceOfHitting = chanceOfHitting < 25 ? 25 : chanceOfHitting;
            // bonuses and other stuff?!?!?!!???
            //not now it seems.. // TODO weapon bonuses, classees, if implemented, and other stufff

            int attempt = UnityEngine.Random.Range(1, 100);
            if (attempt < chanceOfHitting)
            {
                // HIT
                int dmg = (base.Attributes.Strength.Value / 10 + (EquippedWeapon ?? BaseWeapon).Damage);
                dmg = (int)Math.Round(dmg * ((double)UnityEngine.Random.Range(1, 10) / (double)10));
                target.Attributes.Durability -= dmg;

                Being item = target as Being;
                if (item != null && item.IsAlive)
                {
                    this.Attributes.AddXp(item.Attributes.XpWorth);
                    this.ShowLevelUp();
                }
                target.ShowHit();
                //  report.Hits = true;
            }
            else
            {
                // MISS
                ///  report.Hits = false;
            }

            //  return report;
        }

        protected virtual void ShowLevelUp()
        {
       
        }
    }
}
