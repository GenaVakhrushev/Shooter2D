using Shooter.Views;

namespace Shooter.Inventory.Items
{
    public class ItemView : View
    {
        public Item Item { get; private set; }

        public virtual void SetItem(Item item)
        {
            Item = item;
        }
    }
}