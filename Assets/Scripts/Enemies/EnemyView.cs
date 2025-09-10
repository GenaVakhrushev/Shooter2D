using Shooter.Views;

namespace Shooter.Enemies
{
    public class EnemyView : View
    {
        public EnemyModel EnemyModel { get; private set; }

        public void SetEnemyModel(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }
    }
}