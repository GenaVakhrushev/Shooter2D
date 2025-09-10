using DI.Attributes;
using Shooter.Factories;
using Shooter.Inventory.Items.Weapons;
using UnityEngine;

namespace Shooter.Enemies.Spawn
{
    public abstract class EnemySpawner
    {
        private readonly EnemiesFactory enemiesFactory;

        protected EnemySpawner(EnemiesFactory enemiesFactory)
        {
            this.enemiesFactory = enemiesFactory;
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
                Weapon = (Weapon)enemyConfig.WeaponConfig.CreateItem(),
            };
            var view = enemiesFactory.GetEnemyView(model);
            var controller = new EnemyController();
            
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