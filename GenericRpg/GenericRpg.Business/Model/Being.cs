using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
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
        public bool NeedToEat { get; set; }

        public decimal MassToEatDaily { get; set; }

        public bool IsALivingBeing { get; set; }
        public Weapon CurrentWeapon { get; set; }
        private Random random;
        private LivingTarget CurrentAttackTarget {get;set;}

        public Being(Point position)
        {
            random = new Random();
            base.Position = position;
            CurrentWeapon = new Weapon();
            CurrentWeapon.Damage = random.Next(1, 2);
            CurrentWeapon.Range = random.Next(5,10);
            CurrentWeapon.Name = "Hands";

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
            base.Attributes.PossibleMovementUnitWithOneUniversalMovement = 4;

            IsALivingBeing = true;
        }

        public override ActionReport DoAnythingYoucanDoOrWantTo(Phase phase)
        {
           return DecideWhatTodo(phase);
        }

        int sameTargetIteration = 0;
        int currentActionLimitation = 0;

        /// <summary>
        /// The phase calls this, after an universal moment of time has passed
        /// </summary>
        /// <param name="phase"></param>
        public ActionReport DecideWhatTodo(Phase phase)
        {
            if ( currentActionLimitation == 0) { currentActionLimitation = random.Next(4, 8); }
             // AM I alive?
            if (this.IsAlive)
            {
                // Where am I?
                //Phase phase =  FindOutInWhichPhaseIAm();
                // Is there anything I can attack? 

                if (CurrentAttackTarget == null || !CurrentAttackTarget.Being.IsAlive || sameTargetIteration > currentActionLimitation)
                {
                    CurrentAttackTarget = phase.GetLivingBeingClosestTo(this);
                    sameTargetIteration = 0;
                    currentActionLimitation = random.Next(4, 8);
                }
                else
                {
                    sameTargetIteration++;
                }
                
                if (CurrentAttackTarget == null)
                {
                  return  MoveAround(phase);
                }
                else
                {
                    // Am In range?
                    if (AmIInRange(CurrentAttackTarget))
                    {
                     return   Attack(CurrentAttackTarget);
                    }
                    else
                    {
                     return   MoveToward(CurrentAttackTarget);
                    }
                }
            }
            else
            {
                if (this.IsALivingBeing)
                {
                    // if you are dead you keep losing 10 hp
                    // when you have less thatn 200, it will decompose.
                    this.Attributes.Durability -= 10;
                    return new ActionReport();
                }
                else
                {
                    // do nothing
                    return new ActionReport();
                }
            }
        }

        private ActionReport MoveAround(Phase phase)
        {
            if (base.Attributes.PossibleMovementUnitWithOneUniversalMovement == null) { throw new InvalidOperationException("This being cannot move, as he has no movement."); }
            int newX = Position.X;
            int newY = Position.Y;
            // movement can be easily improved
            switch (random.Next(1, 4))
            {
                case 1: newX = base.Position.X + base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
                case 2: newX = base.Position.X - base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
                case 3: newY = base.Position.Y + base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
                case 4: newY = base.Position.Y - base.Attributes.PossibleMovementUnitWithOneUniversalMovement.Value; break;
                default: throw new InvalidOperationException("Not Expected!");
            }

            Point wantedPoint = new Point(newX, newY);
            if (phase.CanIMoveInThePointPlace(this,wantedPoint))
            {
                this.Position = wantedPoint;
            }

            return new ActionReport();
        }

        private ActionReport MoveToward(LivingTarget target)
        {
            // move towards the enemy some how.
            if (base.Attributes.PossibleMovementUnitWithOneUniversalMovement == null) { throw new InvalidOperationException("This being cannot move, as he has no movement."); }
            int movementMade = 0;
            int newX = 0;
            int newY = 0;
            while (movementMade < base.Attributes.PossibleMovementUnitWithOneUniversalMovement)
            {
                newX = CalculatePositionX(target);
                newY = CalculatePositionY(target);
                Point wantedPoint = new Point(newX, newY);
                if (target.PhaseOfExistance.CanIMoveInThePointPlace(this, wantedPoint))
                {
                    this.Position = wantedPoint;
                }
              
                movementMade++;
            }
            return new ActionReport();
        }
        private int CalculatePositionY(LivingTarget target)
        {
            if (Position.Y < target.Being.Position.Y)
            {
                return base.Position.Y + 1;
            }
            else if (Position.Y == target.Being.Position.Y)
            {
                return base.Position.Y;
            }
            else
            {
                return base.Position.Y - 1;
            }
        }
        private int CalculatePositionX(LivingTarget target)
        {
            if (Position.X < target.Being.Position.X)
            {
                return base.Position.X + 1;
            }
            else if (Position.X == target.Being.Position.X)
            {
                return base.Position.X;
            }
            else
            {
                return base.Position.X - 1;
            }
        }

        private AttackReport Attack(LivingTarget livingTarget)
        {
            AttackReport report = new AttackReport();
            report.attackPath = new Tuple<Point, Point>(this.Position, livingTarget.Being.Position);
            // calcualte
            Being target = livingTarget.Being;
            // first if it hits...
            // % based on attributes
            // of myself and 
            int chanceOfHitting = (base.Attributes.Reactivity.Value - (target.Attributes.Reactivity.Value / 2));
            chanceOfHitting = chanceOfHitting < 25 ? 25 : chanceOfHitting;
            // bonuses and other stuff?!?!?!!???
            //not now it seems.. // TODO weapon bonuses, classees, if implemented, and other stufff

            int attempt = random.Next(99) + 1;
            if (attempt < chanceOfHitting)
            {
                // HIT
                int dmg = (base.Attributes.Strength.Value / 10 + CurrentWeapon.Damage);
                dmg = (int)Math.Round(dmg * ((double)random.Next(1, 10) / (double)10));
                target.Attributes.Durability -= dmg;
                report.Hits = true;
            }
            else
            {
                // MISS
                report.Hits = false;
            }

            return report;
        }

        private bool AmIInRange(LivingTarget target)
        {
            if (CurrentWeapon.Range >= target.Distance)
            {
                return true;
            }
            return false;
        }


    }
}
