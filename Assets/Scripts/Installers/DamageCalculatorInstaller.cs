using DI.Installers;
using Shooter.Damage;

namespace Shooter.Installers
{
    public class DamageCalculatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            container.Bind(_ => new DamageCalculator());
        }
    }
}