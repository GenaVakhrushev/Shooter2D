using System;
using Shooter.Inventory.Items;
using UnityEngine;

namespace Shooter.Inventory.Slots
{
    [Serializable]
    public class InventorySlot
    {
        [SerializeField] private ItemConfig itemConfig;

        public ItemConfig GetItemConfig() => itemConfig;
        
        public bool TryPutItem(ItemConfig itemConfigToPut)
        {
            if (CanPutItem(itemConfigToPut))
            {
                PutItem(itemConfigToPut);
                return true;
            }

            return false;
        }
        
        public void PutItem(ItemConfig itemConfigToPut) => itemConfig = itemConfigToPut;
        
        public bool CanPutItem(ItemConfig itemConfigToPut) => itemConfig == null;
        
    }
}