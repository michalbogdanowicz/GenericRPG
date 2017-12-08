﻿using GenericRpg.Business.Model;
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
        public Existence CreateExistence()
        {
            Existence ex = new Existence();

            return ex;
        }

        public Existence CreateLimitedOnePhaseExistence(int numberOfEntities, int minX, int minY, int maxX, int maxY)
        {
            Existence existence = new Existence();
            if (maxX < 1) { throw new ArgumentException("Max X can't be less than 1"); }
            if (maxY < 1) { throw new ArgumentException("Max Y can't be less than 1"); }
            if (minX < 1) { throw new ArgumentException("Min X can't be less than 1"); }
            if (minY < 1) { throw new ArgumentException("Min Y can't be less than 1"); }

            existence.Phases.Add(new Phase("Real", PhaseType.Real, new Point(0,0), new Point(maxX,maxY)));
            for (int i = 0; i < numberOfEntities; i++)
            {
                int posX = random.Next(1, maxX);
                int posY = random.Next(1, maxY);

                existence.Phases.First().UniversalObjectsInside.Add(new Being(new Point(posX, posY)));
            }
            return existence;
        }
    }
}