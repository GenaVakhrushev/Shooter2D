using System;
using DI.Installers;
using UnityEngine;

namespace DI.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        [SerializeField] private MonoInstaller[] monoInstallers = Array.Empty<MonoInstaller>();

        public DIContainer Container { get; private set; }

        protected virtual void Initialize(DIContainer container)
        {
            Container = container;
            Container.BindInstance(Container);
        }

        protected void InstallBindings()
        {
            foreach (var monoInstaller in monoInstallers)
            {
                if (monoInstaller == null)
                {
                    Debug.LogError($"Null installer in context: {this}");
                }
                
                monoInstaller.Initialize(Container);
                monoInstaller.InstallBindings();
            }
        }
    }
}