using System;
using Shooter.Damage;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : View, IDamageable
    {
        private Rigidbody2D rb;
        
        public event Action<float> DamageTaken;
        
        public EnemyModel EnemyModel { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        public void SetEnemyModel(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"{name} take {damage} damage");
            
            DamageTaken?.Invoke(damage);
        }

        public void Move(Vector2 direction, float speed)
        {
            var targetPosition = rb.position + direction * (speed * Time.fixedDeltaTime);
            
            rb.MovePosition(targetPosition);
        }
        
        public void LookAt(Vector2 position, float speed)
        {
            transform.LookAt(position, speed);
        }
    }
}