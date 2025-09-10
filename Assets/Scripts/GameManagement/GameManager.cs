using System.Threading;
using System.Threading.Tasks;
using DI.Attributes;
using Shooter.Enemies;
using Shooter.Enemies.Spawn;
using Shooter.Factories;
using Shooter.Services;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.GameManagement
{
    public class GameManager
    {
        [Inject] private PlayerService playerService;
        [Inject] private EnemiesFactory enemiesFactory;

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
            playerService.SpawnPlayerView();
            spawner = new OffScreenEnemySpawner(enemiesFactory, 2, 2);
            
            StartSpawn();
        }

        private async void StartSpawn()
        {
            for (var i = 0; i < enemiesSpawnParameters.StartSpawnCount; i++)
            {
                spawner.SpawnEnemy(GetRandomConfig());
            }

            spawnTokenSource = new CancellationTokenSource();

            while (!spawnTokenSource.IsCancellationRequested)
            {
                await Task.Delay((int)(enemiesSpawnParameters.SpawnSecondsInterval * 1000));

                if (!Application.isPlaying)
                {
                    return;
                }
                
                spawner.SpawnEnemy(GetRandomConfig());
            }
        }

        private EnemyConfig GetRandomConfig() => enemyConfigs[Random.Range(0, enemyConfigs.Length)];
    }
}