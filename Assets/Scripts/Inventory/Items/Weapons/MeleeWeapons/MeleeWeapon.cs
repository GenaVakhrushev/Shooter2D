using Unity.Plastic.Newtonsoft.Json.Serialization;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public abstract class MeleeWeapon : Weapon
    {
        public float Range { get; private set; }
        public float WaveSpeed { get; private set; }
        
        public event Action Striked;

        protected MeleeWeapon(string name, float damage, float range, float waveSpeed) : base(name, damage)
        {
            Range = range;
            WaveSpeed = waveSpeed;
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