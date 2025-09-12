using Shooter.Bullets;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public abstract class ShootWeaponConfig : WeaponConfig
    {
        public BulletConfig BulletConfig;
        public float BulletLaunchSpeed;
    }
}