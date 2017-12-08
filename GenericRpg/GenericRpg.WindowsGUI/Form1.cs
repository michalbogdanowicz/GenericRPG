using GenericRpg.Business.Manager;
using GenericRpg.Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericRpg.WindowsGUI
{
    public partial class Form1 : Form
    {
        ExistenceManager existenceManager;
        Existence currentExistence;
        Random random;
        public int statingPopulationNumber = 0;
        public Form1()
        {
            InitializeComponent();
            existenceManager = new ExistenceManager();
            random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenerateUniverse_Click(object sender, EventArgs e)
        {
            // this is meant to create the universe
            int number = 0;
            if (!int.TryParse(txtEntitiesNumber.Text, out number))
            {
                MessageBox.Show(String.Format("Please insert a valid integer {0} - {1}", 0, int.MaxValue));
                return;
            }
            currentExistence = existenceManager.CreateLimitedOnePhaseExistence(number, 50,50,500,500);
            statingPopulationNumber = number;
        }




        private void btnAction_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) { timer1.Enabled = false; }
            int timeIntervalToSEt = 0;
            if (!int.TryParse(txtEntitiesNumber.Text, out timeIntervalToSEt))
            {
                MessageBox.Show(String.Format("Please insert a valid integer for the interval {0} - {1}", 0, int.MaxValue));
                return;
            }
            timer1.Interval = timeIntervalToSEt;
            if ( currentExistence == null)
            {
                MessageBox.Show(String.Format("FirstGenerateAnUniverse", 0, int.MaxValue));
                return;
            }
            timer1.Enabled = true;
        }


        private Brush GetBrush(UniversalObject universalObject) {
            Brush brush = null;
            if (universalObject is Being)
            {
                Being being = universalObject as Being;
                if (being.IsAlive)
                {
                    brush = new SolidBrush(Color.Green);
                }
                else
                {
                    brush = new SolidBrush(Color.Red);
                }
            }
            else
            {
                brush = new SolidBrush(Color.Gray);
            }
            return brush;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (currentExistence != null && currentExistence.Phases != null)
            {
                Phase currentPhase = currentExistence.Phases.FirstOrDefault();
                if (currentPhase != null)
                {

                    foreach (UniversalObject universalObject in currentPhase.UniversalObjectsInside)
                    {
                        Brush brush = GetBrush(universalObject);
                        Point[] points = new Point[4];
                        int drawingOffSet = 5;
                        points[0] = new Point(universalObject.Position.X, universalObject.Position.Y);
                        points[1] = new Point(universalObject.Position.X, universalObject.Position.Y + drawingOffSet);
                        points[2] = new Point(universalObject.Position.X + drawingOffSet, universalObject.Position.Y + drawingOffSet);
                        points[3] = new Point(universalObject.Position.X + drawingOffSet, universalObject.Position.Y);
                        e.Graphics.FillPolygon(brush, points);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
          int alive =  currentExistence.Phases.First().GetNumberAlive();
            int dead = statingPopulationNumber - alive;
            lblAlvie.Text = alive.ToString();
            lblDead.Text = dead.ToString();
            currentExistence.MakeAnUniversalTimeUntiPass();
            panel1.Refresh();
        }

        private void uselessmethod1(object sender, EventArgs e)
        {


        }

        private void uslessMethod2(object sender, EventArgs e)
        {

        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
