using DI.Attributes;
using UnityEngine;

namespace DI.Installers
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected DIContainer container;

        public void Initialize(DIContainer container)
        {
            this.container = container;
        }
        
        public abstract void InstallBindings();
    }
}