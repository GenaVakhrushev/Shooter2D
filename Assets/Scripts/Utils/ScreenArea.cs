using UnityEngine;

namespace Shooter.Utils
{
    public static class ScreenArea
    {
        private static Vector2Int lastResolution;
        private static Vector2 min;
        private static Vector2 max;
        
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
        
        public static Vector2 GetMin()
        {
            ValidateResolution();

            return min;
        }

        public static Vector2 GetMax()
        {
            ValidateResolution();

            return max;
        }

        private static void ValidateResolution()
        {
            var resolution = new Vector2Int(Screen.width, Screen.height);
            
            if (resolution != lastResolution)
            {
                lastResolution = resolution;
                min = MainCamera.ScreenToWorldPoint(Vector3.zero);
                max = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            }
        }
    }
}