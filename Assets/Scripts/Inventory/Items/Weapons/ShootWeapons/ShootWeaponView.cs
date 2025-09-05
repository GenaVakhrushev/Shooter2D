using DI.Attributes;
using Shooter.Damage.Bullets;
using Shooter.Factories;
using Shooter.Utils;
using TopDownShooter.Configs;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class ShootWeaponView : ItemView
    {
        [SerializeField] private Transform launchPoint;
        
        [Inject] private ObjectsViewsFactory viewsFactory;
        
        private ShootWeapon shootWeapon;
        
        public override void SetObjectConfig(ObjectConfig objectConfig)
        {
            if (shootWeapon != null)
            {
                shootWeapon.Shot -= ShootWeapon_OnShot;
            }
            base.SetObjectConfig(objectConfig);

            shootWeapon = objectConfig.GetObject() as ShootWeapon;

            if (shootWeapon != null)
            {
                shootWeapon.Shot += ShootWeapon_OnShot;
            }
        }

        private void ShootWeapon_OnShot(float angle)
        {
            var bulletView = (BulletView)viewsFactory.GetView(shootWeapon.GetConfig());
            var direction = ((Vector2)launchPoint.up).Rotate(angle);

            var bulletViewTransform = bulletView.transform;
            bulletViewTransform.position = launchPoint.position;
            bulletViewTransform.rotation = launchPoint.rotation;
            
            bulletView.Launch(direction, shootWeapon.GetBulletLaunchSpeed());
        }
    }
}