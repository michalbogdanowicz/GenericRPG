using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GenericRpg.Business.Model
{
    /// <summary>
    /// The attributes of a being , some might be missing, as it might not apply!
    /// </summary>
    public class Attributes
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
        /// a representation of the health status
        /// </summary>
        public int? Durability { get; set; }
        /// <summary>
        /// Thsi is the amount of units 
        /// </summary>
        public float? Speed { get; set; }
        /// <summary>
        /// In Meters
        /// </summary>
        public decimal? Heigt { get; set; }
        /// <summary>
        /// In Kg
        /// </summary>
        public decimal? Weight { get; set; }

        public long Level { get; set; }

        public long Xp { get; set; }

        public void GotALvlKill(long enemyLevel)
        {
            for (int i = 0; i < enemyLevel; i++) {
                LevelUp();
            }
        }
        public void LevelUp()
        {
            /// </summary>
            Strength += 10;
            Reactivity += 10;
            Mindfullness += 10;
            Intelligence += 10;
            Personality += 10;
            Durability += 10;
          //  Speed += 0.02f;
            Level++;
        }
    }
}
