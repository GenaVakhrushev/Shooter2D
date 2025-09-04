using DI.Attributes;
using Shooter.Models;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Controllers
{
    public class PlayerController
    {
        private ShooterInputActions inputActions;
        private Camera mainCamera;
        
        public PlayerModel Model { get; private set; }
        public PlayerView View { get; private set; }

        [Inject]
        private void Construct(ShooterInputActions inputActions)
        {
            this.inputActions = inputActions;
            mainCamera = Camera.main;
            
            EventFunctions.Tick += Update;
        }
        
        public void SetModel(PlayerModel model)
        {
            Model = model;
        }
        
        public void SetView(PlayerView view)
        {
            View = view;
        }
        
        private void Update()
        {
            HandleMovement();
            HandleLooking();
        }
        
        private void HandleMovement()
        {
            var action = inputActions.Player.Move;

            if (!action.IsPressed())
            {
                return;
            }
            
            var directionNormalized = (Vector3)action.ReadValue<Vector2>().normalized;

            View.transform.position += directionNormalized * (Model.MoveSpeed * Time.deltaTime);
        }

        private void HandleLooking()
        {
            var lookPosition = (Vector2)mainCamera.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());
            var target = View.transform;
            var lookDirection = (lookPosition - (Vector2)target.position).normalized;
            var rotateClockwise = Vector2.Dot(target.right, lookDirection) > 0;
            var rotationAngle = Model.RotationSpeed * Time.deltaTime * (rotateClockwise ? -1 : 1);
            var angleToLookDirection = Vector2.Angle(target.up, lookDirection);
            
            if (rotationAngle > angleToLookDirection)
            {
                rotationAngle = angleToLookDirection;
            }
            
            target.Rotate(0, 0, rotationAngle);
        }
    }
}