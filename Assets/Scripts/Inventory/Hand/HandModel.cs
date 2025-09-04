using System;
using Shooter.Inventory.Items;

namespace Shooter.Inventory.Hand
{
    public class HandModel
    {
        public event Action<ItemConfig> ItemConfigChanged;
        
        public ItemConfig HeldItemConfig { get; private set; }

        public void SetItemConfig(ItemConfig itemConfig)
        {
            HeldItemConfig = itemConfig;
            
            ItemConfigChanged?.Invoke(HeldItemConfig);
        }

        public void ResetItem()
        {
            SetItemConfig(null);
        }
    }
}