using DI.Installers;
using Shooter.Player;
using Shooter.Services;
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