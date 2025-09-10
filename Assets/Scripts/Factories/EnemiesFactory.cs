using Shooter.Databases;
using Shooter.Enemies;

namespace Shooter.Factories
{
    public class EnemiesFactory : ViewsFactory
    {
        public EnemiesFactory(EnemiesDatabase bulletsDatabase)
        {
            foreach (var enemyConfig in bulletsDatabase.EnemyConfigs)
            {
                AddViewPrefab(enemyConfig.EnemyName, enemyConfig.EnemyView);
            }
        }

        public EnemyView GetEnemyView(EnemyModel enemyModel)
        {
            var enemyView = (EnemyView)GetView(enemyModel.EnemyName);
            
            enemyView.SetEnemyModel(enemyModel);

            return enemyView;
        }

        public void ReturnView(EnemyView enemyView) => ReturnView(enemyView.EnemyModel.EnemyName, enemyView);
    }
}