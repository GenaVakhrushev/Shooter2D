using UnityEngine;

namespace Shooter.Utils
{
    public static class ScreenArea
    {
        private static Camera mainCamera;

        private static Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                }

                return mainCamera;
            }
        }

        public static bool InScreenArea(Vector2 point)
        {
            var screenWorldRect = new Rect()
            {
                min = GetMin(),
                max = GetMax(),
            };

            return screenWorldRect.Contains(point);
        }
        
        public static Vector2 GetMin() => MainCamera.ScreenToWorldPoint(Vector3.zero);
        public static Vector2 GetMax() => MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
}