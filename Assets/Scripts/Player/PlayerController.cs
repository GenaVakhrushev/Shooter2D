using Shooter.Controllers;
using Shooter.Inventory.Core;
using Shooter.Inventory.Hand;
using Shooter.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Player
{
    public class PlayerController : Controller<PlayerModel, PlayerView>
    {
        private readonly ShooterInputActions inputActions;
        private readonly Camera mainCamera;
     
        private readonly InventoryController inventoryController;
        private readonly HandController handController;
        
        public PlayerController(ShooterInputActions inputActions, InventoryConfig inventoryConfig)
        {
            this.inputActions = inputActions;
            mainCamera = Camera.main;
            
            inventoryController = new InventoryController();
            inventoryController.SetModel(new InventoryModel());
            foreach (var slot in inventoryConfig.Slots)
            {
                inventoryController.AddSlot(slot);
            }
            
            handController = new HandController();
            handController.SetModel(new HandModel());

            EventFunctions.Tick += Update;
            
            inputActions.Inventory.Slot1.performed += Slot1_OnPerformed;
            inputActions.Inventory.Slot2.performed += Slot2_OnPerformed;
            inputActions.Inventory.Slot3.performed += Slot3_OnPerformed;
            
            inputActions.Player.Use.performed += Use_OnPerformed;
        }

        public override void SetView(PlayerView view)
        {
            base.SetView(view);
            
            inventoryController.SetView(View.GetComponentInChildren<InventoryView>());
            handController.SetView(View.GetComponentInChildren<HandView>());
        }

        private void Update()
        {
            HandleMovement();
            HandleLooking();
        }

        private void Slot1_OnPerformed(InputAction.CallbackContext context) => SelectSlot(0);

        private void Slot2_OnPerformed(InputAction.CallbackContext context) => SelectSlot(1);

        private void Slot3_OnPerformed(InputAction.CallbackContext context) => SelectSlot(2);

        private void Use_OnPerformed(InputAction.CallbackContext context)
        {
            handController.UseItem();
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
        
        private void SelectSlot(int index)
        {
            var itemConfig = inventoryController.GetItemConfig(index);
            var currentItem = handController.GetHeldItemConfig();

            if (itemConfig == currentItem)
            {
                handController.DropItem();
            }
            else
            {
                handController.TakeItem(itemConfig);
            }
        }
    }
}