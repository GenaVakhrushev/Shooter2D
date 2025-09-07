using Shooter.Damage.Bullets;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    public class Rifle : ShootWeapon
    {
        public Rifle(string name, float damage, BulletConfig bulletConfig, float bulletLaunchSpeed) : base(name, damage, bulletConfig, bulletLaunchSpeed)
        {
        }

        public override void Use()
        {
            Shoot();
        }
    }
}