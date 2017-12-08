using GenericRpg.Business.Manager;
using GenericRpg.Business.Model;
using GenericRpg.Business.Model.Reports;
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
        long universalTimeUnit = 0;
        public int statingPopulationNumber = 0;
        Drawer drawer;

        DateTime StartingTime = DateTime.MinValue;
        public Form1()
        {
            InitializeComponent();
            existenceManager = new ExistenceManager();
            random = new Random();
            drawer = new Drawer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblGenerating.Visible = false;
            lblGenratingLabelOnly.Visible = false;
        }

        private async void btnGenerateUniverse_Click(object sender, EventArgs e)
        {
            // this is meant to create the universe
            int number = 0;
            if (!int.TryParse(txtEntitiesNumber.Text, out number))
            {
                MessageBox.Show(String.Format("Please insert a valid integer {0} - {1}", 0, int.MaxValue));
                return;
            }
            universalTimeUnit = 0;
            lblGenerating.Visible = true;
            lblGenratingLabelOnly.Visible = true;

            statingPopulationNumber = number;
            IProgress<int> ProgressForNumberCreated = new Progress<int>( v => {
                ShowUpdate(v);
            });
            currentExistence =  await existenceManager.CreateLimitedOnePhaseExistence(number, 50,50,500,500, ProgressForNumberCreated);

            lblGenerating.Visible = false;
            lblGenratingLabelOnly.Visible = false;
            StartingTime = DateTime.Now;

        }
     
        private void ShowUpdate(int value)
        {
            lblGenerating.Text = string.Format("{0}/{1}", value, statingPopulationNumber);
        }


        System.Threading.Timer currentTimer = null;
        private void btnAction_Click(object sender, EventArgs e)
        {
            int timeIntervalToSet = 0;
            if (!int.TryParse(txtEntitiesNumber.Text, out timeIntervalToSet))
            {
                MessageBox.Show(String.Format("Please insert a valid integer for the interval {0} - {1}", 0, int.MaxValue));
                return;
            }
        
            if ( currentExistence == null)
            {
                MessageBox.Show(String.Format("FirstGenerateAnUniverse", 0, int.MaxValue));
                return;
            }
            if (currentTimer != null)
            {
                StopTimer();
            }
            IProgress<int> theprogress = new Progress<int>(i =>
            {
                timerTick();
            });
            if (currentTimer == null)
            {
                currentTimer = new System.Threading.Timer(o => TimerTickForProgreess(theprogress), null, 0, timeIntervalToSet);
            }
            else
            {
                ChangeTimerInterval(timeIntervalToSet);
            }
          //  currentTimer.Interval = timeIntervalToSet;
          //  currentTimer.Tick += TimerTickEvent;        
          //  currentTimer.Enabled = true;
        }

        private void ChangeTimerInterval(int interval) {
            currentTimer.Change(0, interval);
        }

      
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           // DrawWithGraphics(e.Graphics);
        }

     
        public UniversalTimeUnitPassReport LastReport { get; set; }

        private void TimerTickForProgreess(IProgress<int> progress)
        {
            progress.Report(0);
        }

        Bitmap theBitmap = null;
        private async void timerTick()
        {
            
          int alive =  currentExistence.Phases.First().GetNumberAlive();
            LastReport = currentExistence.MakeAnUniversalTimeUntiPass();

            int dead = statingPopulationNumber - alive;
            lblAlvie.Text = alive.ToString();
            lblDead.Text = dead.ToString();

            universalTimeUnit++;
            lblUniversalTime.Text = universalTimeUnit.ToString();
            lblUTUSecond.Text = String.Format("{0:0.000}",(universalTimeUnit / (DateTime.Now - StartingTime).TotalSeconds));


            if (theBitmap == null) { theBitmap = new Bitmap(500, 500); }
            using (Graphics graphs = Graphics.FromImage(theBitmap))
            {
                if (currentExistence != null || currentExistence.Phases != null || currentExistence.Phases.FirstOrDefault() != null)
                {
                    await drawer.DrawWithGraphics( new List<UniversalObject>(currentExistence.Phases.First().UniversalObjectsInside), graphs, cbShowAttacks.Checked, LastReport);
                }
           
            }

            panel1.BackgroundImage = (Bitmap)theBitmap.Clone();
            panel1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void uselessmethod1(object sender, EventArgs e)
        {


        }

        private void uslessMethod2(object sender, EventArgs e)
        {

        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            if (currentTimer != null)
            {
                StopTimer();
            }
          
        }

        private void StopTimer() {
            currentTimer.Change(0, int.MaxValue);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
