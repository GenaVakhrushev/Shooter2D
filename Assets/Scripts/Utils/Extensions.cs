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
    }
}