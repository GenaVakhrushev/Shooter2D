using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public abstract class MeleeWeapon : Weapon
    {
        private float hitDistance;
        private float range;

        public event Action Striked;

        protected MeleeWeapon(string name, float damage, float hitDistance, float range) : base(name, damage)
        {
            this.hitDistance = hitDistance;
            this.range = range;
        }

        public override void Use()
        {
            Strike();
        }

        private void Strike()
        {
            Striked?.Invoke();
        }
    }
}