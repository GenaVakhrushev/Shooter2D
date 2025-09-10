using DI.Installers;
using Shooter.Enemies;
using Shooter.Enemies.Spawn;
using Shooter.GameManagement;
using UnityEngine;

namespace Shooter.Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private bool autoStart = true;
        [SerializeField] private EnemyConfig[] enemyConfigs;
        [SerializeField] private SpawnParameters enemiesSpawnParameters;

        public override void InstallBindings()
        {
            container.Bind(_ => new GameManager(enemyConfigs, enemiesSpawnParameters));
        }

        private void Awake()
        {
            if (autoStart)
            {
                container.Resolve<GameManager>().StartGame();
            }
        }
    }
}