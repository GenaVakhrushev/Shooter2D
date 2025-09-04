using System.Linq;
using DI.Utils;
using UnityEngine;

namespace DI.Contexts
{
    [DefaultExecutionOrder(-9999)]
    public class SceneContext : Context
    {
        private void Awake()
        {
            var container = new DIContainer(ProjectContext.Instance.Container);
            
            Initialize(container);
            InstallBindings();
            InjectSceneObjects();
        }

        private void InjectSceneObjects()
        {
            var injectables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(behaviour => behaviour.IsInjectable());

            foreach (var injectable in injectables)
            {
                Container.Inject(injectable);
            }
        }
    }
}