namespace Shooter.Inventory.Items.Weapons
{
    public abstract class Weapon : Item
    {
        private float damage;

        protected Weapon(string name, float damage) : base(name)
        {
            this.damage = damage;
        }
    }
}