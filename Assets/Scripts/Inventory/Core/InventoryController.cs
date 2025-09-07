using Shooter.Controllers;
using Shooter.Inventory.Items;
using Shooter.Inventory.Slots;

namespace Shooter.Inventory.Core
{
    public class InventoryController : Controller<InventoryModel, InventoryView>
    {
        public Item GetItem(int slotIndex)
        {
            var slots = Model.Slots;

            if (slotIndex < 0 || slotIndex >= slots.Count)
            {
                return null;
            }

            return slots[slotIndex].Item;
        }

        public void AddSlot(InventorySlot slot)
        {
            Model.Slots.Add(slot);
        }

        public void AddItem(Item item)
        {
            foreach (var slotConfig in Model.Slots)
            {
                if (slotConfig.TryPutItem(item))
                {
                    return;
                }
            }
        }
    }
}