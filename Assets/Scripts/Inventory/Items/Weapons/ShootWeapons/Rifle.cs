using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Rifle : ShootWeapon
    {
        protected override void Shoot()
        {
            Debug.Log("Rifle shoot");
        }
    }
}