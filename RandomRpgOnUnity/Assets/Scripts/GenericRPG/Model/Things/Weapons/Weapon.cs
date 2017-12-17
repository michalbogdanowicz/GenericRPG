namespace GenericRpg.Business.Model
{
    public enum WeaponType
    {
        Natural
    }

    public class Weapon
    {
        public float Range { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public WeaponType WeaponType { get; set; }
        private int standardAttacksPerSecond;
        public int StandardAttacksPerSecond
        {
            get { return standardAttacksPerSecond; }
            set
            {
                standardAttacksPerSecond = value;
                timePause = (1f / (float)value);
            }
        }
        protected float timePause;
        public float RewindPeriod
        {
            get
            {
                return timePause;
            }
        }
    }
}