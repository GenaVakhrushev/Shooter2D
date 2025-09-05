using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Rifle : ShootWeapon
    {
        public override void Use()
        {
            Shoot();
        }
    }
}