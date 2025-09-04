using DI.Installers;
using Shooter.GameManagement;
using UnityEngine;

namespace Shooter.Installers
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private bool autoStart = true;
        
        public override void InstallBindings()
        {
            container.Bind(_ => new GameManager());
        }

        private void Awake()
        {
            if (autoStart)
            {
                container.Resolve<GameManager>().StartGame();
            }
        }
    }
}