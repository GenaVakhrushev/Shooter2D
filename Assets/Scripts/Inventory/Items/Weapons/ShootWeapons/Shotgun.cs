using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Shotgun : ShootWeapon
    {
        [SerializeField] private int bulletsCount = 5;
        [SerializeField] private float spreadAngle = 30;

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