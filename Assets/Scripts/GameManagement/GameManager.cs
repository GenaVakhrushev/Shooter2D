using System.Threading;
using System.Threading.Tasks;
using DI.Attributes;
using Shooter.Enemies;
using Shooter.Enemies.Spawn;
using Shooter.Factories;
using Shooter.Player;
using Shooter.Services;
using UnityEngine;

namespace Shooter.GameManagement
{
    public class GameManager
    {
        [Inject] private PlayerService playerService;
        [Inject] private EnemiesFactory enemiesFactory;
        [Inject] private EnemiesService enemiesService;

        private PlayerView playerView;
        
        private readonly EnemyConfig[] enemyConfigs;
        private readonly SpawnParameters enemiesSpawnParameters;

        private OffScreenEnemySpawner spawner;
        private CancellationTokenSource spawnTokenSource;

        public GameManager(EnemyConfig[] enemyConfigs, SpawnParameters enemiesSpawnParameters)
        {
            this.enemyConfigs = enemyConfigs;
            this.enemiesSpawnParameters = enemiesSpawnParameters;
        }

        public void StartGame()
        {
            playerView = playerService.SpawnPlayerView();
            spawner = new OffScreenEnemySpawner(enemiesFactory, enemiesService, 1, 1, 3, 3);
            
            StartSpawn();
        }

        private async void StartSpawn()
        {
            for (var i = 0; i < enemiesSpawnParameters.StartSpawnCount; i++)
            {
                SpawnEnemy();
            }

            spawnTokenSource = new CancellationTokenSource();

            while (!spawnTokenSource.IsCancellationRequested)
            {
                await Task.Delay((int)(enemiesSpawnParameters.SpawnSecondsInterval * 1000));

                if (!Application.isPlaying)
                {
                    return;
                }

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var enemyController = spawner.SpawnEnemy(GetRandomConfig());
                
            enemyController.SetAttackTarget(playerView.transform);
        }
        
        private EnemyConfig GetRandomConfig() => enemyConfigs[Random.Range(0, enemyConfigs.Length)];
    }
}