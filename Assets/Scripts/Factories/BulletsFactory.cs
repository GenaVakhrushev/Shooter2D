using System.Collections.Generic;
using DI;
using DI.Attributes;
using Shooter.Damage.Bullets;
using Shooter.Databases;
using UnityEngine.Pool;

namespace Shooter.Factories
{
    public class BulletsFactory
    {
        private readonly BulletsDatabase bulletsDatabase;
        private readonly Dictionary<string, BulletView> viewPrefabs = new();
        private readonly Dictionary<string, ObjectPool<BulletView>> pools = new();
        
        [Inject] private DIContainer container;

        public BulletsFactory(BulletsDatabase bulletsDatabase)
        {
            this.bulletsDatabase = bulletsDatabase;
            foreach (var bulletConfig in this.bulletsDatabase.BulletConfigs)
            {
                viewPrefabs.Add(bulletConfig.BulletName, bulletConfig.BulletView);
            }
        }

        public BulletView GetBulletView(Bullet bullet) => GetOrCreatePool(bullet).Get();

        public void ReturnView(BulletView bulletView) => GetOrCreatePool(bulletView.Bullet).Release(bulletView);

        private ObjectPool<BulletView> GetOrCreatePool(Bullet bullet)
        {
            var bulletName = bullet.Name;
            
            if (pools.TryGetValue(bulletName, out var pool))
            {
                return pool;
            }
            
            pool = new ObjectPool<BulletView>(CreateObjectView, ActionOnGetObjectView, ActionOnReleaseObjectView);
            pools.Add(bulletName, pool);

            return pool;

            BulletView CreateObjectView()
            {
                var instance = container.Instantiate(viewPrefabs[bulletName]);
                
                instance.SetBullet(bullet);

                return instance;
            }

            void ActionOnGetObjectView(BulletView bulletView)
            {
                bulletView.gameObject.SetActive(true);
            }

            void ActionOnReleaseObjectView(BulletView bulletView)
            {
                bulletView.gameObject.SetActive(false);
            }
        }
    }
}