using System;
using Shooter.Damage.Bullets;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public abstract class ShootWeapon : Weapon
    {
        private BulletConfig bulletConfig;
        private float bulletLaunchSpeed;

        protected ShootWeapon(string name, float damage, BulletConfig bulletConfig, float bulletLaunchSpeed) : base(name, damage)
        {
            this.bulletConfig = bulletConfig;
            this.bulletLaunchSpeed = bulletLaunchSpeed;
        }

        public event Action<float> Shot;

        public BulletConfig GetConfig() => bulletConfig;
        public float GetBulletLaunchSpeed() => bulletLaunchSpeed;

        protected void Shoot(float angle = 0)
        {
            Shot?.Invoke(angle);
        }
    }
}