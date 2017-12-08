using GenericRpg.Business.Model;
using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.WindowsGUI
{
    class Drawer
    {
        SolidBrush RedBrush;
        SolidBrush GreenBrush;
        SolidBrush Graybush;

        public Drawer()
        {
            GreenBrush = new SolidBrush(Color.Green);
            Graybush = new SolidBrush(Color.Gray);
            RedBrush = new SolidBrush(Color.Red);
        }


        private Brush GetBrush(UniversalObject universalObject)
        {
            Brush brush = null;
            if (universalObject is Being)
            {
                Being being = universalObject as Being;
                if (being.IsAlive)
                {
                    return GreenBrush;
                }
                else
                {
                    return RedBrush;
                }
            }
            else
            {
                return Graybush;
            }

        }

        public async Task DrawWithGraphics(List<UniversalObject> objects, Graphics graphics, bool showAttacks, UniversalTimeUnitPassReport report)
        {
            await Task.Run(() =>
            {
                graphics.Clear(Color.White);
                foreach (UniversalObject universalObject in objects)
                {
                    Brush brush = GetBrush(universalObject);
                    Point[] points = new Point[4];
                    int drawingOffSet = 2;
                    points[0] = new Point(universalObject.Position.X - drawingOffSet, universalObject.Position.Y - drawingOffSet);
                    points[1] = new Point(universalObject.Position.X - drawingOffSet, universalObject.Position.Y + drawingOffSet);
                    points[2] = new Point(universalObject.Position.X + drawingOffSet, universalObject.Position.Y + drawingOffSet);
                    points[3] = new Point(universalObject.Position.X + drawingOffSet, universalObject.Position.Y - drawingOffSet);
                    graphics.FillPolygon(brush, points);
                }
                if (showAttacks)
                {
                    foreach (var attacks in report.Attacks)
                    {
                        graphics.DrawLine(Pens.Black, attacks.Item1, attacks.Item2);
                    }
                }




            }
  );


        }

    }
}
