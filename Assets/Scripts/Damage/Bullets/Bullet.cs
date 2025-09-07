namespace Shooter.Damage.Bullets
{
    public class Bullet
    {
        public string Name { get; private set; }

        public Bullet(string name)
        {
            Name = name;
        }
    }
}