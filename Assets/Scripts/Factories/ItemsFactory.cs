using Shooter.Databases;
using Shooter.Inventory.Items;

namespace Shooter.Factories
{
    public class ItemsFactory : ViewsFactory
    {
        public ItemsFactory(ItemsDatabase itemsDatabase)
        {
            foreach (var itemConfig in itemsDatabase.ItemConfigs)
            {
                AddViewPrefab(itemConfig.ItemName, itemConfig.ItemView);
            }
        }

        public ItemView GetItemView(Item item)
        {
            var itemView = (ItemView)GetView(item.Name);
            
            itemView.SetItem(item);
            
            return itemView;
        }

        public void ReturnView(ItemView itemView) => ReturnView(itemView.Item.Name, itemView);
    }
}