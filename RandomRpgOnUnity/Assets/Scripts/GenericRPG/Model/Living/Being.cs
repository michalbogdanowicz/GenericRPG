using System;
using System.Collections.Generic;
using System.Text;

namespace GenericRpg.Business.Model.Living
{
    public enum Tribe {
        NoTribe = 0,
        Tribe1 = 1,
        Tribe2 = 2,
        Tribe3 = 3,
        Tribe4 = 4,
        Tribe5 = 5,
        Tribe6 = 6,
        Tribe7 = 7,
        Tribe8 = 8,
        Tribe9 = 9,
        Tribe10 = 10
    }

    public class Being : UniversalObject
    {
        public Tribe Tribe ;

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
        public Weapon CurrentWeapon { get; set; }



        public void Start()
        {
            CurrentWeapon = new Weapon
            {
                Damage = UnityEngine.Random.Range(1, 2),
                Range = UnityEngine.Random.Range(5, 10),
                StandardAttacksPerSecond = UnityEngine.Random.Range(2, 3),
                Name = "Hands"
            };

            base.Attributes = new Attributes();
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


        /// <summary>
        /// The phase calls this, after an universal moment of time has passed
        /// </summary>
        /// <param name="phase"></param>
        public virtual void DecideWhatTodo()
        {

            //if (currentActionLimitation == 0) { currentActionLimitation = random.Next(4, 8); }
            //// AM I alive?
            //if (this.IsAlive)
            //{
            //    // Where am I?
            //    //Phase phase =  FindOutInWhichPhaseIAm();
            //    // Is there anything I can attack? 

            //    if (CurrentAttackTarget == null || !CurrentAttackTarget.Being.IsAlive || sameTargetIteration > currentActionLimitation)
            //    {
            //        CurrentAttackTarget = phase.GetLivingBeingClosestTo(this);
            //        sameTargetIteration = 0;
            //        currentActionLimitation = random.Next(4, 8);
            //    }
            //    else
            //    {
            //        sameTargetIteration++;
            //    }

            //    if (CurrentAttackTarget == null)
            //    {
            //        return MoveAround(phase);
            //    }
            //    else
            //    {
            //        // Am In range?
            //        if (AmIInRange(CurrentAttackTarget))
            //        {
            //            return Attack(CurrentAttackTarget);
            //        }
            //        else
            //        {
            //            return MoveToward(CurrentAttackTarget);
            //        }
            //    }
            //}
            //else
            //{
            //    if (this.IsALivingBeing)
            //    {
            //        // if you are dead you keep losing 10 hp
            //        // when you have less thatn 200, it will decompose.
            //        this.Attributes.Durability -= 10;
            //        return new ActionReport();
            //    }
            //    else
            //    {
            //        // do nothing
            //        return new ActionReport();
            //    }
            //}
        }

        private void MoveAround()
        {
            //if (base.Attributes.PossibleMovementUnitWithOneUniversalMovement == null) { throw new InvalidOperationException("This being cannot move, as he has no movement."); }
            //int newX = Position.X;
            //int newY = Position.Y;
            //// movement can be easily improved
            //switch (random.Next(1, 4))
            //{
            //    case 1: newX = base.Position.X + base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
            //    case 2: newX = base.Position.X - base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
            //    case 3: newY = base.Position.Y + base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
            //    case 4: newY = base.Position.Y - base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
            //    default: throw new InvalidOperationException("Not Expected!");
            //}

            //Point wantedPoint = new Point(newX, newY);
            //if (phase.CanIMoveInThePointPlace(this, wantedPoint))
            //{
            //    this.Position = wantedPoint;
            //}

            //return new ActionReport();
        }

        private void MoveToward()
        {
            //// move towards the enemy some how.
            //if (base.Attributes.PossibleMovementUnitWithOneUniversalMovement == null) { throw new InvalidOperationException("This being cannot move, as he has no movement."); }
            //int movementMade = 0;
            //int newX = 0;
            //int newY = 0;
            //while (movementMade < base.Attributes.PossibleMovementUnitWithOneUniversalMovement)
            //{
            //    newX = CalculatePositionX(target);
            //    newY = CalculatePositionY(target);
            //    Point wantedPoint = new Point(newX, newY);
            //    if (target.PhaseOfExistance.CanIMoveInThePointPlace(this, wantedPoint))
            //    {
            //        this.Position = wantedPoint;
            //    }

            //    movementMade++;
            //}
            //return new ActionReport();
        }


        public void Attack(UniversalObject target)
        {
            //AttackReport report = new AttackReport();
            //report.attackPath = new Tuple<Point, Point>(this.Position, livingTarget.Being.Position);
            //// calcualte
            //Being target = livingTarget.Being;
            // first if it hits...
            // % based on attributes
            // of myself and 
            int chanceOfHitting = (base.Attributes.Reactivity.Value - (target.Attributes.Reactivity.Value / 2));
            chanceOfHitting = chanceOfHitting < 25 ? 25 : chanceOfHitting;
            // bonuses and other stuff?!?!?!!???
            //not now it seems.. // TODO weapon bonuses, classees, if implemented, and other stufff

            int attempt = UnityEngine.Random.Range(1,100);
            if (attempt < chanceOfHitting)
            {
                // HIT
                int dmg = (base.Attributes.Strength.Value / 10 + CurrentWeapon.Damage);
                dmg = (int)Math.Round(dmg * ((double)UnityEngine.Random.Range(1, 10) / (double)10));
                target.Attributes.Durability -= dmg;
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

        //private bool AmIInRange(LivingTarget target)
        //{
        //    if (CurrentWeapon.Range >= target.Distance)
        //    {
        //        return true;
        //    }
        //    return false;
        //}


    }
}
