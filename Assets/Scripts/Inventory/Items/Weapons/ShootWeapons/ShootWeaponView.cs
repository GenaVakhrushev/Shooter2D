using DI.Attributes;
using Shooter.Bullets;
using Shooter.Factories;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class ShootWeaponView : ItemView
    {
        [SerializeField] private Transform launchPoint;
        
        [Inject] private BulletsFactory bulletsFactory;
        
        private ShootWeapon shootWeapon;
        
        public override void SetItem(Item item)
        {
            if (shootWeapon != null)
            {
                shootWeapon.Shot -= ShootWeapon_OnShot;
            }
            
            base.SetItem(item);
            shootWeapon = Item as ShootWeapon;

            if (shootWeapon != null)
            {
                shootWeapon.Shot += ShootWeapon_OnShot;
            }
        }

        private void ShootWeapon_OnShot(float angle)
        {
            var bulletConfig = shootWeapon.BulletConfig;
            var bullet = new Bullet(bulletConfig.BulletName);
            var bulletView = bulletsFactory.GetBulletView(bullet);
            var direction = ((Vector2)launchPoint.up).Rotate(angle);
            var bulletViewTransform = bulletView.transform;
            
            bullet.SetDamage(shootWeapon.Damage);
            
            bulletViewTransform.position = launchPoint.position;
            bulletViewTransform.rotation = launchPoint.rotation;
            
            bulletView.SetParentView(this);
            bulletView.Launch(direction, shootWeapon.BulletLaunchSpeed);
        }
    }
}