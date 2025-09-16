using System;
using System.Threading;
using System.Threading.Tasks;
using DI.Attributes;
using Shooter.Enemies;
using Shooter.Enemies.Spawn;
using Shooter.Factories;
using Shooter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shooter.GameManagement
{
    public class GameManager
    {
        [Inject] private ShooterInputActions inputActions;
        [Inject] private PlayerService playerService;
        [Inject] private EnemiesFactory enemiesFactory;
        [Inject] private EnemiesService enemiesService;
        
        private readonly EnemyConfig[] enemyConfigs;
        private readonly SpawnParameters enemiesSpawnParameters;

        private OffScreenEnemySpawner spawner;
        private CancellationTokenSource spawnTokenSource;
        
        public event Action GameLost;

        public GameManager(EnemyConfig[] enemyConfigs, SpawnParameters enemiesSpawnParameters)
        {
            this.enemyConfigs = enemyConfigs;
            this.enemiesSpawnParameters = enemiesSpawnParameters;
        }

        [Inject]
        private void Initialize()
        {
            playerService.PlayerDied += PlayerServiceOnPlayerDied;
        }

        public void StartGame()
        {
            playerService.CreatePlayer();
            spawner = new OffScreenEnemySpawner(enemiesFactory, enemiesService, 1, 1, 3, 3);
            inputActions.Enable();
            
            StartSpawn();
        }

        public void Restart()
        {
            enemiesService.KillAll();
            StartGame();
        }

        private async void StartSpawn()
        {
            for (var i = 0; i < enemiesSpawnParameters.StartSpawnCount; i++)
            {
                SpawnEnemy();
            }

            spawnTokenSource = new CancellationTokenSource();

            while (true)
            {
                await Task.Delay((int)(enemiesSpawnParameters.SpawnSecondsInterval * 1000));

                if (!Application.isPlaying || spawnTokenSource.IsCancellationRequested)
                {
                    return;
                }

                SpawnEnemy();
            }
        }

        private void PlayerServiceOnPlayerDied()
        {
            LoseGame();
        }

        private void LoseGame()
        {
            StopSpawn();
            
            enemiesService.StopEnemies();
            inputActions.Disable();
            
            GameLost?.Invoke();
        }

        private void StopSpawn()
        {
            spawnTokenSource.Cancel(false);
        }

        private void SpawnEnemy()
        {
            var enemyController = spawner.SpawnEnemy(GetRandomConfig());
            
            enemyController.SetAttackTarget(playerService.CurrentView.transform);
        }

        private EnemyConfig GetRandomConfig() => enemyConfigs[Random.Range(0, enemyConfigs.Length)];
    }
}