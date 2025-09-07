using System.Collections.Generic;
using DI;
using DI.Attributes;
using Shooter.Databases;
using Shooter.Inventory.Items;
using UnityEngine.Pool;

namespace Shooter.Factories
{
    public class ItemsFactory
    {
        private readonly ItemsDatabase itemsDatabase;
        private readonly Dictionary<string, ItemView> viewPrefabs = new();
        private readonly Dictionary<string, ObjectPool<ItemView>> pools = new();
        
        [Inject] private DIContainer container;

        public ItemsFactory(ItemsDatabase itemsDatabase)
        {
            this.itemsDatabase = itemsDatabase;
            foreach (var itemConfig in this.itemsDatabase.ItemConfigs)
            {
                viewPrefabs.Add(itemConfig.ItemName, itemConfig.ItemView);
            }
        }

        public ItemView GetItemView(Item item) => GetOrCreatePool(item).Get();

        public void ReturnView(ItemView itemView) => GetOrCreatePool(itemView.Item).Release(itemView);

        private ObjectPool<ItemView> GetOrCreatePool(Item item)
        {
            var itemName = item.Name;
            
            if (pools.TryGetValue(itemName, out var pool))
            {
                return pool;
            }
            
            pool = new ObjectPool<ItemView>(CreateObjectView, ActionOnGetObjectView, ActionOnReleaseObjectView);
            pools.Add(itemName, pool);

            return pool;

            ItemView CreateObjectView()
            {
                var instance = container.Instantiate(viewPrefabs[itemName]);
                
                instance.SetItem(item);

                return instance;
            }

            void ActionOnGetObjectView(ItemView itemView)
            {
                itemView.gameObject.SetActive(true);
            }

            void ActionOnReleaseObjectView(ItemView itemView)
            {
                itemView.gameObject.SetActive(false);
            }
        }
    }
}