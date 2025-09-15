using System;
using DI.Attributes;
using Shooter.Damage;
using Shooter.Factories;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyView : View, IDamageable
    {
        [Inject] private EnemiesFactory enemiesFactory;
        
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

        public void Die()
        {
            enemiesFactory.ReturnView(this);
        }
    }
}