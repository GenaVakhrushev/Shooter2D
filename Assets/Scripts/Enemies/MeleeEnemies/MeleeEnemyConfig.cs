using System;
using UnityEngine;

namespace Shooter.Enemies.MeleeEnemies
{
    [CreateAssetMenu(fileName = nameof(MeleeEnemyConfig), menuName = "Configs/Enemies/" + nameof(MeleeEnemyConfig), order = 0)]
    public class MeleeEnemyConfig : EnemyConfig
    {
        public override Type GetControllerType() => typeof(MeleeEnemyController);
    }
}