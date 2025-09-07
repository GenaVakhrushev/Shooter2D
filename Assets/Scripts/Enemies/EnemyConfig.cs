using System;
using TopDownShooter.Configs;

namespace Shooter.Enemies
{
    public abstract class EnemyConfig : ObjectConfig<EnemyModel>
    {
        public abstract Type GetControllerType();
    }
}