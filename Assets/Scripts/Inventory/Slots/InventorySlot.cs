using Shooter.Inventory.Items;

namespace Shooter.Inventory.Slots
{
    public class InventorySlot
    {
        public ItemConfig ItemConfig { get; private set; }

        public bool TryPutItem(ItemConfig itemConfig)
        {
            if (CanPutItem(itemConfig))
            {
                PutItem(itemConfig);
                return true;
            }

            return false;
        }
        public virtual void PutItem(ItemConfig itemConfig) => ItemConfig = itemConfig;
        
        public virtual bool CanPutItem(ItemConfig itemConfig) => ItemConfig == null;
        
    }
}