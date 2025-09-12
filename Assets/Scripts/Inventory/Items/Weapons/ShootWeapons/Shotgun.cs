using Shooter.Bullets;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Shotgun : ShootWeapon
    {
        private int bulletsCount;
        private float spreadAngle;

        public Shotgun(string name, float damage, BulletConfig bulletConfig, float bulletLaunchSpeed, int bulletsCount, float spreadAngle) : base(name, damage, bulletConfig, bulletLaunchSpeed)
        {
            this.bulletsCount = bulletsCount;
            this.spreadAngle = spreadAngle;
        }

        public override void Use()
        {
            var halfSpreadAngle = spreadAngle / 2;

            for (var i = 0; i < bulletsCount; i++)
            {
                var angle = Random.Range(-halfSpreadAngle, halfSpreadAngle);
                
                Shoot(angle);
            }
        }
    }
}