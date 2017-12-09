namespace GenericRpg.Business.Model
{
    public enum WeaponType {
        Natural
    }

    public class Weapon
    {
        public double Range { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public WeaponType WeaponType { get; set; }
    }
}