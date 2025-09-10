using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    [CreateAssetMenu(fileName = nameof(SwordConfig), menuName = "Configs/Inventory/Items/Weapons/MeleeWeapons/" + nameof(SwordConfig), order = 0)]
    public class SwordConfig : MeleeWeaponConfig
    {
        public override Item CreateItem() => new Sword(ItemName, Damage, Range);
    }
}