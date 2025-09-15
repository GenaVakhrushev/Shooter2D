using Shooter.Factories;
using Shooter.Services;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.Enemies.Spawn
{
    public class OffScreenEnemySpawner : EnemySpawner
    {
        private readonly float minXOffset;
        private readonly float minYOffset;
        
        private readonly float maxXOffset;
        private readonly float maxYOffset;

        public OffScreenEnemySpawner(EnemiesFactory enemiesFactory, EnemiesService enemiesService, float minXOffset, float minYOffset, float maxXOffset, float maxYOffset) : base(enemiesFactory, enemiesService)
        {
            this.minXOffset = minXOffset;
            this.minYOffset = minYOffset;
            this.maxXOffset = maxXOffset;
            this.maxYOffset = maxYOffset;
        }

        protected override Vector3 GetSpawnPosition()
        {
            var angle = Random.Range(0f, 90f) * Mathf.Deg2Rad;
            var minExtend = new Vector2(minXOffset, minYOffset);
            var maxExtend = new Vector2(maxXOffset, maxYOffset);
            var screenMin = ScreenArea.GetMin();
            var screenMax = ScreenArea.GetMax();
            var distanceToMinBorder = GetDistanceToBorder(screenMin - minExtend, screenMax + minExtend, angle);
            var distanceToMaxBorder = GetDistanceToBorder(screenMin - maxExtend, screenMax + maxExtend, angle);
            var offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(distanceToMinBorder, distanceToMaxBorder);
            var spawnPoint = (screenMax + screenMin) / 2 + offset;

            if (Random.value > 0.5f)
            {
                spawnPoint.x *= -1;
            }
            if (Random.value > 0.5f)
            {
                spawnPoint.y *= -1;
            }
            
            return spawnPoint;
        }

        private float GetDistanceToBorder(Vector2 min, Vector2 max, float angle)
        {
            var height = (max.y - min.y) / 2;
            var width = (max.x - min.x) / 2;
            var diagonalAngle = Mathf.Atan(height / width);
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            var distanceToBorder = angle >= diagonalAngle ? height / sin : width / cos;

            return distanceToBorder;
        }

        protected override Quaternion GetSpawnRotation() => Quaternion.identity;
    }
}