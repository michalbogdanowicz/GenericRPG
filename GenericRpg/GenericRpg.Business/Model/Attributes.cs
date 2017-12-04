using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRpg.Business.Model
{
    /// <summary>
    /// The attributes of a being , some might be missing, as it might not apply!
    /// </summary>
    class Attributes
    {
        /// <summary>
        /// The might!
        /// </summary>
        public int? Strength { get; set; }
        /// <summary>
        /// A measure of general ability and speed
        /// </summary>
        public int? Reactivity { get; set; }
        /// <summary>
        /// A measure of attention
        /// </summary>
        public int? Mindfullness { get; set; }
        /// <summary>
        /// A measure of intelligence
        /// </summary>
        public int? Intelligence { get; set; }
        /// <summary>
        /// A measure of social skillls
        /// </summary>
        public int? Personality { get; set; }
        /// <summary>
        /// In Meters
        /// </summary>
        public decimal? Heigt { get; set; }
        /// <summary>
        /// In Kg
        /// </summary>
        public decimal? Weight { get; set; }
        

    }
}
