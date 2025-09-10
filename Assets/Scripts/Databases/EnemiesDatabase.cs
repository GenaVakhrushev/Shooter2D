using Shooter.Enemies;
using UnityEngine;

namespace Shooter.Databases
{
    [CreateAssetMenu(fileName = nameof(EnemiesDatabase), menuName = "Databases/" + nameof(EnemiesDatabase), order = 0)]
    public class EnemiesDatabase : ScriptableObject
    {
        public EnemyConfig[] EnemyConfigs;
    }
}