using System;
using Shooter.Damage;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : View, IDamageable
    {
        private Rigidbody2D rb;
        
        public event Action<float> DamageTaken;

        protected override void Awake()
        {
            base.Awake();
            
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"{name} take {damage} damage");
            DamageTaken?.Invoke(damage);
        }

        public void Move(Vector2 direction, float speed)
        {
            rb.MoveInScreen(direction, speed);
        }

        public void LookAt(Vector2 position, float speed)
        {
            transform.LookAt(position, speed);
        }
    }
}