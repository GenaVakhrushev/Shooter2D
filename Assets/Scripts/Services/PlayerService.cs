using DI;
using DI.Attributes;
using Shooter.Configs;
using Shooter.Controllers;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Services
{
    public class PlayerService
    {
        private readonly PlayerConfig config;
        private readonly Transform spawnPoint;
        
        [Inject] private DIContainer container;

        public PlayerService(PlayerConfig config, Transform spawnPoint)
        {
            this.config = config;
            this.spawnPoint = spawnPoint;
        }

        public PlayerView ViewPrefab { get; private set; }

        public void SetPrefab(PlayerView view)
        {
            ViewPrefab = view;
        }

        public PlayerView SpawnPlayerView()
        {
            var view = container.Instantiate(ViewPrefab);
            var controller = new PlayerController();
            
            container.Inject(controller);
            controller.SetView(view);
            controller.SetModel(config.PlayerModel);

            var viewTransform = view.transform;
            viewTransform.position = spawnPoint.position;
            viewTransform.rotation = spawnPoint.rotation;

            return view;
        }
    }
}