namespace Shooter.Inventory.Items.Weapons
{
    public abstract class Weapon : Item
    {
        public float Damage { get; private set; }

        protected Weapon(string name, float damage) : base(name)
        {
            Damage = damage;
        }
    }
}