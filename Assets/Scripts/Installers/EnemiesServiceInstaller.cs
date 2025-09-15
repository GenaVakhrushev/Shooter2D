using DI.Installers;
using Shooter.Services;

namespace Shooter.Installers
{
    public class EnemiesServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            container.Bind(_ => new EnemiesService());
        }
    }
}