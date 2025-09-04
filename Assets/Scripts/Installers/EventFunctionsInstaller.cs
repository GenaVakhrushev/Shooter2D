using DI.Installers;
using Shooter.Utils;
using UnityEngine;

namespace Shooter.Installers
{
    public class EventFunctionsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var eventFunctions = new GameObject("EventFunctions").AddComponent<EventFunctions>();
            
            DontDestroyOnLoad(eventFunctions.gameObject);
            
            container.BindInstance(eventFunctions);
        }
    }
}