using DI.Attributes;
using Shooter.Factories;
using Shooter.Utils;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Damage.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletView : ObjectView
    {
        [Inject] private ObjectsViewsFactory viewsFactory;
        
        private Rigidbody2D rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!ScreenArea.InScreenArea(transform.position))
            {
                viewsFactory.ReturnView(this);
            }
        }

        public void Launch(Vector2 directionNormalized, float speed)
        {
            rb.linearVelocity = directionNormalized * speed;
        }
    }
}