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
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"{name} take {damage} damage");
            DamageTaken?.Invoke(damage);
        }

        public void Move(Vector2 direction, float speed)
        {
            var targetPosition = (Vector2)transform.position + direction * (speed * Time.fixedDeltaTime);
            var screenMin = ScreenArea.GetMin();
            var screenMax = ScreenArea.GetMax();
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, screenMin.x, screenMax.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, screenMin.y, screenMax.y);
            
            rb.MovePosition(targetPosition);
        }
    }
}