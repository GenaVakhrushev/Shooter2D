namespace Shooter.Bullets
{
    public class Bullet
    {
        public string Name { get; private set; }

        public float Damage { get; private set; }

        public Bullet(string name)
        {
            Name = name;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }
    }
}