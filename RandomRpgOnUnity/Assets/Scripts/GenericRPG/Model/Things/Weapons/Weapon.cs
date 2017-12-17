namespace GenericRpg.Business.Model
{
    public enum WeaponType
    {
        Natural
    }

    public enum PresetWeapons
    {
        Fists,
        Bow,
        Sword
    }

    public class Weapon
    {
        public int Damage { get; set; }
        public float Range { get; set; }
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

        public Weapon(int damage, float range, string name, WeaponType weaponType, int sAttacksPerSecond)
        {
            this.Damage = damage;
            this.Range = range;
            this.Name = name;
            this.WeaponType = WeaponType;
            this.standardAttacksPerSecond = sAttacksPerSecond;
        }

        public Weapon(PresetWeapons presetWeapons)
        {
            switch (presetWeapons)
            {
                case PresetWeapons.Fists:
                    Damage = 1;
                    Range = 1f;
                    StandardAttacksPerSecond = 2;
                    Name = "Hands/Fists";
                    break;
                case PresetWeapons.Bow:
                    Damage = 5;
                    Range = 10f;
                    StandardAttacksPerSecond = 2;
                    Name = "Bow";

                    break;
                case PresetWeapons.Sword:
                    Damage = 3;
                    Range = 1.1f;
                    StandardAttacksPerSecond = 2;
                    Name = "Sword";

                    break;
                default: throw new System.NotImplementedException("Impossible to find the specified weapon, please specify");
            }
        }

    }
}