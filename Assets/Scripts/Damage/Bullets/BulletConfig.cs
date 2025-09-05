using TopDownShooter.Configs;
using UnityEngine;

namespace Shooter.Damage.Bullets
{
    [CreateAssetMenu(fileName = nameof(BulletConfig), menuName = "Configs/" + nameof(BulletConfig), order = 0)]
    public class BulletConfig : ObjectConfig<Bullet>
    {
        
    }
}