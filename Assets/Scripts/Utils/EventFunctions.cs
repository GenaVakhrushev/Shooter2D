using System;
using UnityEngine;

namespace Shooter.Utils
{
    public class EventFunctions : MonoBehaviour
    {
        public static event Action Tick;
        public static event Action FixedTick;

        private void Update()
        {
            Tick?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedTick?.Invoke();
        }
    }
}