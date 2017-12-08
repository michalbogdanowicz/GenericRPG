using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    public enum PhaseType {
        Real,
        CraddleOfCreation,
        EnergyBased
    }

    public class Phase
    {
        public Phase(string name,PhaseType phaseType, Point mimPoint, Point maxPoint)
        {
            this.Name = name;
            this.Type = phaseType;
            UniversalObjectsInside = new List<UniversalObject>();
            ObjectToRemoveFromExistanceOnNextOccasion = new List<UniversalObject>();
            this.MinPoint = mimPoint;
            this.MaxPoint = maxPoint;
        }

        public string Name { get; set; }
        public List<UniversalObject> UniversalObjectsInside { get; set; }
        public PhaseType Type { get; set; }

        public Point MinPoint { get; set; }
        public Point MaxPoint { get; set; }

        public List<UniversalObject> ObjectToRemoveFromExistanceOnNextOccasion { get; set; }

        public UniversalTimeUnitPassReport MankeAnUniversalTimeUntiPass()
        {
            UniversalTimeUnitPassReport universalTimeUnitPassReport = new UniversalTimeUnitPassReport();

            if (ObjectToRemoveFromExistanceOnNextOccasion.Count != 0)
            {
                foreach (UniversalObject universalObject in ObjectToRemoveFromExistanceOnNextOccasion)
                {
                    UniversalObjectsInside.Remove(universalObject);
                }
            }

            foreach (UniversalObject universalObject in UniversalObjectsInside)
            {
                Being being = universalObject as Being;
                if (being != null)
                {
                    if (being.IsALivingBeing )
                    {
                        if (being.IsAlive) {
                            universalTimeUnitPassReport.AliveBeings++;
                        }
                        if (being.IsDecomposed)
                        {
                            ObjectToRemoveFromExistanceOnNextOccasion.Add(universalObject);
                        }
                    }
                    

                }
          
                  ActionReport actionReport = universalObject.DoAnythingYoucanDoOrWantTo(this);
                AttackReport attackReport = actionReport as AttackReport;
                if (attackReport != null) {
                    universalTimeUnitPassReport.Attacks.Add(attackReport.attackPath);
                }
            }

            return universalTimeUnitPassReport;
        }

        internal LivingTarget GetLivingBeingClosestTo(Being callerBeing)
        {
            Being closest = null;
            double closestDistance = double.MaxValue;
            foreach (UniversalObject uObj in UniversalObjectsInside)
            {
                if (uObj != null &&  uObj is Being ) {
                    Being being = uObj as Being;
                    if (being != callerBeing && being.IsAlive) {
                        double distance = GetDistanceBetween(callerBeing.Position, being.Position);
                        if (distance < closestDistance) {
                            closestDistance = distance;
                            closest = being;
                        }
                    }
                    
                }
       
            }


            if (closest != null) {
                LivingTarget target = new LivingTarget();
                target.Being = closest;
                target.Distance = closestDistance;
                target.PhaseOfExistance = this;
                return target;
            }

            return null;

        }

        private double GetDistanceBetween(Point p1, Point p2) {
            return Math.Sqrt(Math.Pow((p1.X - p2.X) ,2) + Math.Pow((p1.Y - p2.Y), 2));
        }

        public int GetNumberAlive()
        {
            int count = 0;
            foreach (UniversalObject uObj in UniversalObjectsInside)
            {
                if (uObj != null && uObj is Being)
                {
                    Being being = uObj as Being;
                  if (being.IsAlive) { count++; }
                }
            }
            return count;
        }

        internal bool CanIMoveInThePOintPlace(Being being, Point wantedPoint)
        {
            if (this.UniversalObjectsInside.Where(i =>DoOverlap( i.Position , wantedPoint)).FirstOrDefault() != null)
            {
                return false;
            }
            return true;
     
        }

        private bool DoOverlap(Point lowerBound, Point adversary)
        {

            Point uppperBound = new Point(lowerBound.X + 4, lowerBound.Y + 4);

            if (adversary.X >= lowerBound.X && adversary.X <= uppperBound.X &&
            adversary.Y >= lowerBound.Y && adversary.Y <= uppperBound.Y) { return true; }


            return false;
        }
    }
}
