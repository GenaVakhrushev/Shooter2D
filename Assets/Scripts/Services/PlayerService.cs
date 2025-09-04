using DI;
using DI.Attributes;
using Shooter.Player;
using UnityEngine;

namespace Shooter.Services
{
    public class PlayerService
    {
        private readonly PlayerConfig config;
        private readonly Transform spawnPoint;
        
        [Inject] private DIContainer container;
        [Inject] private ShooterInputActions inputActions;

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
            var controller = new PlayerController(inputActions, config.InventoryConfig);
            
            controller.SetView(view);
            controller.SetModel(config.PlayerModel);

            var viewTransform = view.transform;
            viewTransform.position = spawnPoint.position;
            viewTransform.rotation = spawnPoint.rotation;

            return view;
        }
    }
}