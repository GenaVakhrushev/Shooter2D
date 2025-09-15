using Shooter.HP;
using Shooter.Inventory.Items.Weapons;
using UnityEngine;

namespace Shooter.Enemies
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/Enemies/" + nameof(EnemyConfig), order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        public string EnemyName;
        public float MoveSpeed;
        public float RotationSpeed;
        public float AttackDistance;
        public float AttackLookAngle;
        public float AttacksPerSecond;
        public HPConfig HPConfig;
        public WeaponConfig WeaponConfig;
        public EnemyView EnemyView;
    }
}