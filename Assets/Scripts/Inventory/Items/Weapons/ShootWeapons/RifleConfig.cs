using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    [CreateAssetMenu(fileName = nameof(RifleConfig), menuName = "Configs/Inventory/Items/Weapons/ShootWeapons/" + nameof(RifleConfig), order = 0)]
    public class RifleConfig : ShootWeaponConfig
    {
        public override Item CreateItem() => new Rifle(ItemName, Damage, BulletConfig, BulletLaunchSpeed);
    }
}