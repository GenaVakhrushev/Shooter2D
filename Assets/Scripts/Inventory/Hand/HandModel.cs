using System;
using Shooter.Inventory.Items;

namespace Shooter.Inventory.Hand
{
    public class HandModel
    {
        public event Action<Item> ItemChanged;
        
        public Item HeldItem { get; private set; }

        public void SetItem(Item item)
        {
            HeldItem = item;
            
            ItemChanged?.Invoke(HeldItem);
        }

        public void ResetItem()
        {
            SetItem(null);
        }
    }
}