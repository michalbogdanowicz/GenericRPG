using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    public enum PhaseType
    {
        Real,
        CraddleOfCreation,
        EnergyBased
    }

    public class Phase
    {
        public Phase(string name, PhaseType phaseType, Point mimPoint, Point maxPoint)
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
                    if (being.IsALivingBeing)
                    {
                        if (being.IsAlive)
                        {
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
                if (attackReport != null)
                {
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
                if (uObj != null && uObj is Being)
                {
                    Being being = uObj as Being;
                    if (being != callerBeing && being.IsAlive)
                    {
                        double distance = GetDistanceBetween(callerBeing.Position, being.Position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closest = being;
                        }
                    }

                }

            }


            if (closest != null)
            {
                LivingTarget target = new LivingTarget();
                target.Being = closest;
                target.Distance = closestDistance;
                target.PhaseOfExistance = this;
                return target;
            }

            return null;

        }

        private double GetDistanceBetween(Point p1, Point p2)
        {
            // with the normal calculation we just take the points.
            // the things are 3 x 3. that means that 2 has to added adn removed. maybe use rectangles..
            Point callerLeftUpper = new Point(p1.X - 2, p1.Y - 2);
            Point adversaryLeftUpper = new Point(p2.X - 2, p2.Y - 2);

            Rectangle callerRectangle = new Rectangle(callerLeftUpper.X, callerLeftUpper.Y, 4, 4);
            Rectangle adversayRectangle = new Rectangle(adversaryLeftUpper.X, adversaryLeftUpper.Y, 4, 4);

            return Distance(callerLeftUpper.X, adversaryLeftUpper.X, callerLeftUpper.Y, adversaryLeftUpper.Y);
            //Pseudo code
            //int adx1 = adversayRectangle.X;
            //int ady1 = adversayRectangle.Y;
            //int adx2 = adversayRectangle.X + adversayRectangle.Width;
            //int ady2 = adversayRectangle.Y;
            //int adx3 = adversayRectangle.X + adversayRectangle.Width;
            //int ady3 = adversayRectangle.Y + adversayRectangle.Height;
            //int adx4 = adversayRectangle.X;
            //int ady4 = adversayRectangle.Y + adversayRectangle.Height;


            //List<double> distances = new List<double>();

            //int x = callerRectangle.X;
            //int y = callerRectangle.Y;

            //distances.Add(Distance(x, adx1, y, ady1));
            //distances.Add(Distance(x, adx2, y, ady2));
            //distances.Add(Distance(x, adx3, y, ady3));
            //distances.Add(Distance(x, adx4, y, ady4));

            //x = callerRectangle.X + callerRectangle.Width;
            
            //distances.Add(Distance(x, adx1, y, ady1));
            //distances.Add(Distance(x, adx2, y, ady2));
            //distances.Add(Distance(x, adx3, y, ady3));
            //distances.Add(Distance(x, adx4, y, ady4));
            //x = callerRectangle.X + callerRectangle.Width;
            //y = callerRectangle.Y + callerRectangle.Height;
            //distances.Add(Distance(x, adx1, y, ady1));
            //distances.Add(Distance(x, adx2, y, ady2));
            //distances.Add(Distance(x, adx3, y, ady3));
            //distances.Add(Distance(x, adx4, y, ady4));

            //x = callerRectangle.X;
            //y = callerRectangle.Y + callerRectangle.Height;
            //distances.Add(Distance(x, adx1, y, ady1));
            //distances.Add(Distance(x, adx2, y, ady2));
            //distances.Add(Distance(x, adx3, y, ady3));
            //distances.Add(Distance(x, adx4, y, ady4));

            //return distances.Min();
        }

        private double Distance(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2));
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

        internal bool CanIMoveInThePointPlace(Being being, Point wantedPoint)
        {
            if (this.UniversalObjectsInside.Where(i => DoOverlap(i.Position, wantedPoint) && i != being).FirstOrDefault() != null)
            {
                return false;
            }
            return true;

        }

        private bool DoOverlap(Point caller, Point adversary)
        {
            Point callerLeftUpper = new Point(caller.X - 2, caller.Y - 2);
            Point adversaryLeftUpper = new Point(adversary.X - 2, adversary.Y - 2);

            Rectangle callerRectangle = new Rectangle(callerLeftUpper.X, callerLeftUpper.Y, 4, 4);
            Rectangle adversayRectangle = new Rectangle(adversaryLeftUpper.X, adversaryLeftUpper.Y, 4, 4);
            return callerRectangle.IntersectsWith(adversayRectangle);

            //if (adversary.X >= lowerBound.X && adversary.X <= uppperBound.X &&
            //adversary.Y >= lowerBound.Y && adversary.Y <= uppperBound.Y) { return true; }


            //return false;
        }
    }
}
