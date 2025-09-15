using Shooter.Factories;
using Shooter.HP;
using Shooter.Inventory.Items.Weapons;
using Shooter.Services;
using UnityEngine;

namespace Shooter.Enemies.Spawn
{
    public abstract class EnemySpawner
    {
        private readonly EnemiesFactory enemiesFactory;
        private readonly EnemiesService enemiesService;

        protected EnemySpawner(EnemiesFactory enemiesFactory, EnemiesService enemiesService)
        {
            this.enemiesFactory = enemiesFactory;
            this.enemiesService = enemiesService;
        }

        public EnemyController SpawnEnemy(EnemyConfig enemyConfig)
        {
            var model = new EnemyModel()
            {
                EnemyName = enemyConfig.EnemyName,
                MoveSpeed = enemyConfig.MoveSpeed,
                RotationSpeed = enemyConfig.RotationSpeed,
                AttackDistance = enemyConfig.AttackDistance,
                AttackLookAngle = enemyConfig.AttackLookAngle,
                AttacksPerSecond = enemyConfig.AttacksPerSecond,
                HPModel = new HPModel(enemyConfig.HPConfig.InitialHP, enemyConfig.HPConfig.MaxHP),
                Weapon = (Weapon)enemyConfig.WeaponConfig.CreateItem(),
            };
            var view = enemiesFactory.GetEnemyView(model);
            var controller = enemiesService.GetController<EnemyController>();
            
            controller.SetModel(model);
            controller.SetView(view);

            view.transform.position = GetSpawnPosition();
            view.transform.rotation = GetSpawnRotation();

            return controller;
        }

        protected abstract Vector3 GetSpawnPosition();
        protected abstract Quaternion GetSpawnRotation();
    }
}