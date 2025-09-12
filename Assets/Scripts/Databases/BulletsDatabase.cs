using Shooter.Bullets;
using UnityEngine;

namespace Shooter.Databases
{
    [CreateAssetMenu(fileName = nameof(BulletsDatabase), menuName = "Databases/" + nameof(BulletsDatabase), order = 0)]
    public class BulletsDatabase : ScriptableObject
    {
        public BulletConfig[] BulletConfigs;
    }
}