using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model.Reports
{
    public class UniversalTimeUnitPassReport
    {
        public int AliveBeings { get; set; }
        public int AllBeings { get; set; }
        public List<Tuple<Point, Point>> Attacks { get; set; }

        public UniversalTimeUnitPassReport() {
            this.AliveBeings = 0;
            this.AllBeings = 0;
            this.Attacks = new List<Tuple<Point, Point>>();
        }
    }
}
