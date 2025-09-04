using System;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public abstract class ShootWeapon : Weapon
    {
        public event Action Shot;
        
        public override void Use()
        {
            Shoot();
            
            Shot?.Invoke();
        }

        protected abstract void Shoot();
    }
}