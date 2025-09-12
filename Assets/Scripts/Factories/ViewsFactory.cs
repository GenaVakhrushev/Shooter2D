using System.Collections.Generic;
using DI;
using DI.Attributes;
using Shooter.Views;
using UnityEngine.Pool;

namespace Shooter.Factories
{
    public abstract class ViewsFactory
    {
        [Inject] private DIContainer container;
        
        private readonly Dictionary<string, View> viewPrefabs = new();
        private readonly Dictionary<string, ObjectPool<View>> pools = new();

        protected void AddViewPrefab(string key, View view) => viewPrefabs.Add(key, view);
        protected View GetView(string key) => GetOrCreatePool(key).Get();
        protected void ReturnView(string key, View view) => GetOrCreatePool(key).Release(view);

        private ObjectPool<View> GetOrCreatePool(string key)
        {
            if (pools.TryGetValue(key, out var pool))
            {
                return pool;
            }
            
            pool = new ObjectPool<View>(CreateObjectView, ActionOnGetObjectView, ActionOnReleaseObjectView);
            pools.Add(key, pool);

            return pool;
            
            View CreateObjectView() => container.Instantiate(viewPrefabs[key]);
            void ActionOnGetObjectView(View view) => view.gameObject.SetActive(true);
            void ActionOnReleaseObjectView(View view)
            {
                view.SetParentView(null);
                view.gameObject.SetActive(false);
            }
        }
    }
}