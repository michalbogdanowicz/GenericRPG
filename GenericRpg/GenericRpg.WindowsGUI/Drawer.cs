using GenericRpg.Business.Model;
using GenericRpg.Business.Model.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<Bitmap> Draw(CancellationToken cancel, List<UniversalObject> objects, bool showAttacks, UniversalTimeUnitPassReport report, Bitmap otherBitmap)
        {
            Bitmap bitmap = (Bitmap)otherBitmap.Clone();
            await Task.Run(() =>
            {
                try
                {

                    cancel.ThrowIfCancellationRequested();
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        cancel.ThrowIfCancellationRequested();
                        graphics.Clear(Color.White);
                        List<RectangleF> greenStuff = new List<RectangleF>();
                        List<RectangleF> redStuff = new List<RectangleF>();
                        List<RectangleF> grayStuff = new List<RectangleF>();
                        foreach (UniversalObject universalObject in objects)
                        {
                            cancel.ThrowIfCancellationRequested();
                            Brush brush = GetBrush(universalObject);
                            Point[] points = new Point[4];
                            int drawingOffSet = 2;
                            Point backedUpPoint = new Point(universalObject.Position.X - drawingOffSet, universalObject.Position.Y - drawingOffSet);
                            RectangleF rect = new RectangleF(backedUpPoint, new Size(4, 4));
                            if (universalObject is Being)
                            {
                                Being being = universalObject as Being;
                                if (being.IsAlive)
                                {
                                    greenStuff.Add(rect);
                                }
                                else
                                {
                                    redStuff.Add(rect);
                                }
                            }
                            else
                            {
                                grayStuff.Add(rect);
                            }


                        }
                        if (showAttacks)
                        {
                            foreach (var attacks in report.Attacks)
                            {
                                cancel.ThrowIfCancellationRequested();
                                graphics.DrawLine(Pens.Black, attacks.Item1, attacks.Item2);
                            }
                        }
                        cancel.ThrowIfCancellationRequested();
                        if (greenStuff.Count != 0) { graphics.FillRectangles(GreenBrush, greenStuff.ToArray()); }
                        if (redStuff.Count != 0) { graphics.FillRectangles(RedBrush, redStuff.ToArray()); }
                        if (grayStuff.Count != 0) { graphics.FillRectangles(Graybush, grayStuff.ToArray()); }
                    }

                }
                catch (OperationCanceledException)
                {

                }
            }


  ); return bitmap;



        }


    }
}
