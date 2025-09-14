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
        private bool reachPosition;
        private bool reachRotation;

        public EnemyController()
        {
            handController = new HandController();
            
            EventFunctions.Tick += Update;
            EventFunctions.FixedTick += FixedUpdate;
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
            var attackCooldownPassed = Time.time >= lastAttackTime + 1f / Model.AttacksPerSecond;
            reachRotation = Vector2.Angle(toTarget, viewTransform.up) <= Model.AttackLookAngle;

            if (reachPosition && reachRotation && attackCooldownPassed)
            {
                handController.UseItem();
                lastAttackTime = Time.time;
            }
            
            if (!reachRotation)
            {
                View.LookAt(attackTarget.position, Model.RotationSpeed);
            }
        }
        
        private void FixedUpdate()
        {
            if (attackTarget == null || View == null || Model == null)
            {
                return;
            }
            
            var viewTransform = View.transform;
            var targetPosition = attackTarget.position;
            var toTarget = targetPosition - viewTransform.position;

            reachPosition = toTarget.sqrMagnitude <= Model.AttackDistance * Model.AttackDistance;
            
            if (!reachPosition)
            {
                var moveDirection = toTarget.normalized;
                
                View.Move(moveDirection, Model.MoveSpeed);
            }
        }

        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }
    }
}