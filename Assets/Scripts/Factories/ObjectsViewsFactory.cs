using System.Collections.Generic;
using DI;
using DI.Attributes;
using Shooter.Inventory.Slots;
using Shooter.Views;
using TopDownShooter.Configs;
using UnityEngine.Pool;

namespace Shooter.Factories
{
    public class ObjectsViewsFactory
    {
        private readonly Dictionary<ObjectConfig, ObjectPool<ObjectView>> pools = new();
        
        [Inject] private DIContainer container;

        public View GetView(ObjectConfig config) => GetOrCreatePool(config).Get();

        public void ReturnView(ObjectView objectView) => GetOrCreatePool(objectView.ObjectConfig).Release(objectView);

        private ObjectPool<ObjectView> GetOrCreatePool(ObjectConfig config)
        {
            return pools.TryGetValue(config, out var pool)
                ? pool
                : new ObjectPool<ObjectView>(CreateObjectView, ActionOnGetObjectView, ActionOnReleaseObjectView);

            ObjectView CreateObjectView()
            {
                var instance = container.Instantiate(config.ViewPrefab);
                
                instance.SetObjectConfig(config);

                return instance;
            }

            void ActionOnGetObjectView(ObjectView objectView)
            {
                objectView.gameObject.SetActive(true);
            }

            void ActionOnReleaseObjectView(ObjectView objectView)
            {
                objectView.gameObject.SetActive(false);
            }
        }
    }
}