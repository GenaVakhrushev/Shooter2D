using System;
using Shooter.Bullets;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public abstract class ShootWeapon : Weapon
    {
        public BulletConfig BulletConfig { get; private set; }
        public float BulletLaunchSpeed { get; private set; }

        protected ShootWeapon(string name, float damage, BulletConfig bulletConfig, float bulletLaunchSpeed) : base(name, damage)
        {
            BulletConfig = bulletConfig;
            BulletLaunchSpeed = bulletLaunchSpeed;
        }

        public event Action<float> Shot;

        protected void Shoot(float angle = 0)
        {
            Shot?.Invoke(angle);
        }
    }
}