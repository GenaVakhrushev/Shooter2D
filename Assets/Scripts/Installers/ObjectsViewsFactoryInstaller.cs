using DI.Installers;
using Shooter.Factories;

namespace Shooter.Installers
{
    public class ObjectsViewsFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            container.Bind(_ => new ObjectsViewsFactory());
        }
    }
}