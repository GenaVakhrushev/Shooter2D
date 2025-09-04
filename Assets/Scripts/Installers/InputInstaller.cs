using DI.Installers;

namespace Shooter.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            container.Bind(_ =>
            {
                var inputActions = new ShooterInputActions();
                
                inputActions.Enable();
                
                return inputActions;
            });
        }
    }
}