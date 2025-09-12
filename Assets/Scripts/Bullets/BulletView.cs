using DI.Attributes;
using Shooter.Damage;
using Shooter.Factories;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BulletView : View, IDamageDealer
    {
        public Bullet Bullet { get; private set; }

        [Inject] private BulletsFactory bulletsFactory;
        [Inject] private DamageCalculator damageCalculator;

        private Rigidbody2D rb;
        private Collider2D col;

        protected override void Awake()
        {
            base.Awake();
            
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void Update()
        {
            if (!ScreenArea.InScreenArea(transform.position))
            {
                bulletsFactory.ReturnView(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }
            
            if (other.TryGetComponent(out IDamageable damageable))
            {
                var damage = damageCalculator.CalculateDamage(this, damageable);

                if (damage > 0)
                {
                    damageable.TakeDamage(damage);
                    bulletsFactory.ReturnView(this);
                }
            }
        }

        public float GetDamage() => Bullet.Damage;

        public virtual void SetBullet(Bullet bullet)
        {
            Bullet = bullet;
        }

        public void Launch(Vector2 directionNormalized, float speed)
        {
            rb.linearVelocity = directionNormalized * speed;
        }
    }
}