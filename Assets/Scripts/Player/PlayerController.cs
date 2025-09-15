using Shooter.Controllers;
using Shooter.HP;
using Shooter.Inventory.Core;
using Shooter.Inventory.Hand;
using Shooter.Inventory.Slots;
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
        private readonly HPController hpController;
        
        public PlayerController(ShooterInputActions inputActions, InventoryConfig inventoryConfig)
        {
            this.inputActions = inputActions;
            mainCamera = Camera.main;
            
            inventoryController = new InventoryController();
            inventoryController.SetModel(new InventoryModel());
            foreach (var slotConfig in inventoryConfig.SlotConfigs)
            {
                var slot = new InventorySlot();
                var itemConfig = slotConfig.ItemConfig;

                if (itemConfig != null)
                {
                    slot.PutItem(slotConfig.ItemConfig.CreateItem());
                }
                
                inventoryController.AddSlot(slot);
            }
            
            handController = new HandController();
            handController.SetModel(new HandModel());

            hpController = new HPController();

            EventFunctions.Tick += Update;
            EventFunctions.FixedTick += FixedUpdate;
            
            inputActions.Inventory.Slot1.performed += Slot1_OnPerformed;
            inputActions.Inventory.Slot2.performed += Slot2_OnPerformed;
            inputActions.Inventory.Slot3.performed += Slot3_OnPerformed;
            
            inputActions.Player.Use.performed += Use_OnPerformed;
        }

        public override void SetView(PlayerView view)
        {
            if (View != null)
            {
                View.DamageTaken -= ViewOnDamageTaken;
            }
            
            base.SetView(view);
            
            inventoryController.SetView(View.GetComponentInChildren<InventoryView>());
            handController.SetView(View.GetComponentInChildren<HandView>());
            hpController.SetView(View.GetComponentInChildren<HPView>());
            
            View.DamageTaken += ViewOnDamageTaken;
        }

        public override void SetModel(PlayerModel model)
        {
            base.SetModel(model);
            
            hpController.SetModel(Model.HPModel);
        }

        private void ViewOnDamageTaken(float damage)
        {
            hpController.RemoveHP(damage);
        }

        private void Update()
        {
            HandleLooking();
        }
        
        private void FixedUpdate()
        {
            HandleMovement();
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
            View.Move(directionNormalized, Model.MoveSpeed);
        }

        private void HandleLooking()
        {
            var lookPosition = (Vector2)mainCamera.ScreenToWorldPoint(inputActions.Player.Look.ReadValue<Vector2>());
            
            View.LookAt(lookPosition, Model.RotationSpeed);
        }
        
        private void SelectSlot(int index)
        {
            var item = inventoryController.GetItem(index);
            var currentItem = handController.GetHeldItem();

            if (item == currentItem)
            {
                handController.DropItem();
            }
            else
            {
                handController.TakeItem(item);
            }
        }
    }
}