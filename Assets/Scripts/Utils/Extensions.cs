using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shooter.Utils
{
    public static class Extensions
    {
        public static IEnumerable<Type> GetInheritors(this Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
        }
        
        public static Vector2 Rotate(this Vector2 direction, float angleDegrees)
        {
            var rad = Mathf.Deg2Rad * -angleDegrees;
            var cos = Mathf.Cos(rad);
            var sin = Mathf.Sin(rad);
            return new Vector2(direction.x * cos - direction.y * sin, direction.x * sin + direction.y * cos);
        }
        
        public static void LookAt(this Transform transform, Vector2 lookPosition, float rotationSpeed)
        {
            var lookDirection = (lookPosition - (Vector2)transform.position).normalized;
            var rotateClockwise = Vector2.Dot(transform.right, lookDirection) > 0;
            var rotationAngle = rotationSpeed * Time.deltaTime * (rotateClockwise ? -1 : 1);
            var angleToLookDirection = Vector2.Angle(transform.up, lookDirection);
            
            if (rotationAngle > angleToLookDirection)
            {
                rotationAngle = angleToLookDirection;
            }
            
            transform.Rotate(0, 0, rotationAngle);
        }

        public static void MoveInScreen(this Rigidbody2D rb, Vector2 direction, float speed)
        {
            var targetPosition = rb.position + direction * (speed * Time.fixedDeltaTime);
            var screenMin = ScreenArea.GetMin();
            var screenMax = ScreenArea.GetMax();
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, screenMin.x, screenMax.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, screenMin.y, screenMax.y);
            
            rb.MovePosition(targetPosition);
        }
    }
}