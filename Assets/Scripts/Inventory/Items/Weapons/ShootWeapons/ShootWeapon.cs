using System;
using Shooter.Damage.Bullets;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public abstract class ShootWeapon : Weapon
    {
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private float bulletLaunchSpeed;
        
        public event Action<float> Shot;

        public BulletConfig GetConfig() => bulletConfig;
        public float GetBulletLaunchSpeed() => bulletLaunchSpeed;

        protected void Shoot(float angle = 0)
        {
            Shot?.Invoke(angle);
        }
    }
}