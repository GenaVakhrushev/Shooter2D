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
            var resolution = new Vector2(Screen.width, Screen.height);
            var min = MainCamera.ScreenToWorldPoint(Vector3.zero);
            var max = MainCamera.ScreenToWorldPoint(resolution);
            var screenWorldRect = new Rect()
            {
                min = min,
                max = max,
            };

            return screenWorldRect.Contains(point);
        }
    }
}