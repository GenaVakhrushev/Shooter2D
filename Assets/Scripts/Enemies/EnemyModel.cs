using Shooter.Inventory.Items.Weapons;

namespace Shooter.Enemies
{
    public class EnemyModel
    {
        public string EnemyName;
        public float MoveSpeed;
        public float RotationSpeed;
        public float AttackDistance;
        public float AttackLookAngle;
        public float AttacksPerSecond;
        public Weapon Weapon;
    }
}