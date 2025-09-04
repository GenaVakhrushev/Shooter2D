using DI.Installers;
using Shooter.Configs;
using Shooter.Services;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView playerPrefab;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private Transform spawnPoint;
        
        public override void InstallBindings()
        {
            container.Bind(_ =>
            {
                var service = new PlayerService(playerConfig, spawnPoint);
                
                service.SetPrefab(playerPrefab);

                return service;
            });
        }
    }
}