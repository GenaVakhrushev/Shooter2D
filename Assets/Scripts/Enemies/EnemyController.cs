using Shooter.Controllers;
using Shooter.Inventory.Hand;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.Enemies
{
    public class EnemyController : Controller<EnemyModel, EnemyView>
    {
        private readonly HandController handController;
        
        private Transform attackTarget;
        private float lastAttackTime;

        public EnemyController()
        {
            handController = new HandController();
            
            EventFunctions.Tick += Update;
        }

        public override void SetModel(EnemyModel model)
        {
            base.SetModel(model);

            lastAttackTime = 0;

            var handModel = new HandModel();
            
            handModel.SetItem(Model.Weapon);
            handController.SetModel(handModel);
        }

        public override void SetView(EnemyView view)
        {
            base.SetView(view);
            
            handController.SetView(view.GetComponentInChildren<HandView>());
        }

        private void Update()
        {
            if (attackTarget == null || View == null || Model == null)
            {
                return;
            }
            
            var viewTransform = View.transform;
            var targetPosition = attackTarget.position;
            var toTarget = targetPosition - viewTransform.position;

            if (toTarget.sqrMagnitude <= Model.AttackDistance && Vector2.Angle(toTarget, viewTransform.up) <= Model.AttackLookAngle)
            {
                if (Time.time >= lastAttackTime + 1f / Model.AttacksPerSecond)
                {
                    handController.UseItem();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                //Movement
                var moveDirection = toTarget.normalized;
                viewTransform.position += moveDirection * (Model.MoveSpeed * Time.deltaTime);
            
                //Rotation
                viewTransform.LookAt(targetPosition, Model.RotationSpeed);
            }
        }

        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }
    }
}