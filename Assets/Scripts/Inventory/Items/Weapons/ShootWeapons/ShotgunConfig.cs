using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.ShootWeapons
{
    [CreateAssetMenu(fileName = nameof(ShotgunConfig), menuName = "Configs/Inventory/Items/Weapons/ShootWeapons/" + nameof(ShotgunConfig), order = 0)]
    public class ShotgunConfig : ShootWeaponConfig
    {
        public int BulletsCount;
        public float SpreadAngle;

        public override Item CreateItem() => new Shotgun(ItemName, Damage, BulletConfig, BulletLaunchSpeed, BulletsCount, SpreadAngle);
    }
}