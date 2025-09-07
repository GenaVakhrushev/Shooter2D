using Shooter.Inventory.Items;
using UnityEngine;

namespace Shooter.Inventory.Slots
{
    [CreateAssetMenu(fileName = nameof(InventorySlotConfig), menuName = "Configs/Inventory/" + nameof(InventorySlotConfig), order = 0)]
    public class InventorySlotConfig : ScriptableObject
    {
        public ItemConfig ItemConfig;
    }
}