using Shooter.Controllers;
using Shooter.Inventory.Items;
using Shooter.Inventory.Slots;

namespace Shooter.Inventory.Core
{
    public class InventoryController : Controller<InventoryModel, InventoryView>
    {
        public ItemConfig GetItemConfig(int slotIndex)
        {
            var slots = Model.Slots;

            if (slotIndex < 0 || slotIndex >= slots.Count)
            {
                return null;
            }

            return slots[slotIndex].GetItemConfig();
        }

        public void AddSlot(InventorySlot slot)
        {
            Model.Slots.Add(slot);
        }

        public void AddItem(ItemConfig itemConfig)
        {
            foreach (var slotConfig in Model.Slots)
            {
                if (slotConfig.TryPutItem(itemConfig))
                {
                    return;
                }
            }
        }
    }
}