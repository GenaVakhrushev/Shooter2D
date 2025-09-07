using Shooter.Inventory.Items;

namespace Shooter.Inventory.Slots
{
    public class InventorySlot
    {
        public Item Item { get; private set; }
        
        public bool TryPutItem(Item itemToPut)
        {
            if (CanPutItem(itemToPut))
            {
                PutItem(itemToPut);
                return true;
            }

            return false;
        }
        
        public void PutItem(Item itemToPut) => Item = itemToPut;
        
        public bool CanPutItem(Item itemToPut) => Item == null;
        
    }
}