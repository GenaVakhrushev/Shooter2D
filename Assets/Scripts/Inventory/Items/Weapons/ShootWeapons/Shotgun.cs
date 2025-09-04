using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Shotgun : ShootWeapon
    {
        [SerializeField] private int bulletsCount = 5;
        [SerializeField] private float spreadAngle = 30;
        
        protected override void Shoot()
        {
            Debug.Log("Shotgun shoot");
        }
    }
}