using DI.Utils;
using UnityEngine;

namespace DI.Contexts
{
    public class ProjectContext : Context
    {
        public static ProjectContext Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var prefab = Resources.Load<ProjectContext>(Settings.PROJECT_CONTEXT_PREFAB_NAME);

            if (prefab == null)
            {
                prefab = new GameObject(Settings.PROJECT_CONTEXT_PREFAB_NAME).AddComponent<ProjectContext>();
            }
            else
            {
                prefab = Instantiate(prefab);
            }
            
            prefab.Initialize(new DIContainer());
            prefab.InstallBindings();
            
            Instance = prefab;
            
            DontDestroyOnLoad(prefab.gameObject);
        }
    }
}