using Shooter.Inventory.Slots;
using UnityEngine;

namespace Shooter.Inventory.Core
{
    [CreateAssetMenu(fileName = nameof(InventoryConfig), menuName = "Configs/Inventory/" + nameof(InventoryConfig), order = 0)]
    public class InventoryConfig : ScriptableObject
    {
        public InventorySlotConfig[] SlotConfigs;
    }
}