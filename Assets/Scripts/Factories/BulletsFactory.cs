using Shooter.Bullets;
using Shooter.Databases;

namespace Shooter.Factories
{
    public class BulletsFactory : ViewsFactory
    {
        public BulletsFactory(BulletsDatabase bulletsDatabase)
        {
            foreach (var bulletConfig in bulletsDatabase.BulletConfigs)
            {
                AddViewPrefab(bulletConfig.BulletName, bulletConfig.BulletView);
            }
        }

        public BulletView GetBulletView(Bullet bullet)
        {
            var bulletView = (BulletView)GetView(bullet.Name);
            
            bulletView.SetBullet(bullet);

            return bulletView;
        }

        public void ReturnView(BulletView bulletView) => ReturnView(bulletView.Bullet.Name, bulletView);
    }
}