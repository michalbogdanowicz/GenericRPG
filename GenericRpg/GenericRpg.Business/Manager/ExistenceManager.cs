using GenericRpg.Business.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Manager
{
    public class ExistenceManager
    {
        Random random = new Random();
      public  EventHandler ReportForCreation;

        public Existence CreateExistence()
        {
            Existence ex = new Existence();

            return ex;
        }

        public async Task<Existence> CreateLimitedOnePhaseExistence(int numberOfEntities, int minX, int minY, int maxX, int maxY, IProgress<int> progress)
        {
            Existence existence = new Existence();
            if (maxX < 1) { throw new ArgumentException("Max X can't be less than 1"); }
            if (maxY < 1) { throw new ArgumentException("Max Y can't be less than 1"); }
            if (minX < 1) { throw new ArgumentException("Min X can't be less than 1"); }
            if (minY < 1) { throw new ArgumentException("Min Y can't be less than 1"); }

            existence.Phases.Add(new Phase("Real", PhaseType.Real, new Point(minX, minY), new Point(maxX, maxY)));
            await  Task.Run(() =>
               {
                   for (int i = 0; i < numberOfEntities; i++)
                   {
                       int posX = 0;
                       int posY = 0;
                       do
                       {
                           posX = random.Next(minX, maxX);
                           posY = random.Next(minX, maxY);
                       }
                       while (existence.Phases.First().UniversalObjectsInside.Where(j => DoOverlap(j.Position, new Point(posX, posY))).FirstOrDefault() != null);

                       existence.Phases.First().UniversalObjectsInside.Add(new Being(new Point(posX, posY)));
                       progress.Report(i + 1);
                   }
               }
               );

            return existence;
        }

        private bool DoOverlap(Point caller, Point adversary)
        {
            Point callerLeftUpper = new Point(caller.X - 2, caller.Y - 2);
            Point adversaryLeftUpper = new Point(adversary.X - 2, adversary.Y - 2);

            Rectangle callerRectangle = new Rectangle(callerLeftUpper.X, callerLeftUpper.Y, 4, 4);
            Rectangle adversayRectangle = new Rectangle(adversaryLeftUpper.X, adversaryLeftUpper.Y, 4, 4);
            return callerRectangle.IntersectsWith(adversayRectangle);
        }
    }
}
