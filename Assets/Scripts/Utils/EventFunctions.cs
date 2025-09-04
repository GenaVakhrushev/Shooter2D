using System;
using UnityEngine;

namespace Shooter.Utils
{
    public class EventFunctions : MonoBehaviour
    {
        public static event Action Tick;

        private void Update()
        {
            Tick?.Invoke();
        }
    }
}