using System;
using Shooter.Controllers;
using Shooter.HP;
using Shooter.Inventory.Hand;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.Enemies
{
    public class EnemyController : Controller<EnemyModel, EnemyView>
    {
        private readonly HandController handController;
        private readonly HPController hpController;
        
        private Transform attackTarget;
        private float lastAttackTime;
        private bool reachPosition;
        private bool reachRotation;

        public event Action EnemyDied;

        public EnemyController()
        {
            handController = new HandController();
            
            hpController = new HPController();
            hpController.LostHP += HpControllerOnLostHP;
            
            EventFunctions.Tick += Update;
            EventFunctions.FixedTick += FixedUpdate;
        }

        private void HpControllerOnLostHP()
        {
            if (View)
            {
                View.Die();
            }

            EnemyDied?.Invoke();
        }

        public override void SetModel(EnemyModel model)
        {
            base.SetModel(model);

            lastAttackTime = 0;

            if (Model != null)
            {
                var handModel = new HandModel();

                handModel.SetItem(Model.Weapon);
                handController.SetModel(handModel);

                hpController.SetModel(Model.HPModel);
            }
            else
            {
                handController.SetModel(null);
                hpController.SetModel(null);
            }
        }

        public override void SetView(EnemyView view)
        {
            if (View != null)
            {
                View.DamageTaken -= ViewOnDamageTaken;
            }
            
            base.SetView(view);

            if (View != null)
            {
                handController.SetView(View.GetComponentInChildren<HandView>());
                hpController.SetView(View.GetComponentInChildren<HPView>());

                View.DamageTaken += ViewOnDamageTaken;
            }
            else
            {
                handController.SetView(null);
                hpController.SetView(null);
            }
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

        private void ViewOnDamageTaken(float damage)
        {
            hpController.RemoveHP(damage);
        }

        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }
    }
}