using Shooter.Inventory.Slots;
using UnityEngine;

namespace Shooter.Inventory.Core
{
    [CreateAssetMenu(fileName = nameof(InventoryConfig), menuName = "Configs/" +nameof(InventoryConfig), order = 0)]
    public class InventoryConfig : ScriptableObject
    {
        public InventorySlot[] Slots;
    }
}