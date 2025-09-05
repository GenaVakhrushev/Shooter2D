using TopDownShooter.Configs;

namespace Shooter.Views
{
    public class ObjectView : View
    {
        public ObjectConfig ObjectConfig { get; private set; }

        public virtual void SetObjectConfig(ObjectConfig objectConfig)
        {
            ObjectConfig = objectConfig;
        }
    }
}