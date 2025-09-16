using System;
using DI;
using DI.Attributes;
using Shooter.HP;
using Shooter.Player;
using UnityEngine;

namespace Shooter.Services
{
    public class PlayerService
    {
        private PlayerController playerController;
        
        private readonly PlayerConfig config;
        private readonly Transform spawnPoint;
        
        [Inject] private DIContainer container;
        [Inject] private ShooterInputActions inputActions;

        public PlayerView ViewPrefab { get; private set; }
        public PlayerView CurrentView => playerController.View;

        public event Action PlayerDied;

        public PlayerService(PlayerConfig config, Transform spawnPoint)
        {
            this.config = config;
            this.spawnPoint = spawnPoint;
        }

        public void SetPrefab(PlayerView view)
        {
            ViewPrefab = view;
        }

        public void CreatePlayer()
        {
            playerController = GetOrCreateController();
            var view = playerController.View ? playerController.View : container.Instantiate(ViewPrefab);
            var model = new PlayerModel
            {
                MoveSpeed = config.MoveSpeed,
                RotationSpeed = config.RotationSpeed,
                HPModel = new HPModel(config.HPConfig.InitialHP, config.HPConfig.MaxHP),
            };
            
            playerController.SetView(view);
            playerController.SetModel(model);

            var viewTransform = view.transform;
            viewTransform.position = spawnPoint.position;
            viewTransform.rotation = spawnPoint.rotation;
        }

        private PlayerController GetOrCreateController()
        {
            if (playerController == null)
            {
                playerController = new PlayerController(inputActions, config.InventoryConfig);
                playerController.Died += PlayerControllerOnDied;
            }

            return playerController;
        }

        private void PlayerControllerOnDied()
        {
            PlayerDied?.Invoke();
        }
    }
}